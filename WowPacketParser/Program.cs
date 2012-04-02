using System;
using System.Collections.Generic;
using System.Data;
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
using WowPacketParser.Store.Objects;
using WowPacketParser.Store.SQL;

namespace WowPacketParser
{
    public static class Program
    {
        private static readonly Object LockStats = new Object();
        private static int GlobalStatsOk;
        private static int GlobalStatsSkip;
        private static int GlobalStatsError;
        private static int GlobalStatsTotal;

        private static bool GetFiles(ref List<string> files)
        {
            if (files.Count == 1 && files[0].Contains('*'))
            {
                try
                {
                    files = Directory.GetFiles(@".\", files[0]).ToList();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.GetType());
                    Trace.WriteLine(ex.Message);
                    Trace.WriteLine(ex.StackTrace);
                    return false;
                }
            }

            for (var i = 0; i < files.Count; ++i)
            {
                if (!File.Exists(files[i]))
                {
                    Trace.WriteLine("File " + files[i] + " was not found, removed.");
                    files.RemoveAt(i);
                    --i;
                }
            }

            if (files.Count == 0)
            {
                Trace.WriteLine("No files specified.");
                return false;
            }

            return true;
        }

        private static void ReadDB()
        {
            if (!SQLConnector.Enabled) return;

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

        private static void DumpSQLs(string prefix, string fileName, SQLOutputFlags sqlOutput)
        {
            using (var store = new SQLStore(fileName))
            {
                if (sqlOutput.HasAnyFlag(SQLOutputFlags.GameObjectTemplate))
                    store.WriteData(Builder.GameObjectTemplate());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.GameObjectSpawns))
                    store.WriteData(Builder.GameObjectSpawns());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.QuestTemplate))
                    store.WriteData(Builder.QuestTemplate());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.QuestPOI))
                    store.WriteData(Builder.QuestPOI());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureTemplate))
                {
                    store.WriteData(Builder.NpcTemplate());
                    store.WriteData(Builder.NpcTemplateNonWDB());
                }

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureSpawns))
                    store.WriteData(Builder.CreatureSpawns());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcTrainer))
                    store.WriteData(Builder.NpcTrainer());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcVendor))
                    store.WriteData(Builder.NpcVendor());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcText))
                    store.WriteData(Builder.PageText());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.PageText))
                    store.WriteData(Builder.NpcText());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.Gossip))
                    store.WriteData(Builder.Gossip());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.Loot))
                    store.WriteData(Builder.Loot());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.SniffData | SQLOutputFlags.SniffDataOpcodes))
                    store.WriteData(Builder.SniffData());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.StartInformation))
                    store.WriteData(Builder.StartInformation());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.ObjectNames))
                    store.WriteData(Builder.ObjectNames());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureEquip))
                    store.WriteData(Builder.CreatureEquip());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureMovement))
                    store.WriteData(Builder.CreatureMovement());

                Trace.WriteLine(store.WriteToFile()
                                    ? string.Format("{0}: Saved file to '{1}'", prefix, fileName)
                                    : "No SQL files created -- empty.");
            }
        }

        private static void ReadFile(string file, string prefix)
        {
            // If our dump format requires a .txt to be created,
            // check if we can write to that .txt before starting parsing
            if (Settings.DumpFormat != DumpFormatType.Pkt)
            {
                var outFileName = Path.ChangeExtension(file, null) + "_parsed.txt";
                if (Utilities.FileIsInUse(outFileName))
                {
                    Trace.WriteLine(string.Format("Save file {0} is in use, parsing will not be done.", outFileName));
                    return;
                }
            }

            Trace.WriteLine(string.Format("{0}: Reading packets...", prefix));

            try
            {
                var packets = Reader.Read(file);
                if (packets.Count == 0)
                {
                    Trace.WriteLine(string.Format("{0}: Packet count is 0", prefix));
                    return;
                }

                if (Settings.DumpFormat == DumpFormatType.Pkt)
                {
                    const SniffType format = SniffType.Pkt;
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
                        BinaryPacketWriter.Write(format, dumpFileName, Encoding.ASCII, packets);
                    }
                }
                else
                {
                    Trace.WriteLine(string.Format("{0}: Parsing {1} packets. Assumed version {2}", prefix, packets.Count, ClientVersion.VersionString));

                    var total = (uint)packets.Count;
                    var startTime = DateTime.Now;
                    var outFileName = Path.ChangeExtension(file, null) + "_parsed";
                    var outLogFileName = outFileName + ".txt";

                    if (Settings.ThreadsParse == 0) // Number of threads is automatically choosen by the Parallel library
                        packets.AsParallel().SetCulture().ForAll(packet => Handler.Parse(packet));
                    else
                        packets.AsParallel().SetCulture().WithDegreeOfParallelism(Settings.ThreadsParse).ForAll(packet => Handler.Parse(packet));

                    if (Settings.DumpFormat != DumpFormatType.None)
                    {
                        Trace.WriteLine(string.Format("{0}: Saved file to '{1}'", prefix, outLogFileName));
                        Handler.WriteToFile(packets, outLogFileName);
                    }

                    // Force to close allocated resources
                    foreach (var packet in packets)
                        packet.Dispose();

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

                        packet.ClosePacket();
                    }
                    packets.Clear();

                    if (Settings.StatsOutput.HasAnyFlag(StatsOutputFlags.Global))
                    {
                        lock (LockStats)
                        {
                            GlobalStatsOk = statsOk;
                            GlobalStatsError = statsError;
                            GlobalStatsSkip = statsSkip;
                            GlobalStatsTotal = (int)total;
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
            var files = args.ToList();
            if (!GetFiles(ref files))
            {
                EndPrompt();
                return;
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            ClientVersion.SetVersion(Settings.ClientBuild);

            if (Settings.FilterPacketNumLow < 0)
                throw new ConstraintException("FilterPacketNumLow must be positive");

            if (Settings.FilterPacketNumHigh < 0)
                throw new ConstraintException("FilterPacketNumHigh must be positive");

            if (Settings.FilterPacketNumLow > Settings.FilterPacketNumHigh)
                throw new ConstraintException("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

            // Disable DB when we don't need its data (dumping to a binary file)
            if (Settings.DumpFormat == DumpFormatType.Pkt)
            {
                SQLConnector.Enabled = false;
                SSHTunnel.Enabled = false;
            }
            else
                Filters.Initialize();

            // Read DB
            ReadDB();

            var numberOfThreadsRead = Settings.ThreadsRead != 0 ? Settings.ThreadsRead.ToString(CultureInfo.InvariantCulture) : "a recommended number of";
            var numberOfThreadsParse = Settings.ThreadsParse != 0 ? Settings.ThreadsParse.ToString(CultureInfo.InvariantCulture) : "a recommended number of";
            Trace.WriteLine(string.Format("Using {0} threads to process {1} files", numberOfThreadsRead, files.Count));

            var startTime = DateTime.Now;
            var count = 0;

            if (Settings.ThreadsRead == 0) // Number of threads is automatically choosen by the Parallel library
                files.AsParallel().SetCulture()
                    .ForAll(file =>
                        ReadFile(file, "[" + (++count).ToString(CultureInfo.InvariantCulture) + "/" + files.Count + " " + file + "]"));
            else
                files.AsParallel().SetCulture().WithDegreeOfParallelism(Settings.ThreadsRead)
                    .ForAll(file => ReadFile(file, "[" + (++count).ToString(CultureInfo.InvariantCulture) + "/" + files.Count + " " + file + "]"));

            if (Settings.StatsOutput.HasAnyFlag(StatsOutputFlags.Global))
            {
                var span = DateTime.Now.Subtract(startTime);
                Trace.WriteLine(string.Format("Parsed {0} packets from {1} files: {2:F3}% successfully, {3:F3}% with errors and skipped {4:F3}% in {5} Minutes, {6} Seconds and {7} Milliseconds using {8} threads",
                    GlobalStatsTotal, files.Count, (double)GlobalStatsOk / GlobalStatsTotal * 100,
                    (double)GlobalStatsError / GlobalStatsTotal * 100, (double)GlobalStatsSkip / GlobalStatsTotal * 100,
                    span.Minutes, span.Seconds, span.Milliseconds, numberOfThreadsParse));
            }

            string sqlFileName;
            if (String.IsNullOrWhiteSpace(Settings.SQLFileName))
                sqlFileName = files.Aggregate(string.Empty, (current, file) => current + Path.GetFileNameWithoutExtension(file)) + ".sql";
            else
                sqlFileName = Settings.SQLFileName;

            DumpSQLs("Dumping global sql", sqlFileName, Settings.SQLOutput);

            SQLConnector.Disconnect();
            SSHTunnel.Disconnect();
            Logger.WriteErrors();
            EndPrompt();
        }

        private static void SetUpListeners()
        {
            Trace.Listeners.Clear();

            using (var consoleListener = new ConsoleTraceListener(true))
                Trace.Listeners.Add(consoleListener);

            if (Settings.ParsingLog)
            {
                using (var fileListener = new TextWriterTraceListener(string.Format("parsing_log_{0}.txt", Path.GetRandomFileName())))
                {
                    fileListener.Name = "ConsoleMirror";
                    Trace.Listeners.Add(fileListener);
                }
            }

            Trace.AutoFlush = true;
        }
    }
}
