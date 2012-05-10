using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using Builder = WowPacketParser.SQL.Builder;

namespace WowPacketParser
{
    public static class Program
    {
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

            var count = 0;
            foreach (var file in files)
                new SniffFile(file, Settings.DumpFormat, Settings.SplitOutput, Tuple.Create(++count, files.Count), Settings.SQLOutput).ProcessFile();

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
    }
}
