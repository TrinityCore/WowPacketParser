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
                return;
            }

            // SQLConnector.Connect(); // Connect to DB - we should only connect when it is needed, move this
#region DebugOutput
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            TimeSpan span;
#endregion
            try
            {
                string file = args[0]; // first argument
                string filters = ConfigurationManager.AppSettings["Filters"];
                string sqloutput = ConfigurationManager.AppSettings["SQLOutput"];
                string nodump = ConfigurationManager.AppSettings["NoDump"];

                Console.WriteLine("Reading file: \n" + file.ToString());
                var packets = Reader.Read(file, filters);
                if (packets == null)
                {
                    Console.WriteLine("Could not open file " + file + " for reading.");
                    return;
                }
#region DebugOutput
                Console.WriteLine("File readable.\n");
                Console.WriteLine("Reading DBCs");
                startTime = DateTime.Now;
#endregion
                if (packets.Count() > 0)
                {
                    var fullPath = Utilities.GetPathFromFullPath(file);
                    SQLStore.Initialize(Path.Combine(fullPath, file + ".sql"), sqloutput);                    

                    new DBC.DBCLoader();
#region DebugOutput
                    Console.WriteLine("Finished reading dbcs");
                    endTime = DateTime.Now;
                    span = endTime.Subtract(startTime);
                    Console.WriteLine("Dbc loading took us {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);

                    Console.WriteLine("\nStart parsing {0} packets...", packets.Count());
#endregion
                    Handler.InitializeLogFile(Path.Combine(fullPath, file + ".txt"), nodump);
                    startTime = DateTime.Now;
                    foreach (var packet in packets)
                        Handler.Parse(packet);

                    SQLStore.WriteToFile();
                    Handler.WriteToFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            SQLConnector.Disconnect();
#region DebugOutput
            endTime = DateTime.Now;
            span = endTime.Subtract(startTime);
            // Need to open a new writer to console, last one was redirected to the file and is now closed.
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.WriteLine("Parsing took us {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
            Console.Read();
#endregion
        }
    }
}
