using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleChannelNotifyJoined(Packet packet)
        {
            var bits544 = packet.ReadBits(8);
            var bits24 = packet.ReadBits(10);

            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadInt32("ChatChannelID");
            packet.ReadUInt64("InstanceID");

            packet.ReadWoWString("Channel", bits544);
            packet.ReadWoWString("ChannelWelcomeMsg", bits24);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_LEFT, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleChannelNotifyLeft(Packet packet)
        {
            var bits20 = packet.ReadBits(8);
            packet.ReadBit("Suspended");
            packet.ReadInt32("ChatChannelID");
            packet.ReadWoWString("Channel", bits20);
        }
    }
}
