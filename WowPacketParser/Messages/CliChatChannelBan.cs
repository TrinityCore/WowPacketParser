using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatChannelBan
    {
        public string Name;
        public string ChannelName;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_BAN, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V6_0_2_19033)]
        // 4.3.4
        public static void HandleChannelBan(Packet packet)
        {
            var channelLength = packet.ReadBits(8);
            var nameLength = packet.ReadBits(7);
            packet.ReadWoWString("Channel", channelLength);
            packet.ReadWoWString("Player to ban", nameLength);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_BAN, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc1(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(9);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }
    }
}
