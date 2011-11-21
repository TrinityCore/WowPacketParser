using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.SQL;
using DBCStore = WowPacketParser.DBC.DBCStore.DBC;

namespace WowPacketParser
{
    public static class Program
    {
        private static void ReadFile(string file, string[] filters, string[] ignoreFilters, int packetNumberLow, int packetNumberHigh, int packetsToRead, DumpFormatType dumpFormat, int threads, bool sqlOutput, bool prompt)
        {
            string fileName = Path.GetFileName(file);
            Console.WriteLine("{0}: Opening file", fileName);
            Console.WriteLine("{0}: Reading packets...", fileName);

            try
            {
                var packets = Reader.Read(file, filters, ignoreFilters, packetNumberLow, packetNumberHigh, packetsToRead, (dumpFormat == DumpFormatType.SummaryHeader));
                if (packets.Count <= 0)
                {
                    Console.WriteLine("{0}: Packet count is 0", fileName);
                    return;
                }

                if (dumpFormat == DumpFormatType.Bin || dumpFormat == DumpFormatType.Pkt)
                {
                    SniffType format = dumpFormat == DumpFormatType.Bin ? SniffType.Bin : SniffType.Pkt;
                    var fileExtension = dumpFormat.ToString().ToLower();
                    Console.WriteLine("{0}: Copying {1} packets to .{2} format...", fileName, packets.Count, fileExtension);

                    var dumpFileName = Path.ChangeExtension(file, null) + "_excerpt." + fileExtension;
                    var writer = new BinaryPacketWriter(format, dumpFileName, Encoding.ASCII);
                    writer.Write(packets);
                }
                else
                {
                    var numberOfThreads = threads != 0 ? threads.ToString() : "a recommended number of";

                    Console.WriteLine("{0}: Assumed version: {1}", fileName, ClientVersion.GetVersionString());
                    Console.WriteLine("{0}: Parsing {1} packets with {2} threads...", fileName, packets.Count, numberOfThreads);

                    Statistics.Total = (uint)packets.Count;

                    Statistics.StartTime = DateTime.Now;
                    var outFileName = Path.ChangeExtension(file, null) + "_parsed";
                    var outLogFileName = outFileName + ".txt";
                    var outSqlFileName = outFileName + ".sql";

                    SQLStore.Initialize(outSqlFileName, sqlOutput);

                    bool headersOnly = (dumpFormat == DumpFormatType.TextHeader || dumpFormat == DumpFormatType.SummaryHeader);
                    if (threads == 0) // Number of threads is automatically choosen by the Parallel library
                        packets.AsParallel().SetCulture().ForAll(packet => Handler.Parse(packet, headersOnly));
                    else
                        packets.AsParallel().SetCulture().WithDegreeOfParallelism(threads).ForAll(packet => Handler.Parse(packet, headersOnly));

                    Console.WriteLine("{0}: Writing data to file...", fileName);

                    if (sqlOutput)
                    {
                        // Experimental, will remove
                        SQLStore.WriteData(Builder.QuestTemplate());
                        SQLStore.WriteData(Builder.NpcTrainer());
                        SQLStore.WriteData(Builder.NpcVendor());
                        SQLStore.WriteData(Builder.NpcTemplate());
                        SQLStore.WriteData(Builder.GameObjectTemplate());
                        SQLStore.WriteData(Builder.PageText());
                        SQLStore.WriteData(Builder.NpcText());
                    }

                    SQLStore.WriteToFile();
                    if (dumpFormat != DumpFormatType.None)
                        Handler.WriteToFile(packets, outLogFileName);

                    Statistics.EndTime = DateTime.Now;

                    Console.WriteLine(Statistics.Stats(fileName));
                    Console.WriteLine("{0}: Saved file to '{1}'", fileName, outLogFileName);
                    Console.WriteLine();
                    Statistics.Reset();
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

        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // Read config options
            string[] filters = null;
            string[] ignoreFilters = null;
            bool sqlOutput = false;
            DumpFormatType dumpFormat = DumpFormatType.Text;
            int packetsToRead = 0; // 0 -> All packets
            int packetNumberLow = 0; // 0 -> No low limit
            int packetNumberHigh = 0; // 0 -> No high limit
            bool prompt = false;
            int threads = 0;
            try
            {
                ClientVersion.SetVersion(Settings.GetEnum<ClientVersionBuild>("ClientBuild"));

                packetNumberLow = Settings.GetInt32("FilterPacketNumLow");
                packetNumberHigh = Settings.GetInt32("FilterPacketNumHigh");

                if (packetNumberLow > 0 && packetNumberHigh > 0 && packetNumberLow > packetNumberHigh)
                    throw new Exception("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

                string filtersString = Settings.GetString("Filters");
                if (filtersString != null)
                    filters = filtersString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                filtersString = Settings.GetString("IgnoreFilters");
                if (filtersString != null)
                    ignoreFilters = filtersString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                sqlOutput = Settings.GetBoolean("SQLOutput");
                dumpFormat = (DumpFormatType)Settings.GetInt32("DumpFormat");
                packetsToRead = Settings.GetInt32("PacketsNum");
                prompt = Settings.GetBoolean("ShowEndPrompt");
                threads = Settings.GetInt32("Threads");

                // Disable DB and DBCs when we don't need its data (dumping to a binary file)
                if (dumpFormat == DumpFormatType.Bin || dumpFormat == DumpFormatType.Pkt)
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

            if (threads == 0) // Number of threads is automatically choosen by the Parallel library
                files.AsParallel().SetCulture().ForAll(file => ReadFile(file, filters, ignoreFilters, packetNumberLow, packetNumberHigh, packetsToRead, dumpFormat, threads, sqlOutput, prompt));
            else
                files.AsParallel().SetCulture().WithDegreeOfParallelism(threads).ForAll(file => ReadFile(file, filters, ignoreFilters, packetNumberLow, packetNumberHigh, packetsToRead, dumpFormat, threads, sqlOutput, prompt));
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
