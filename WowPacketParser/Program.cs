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
using DBCStore = WowPacketParser.DBC.DBCStore.DBC;

namespace WowPacketParser
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // Read config options
            string[] filters = null;
            string[] ignoreFilters = null;
            bool sqlOutput = false;
            bool noDump = false;
            int packetsToRead = 0; // 0 -> All packets
            int packetNumberLow = 0; // 0 -> No low limit
            int packetNumberHigh = 0; // 0 -> No high limit
            bool prompt = false;

            try
            {
                packetNumberLow = int.Parse(ConfigurationManager.AppSettings["FilterPacketNumLow"]);
                packetNumberHigh = int.Parse(ConfigurationManager.AppSettings["FilterPacketNumHigh"]);

                if (packetNumberLow > 0 && packetNumberHigh > 0 && packetNumberLow > packetNumberHigh)
                    throw new System.Exception("FilterPacketNumLow must be less or equal than FilterPacketNumHigh");

                string filtersString = ConfigurationManager.AppSettings["Filters"];
                if (filtersString != null)
                    filters = filtersString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                filtersString = ConfigurationManager.AppSettings["IgnoreFilters"];
                if (filtersString != null)
                    ignoreFilters = filtersString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                sqlOutput = ConfigurationManager.AppSettings["SQLOutput"].Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase);
                noDump = ConfigurationManager.AppSettings["NoDump"].Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase);
                packetsToRead = int.Parse(ConfigurationManager.AppSettings["PacketsNum"]);
                prompt = ConfigurationManager.AppSettings["ShowEndPrompt"].Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            // Quit if no arguments are given
            if (args.Length == 0)
            {
                Console.WriteLine("Could not find file for reading.");
                EndPrompt(prompt);
                return;
            }

            // Read DBCs
            if (DBCStore.Enabled())
            {
                var startTime = DateTime.Now;
                Console.WriteLine("Loading DBCs");

                new DBC.DBCLoader();

                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Console.WriteLine("Finished loading DBCs - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
                Console.WriteLine();
            }

            // Read binaries
            string [] files = args;
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
                }
            }

            foreach (string file in files)
            {
                Console.WriteLine("Reading file [" + System.IO.Path.GetFileName(file) + "]");
                try
                {
                    var packets = Reader.Read(file, filters, ignoreFilters, packetNumberLow, packetNumberHigh, packetsToRead);
                    if (packets.Count > 0)
                    {
                        ClientVersion.SetVersion(packets[0].GetTime());

                        // debug, will remove
                        Console.WriteLine("TIME PACKET: " + packets[0].GetTime());
                        Console.WriteLine("VERSION: " + ClientVersion.Version);
                        
                        Console.WriteLine("Parsing {0} packets...", packets.Count());
                        var startTime = DateTime.Now;

                        var fullPath = Utilities.GetPathFromFullPath(file);
                        SQLStore.Initialize(Path.Combine(fullPath, file + ".sql"), sqlOutput);

                        Handler.InitializeLogFile(Path.Combine(fullPath, file + ".txt"), noDump);
                        foreach (var packet in packets)
                            Handler.Parse(packet);

                        SQLStore.WriteToFile();
                        Handler.WriteToFile();

                        var endTime = DateTime.Now;
                        var span = endTime.Subtract(startTime);
                        // Need to open a new writer to console, last one was redirected to the file and is now closed.
                        var standardOutput = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true};
                        Console.SetOut(standardOutput);
                        Console.WriteLine("Finished parsing in - {0} Minutes, {1} Seconds and {2} Milliseconds.", span.Minutes, span.Seconds, span.Milliseconds);
                        Console.WriteLine();
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
                    EndPrompt(prompt);
                }
            }
        }

        private static void EndPrompt(bool prompt)
        {
            if (!prompt)
                return;

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}
