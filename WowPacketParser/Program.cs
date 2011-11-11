using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WowPacketParser.DBC;
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
            int threads = 0;
            try
            {
                ClientVersion.Build = Settings.GetEnum<ClientVersionBuild>("ClientBuild");

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
                threads = Settings.GetInt32("Threads");

                // Atm, we can't output sql with multiple threads
                // sql output needs to be written to a "buffer" (similar to Packet.Writer)
                // or done at the end of parsing (prefered option)
                if (sqlOutput)
                {
                    threads = 1;
                    Console.WriteLine("Thread number forced to 1 because SQL Output is enabled (temporary behaviour).");
                }

                // Disable DB and DBCs when we don't need its data (dumping to a binary file)
                if (dumpFormat == SniffType.Bin || dumpFormat == SniffType.Pkt)
                {
                    DBCStore.Enabled = false;
                    SQLConnector.Enabled = false;
                }
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

                new DBCLoader();

                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Console.WriteLine("Finished loading DBCs - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
                Console.WriteLine();
            }

            // Read DB
            if (SQLConnector.Enabled)
            {
                var startTime = DateTime.Now;
                Console.WriteLine("Loading DB...");

                try
                {
                    SQLConnector.Connect();
                    SQLDatabase.GrabData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    SQLConnector.Enabled = false; // Something failed, disabling everything SQL related
                }
                
                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Console.WriteLine("Finished loading DB - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
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
                            var numberOfThreads = threads != 0 ? threads.ToString() : "a recommended number of";

                            Console.WriteLine("Assumed version: {0}", ClientVersion.Build);
                            Console.WriteLine("Parsing {0} packets with {1} threads...", packets.Count, numberOfThreads);

                            Statistics.Total = (uint) packets.Count;

                            Statistics.StartTime = DateTime.Now;
                            var outFileName = Path.ChangeExtension(file, null) + "_parsed";
                            var outLogFileName = outFileName + ".txt";
                            var outSqlFileName = outFileName + ".sql";

                            SQLStore.Initialize(outSqlFileName, sqlOutput);

                            if (threads == 0) // Number of threads is automatically choosen by the Parallel library
                                packets.AsParallel().SetCulture().ForAll(Handler.Parse);
                            else
                                packets.AsParallel().SetCulture().WithDegreeOfParallelism(threads).ForAll(Handler.Parse);

                            SQLStore.WriteToFile();

                            Handler.InitializeLogFile(outLogFileName, dumpFormat == SniffType.None);
                            foreach (var packet in packets)
                                Console.WriteLine(packet.Writer);
                            Handler.WriteToFile();

                            Statistics.EndTime = DateTime.Now;

                            // Need to open a new writer to console, last one was redirected to the file and is now closed.
                            var standardOutput = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                            Console.SetOut(standardOutput);
                            Console.WriteLine(Statistics.Stats());
                            Console.WriteLine("Saved file to '{0}'", outLogFileName);
                            Console.WriteLine();
                            Statistics.Reset();
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
