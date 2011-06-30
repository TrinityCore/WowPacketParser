using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL.Store;

namespace WowPacketParser
{
    public static class Program
    {
        public static CommandLine CmdLine { get; private set; }

        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      
            CmdLine = new CommandLine(args);

            Connector.Connect(); // Connect to DB

            string file;
            string filters;
            string sqloutput;
            string nodump;

            try
            {
                file = CmdLine.GetValue("-file");
                filters = CmdLine.GetValue("-filters");
                sqloutput = CmdLine.GetValue("-sql");
                nodump = CmdLine.GetValue("-nodump");
            }
            catch (IndexOutOfRangeException)
            {
                PrintUsage("All command line options require an argument.");
                Connector.Disconnect();
                return;
            }

            try
            {
                var packets = Reader.Read("kszor", file);
                if (packets == null)
                {
                    PrintUsage("Could not open file " + file + " for reading.");
                    Connector.Disconnect();
                    return;
                }

                if (packets.Count() > 0)
                {
                    var fullPath = Utilities.GetPathFromFullPath(file);
                    Handler.InitializeLogFile(Path.Combine(fullPath, file + ".txt"), nodump);
                    Store.Initialize(Path.Combine(fullPath, file + ".sql"), sqloutput);

                    var appliedFilters = filters.Split(',');

                    foreach (var packet in packets)
                    {
                        var opcode = packet.GetOpcode().ToString();
                        if (!string.IsNullOrEmpty(filters))
                        {
                            foreach (var opc in appliedFilters)
                            {
                                if (!opcode.Contains(opc))
                                    continue;

                                Handler.Parse(packet);
                                break;
                            }
                        }
                        else
                            Handler.Parse(packet);
                    }

                    Store.WriteToFile();
                    Handler.WriteToFile();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType());
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Connector.Disconnect();
            Console.ResetColor();
        }

        public static void PrintUsage(string error)
        {
            var n = Environment.NewLine;

            if (!string.IsNullOrEmpty(error))
                Console.WriteLine(error + n);

            var usage = "Usage: WoWPacketParser -file <input file> " +
                "[-filters opcode1,opcode2,...] [-sql <boolean>] [-nodump <boolean>]" + n + n +
                "-file\t\tThe file to read packets from." + n +
                "-filters\tComma-separated list of opcodes to parse." + n +
                "-sql\t\tSet to True to Activate SQL dumping." + n +
                "-nodump\t\tSet to True to disable file logging.";

            Console.WriteLine(usage);
        }
    }
}
