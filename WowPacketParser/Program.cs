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
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Store.SQL;

namespace WowPacketParser
{
    public static class Program
    {
        private static string[] GetFiles(string[] args)
        {
            var files = args;
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
                    files = new string[0];
                }
            }
            return files;
        }

        private static void ReadDB()
        {
            if (SQLConnector.Enabled)
            {
                // Enable SSH Tunnel
                if (SSHTunnel.Enabled)
                {
                    Console.WriteLine("Enabling SSH Tunnel");
                    SSHTunnel.Connect();
                }

                var startTime = DateTime.Now;
                Console.WriteLine("Loading DB...");

                try
                {
                    SQLConnector.Connect();
                    SQLDatabase.GrabData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    SQLConnector.Enabled = false; // Something failed, disabling everything SQL related
                }
                
                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Console.WriteLine("Finished loading DB - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
                Console.WriteLine();
            }
        }

        private static void DumpSQLs(string prefix, string fileName, Builder builder, SQLOutputFlags sqlOutput)
        {
            if (builder == null)
                return;

            var store = new SQLStore(fileName);

            if (sqlOutput.HasFlag(SQLOutputFlags.GameObjectTemplate))
                store.WriteData(builder.GameObjectTemplate());

            if (sqlOutput.HasFlag(SQLOutputFlags.GameObjectSpawns))
                store.WriteData(builder.GameObjectSpawns());

            if (sqlOutput.HasFlag(SQLOutputFlags.QuestTemplate))
                store.WriteData(builder.QuestTemplate());

            if (sqlOutput.HasFlag(SQLOutputFlags.QuestPOI))
                store.WriteData(builder.QuestPOI());

            if (sqlOutput.HasFlag(SQLOutputFlags.CreatureTemplate))
                store.WriteData(builder.NpcTemplate());

            if (sqlOutput.HasFlag(SQLOutputFlags.CreatureSpawns))
                store.WriteData(builder.CreatureSpawns());

            if (sqlOutput.HasFlag(SQLOutputFlags.NpcTrainer))
                store.WriteData(builder.NpcTrainer());

            if (sqlOutput.HasFlag(SQLOutputFlags.NpcVendor))
                store.WriteData(builder.NpcVendor());

            if (sqlOutput.HasFlag(SQLOutputFlags.NpcText))
                store.WriteData(builder.PageText());

            if (sqlOutput.HasFlag(SQLOutputFlags.PageText))
                store.WriteData(builder.NpcText());

            if (sqlOutput.HasFlag(SQLOutputFlags.Gossip))
                store.WriteData(builder.Gossip());

            if (sqlOutput.HasFlag(SQLOutputFlags.Loot))
                store.WriteData(builder.Loot());

            if (sqlOutput.HasFlag(SQLOutputFlags.SniffData))
                store.WriteData(builder.SniffData());

            if (sqlOutput.HasFlag(SQLOutputFlags.StartInformation))
                store.WriteData(builder.StartInformation());

            if (sqlOutput.HasFlag(SQLOutputFlags.ObjectNames))
                store.WriteData(builder.ObjectNames());

            Console.WriteLine("{0}: Saved file to '{1}'", prefix, fileName);
            store.WriteToFile();
        }

        private static void ReadFile(string file, Stuffing globalStuffing, Builder globalBuilder)
        {
            string[] filters = Settings.GetStringList("Filters", null);
            string[] ignoreFilters = Settings.GetStringList("IgnoreFilters", null);
            int packetsToRead = Settings.GetInt32("FilterPacketsNum", 0);
            int packetNumberLow = Settings.GetInt32("FilterPacketNumLow", 0);
            int packetNumberHigh = Settings.GetInt32("FilterPacketNumHigh", 0);
            DumpFormatType dumpFormat = Settings.GetEnum<DumpFormatType>("DumpFormat", DumpFormatType.Text);
            SQLOutputFlags sqlOutput = Settings.GetEnum<SQLOutputFlags>("SQLOutput", SQLOutputFlags.None);
            int threads = Settings.GetInt32("Threads", 0);

            // If our dump format requires a .txt to be created,
            // check if we can write to that .txt before starting parsing
            if (dumpFormat != DumpFormatType.Bin && dumpFormat != DumpFormatType.Pkt)
            {
                var outFileName = Path.ChangeExtension(file, null) + "_parsed.txt";
                if (Utilities.FileIsInUse(outFileName))
                {
                    Console.WriteLine("Save file {0} is in use, parsing will not be done.", outFileName);
                    return;
                }
            }

            var stuffing = globalStuffing != null ? globalStuffing : new Stuffing();
            var fileInfo = new SniffFileInfo { FileName = file, Stuffing = stuffing };
            var fileName = Path.GetFileName(fileInfo.FileName);

            Console.WriteLine("{0}: Opening file", fileName);
            Console.WriteLine("{0}: Reading packets...", fileName);

            Builder builder = globalBuilder != null ? globalBuilder : sqlOutput > 0 ? new Builder(stuffing) : null;

            try
            {
                var packets = Reader.Read(fileInfo, filters, ignoreFilters, packetNumberLow, packetNumberHigh, packetsToRead, (dumpFormat == DumpFormatType.SummaryHeader));
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

                    var total = (uint)packets.Count;
                    var startTime = DateTime.Now;
                    var outFileName = Path.ChangeExtension(file, null) + "_parsed";
                    var outLogFileName = outFileName + ".txt";

                    bool headersOnly = (dumpFormat == DumpFormatType.TextHeader || dumpFormat == DumpFormatType.SummaryHeader);
                    if (threads == 0) // Number of threads is automatically choosen by the Parallel library
                        packets.AsParallel().SetCulture().ForAll(packet => Handler.Parse(packet, headersOnly));
                    else
                        packets.AsParallel().SetCulture().WithDegreeOfParallelism(threads).ForAll(packet => Handler.Parse(packet, headersOnly));

                    Console.WriteLine("{0}: Writing data to file...", fileName);

                    if (sqlOutput > 0 && globalStuffing == null) // No global Stuffing, write sql data to particular sql file
                    {
                        var outSqlFileName = outFileName + ".sql";
                        DumpSQLs(fileName, outSqlFileName, builder, sqlOutput);
                    }

                    if (dumpFormat != DumpFormatType.None)
                    {
                        Console.WriteLine("{0}: Saved file to '{1}'", fileName, outLogFileName);
                        Handler.WriteToFile(packets, outLogFileName);
                    }

                    var span = DateTime.Now.Subtract(startTime);
                    var statsOk = 0;
                    var statsError = 0;
                    var statsSkip = 0;

                    foreach (var packet in packets)
                    {
                        if (!packet.WriteToFile)
                            statsSkip++;
                        else
                        {
                            switch (packet.Status)
                            {
                                case ParsedStatus.Success:
                                    statsOk++;
                                    break;
                                case ParsedStatus.WithErrors:
                                    statsError++;
                                    break;
                                case ParsedStatus.NotParsed:
                                    statsSkip++;
                                    break;
                            }
                        }
                    }
                    
                    Console.WriteLine("{0}: Parsed {1:F1}% packets successfully, {2:F1}% with errors and skipped {3:F1}% in {4} Minutes, {5} Seconds and {6} Milliseconds.",
                        fileName, (double)statsOk / total * 100, (double)statsError / total * 100, (double)statsSkip / total * 100,
                        span.Minutes, span.Seconds, span.Milliseconds);

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static void EndPrompt()
        {
            if (Settings.GetBoolean("ShowEndPrompt", false))
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.WriteLine();
            }
        }

        private static void Main(string[] args)
        {
            string[] files = GetFiles(args);
            if (files.Length == 0)
            {
                if (args.Length == 0)
                    Console.WriteLine("No files specified.");
                else
                    Console.WriteLine("No files found with pattern {0}", args[0]);
                EndPrompt();
                return;
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            ClientVersion.SetVersion(Settings.GetEnum<ClientVersionBuild>("ClientBuild", ClientVersionBuild.Zero));

            int packetNumberLow = Settings.GetInt32("FilterPacketNumLow", 0);
            if (packetNumberLow < 0)
                throw new Exception("FilterPacketNumLow must be positive");

            int packetNumberHigh = Settings.GetInt32("FilterPacketNumHigh", 0);
            if (packetNumberHigh < 0)
                throw new Exception("FilterPacketNumHigh must be positive");

            if (packetNumberLow > packetNumberHigh)
                throw new Exception("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

            DumpFormatType dumpFormat = Settings.GetEnum<DumpFormatType>("DumpFormat", DumpFormatType.Text);

            // Disable DB when we don't need its data (dumping to a binary file)
            if (dumpFormat == DumpFormatType.Bin || dumpFormat == DumpFormatType.Pkt)
            {
                SQLConnector.Enabled = false;
                SSHTunnel.Enabled = false;
            }
            else
                Filters.Initialize();

            // Read DB
            ReadDB();

            Stuffing stuffing = null;
            Builder builder = null;

            SQLOutputFlags sqlOutput = Settings.GetEnum<SQLOutputFlags>("SQLOutput", SQLOutputFlags.None);
            string outSqlFileName = Settings.GetString("SQLFileName", "");
            if (sqlOutput > 0 && outSqlFileName.Length > 0)
            {
                stuffing = new Stuffing();
                builder = new Builder(stuffing);
            }

            int threads = Settings.GetInt32("Threads", 0);
            if (threads == 0) // Number of threads is automatically choosen by the Parallel library
                files.AsParallel().SetCulture()
                    .ForAll(file => ReadFile(file, stuffing, builder));
            else
                files.AsParallel().SetCulture().WithDegreeOfParallelism(threads)
                    .ForAll(file => ReadFile(file, stuffing, builder));

            DumpSQLs("Dumping global sql", outSqlFileName, builder, sqlOutput);

            SQLConnector.Disconnect();
            SSHTunnel.Disconnect();
            Logger.WriteErrors();
            EndPrompt();
        }
    }
}
