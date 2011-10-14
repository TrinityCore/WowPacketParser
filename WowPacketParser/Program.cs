using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using WowPacketParser.Enums;
using DBCStore = WowPacketParser.DBC.DBCStore.DBC;

namespace WowPacketParser
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // Read config options
            string[] filters = null;
            string[] ignoreFilters = null;
            bool sqlOutput = false;
            SniffType dumpFormat = SniffType.Text;
            int packetsToRead = 0; // 0 -> All packets
            int packetNumberLow = 0; // 0 -> No low limit
            int packetNumberHigh = 0; // 0 -> No high limit
            bool prompt = false;

            try
            {
                packetNumberLow = Settings.GetInt32("FilterPacketNumLow");
                packetNumberHigh = Settings.GetInt32("FilterPacketNumHigh");

                if (packetNumberLow > 0 && packetNumberHigh > 0 && packetNumberLow > packetNumberHigh)
                    throw new Exception("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

                string filtersString = Settings.GetString("Filters");
                if (filtersString != null)
                    filters = filtersString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                filtersString = Settings.GetString("IgnoreFilters");
                if (filtersString != null)
                    ignoreFilters = filtersString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                sqlOutput = Settings.GetBoolean("SQLOutput");
                dumpFormat = (SniffType)Settings.GetInt32("DumpFormat");
                packetsToRead = Settings.GetInt32("PacketsNum");
                prompt = Settings.GetBoolean("ShowEndPrompt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            // Quit if no arguments are given
            if (args.Length == 0)
            {
                Console.WriteLine("No files specified.");
                EndPrompt(prompt);
                return;
            }

            // Read DBCs
            if (DBCStore.Enabled)
            {
                var startTime = DateTime.Now;
                Console.WriteLine("Loading DBCs...");

                new DBC.DBCLoader();

                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Console.WriteLine("Finished loading DBCs - {0} Minutes, {1} Seconds and {2} Milliseconds.{3}", span.Minutes, span.Seconds, span.Milliseconds, Environment.NewLine);
            }

            // Read binaries
            string[] files = args;
            if (args.Length == 1 && args[0].Contains('*'))
            {
                try
                {
                    files = Directory.GetFiles(@".\", args[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }

            foreach (string file in files)
            {
                var fileName = Path.GetFileName(file);
                Console.WriteLine("Opening file '{0}'", fileName);
                Console.WriteLine("Reading packets...");

                try
                {
                    var packets = Reader.Read(file, filters, ignoreFilters, packetNumberLow, packetNumberHigh, packetsToRead);
                    if (packets.Count > 0)
                    {
                        var directoryPath = Path.GetDirectoryName(file);

                        if (dumpFormat == SniffType.Bin)
                        {
                            Console.WriteLine("Copying {0} packets in .bin format...", packets.Count());
                            var dumpFileName = Path.Combine(directoryPath, fileName + ".bin");
                            File.Delete(dumpFileName);
                            BinaryWriter writer = new BinaryWriter(File.Open(dumpFileName, FileMode.Create));
                            foreach (var packet in packets)
                            {
                                writer.Write((Int32)packet.Opcode);
                                writer.Write((Int32)packet.GetLength());
                                writer.Write((Int32)Utilities.GetUnixTimeFromDateTime(packet.Time));
                                writer.Write((char)packet.Direction);
                                writer.Write(packet.GetStream(0));
                            }
                            writer.Flush();
                            writer.Close();
                            writer = null;
                        }
                        else if (dumpFormat == SniffType.Pkt)
                        {
                            Console.WriteLine("Copying {0} packets in .pkt format...", packets.Count());
                            var dumpFileName = Path.Combine(directoryPath, fileName + ".pkt");
                            File.Delete(dumpFileName);
                            BinaryWriter writer = new BinaryWriter(File.Open(dumpFileName, FileMode.Create));
                            foreach (var packet in packets)
                            {
                                writer.Write((UInt16)packet.Opcode);
                                writer.Write((Int32)packet.GetLength());
                                writer.Write((Byte)packet.Direction);
                                writer.Write((UInt64)Utilities.GetUnixTimeFromDateTime(packet.Time));
                                writer.Write(packet.GetStream(0));
                            }
                            writer.Flush();
                            writer.Close();
                            writer = null;                        }
                        else
                        {
                            ClientVersion.SetVersion(packets[0].Time);
                            Console.WriteLine("Assumed version: {0}", ClientVersion.Version);

                            Console.WriteLine("Parsing {0} packets...", packets.Count);
                            var startTime = DateTime.Now;

                            SQLStore.Initialize(Path.Combine(directoryPath, fileName + ".sql"), sqlOutput);
                            Handler.InitializeLogFile(Path.Combine(directoryPath, fileName + ".txt"), dumpFormat == SniffType.None);
                            foreach (var packet in packets)
                                Handler.Parse(packet);

                            SQLStore.WriteToFile();
                            Handler.WriteToFile();

                            var endTime = DateTime.Now;
                            var span = endTime.Subtract(startTime);
                            // Need to open a new writer to console, last one was redirected to the file and is now closed.
                            var standardOutput = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                            Console.SetOut(standardOutput);
                            Console.WriteLine("Finished parsing in - {0} Minutes, {1} Seconds and {2} Milliseconds.{3}", span.Minutes, span.Seconds, span.Milliseconds, Environment.NewLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                finally
                {
                    EndPrompt(prompt);
                }
            }
        }

        private static void EndPrompt(bool prompt)
        {
            if (!prompt)
                return;

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
