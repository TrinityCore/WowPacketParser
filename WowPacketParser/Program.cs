using System;
using System.Diagnostics;
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
using WowPacketParser.Saving;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Store.SQL;

namespace WowPacketParser
{
    public static class Program
    {
        private static readonly Object _lockStats = new Object();
        private static int _globalStatsOk;
        private static int _globalStatsSkip;
        private static int _globalStatsError;
        private static int _globalStatsTotal;

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
                    Trace.WriteLine(ex.GetType());
                    Trace.WriteLine(ex.Message);
                    Trace.WriteLine(ex.StackTrace);
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
                    Trace.WriteLine("Enabling SSH Tunnel");
                    SSHTunnel.Connect();
                }

                var startTime = DateTime.Now;
                Trace.WriteLine("Loading DB...");

                try
                {
                    SQLConnector.Connect();
                    SQLDatabase.GrabData();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.GetType());
                    Trace.WriteLine(ex.Message);
                    Trace.WriteLine(ex.StackTrace);
                    SQLConnector.Enabled = false; // Something failed, disabling everything SQL related
                }
                
                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Trace.WriteLine(string.Format("Finished loading DB in {0}.", span.ToFormattedString()));
                Trace.WriteLine(Environment.NewLine);
            }
        }

        private static void DumpSQLs(string prefix, string fileName, Builder builder, SQLOutputFlags sqlOutput)
        {
            if (builder == null)
                return;

            var store = new SQLStore(fileName);

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.GameObjectTemplate))
                store.WriteData(builder.GameObjectTemplate());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.GameObjectSpawns))
                store.WriteData(builder.GameObjectSpawns());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.QuestTemplate))
                store.WriteData(builder.QuestTemplate());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.QuestPOI))
                store.WriteData(builder.QuestPOI());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureTemplate))
                store.WriteData(builder.NpcTemplate());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureSpawns))
                store.WriteData(builder.CreatureSpawns());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcTrainer))
                store.WriteData(builder.NpcTrainer());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcVendor))
                store.WriteData(builder.NpcVendor());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcText))
                store.WriteData(builder.PageText());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.PageText))
                store.WriteData(builder.NpcText());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.Gossip))
                store.WriteData(builder.Gossip());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.Loot))
                store.WriteData(builder.Loot());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.SniffData | SQLOutputFlags.SniffDataOpcodes))
                store.WriteData(builder.SniffData());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.StartInformation))
                store.WriteData(builder.StartInformation());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.ObjectNames))
                store.WriteData(builder.ObjectNames());

            if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureEquip))
                store.WriteData(builder.CreatureEquip());

            Trace.WriteLine(string.Format("{0}: Saved file to '{1}'", prefix, fileName));
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
                    Trace.WriteLine(string.Format("Save file {0} is in use, parsing will not be done.", outFileName));
                    return;
                }
            }

            var stuffing = globalStuffing ?? new Stuffing();
            var fileInfo = new SniffFileInfo { FileName = file, Stuffing = stuffing };
            var fileName = Path.GetFileName(fileInfo.FileName);

            Trace.WriteLine(string.Format("{0}: Reading packets...", prefix));

            Builder builder = globalBuilder ?? (Settings.SQLOutput > 0 ? new Builder(stuffing) : null);

            try
            {
                var packets = Reader.Read(fileInfo);
                if (packets.Count == 0)
                {
                    Trace.WriteLine(string.Format("{0}: Packet count is 0", prefix));
                    return;
                }

                if (Settings.DumpFormat == DumpFormatType.Bin || Settings.DumpFormat == DumpFormatType.Pkt)
                {
                    SniffType format = Settings.DumpFormat == DumpFormatType.Bin ? SniffType.Bin : SniffType.Pkt;
                    var fileExtension = Settings.DumpFormat.ToString().ToLower();

                    if (Settings.SplitOutput)
                    {
                        Trace.WriteLine(string.Format("{0}: Splitting {1} packets to multiple files in {2} format...", prefix, packets.Count, fileExtension));
                        SplitBinaryPacketWriter.Write(packets, Encoding.ASCII);
                    }
                    else
                    {
                        Trace.WriteLine(string.Format("{0}: Copying {1} packets to .{2} format...", prefix, packets.Count, fileExtension));

                        var dumpFileName = Path.ChangeExtension(file, null) + "_excerpt." + fileExtension;
                        var writer = new BinaryPacketWriter(format, dumpFileName, Encoding.ASCII);
                        writer.Write(packets);
                    }
                }
                else
                {
                    Trace.WriteLine(string.Format("{0}: Parsing {1} packets. Assumed version {2}", prefix, packets.Count, ClientVersion.GetVersionString()));

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
                        Trace.WriteLine(string.Format("{0}: Saved file to '{1}'", prefix, outLogFileName));
                        Handler.WriteToFile(packets, outLogFileName);
                    }

                    if (Settings.StatsOutput == StatsOutputFlags.None)
                        return;

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

                    if (Settings.StatsOutput.HasAnyFlag(StatsOutputFlags.Global))
                    {
                        lock (_lockStats)
                        {
                            _globalStatsOk = statsOk;
                            _globalStatsError = statsError;
                            _globalStatsSkip = statsSkip;
                            _globalStatsTotal = (int)total;
                        }
                    }

                    if (Settings.StatsOutput.HasAnyFlag(StatsOutputFlags.Local))
                    {
                        Trace.WriteLine(string.Format("{0}: Parsed {1:F3}% packets successfully, {2:F3}% with errors and skipped {3:F3}% in {4}.",
                            prefix, (double)statsOk / total * 100, (double)statsError / total * 100, (double)statsSkip / total * 100,
                            span.ToFormattedString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.GetType());
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
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
            SetUpListeners();

            string[] files = GetFiles(args);
            if (files.Length == 0)
            {
                Trace.WriteLine(args.Length == 0
                                    ? "No files specified."
                                    : string.Format("No files found with pattern {0}", args[0]));
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

            var numberOfThreads = Settings.Threads != 0 ? Settings.Threads.ToString(CultureInfo.InvariantCulture) : "a recommended number of";
            Trace.WriteLine(string.Format("Using {0} threads to process {1} files", numberOfThreads, files.Length));

            var startTime = DateTime.Now;
            var count = 0;

            if (Settings.Threads == 0) // Number of threads is automatically choosen by the Parallel library
                files.AsParallel().SetCulture()
                    .ForAll(file =>
                        ReadFile(file, stuffing, builder, "[" + (++count).ToString(CultureInfo.InvariantCulture) + "/" + files.Length + " " + file + "]"));
            else
                files.AsParallel().SetCulture().WithDegreeOfParallelism(Settings.Threads)
                    .ForAll(file => ReadFile(file, stuffing, builder, "[" + (++count).ToString(CultureInfo.InvariantCulture) + "/" + files.Length + " " + file + "]"));

            if (Settings.StatsOutput.HasAnyFlag(StatsOutputFlags.Global))
            {
                var span = DateTime.Now.Subtract(startTime);
                Trace.WriteLine(string.Format("Parsed {0} packets from {1} files: {2:F3}% successfully, {3:F3}% with errors and skipped {4:F3}% in {5} Minutes, {6} Seconds and {7} Milliseconds.",
                    _globalStatsTotal, files.Length, (double)_globalStatsOk / _globalStatsTotal * 100,
                    (double)_globalStatsError / _globalStatsTotal * 100, (double)_globalStatsSkip / _globalStatsTotal * 100,
                    span.Minutes, span.Seconds, span.Milliseconds));
            }

            DumpSQLs("Dumping global sql", Settings.SQLFileName, builder, Settings.SQLOutput);

            SQLConnector.Disconnect();
            SSHTunnel.Disconnect();
            Logger.WriteErrors();
            EndPrompt();
        }

        private static void SetUpListeners()
        {
            Trace.Listeners.Clear();

            var consoleListener = new ConsoleTraceListener(true);
            Trace.Listeners.Add(consoleListener);

            if (Settings.ParsingLog)
            {
                var fileListener =
                    new TextWriterTraceListener(string.Format("parsing_log_{0}.txt", Path.GetRandomFileName()));
                fileListener.Name = "ConsoleMirror";
                Trace.Listeners.Add(fileListener);
            }

            Trace.AutoFlush = true;
        }
    }
}
