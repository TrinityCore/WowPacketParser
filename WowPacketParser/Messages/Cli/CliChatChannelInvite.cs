using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatChannelInvite
    {
        public string ChannelName;
        public string Name;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelInvite(Packet packet)
        {
            packet.ReadCString("Channel Name");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_INVITE, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelInvite434(Packet packet)
        {
            var nameLen = packet.ReadBits(7);
            var channelLen = packet.ReadBits(8);

            packet.ReadWoWString("Name", nameLen);
            packet.ReadWoWString("Channel Name", channelLen);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_INVITE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc1(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(9);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }
    }
}
