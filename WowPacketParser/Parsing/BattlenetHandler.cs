using System;
using System.Collections.Generic;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing
{
    public static class BattlenetHandler
    {
        private static Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>> LoadDefaultBattlenetHandlers()
        {
            return LoadBattlenetHandlers(new Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>>(), Assembly.GetExecutingAssembly());
        }

        public static void LoadBattlenetHandlers(Assembly asm)
        {
            BattlenetHandlers.Clear();
            LoadBattlenetHandlers(BattlenetHandlers, asm);
        }

        private static Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>> LoadBattlenetHandlers(Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>> handlers, Assembly asm)
        {
            foreach (var type in asm.GetTypes())
                foreach (var methodInfo in type.GetMethods())
                    foreach (var msgAttr in (BattlenetParserAttribute[])methodInfo.GetCustomAttributes(typeof(BattlenetParserAttribute), false))
                        handlers.Add(msgAttr.Header, (Action<BattlenetPacket>)Delegate.CreateDelegate(typeof(Action<BattlenetPacket>), methodInfo));

            return handlers;
        }

        private static readonly Dictionary<BattlenetPacketHeader, Action<BattlenetPacket>> BattlenetHandlers = LoadDefaultBattlenetHandlers();

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
