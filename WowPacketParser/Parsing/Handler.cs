using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    public static class Handler
    {
        private static Dictionary<int, Action<Packet>> LoadHandlers()
        {
            var handlers = new Dictionary<int, Action<Packet>>(1000);

            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                //if (type.Namespace != "WowPacketParser.Parsing.Parsers")
                //    continue;

                if (!type.IsAbstract)
                    continue;

                if (!type.IsPublic)
                    continue;

                var methods = type.GetMethods();

                foreach (var method in methods)
                {
                    if (!method.IsPublic)
                        continue;

                    var attrs = (ParserAttribute[])method.GetCustomAttributes(typeof(ParserAttribute), false);

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
                        if (opc == 0)
                            continue;

                        var del = (Action<Packet>)Delegate.CreateDelegate(typeof(Action<Packet>), method);

                        if (handlers.ContainsKey(opc))
                        {
                            Trace.WriteLine(string.Format("Error: (Build: {0}) tried to overwrite delegate for opcode {1} ({2}); new handler: {3}; old handler: {4}",
                                ClientVersion.Build, opc, Opcodes.GetOpcodeName(opc), del.Method, handlers[opc].Method));
                            continue;
                        }

                        handlers[opc] = del;
                    }
                }
            }

            return handlers;
        }

        private static readonly Dictionary<int, Action<Packet>> Handlers = LoadHandlers();

        public static void Parse(Packet packet, bool isMultiple = false)
        {
            ParsedStatus status;

            var opcode = packet.Opcode;

            packet.WriteLine(packet.GetHeader(isMultiple));

            if (opcode == 0)
                return;

            Action<Packet> handler;
            if (Handlers.TryGetValue(opcode, out handler))
            {
                if (Settings.DumpFormat == DumpFormatType.SniffDataOnly)
                {
                    var attrs = handler.Method.GetCustomAttributes(typeof (HasSniffDataAttribute), false);

                    packet.AddSniffData(StoreNameType.Opcode, packet.Opcode, Opcodes.GetOpcodeName(packet.Opcode));

                    if (attrs.Length == 0)
                    {
                        packet.Status = ParsedStatus.NotParsed;
                        return; // skip parsing "useless" packets when in SniffData-only-mode
                    }
                }

                try
                {
                    handler(packet);

                    if (packet.Position == packet.Length)
                        status = ParsedStatus.Success;
                    else
                    {
                        var pos = packet.Position;
                        var len = packet.Length;
                        packet.WriteLine("Packet not fully read! Current position is {0}, length is {1}, and diff is {2}.",
                            pos, len, len - pos);

                        if (len < 300) // If the packet isn't "too big" and it is not full read, print its hex table
                            packet.AsHex();

                        status = ParsedStatus.WithErrors;
                    }
                }
                catch (Exception ex)
                {
                    packet.WriteLine(ex.GetType().ToString());
                    packet.WriteLine(ex.Message);
                    packet.WriteLine(ex.StackTrace);

                    status = ParsedStatus.WithErrors;
                }
            }
            else
            {
                packet.AsHex();
                status = ParsedStatus.NotParsed;
            }

            if (!isMultiple)
            {
                packet.Status = status;

                if (Settings.DumpFormat != DumpFormatType.SniffDataOnly)
                {
                    // added before for this type
                    var data = status == ParsedStatus.Success ? Opcodes.GetOpcodeName(packet.Opcode) : status.ToString();
                    packet.AddSniffData(StoreNameType.Opcode, packet.Opcode, data);
                }
            }
        }
    }
}
