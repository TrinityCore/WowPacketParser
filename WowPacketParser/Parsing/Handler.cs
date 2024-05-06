using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            LoadHandlersInto(VersionHandlers, Assembly.GetExecutingAssembly(), ClientVersionBuild.Zero);

            return VersionHandlers;
        }

        // TEMPORARY HACK
        // This is needed because currently opcode handlers are only registersd if version is matching
        // so we need to clear them and reinitialize default handlers
        // This will be obsolete when all version specific stuff is moved to their own modules
        public static void ResetHandlers()
        {
            VersionHandlers.Clear();
        }

        public static Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> LoadHandlers(Assembly asm, ClientVersionBuild build)
        {
            LoadHandlersInto(VersionHandlers, asm, build);
            return VersionHandlers;
        }

        private static void LoadHandlersInto(Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> handlers, Assembly asm, ClientVersionBuild build)
        {
            var types = asm.GetTypes();
            foreach (Type type in types)
            {
                if (!type.IsAbstract || !type.IsPublic)
                    continue;

                var methods = type.GetMethods().Where(m =>
                {
                    var parameters = m.GetParameters();
                    return parameters.Length > 0 && parameters[0].ParameterType == typeof(Packet);
                });

                foreach (MethodInfo method in methods)
                {
                    if (!method.IsPublic)
                        continue;

                    var parseAttrs = method.GetCustomAttributes(typeof(ParserAttribute), false).Where(x =>
                        ((ParserAttribute)x).Opcode != Opcode.NULL_OPCODE);        

                    List<KeyValuePair<ClientVersionBuild, Opcode>> pairs = new();

                    foreach (ParserAttribute attr in parseAttrs)
                    {
                        if (!attr.OnlyForSpecificBuild)
                        {
                            pairs.Add(new KeyValuePair<ClientVersionBuild, Opcode>(build, attr.Opcode));
                        }

                        var buildMatchAttrs = method.GetCustomAttributes(typeof(BuildMatchAttribute), false).Where(x =>
                        ((BuildMatchAttribute)x).BuildVersion != ClientVersionBuild.None);

                        foreach (BuildMatchAttribute buildMatch in buildMatchAttrs)
                        {
                            pairs.Add(new KeyValuePair<ClientVersionBuild, Opcode>(buildMatch.BuildVersion, attr.Opcode));
                        }
                    }

                    if (pairs.Count <= 0)
                        continue;

                    foreach (var key in pairs)
                    {
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
                            Direction direction = key.Value.ToString()[0] == 'S' ? Direction.ServerToClient : Direction.ClientToServer;
                            // ReSharper disable once UseStringInterpolation
                            Trace.WriteLine(string.Format("Error: (Build: {0}) tried to overwrite delegate for opcode {1} ({2}); new handler: {3}; old handler: {4}",
                                ClientVersion.Build, Opcodes.GetOpcode(key.Value, direction), key.Value, del.Method, handlers[key].Method));
                            continue;
                        }

                        handlers[key] = del;
                    }
                }
            }
        }

        private static readonly Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>> VersionHandlers = new Dictionary<KeyValuePair<ClientVersionBuild, Opcode>, Action<Packet>>(1000);

        public static void Parse(Packet packet, bool isMultiple = false)
        {
            ParsedStatus status;

            packet.WriteLine(packet.GetHeader(isMultiple));

            if (packet.Opcode == 0)
                return;

            var opcode = Opcodes.GetOpcode(packet.Opcode, packet.Direction);
            var key = new KeyValuePair<ClientVersionBuild, Opcode>(ClientVersion.Build, opcode);

            Action<Packet> handler;
            var hasHandler = VersionHandlers.TryGetValue(key, out handler);

            ClientVersionBuild tmpFallback = ClientVersion.VersionDefiningBuild;
            while (ClientVersion.HasFallback(tmpFallback) && !hasHandler && tmpFallback != ClientVersionBuild.Zero)
            {
                // If no handler was found, try to find a handler
                key = new KeyValuePair<ClientVersionBuild, Opcode>(tmpFallback, opcode);
                hasHandler = VersionHandlers.TryGetValue(key, out handler);

                tmpFallback = ClientVersion.FallbackVersionDefiningBuild(tmpFallback, ClientVersion.VersionDefiningBuild);
            }

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
    }
}
