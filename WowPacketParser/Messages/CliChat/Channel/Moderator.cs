using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.CliChat.Channel
{
    public unsafe struct Moderator
    {
        public string ChannelName;
        public string Name;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_MODERATOR, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc1(Packet packet)
        {
            var lenChannelName = packet.ReadBits(7);
            var lenName = packet.ReadBits(9);

            packet.ReadWoWString("ChannelName", lenChannelName);
            packet.ReadWoWString("Name", lenName);
        }
    }
}
