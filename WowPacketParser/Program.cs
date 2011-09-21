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
            try
            {
                string file = args[0]; // first argument
                string filters = ConfigurationManager.AppSettings["Filters"];
                string sqloutput = ConfigurationManager.AppSettings["SQLOutput"];
                string nodump = ConfigurationManager.AppSettings["NoDump"];
                int packetsToRead = 0;
                try
                {
                    packetsToRead = int.Parse(ConfigurationManager.AppSettings["PacketsNum"]);
                }
                catch(Exception){}

                Console.WriteLine("Reading file: " + file);
                var packets = Reader.Read(file, filters, packetsToRead);
                if (packets == null)
                {
                    Console.WriteLine("Could not open file " + file + " for reading.");
                    EndPrompt();
                    return;
                }

                if (packets.Count() > 0)
                {
                    var fullPath = Utilities.GetPathFromFullPath(file);
                    SQLStore.Initialize(Path.Combine(fullPath, file + ".sql"), sqloutput);

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
                    }
                    Console.WriteLine("Started parsing {0} packets...", packets.Count());
                    startTime = DateTime.Now;
#endregion
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
                    Console.WriteLine("Finished parsing - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
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

            SQLConnector.Disconnect();
        }

        private static void EndPrompt()
        {
            int noPrompt = 0;
            try
            {
                noPrompt = int.Parse(ConfigurationManager.AppSettings["NoPrompt"]);
            }
            catch(Exception){}
            if (noPrompt != 1)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
