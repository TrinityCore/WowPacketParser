using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatChannelList
    {
        public string ChannelName;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.Zero, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleChannelMisc(Packet packet)
        {
            packet.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleChannelList530(Packet packet)
        {
            packet.ReadUInt32("Flags");
            var password = packet.ReadBits(7);
            packet.ReadBit();
            var length = packet.ReadBits(7);
            packet.ReadBit();

            packet.ReadWoWString("Password", password);
            packet.ReadWoWString("Channel Name", length);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_1_17538)]
        public static void HandleChannelList540(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadBit();
            var length = packet.ReadBits("", 7);
            packet.ReadBit();
            packet.ReadBits("HasPassword", 7);

            packet.ReadWoWString("Channel Name", length);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleChannelList541(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadBit();
            var length = packet.ReadBits(7);
            packet.ReadBits(7);
            packet.ReadBit("HasPassword");

            packet.ReadWoWString("Channel Name", length);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleChannelList542(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadBit();
            packet.ReadBits(7);
            packet.ReadBit();
            var length = packet.ReadBits(7);

            packet.ReadWoWString("Channel Name", length);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelList547(Packet packet)
        {
            var channelLength = packet.ReadBits(7);
            packet.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc2(Packet packet)
        {
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }
    }
}
