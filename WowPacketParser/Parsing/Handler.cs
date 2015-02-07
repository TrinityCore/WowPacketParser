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
        public static Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> LoadDefaultHandlers()
        {
            var handlers = new Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>>(1000);

            LoadHandlersInto(handlers, Assembly.GetExecutingAssembly(), ClientVersionBuild.Zero);

            return handlers;
        }

        // TEMPORARY HACK
        // This is needed because currently opcode handlers are only registersd if version is matching
        // so we need to clear them and reinitialize default handlers
        // This will be obsolete when all version specific stuff is moved to their own modules
        public static void ResetHandlers()
        {
            VersionHandlers.Clear();
            LoadHandlersInto(VersionHandlers, Assembly.GetExecutingAssembly(), ClientVersionBuild.Zero);
        }

        public static Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> LoadHandlers(Assembly asm, ClientVersionBuild build)
        {
            LoadHandlersInto(VersionHandlers, asm, build);
            return VersionHandlers;
        }

        private static void LoadHandlersInto(Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> handlers, Assembly asm, ClientVersionBuild build)
        {
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
                        if (opc == Opcode.NULL_OPCODE)
                            continue;

                        var key = new KeyValuePair<ClientVersionBuild, Opcode>(build, opc);

                        var del = (Action<Packet>)Delegate.CreateDelegate(typeof(Action<Packet>), method);

                        if (handlers.ContainsKey(key))
                        {
                            // @TODO This is a hack to keep things easy regarding declaration of opcodes.
                            // Ideally, we would split the opcodes into three different enums:
                            // ClientOpcodes, ServerOpcodes, BidirectionalOpcodes
                            // The first two are obvious as to what they would contain.
                            // The last one would be MSG_, UMSG_, TEST_, etc... opcodes
                            // However that's just too much pain to do considering the mess Blizzard does
                            // by naming their opcodes sometimes without following their own rules.
                            var direction = attr.Opcode.ToString()[0] == 'S' ? Direction.ServerToClient : Direction.ClientToServer;
                            Trace.WriteLine(string.Format("Error: (Build: {0}) tried to overwrite delegate for opcode {1} ({2}); new handler: {3}; old handler: {4}",
                                ClientVersion.Build, Opcodes.GetOpcode(attr.Opcode, direction), attr.Opcode, del.Method, handlers[key].Method));
                            continue;
                        }

                        handlers[key] = del;
                    }
                }
            }
        }

        private static readonly Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> VersionHandlers = LoadDefaultHandlers();

        public static void Parse(Packet packet, bool isMultiple = false)
        {
            ParsedStatus status;

            var opcode = Opcodes.GetOpcode(packet.Opcode, packet.Direction);
            if (opcode == Opcode.NULL_OPCODE)
                opcode = Opcodes.GetOpcode(packet.Opcode, Direction.Bidirectional);

            packet.WriteLine(packet.GetHeader(isMultiple));

            if (packet.Opcode == 0)
                return;

            var key = new KeyValuePair<ClientVersionBuild, Opcode>(ClientVersion.VersionDefiningBuild, opcode);

            Action<Packet> handler;
            var hasHandler = VersionHandlers.TryGetValue(key, out handler);
            if (!hasHandler)
            {
                // If no handler was found, try to find a handler that works for any version.
                key = new KeyValuePair<ClientVersionBuild, Opcode>(ClientVersionBuild.Zero, opcode);
                hasHandler = VersionHandlers.TryGetValue(key, out handler);
            }

            if (hasHandler && Settings.DumpFormat != DumpFormatType.HexOnly)
            {
                if (Settings.DumpFormat == DumpFormatType.SniffDataOnly)
                {
                    var attrs = handler.Method.GetCustomAttributes(typeof(HasSniffDataAttribute), false);

                    packet.AddSniffData(StoreNameType.Opcode, packet.Opcode, Opcodes.GetOpcodeName(packet.Opcode, packet.Direction));

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
                        packet.WriteLine("Packet not fully read! Current position: {0} Length: {1} Bytes remaining: {2}.",
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
                status = opcode == Opcode.NULL_OPCODE ? ParsedStatus.NotParsed : ParsedStatus.NoStructure;
            }

            if (!isMultiple)
            {
                packet.Status = status;

                if (Settings.DumpFormat != DumpFormatType.SniffDataOnly)
                {
                    // added before for this type
                    var data = status == ParsedStatus.Success ? Opcodes.GetOpcodeName(packet.Opcode, packet.Direction) : status.ToString();
                    packet.AddSniffData(StoreNameType.Opcode, packet.Opcode, data);
                }
            }
        }

        private static Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>> LoadBattlenetHandlers()
        {
            var handlers = new Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>>();
            var currentAsm = Assembly.GetExecutingAssembly();

            foreach (var type in currentAsm.GetTypes())
                foreach (var methodInfo in type.GetMethods())
                    foreach (var msgAttr in (BattlenetParserAttribute[])methodInfo.GetCustomAttributes(typeof(BattlenetParserAttribute), false))
                        handlers.Add(msgAttr.Header, (Action<BattlenetPacket>)Delegate.CreateDelegate(typeof(Action<BattlenetPacket>), methodInfo));

            return handlers;
        }

        private static readonly Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>> BattlenetHandlers = LoadBattlenetHandlers();

        public static void ParseBattlenet(Packet packet)
        {
            try
            {
                var bnetPacket = new BattlenetPacket(packet);
                Action<BattlenetPacket> handler;

                bnetPacket.Stream.WriteLine(bnetPacket.GetHeader());

                if (BattlenetHandlers.TryGetValue(bnetPacket.Header, out handler))
                {
                    handler(bnetPacket);
                    packet.Status = ParsedStatus.Success;
                }
                else
                {
                    packet.AsHex();
                    packet.Status = ParsedStatus.NotParsed;
                }
            }
            catch (Exception ex)
            {
                packet.WriteLine(ex.GetType().ToString());
                packet.WriteLine(ex.Message);
                packet.WriteLine(ex.StackTrace);

                packet.Status = ParsedStatus.WithErrors;
            }
        }
    }
}
