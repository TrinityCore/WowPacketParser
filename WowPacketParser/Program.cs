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

        private static void ReadFile(string file, Stuffing globalStuffing, Builder globalBuilder, string prefix)
        {
            // If our dump format requires a .txt to be created,
            // check if we can write to that .txt before starting parsing
            if (Settings.DumpFormat != DumpFormatType.Bin && Settings.DumpFormat != DumpFormatType.Pkt)
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

            Console.WriteLine("{0}: Reading packets...", prefix);

            Builder builder = globalBuilder != null ? globalBuilder : Settings.SQLOutput > 0 ? new Builder(stuffing) : null;

            try
            {
                var packets = Reader.Read(fileInfo);
                if (packets.Count <= 0)
                {
                    Console.WriteLine("{0}: Packet count is 0", prefix);
                    return;
                }

                if (Settings.DumpFormat == DumpFormatType.Bin || Settings.DumpFormat == DumpFormatType.Pkt)
                {
                    SniffType format = Settings.DumpFormat == DumpFormatType.Bin ? SniffType.Bin : SniffType.Pkt;
                    var fileExtension = Settings.DumpFormat.ToString().ToLower();
                    Console.WriteLine("{0}: Copying {1} packets to .{2} format...", prefix, packets.Count, fileExtension);

                    var dumpFileName = Path.ChangeExtension(file, null) + "_excerpt." + fileExtension;
                    var writer = new BinaryPacketWriter(format, dumpFileName, Encoding.ASCII);
                    writer.Write(packets);
                }
                else
                {
                    Console.WriteLine("{0}: Parsing {1} packets. Assumed version {2}", prefix, packets.Count, ClientVersion.GetVersionString());

                    var total = (uint)packets.Count;
                    var startTime = DateTime.Now;
                    var outFileName = Path.ChangeExtension(file, null) + "_parsed";
                    var outLogFileName = outFileName + ".txt";

                    bool headersOnly = (Settings.DumpFormat == DumpFormatType.TextHeader || Settings.DumpFormat == DumpFormatType.SummaryHeader);
                    if (Settings.Threads == 0) // Number of threads is automatically choosen by the Parallel library
                        packets.AsParallel().SetCulture().ForAll(packet => Handler.Parse(packet, headersOnly));
                    else
                        packets.AsParallel().SetCulture().WithDegreeOfParallelism(Settings.Threads).ForAll(packet => Handler.Parse(packet, headersOnly));

                    if (Settings.SQLOutput > 0 && globalStuffing == null) // No global Stuffing, write sql data to particular sql file
                    {
                        var outSqlFileName = outFileName + ".sql";
                        DumpSQLs(fileName, outSqlFileName, builder, Settings.SQLOutput);
                    }

                    if (Settings.DumpFormat != DumpFormatType.None)
                    {
                        Console.WriteLine("{0}: Saved file to '{1}'", prefix, outLogFileName);
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
                        prefix, (double)statsOk / total * 100, (double)statsError / total * 100, (double)statsSkip / total * 100,
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
            if (Settings.ShowEndPrompt)
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
            ClientVersion.SetVersion(Settings.ClientBuild);

            if (Settings.FilterPacketNumLow < 0)
                throw new Exception("FilterPacketNumLow must be positive");

            if (Settings.FilterPacketNumHigh < 0)
                throw new Exception("FilterPacketNumHigh must be positive");

            if (Settings.FilterPacketNumLow > Settings.FilterPacketNumHigh)
                throw new Exception("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

            // Disable DB when we don't need its data (dumping to a binary file)
            if (Settings.DumpFormat == DumpFormatType.Bin || Settings.DumpFormat == DumpFormatType.Pkt)
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

            if (Settings.SQLOutput > 0 && Settings.SQLFileName.Length > 0)
            {
                stuffing = new Stuffing();
                builder = new Builder(stuffing);
            }

            var numberOfThreads = Settings.Threads != 0 ? Settings.Threads.ToString() : "a recommended number of";
            Console.WriteLine("Using {0} threads to process {1} files", numberOfThreads, files.Length);
            var count = 0;
            if (Settings.Threads == 0) // Number of threads is automatically choosen by the Parallel library
                files.AsParallel().SetCulture()
                    .ForAll(file =>
                        ReadFile(file, stuffing, builder, "[" + (++count).ToString() + "/" + files.Length + " " + file + "]"));
            else
                files.AsParallel().SetCulture().WithDegreeOfParallelism(Settings.Threads)
                    .ForAll(file => ReadFile(file, stuffing, builder, "[" + (++count).ToString() + "/" + files.Length + " " + file + "]"));

            DumpSQLs("Dumping global sql", Settings.SQLFileName, builder, Settings.SQLOutput);

            SQLConnector.Disconnect();
            SSHTunnel.Disconnect();
            Logger.WriteErrors();
            EndPrompt();
        }
    }
}
