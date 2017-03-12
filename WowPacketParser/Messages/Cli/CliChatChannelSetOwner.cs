using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliChatChannelSetOwner
    {
        public string Name;
        public string ChannelName;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_SET_OWNER, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc1(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(9);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }
    }
}
