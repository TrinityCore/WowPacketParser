using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatChannelSilenceVoice
    {
        public string ChannelName;
        public string Name;

        [Parser(Opcode.CMSG_CHAT_CHANNEL_SILENCE_VOICE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelSilencing(Packet packet)
        {
            packet.ReadCString("Channel Name");
            packet.ReadCString("Player Name");
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_SILENCE_VOICE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleChannelMisc2(Packet packet)
        {
            var bits108 = packet.ReadBits(7);
            packet.ReadWoWString("ChannelName", bits108);
        }
    }
}
