using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatChannelUnban
    {
        public string Name;
        public string ChannelName;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNBAN, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc2(Packet packet) // FIXME: missing name?
        {
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }
    }
}
