using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using System;
using PacketParser.DataStructures;
using System.IO;

namespace PacketParser.Parsing
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
                //if (type.Namespace != "PacketParser.Parsing.Parsers")
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
        [ThreadStatic]
        private static Dictionary<int, Action<Packet>> Handlers = null;
        [ThreadStatic]
        private static int lastUID;

        public static void InitForClientVersion()
        {
            lastUID = 0;
            Handlers = LoadHandlers();
        }

        public static void Parse(Packet packet, bool checkLength = true)
        {
            var opcode = packet.Opcode;
            packet.ParseID = lastUID++;

            Action<Packet> handler;
            if (Handlers.TryGetValue(opcode, out handler))
            {
                try
                {
                    handler(packet);

                    if (!checkLength || packet.Position == packet.Length)
                        packet.Status = ParsedStatus.Success;
                    else
                    {
                        var pos = packet.Position;
                        var len = packet.Length;
                        packet.ErrorMessage = String.Format("Packet not fully read! Current position is {0}, length is {1}, and diff is {2}.", pos, len, len - pos);
                        packet.Status = ParsedStatus.WithErrors;
                    }
                }
                catch (Exception ex)
                {
                    packet.ErrorMessage = ex.GetType().ToString() + "\n" + ex.Message + "\n" + ex.StackTrace;

                    packet.Status = ParsedStatus.WithErrors;
                }
            }
            else
            {
                packet.Status = ParsedStatus.NotParsed;
            }
        }
    }
}
