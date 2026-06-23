using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class SessionHandler
    {
        public static void ReadBleepToken(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            var tokenLength = packet.ReadBits(5);
            var proxyIdLength = packet.ReadBits(24);
            var addressLength = packet.ReadBits(6);
            packet.ReadInt64("TokenLifespanNanoSeconds", idx);
            packet.ReadWoWString("Token", tokenLength, idx);
            packet.ReadDynamicString("ProxyId", proxyIdLength, idx);
            packet.ReadWoWString("Address", addressLength, idx);
        }

        public static void ReadConnectPayload(Packet packet, params object[] idx)
        {
            AddressType type = packet.ReadByteE<AddressType>("Type", idx);
            switch (type)
            {
                case AddressType.IPv4:
                    packet.ReadIPAddress("Address", idx);
                    break;
                case AddressType.IPv6:
                    packet.ReadIPv6Address("Address", idx);
                    break;
                case AddressType.NamedSocket:
                    packet.ReadWoWString("Address", 128, idx);
                    break;
                default:
                    break;
            }

            packet.ReadUInt16("Port", idx);
            ReadBleepToken(packet, idx, "Token");
        }

        [Parser(Opcode.SMSG_CONNECT_TO, ClientVersionBuild.V12_0_7_68182)]
        public static void HandleConnectTo(Packet packet)
        {
            var payloadCount = packet.ReadUInt32();
            packet.ReadUInt32E<ConnectToSerial>("Serial");
            packet.ReadByte("Con");
            packet.ReadUInt64("Key");
            packet.ReadUInt32("NativeRealmAddress");
            packet.ReadUInt32("Key3");

            for (var i = 0u; i < payloadCount; ++i)
                ReadConnectPayload(packet, "Payload", i);
        }
    }
}
