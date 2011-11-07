using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    public static class Handler
    {
        static Handler()
        {
            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsAbstract)
                    continue;

                if (!type.IsPublic)
                    continue;

                var methods = type.GetMethods();

                foreach (var method in methods)
                {
                    if (!method.IsPublic)
                        continue;

                    var attrs = (ParserAttribute[])method.GetCustomAttributes(typeof(ParserAttribute),
                        false);

                    if (attrs.Length <= 0)
                        continue;

                    var parms = method.GetParameters();

                    if (parms.Length <= 0)
                        continue;

                    if (parms[0].ParameterType != typeof(Packet))
                        continue;

                    foreach (var attr in attrs)
                    {
                        var opc = attr.Opcode;

                        var del = (Action<Packet>)Delegate.CreateDelegate(typeof(Action<Packet>), method);

                        Handlers[opc] = del;
                    }
                }
            }
        }

        public static void InitializeLogFile(string file, bool noDump)
        {
            _noDump = noDump;
            if (_noDump)
                return;

            File.Delete(file);
            _writer = new StreamWriter(file, true);
            Console.SetOut(_writer);
        }

        private static bool _noDump;

        private static StreamWriter _writer;

        private static readonly Dictionary<int, Action<Packet>> Handlers =
            new Dictionary<int, Action<Packet>>();

        public static void WriteToFile()
        {
            if (_noDump)
                return;

            _writer.Flush();
            _writer.Close();
            _writer = null;
        }

        public static void Parse(Packet packet, ref Statistics stats)
        {
            var opcode = packet.Opcode;

            Console.WriteLine("{0}: {1} (0x{2}) Length: {3} Time: {4} Number: {5}",
                packet.Direction, Opcodes.GetOpcodeName(opcode), opcode.ToString("X4"),
                packet.GetLength(), packet.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"), packet.Number);

            Action<Packet> handler;
            if (Handlers.TryGetValue(opcode, out handler))
            {
                try
                {
                    handler(packet);

                    if (packet.GetPosition() == packet.GetLength())
                        stats.PacketsSuccessfullyParsed++;
                    else
                    {
                        var pos = packet.GetPosition();
                        var len = packet.GetLength();
                        Console.WriteLine("Packet not fully read! Current position is {0}, length is {1}, and diff is {2}.",
                            pos, len, len - pos);

                        if (len < 300) // If the packet isn't "too big" and it is not full read, print its hex table
                            Console.WriteLine(packet.AsHex());

                        stats.PacketsParsedWithErrors++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType());
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);

                    stats.PacketsParsedWithErrors++;
                }
            }
            else
            {
                Console.WriteLine(packet.AsHex());
                stats.PacketsNotParsed++;
            }

            Console.WriteLine();
        }
    }
}
