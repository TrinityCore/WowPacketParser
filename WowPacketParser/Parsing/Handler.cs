using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

        private static readonly Dictionary<int, Action<Packet>> Handlers =
            new Dictionary<int, Action<Packet>>();

        public static void WriteToFile(List<Packet> packets, string file)
        {
            File.Delete(file);
            var writer = new StreamWriter(file, true);

            foreach (var packet in packets)
                writer.WriteLine(packet.Writer);

            writer.Flush();
            writer.Close();
            writer = null;
        }

        public static void Parse(Packet packet, bool headerOnly = false, bool isMultiple = false)
        {
            var opcode = packet.Opcode;

            packet.Writer.WriteLine("{0}: {1} (0x{2}) Length: {3} Time: {4} Number: {5}{6}",
                packet.Direction, Opcodes.GetOpcodeName(opcode), opcode.ToString("X4"),
                packet.GetLength(), packet.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                packet.Number, isMultiple ? " (part of another packet)" : "");

            if (headerOnly)
            {
                if (!isMultiple)
                {
                    lock (Handlers)
                    {
                        Statistics.PacketsSuccessfullyParsed++;
                    }
                }
                return;
            }

            Action<Packet> handler;
            if (Handlers.TryGetValue(opcode, out handler))
            {
                try
                {
                    handler(packet);

                    if (packet.GetPosition() == packet.GetLength())
                    {
                        if (!isMultiple)
                        {
                            lock (Handlers)
                            {
                                Statistics.PacketsSuccessfullyParsed++;
                            }
                        }
                    }
                    else
                    {
                        var pos = packet.GetPosition();
                        var len = packet.GetLength();
                        packet.Writer.WriteLine("Packet not fully read! Current position is {0}, length is {1}, and diff is {2}.",
                            pos, len, len - pos);

                        if (len < 300) // If the packet isn't "too big" and it is not full read, print its hex table
                            packet.Writer.WriteLine(packet.AsHex());

                        if (!isMultiple)
                        {
                            lock (Handlers)
                            {
                                Statistics.PacketsParsedWithErrors++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    packet.Writer.WriteLine(ex.GetType());
                    packet.Writer.WriteLine(ex.Message);
                    packet.Writer.WriteLine(ex.StackTrace);

                    if (!isMultiple)
                    {
                        lock (Handlers)
                        {
                            Statistics.PacketsParsedWithErrors++;
                        }
                    }
                }
            }
            else
            {
                packet.Writer.WriteLine(packet.AsHex());
                if (!isMultiple)
                {
                    lock (Handlers)
                    {
                        Statistics.PacketsNotParsed++;
                    }
                }
            }
        }
    }
}
