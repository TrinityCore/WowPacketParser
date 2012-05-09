using System;
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
using Builder = WowPacketParser.SQL.Builder;

namespace WowPacketParser
{
    public static class Program
    {
        private static readonly Object LockStats = new Object();
        private static int GlobalStatsOk;
        private static int GlobalStatsSkip;
        private static int GlobalStatsError;
        private static int GlobalStatsTotal;

        private static void Main(string[] args)
        {
            Utilities.SetUpListeners();
            var files = args.ToList();
            if (files.Count == 0)
            {
                PrintUsage();
                return;
            }

            // config options are handled in Misc.Settings
            Utilities.RemoveConfigOptions(ref files);

            if (!Utilities.GetFiles(ref files))
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

            if (Settings.FilterPacketNumLow > 0 && Settings.FilterPacketNumHigh > 0
                && Settings.FilterPacketNumLow > Settings.FilterPacketNumHigh)
                throw new ConstraintException("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

            // Disable DB when we don't need its data (dumping to a binary file)
            if (Settings.DumpFormat == DumpFormatType.Pkt)
            {
                SQLConnector.Enabled = false;
                SSHTunnel.Enabled = false;
            }
            else
                Filters.Initialize();

            SQLConnector.ReadDB();

            var numberOfThreadsRead = Settings.ThreadsRead != 0 ? Settings.ThreadsRead.ToString(CultureInfo.InvariantCulture) : "a recommended number of";
            var numberOfThreadsParse = Settings.ThreadsParse != 0 ? Settings.ThreadsParse.ToString(CultureInfo.InvariantCulture) : "a recommended number of";
            Trace.WriteLine(string.Format("Using {0} threads to process {1} files", numberOfThreadsRead, files.Count));

            var startTime = DateTime.Now;
            var count = 0;

            switch (Settings.ThreadsRead)
            {
                case 0: // Number of threads is automatically chosen by the Parallel library
                {
                    files.AsParallel().SetCulture()
                        .ForAll(file =>
                        ReadFile(file, "[" + (++count).ToString(CultureInfo.InvariantCulture) + "/" + files.Count + " " + file + "]"));
                    break;
                }
                case 1: // No multithreading, lowest amount of memory use
                {
                    foreach (var file in files)
                        ReadFile(file, "[" + (++count).ToString(CultureInfo.InvariantCulture) + "/" + files.Count + " " + file + "]");
                    break;
                }
                default: // User defined number of threads
                {
                    files.AsParallel().SetCulture().WithDegreeOfParallelism(Settings.ThreadsRead)
                        .ForAll(file =>
                        ReadFile(file, "[" + (++count).ToString(CultureInfo.InvariantCulture) + "/" + files.Count + " " + file + "]"));
                    break;
                }
            }

            if (Settings.StatsOutput.HasAnyFlag(StatsOutputFlags.Global))
            {
                var span = DateTime.Now.Subtract(startTime);
                Trace.WriteLine(string.Format("Parsed {0} packets from {1} files: {2:F3}% successfully, {3:F3}% with errors and skipped {4:F3}% in {5} Minutes, {6} Seconds and {7} Milliseconds using {8} threads",
                    GlobalStatsTotal, files.Count, (double)GlobalStatsOk / GlobalStatsTotal * 100,
                    (double)GlobalStatsError / GlobalStatsTotal * 100, (double)GlobalStatsSkip / GlobalStatsTotal * 100,
                    span.Minutes, span.Seconds, span.Milliseconds, numberOfThreadsParse));
            }

            if (Settings.SQLOutput != 0)
            {
                string sqlFileName;
                if (!String.IsNullOrWhiteSpace(Settings.SQLFileName))
                    sqlFileName = Settings.SQLFileName;
                else if (files.Count == 1)
                    sqlFileName = string.Format("{0}_{1}.sql", Utilities.FormattedDateTimeForFiles(), Path.GetFileName(files[0]));
                else
                    sqlFileName = string.Format("{0}_multi_files.sql", Utilities.FormattedDateTimeForFiles());

                Builder.DumpSQL("Dumping global sql", sqlFileName, Settings.SQLOutput);
            }

            SQLConnector.Disconnect();
            SSHTunnel.Disconnect();
            Logger.WriteErrors();
            EndPrompt();
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

                    switch (Settings.ThreadsParse)
                    {
                        case 0: // Number of threads is automatically chosen by the Parallel library
                        {
                            packets.AsParallel().SetCulture().ForAll(packet => Handler.Parse(packet));
                            break;
                        }
                        case 1: // No multithreading, lowest amount of memory use
                        {
                            foreach (var packet in packets)
                                Handler.Parse(packet);
                            break;
                        }
                        default: // User defined number of threads
                        {
                            packets.AsParallel().SetCulture().WithDegreeOfParallelism(Settings.ThreadsParse).ForAll(packet => Handler.Parse(packet));
                            break;
                        }
                    }

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

        private static void PrintUsage()
        {
            Console.WriteLine("Error: No files selected to be parsed.");
            Console.WriteLine("Usage: Drag a file, or group of files on the executable to parse it.");
            Console.WriteLine("Command line usage: WowPacketParser.exe [/ConfigFile path /Option1 value1 ...] filetoparse1 ...");
            Console.WriteLine("/ConfigFile path - file to read config from, default: WowPacketParser.exe.config.");
            Console.WriteLine("/Option1 value1 - override Option1 setting from config file with value1.");
            Console.WriteLine("Configuration: Modify WowPacketParser.exe.config file.");
            EndPrompt();
        }
    }
}
