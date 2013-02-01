using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Parsing.Parsers;

namespace WowPacketParser
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            SetUpConsole();

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

            if (Settings.FilterPacketNumLow < 0)
                throw new ConstraintException("FilterPacketNumLow must be positive");

            if (Settings.FilterPacketNumHigh < 0)
                throw new ConstraintException("FilterPacketNumHigh must be positive");

            if (Settings.FilterPacketNumLow > 0 && Settings.FilterPacketNumHigh > 0
                && Settings.FilterPacketNumLow > Settings.FilterPacketNumHigh)
                throw new ConstraintException("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

            // Disable DB when we don't need its data (dumping to a binary file)
            if (Settings.DumpFormat == DumpFormatType.None || Settings.DumpFormat == DumpFormatType.Pkt ||
                Settings.DumpFormat == DumpFormatType.PktSplit || Settings.DumpFormat == DumpFormatType.SniffDataOnly)
            {
                SQLConnector.Enabled = false;
                SSHTunnel.Enabled = false;
            }
            else
                Filters.Initialize();

            SQLConnector.ReadDB();

            var count = 0;
            foreach (var file in files)
            {
                SessionHandler.z_streams.Clear();
                ClientVersion.SetVersion(Settings.ClientBuild);
                new SniffFile(file, Settings.DumpFormat, Tuple.Create(++count, files.Count)).ProcessFile();
            }

            if (!String.IsNullOrWhiteSpace(Settings.SQLFileName))
                Builder.DumpSQL("Dumping global sql", Settings.SQLFileName, "# multiple files\n");

            SQLConnector.Disconnect();
            SSHTunnel.Disconnect();
            Logger.WriteErrors();

            EndPrompt();
        }

        private static void EndPrompt(bool forceKey = false)
        {
            if (Settings.ShowEndPrompt || forceKey)
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
            EndPrompt(true);
        }

        private static void SetUpConsole()
        {
            Console.Title = "WowPacketParser";

            Trace.Listeners.Clear();

            using (var consoleListener = new ConsoleTraceListener(true))
                Trace.Listeners.Add(consoleListener);

            if (Settings.ParsingLog)
            {
                using (var fileListener = new TextWriterTraceListener(String.Format("{0}_log.txt", Utilities.FormattedDateTimeForFiles())))
                {
                    fileListener.Name = "ConsoleMirror";
                    Trace.Listeners.Add(fileListener);
                }
            }

            Trace.AutoFlush = true;
        }
    }
}
