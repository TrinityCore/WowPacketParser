using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatChannelDisplayList
    {
        public string ChannelName;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc(Packet packet)
        {
            packet.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_DISPLAY_LIST, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc2(Packet packet)
        {
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }
    }
}
