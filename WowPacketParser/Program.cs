using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
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
                ClientVersion.Version = Settings.GetEnum<ClientVersionBuild>("ClientBuild");

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
                Console.WriteLine("Finished loading DBCs - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
                Console.WriteLine();
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
                Console.WriteLine("Opening file '{0}'", Path.GetFileName(file));
                Console.WriteLine("Reading packets...");

                try
                {
                    var packets = Reader.Read(file, filters, ignoreFilters, packetNumberLow, packetNumberHigh, packetsToRead);
                    if (packets.Count > 0)
                    {
                        if (dumpFormat == SniffType.Bin || dumpFormat == SniffType.Pkt)
                        {
                            var fileExtension = dumpFormat.ToString().ToLower();
                            Console.WriteLine("Copying {0} packets to .{1} format...", packets.Count, fileExtension);

                            var dumpFileName = Path.ChangeExtension(file, null) + "_excerpt." + fileExtension;
                            var writer = new BinaryPacketWriter(dumpFormat, dumpFileName, Encoding.ASCII);
                            writer.Write(packets);
                        }
                        else
                        {
                            Console.WriteLine("Assumed version: {0}", ClientVersion.Version);
                            Console.WriteLine("Parsing {0} packets...", packets.Count);

                            var stats = new Statistics() { Total = (uint)packets.Count };

                            stats.StartTime = DateTime.Now;
                            var outFileName = Path.ChangeExtension(file, null) + "_parsed";

                            SQLStore.Initialize(outFileName + ".sql", sqlOutput);
                            Handler.InitializeLogFile(outFileName + ".txt", dumpFormat == SniffType.None);

                            foreach (var packet in packets)
                                Handler.Parse(packet, ref stats);

                            SQLStore.WriteToFile();
                            Handler.WriteToFile();

                            stats.EndTime = DateTime.Now;

                            // Need to open a new writer to console, last one was redirected to the file and is now closed.
                            var standardOutput = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                            Console.SetOut(standardOutput);
                            Console.WriteLine(stats.Stats());
                            Console.WriteLine("Saved file to '{0}'", outFileName);
                            Console.WriteLine();
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
