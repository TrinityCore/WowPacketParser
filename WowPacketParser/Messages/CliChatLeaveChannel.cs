using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatLeaveChannel
    {
        public int ZoneChannelID;
        public string ChannelName;

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleChannelLeave(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            packet.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelLeave547(Packet packet)
        {
            packet.ReadInt32("Channel Id");
            var channelLength = packet.ReadBits(7);
            packet.ReadWoWString("Channel Name", channelLength);
        }

        [Parser(Opcode.CMSG_CHAT_LEAVE_CHANNEL, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelLeave602(Packet packet)
        {
            packet.ReadInt32("ZoneChannelID");
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }
    }
}
