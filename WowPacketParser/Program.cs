using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL;
using DBCc = WowPacketParser.DBC.DBCStore.DBC;

namespace WowPacketParser
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (args.Length == 0) // Couldn't use args == null here...
            {
                Console.WriteLine("Could not find file for reading.");
                EndPrompt();
                return;
            }

            // SQLConnector.Connect(); // Connect to DB - we should only connect when it is needed, move this

            #region DebugOutput
            DateTime startTime;
            DateTime endTime;
            TimeSpan span;
            #endregion

            string filters = ConfigurationManager.AppSettings["Filters"];
            string sqloutput = ConfigurationManager.AppSettings["SQLOutput"];
            string nodump = ConfigurationManager.AppSettings["NoDump"];
            int packetsToRead = 0;
            try
            {
                packetsToRead = int.Parse(ConfigurationManager.AppSettings["PacketsNum"]);
            }
            catch (Exception) {}

            if (DBCc.Enabled())
            {
                #region DebugOutput
                startTime = DateTime.Now;
                Console.WriteLine("Loading DBCs");
                #endregion
                new DBC.DBCLoader();
                #region DebugOutput
                endTime = DateTime.Now;
                span = endTime.Subtract(startTime);
                Console.WriteLine("Finished loading DBCs - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
                Console.WriteLine();
                #endregion
            }

            string [] files = args;
            foreach (string file in files)
            {
                Console.WriteLine("Reading file [" + System.IO.Path.GetFileName(file) + "]");
                try
                {
                    var packets = Reader.Read(file, filters, packetsToRead);
                    if (packets == null)
                    {
                        #region DebugOutput
                        Console.Clear();
                        Console.WriteLine("Could not open file [" + file + "] for reading.");
                        EndPrompt();
                        Console.Clear();
                        #endregion
                        continue;
                    }

                    if (packets.Count() > 0)
                    {
                        #region DebugOutput
                        Console.WriteLine("Parsing {0} packets...", packets.Count());
                        startTime = DateTime.Now;
                        #endregion

                        var fullPath = Utilities.GetPathFromFullPath(file);
                        SQLStore.Initialize(Path.Combine(fullPath, file + ".sql"), sqloutput);

                        Handler.InitializeLogFile(Path.Combine(fullPath, file + ".txt"), nodump);
                        foreach (var packet in packets)
                            Handler.Parse(packet);

                        SQLStore.WriteToFile();
                        Handler.WriteToFile();
                        #region DebugOutput
                        endTime = DateTime.Now;
                        span = endTime.Subtract(startTime);
                        // Need to open a new writer to console, last one was redirected to the file and is now closed.
                        StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                        standardOutput.AutoFlush = true;
                        Console.SetOut(standardOutput);
                        Console.WriteLine("Finished parsing in - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
                        Console.WriteLine();
                        #endregion
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
                    EndPrompt();
                }
            }

            SQLConnector.Disconnect();
        }

        private static void EndPrompt()
        {
            bool Prompt = ConfigurationManager.AppSettings["ShowEndPrompt"].Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase);
            if (Prompt)
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                Console.WriteLine();
            }
        }
    }
}
