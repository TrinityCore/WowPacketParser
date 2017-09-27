using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.CliChat.Channel
{
    public unsafe struct UnsilenceVoice
    {
        public string ChannelName;
        public string Name;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_VOICE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelSilencing(Packet packet)
        {
            packet.ReadCString("Channel Name");
            packet.ReadCString("Player Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_UNSILENCE_VOICE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc2(Packet packet)
        {
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }
    }
}
