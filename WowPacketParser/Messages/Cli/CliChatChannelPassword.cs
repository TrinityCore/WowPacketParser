using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatChannelPassword
    {
        public string Name;
        public string ChannelName;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleChannelPassword(Packet packet)
        {
            packet.ReadCString("Channel Name");
            packet.ReadCString("Password");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelPassword434(Packet packet)
        {
            var channelLen = packet.ReadBits(8);
            var passLen = packet.ReadBits(7);

            packet.ReadWoWString("Channel Name", channelLen);
            packet.ReadWoWString("Password", passLen);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_PASSWORD, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelPassword602(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(7);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }
    }
}
