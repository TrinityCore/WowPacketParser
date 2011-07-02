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
      
            // SQLConnector.Connect(); // Connect to DB - we should only connect when it is needed, move this
            var DBCloader = new DBC.DBCLoader();

            string filters = ConfigurationManager.AppSettings["Filters"];
            string sqloutput = ConfigurationManager.AppSettings["SQLOutput"];
            string nodump = ConfigurationManager.AppSettings["NoDump"];

            try
            {
                var file = args[0]; // first argument

                var packets = Reader.Read(file, filters);
                if (packets == null)
                {
                    Console.WriteLine("Could not open file " + file + " for reading.");
                    SQLConnector.Disconnect();
                    return;
                }

                if (packets.Count() > 0)
                {
                    var fullPath = Utilities.GetPathFromFullPath(file);
                    Handler.InitializeLogFile(Path.Combine(fullPath, file + ".txt"), nodump);
                    SQLStore.Initialize(Path.Combine(fullPath, file + ".sql"), sqloutput);                    

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
            Console.ResetColor();
        }
    }
}
