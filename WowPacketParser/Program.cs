﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing.Parsers;
using WowPacketParser.SQL;
using WowPacketParser.DBC;

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
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            // Disable DB when we don't need its data (dumping to a binary file)
            if (!Settings.DumpFormatWithSQL())
            {
                SQLConnector.Enabled = false;
                SSHTunnel.Enabled = false;
            }
            else
                Filters.Initialize();

            SQLConnector.ReadDB();

            if (Settings.UseDBC)
            {
                var startTime = DateTime.Now;

                DBC.DBC.Load();

                var span = DateTime.Now.Subtract(startTime);
                Trace.WriteLine($"DBC loaded in { span.ToFormattedString() }.");
            }

            var count = 0;
            foreach (var file in files)
            {
                SessionHandler.ZStreams.Clear();
                ClientVersion.SetVersion(Settings.ClientBuild);
                try
                {
                    var sf = new SniffFile(file, Settings.DumpFormat, Tuple.Create(++count, files.Count));
                    sf.ProcessFile();
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Can't process {file}. Skipping. Message: {ex.Message}");
                }

            }

            if (!string.IsNullOrWhiteSpace(Settings.SQLFileName) && Settings.DumpFormatWithSQL())
                Builder.DumpSQL("Dumping global sql", Settings.SQLFileName, SniffFile.GetHeader("multi"));

            SQLConnector.Disconnect();
            SSHTunnel.Disconnect();

            if (Settings.LogErrors)
                Logger.WriteErrors();

            Trace.Listeners.Remove("ConsoleMirror");

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
            Console.WriteLine("Command line usage: WowPacketParser.exe [--ConfigFile path --Option1 value1 ...] filetoparse1 ...");
            Console.WriteLine("--ConfigFile path - file to read config from, default: WowPacketParser.exe.config.");
            Console.WriteLine("--Option1 value1 - override Option1 setting from config file with value1.");
            Console.WriteLine("Configuration: Modify WowPacketParser.exe.config file.");
            EndPrompt(true);
        }

        private static void SetUpConsole()
        {
            Console.Title = "WowPacketParser";

            Trace.Listeners.Clear();

            using (ConsoleTraceListener consoleListener = new ConsoleTraceListener(true))
                Trace.Listeners.Add(consoleListener);

            if (Settings.ParsingLog)
            {
                using (TextWriterTraceListener fileListener = new TextWriterTraceListener($"{Utilities.FormattedDateTimeForFiles()}_log.txt"))
                {
                    fileListener.Name = "ConsoleMirror";
                    Trace.Listeners.Add(fileListener);
                }
            }

            Trace.AutoFlush = true;
        }
    }
}
