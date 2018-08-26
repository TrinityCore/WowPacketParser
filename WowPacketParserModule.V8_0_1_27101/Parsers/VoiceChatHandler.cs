using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class VoiceChat
    {
        [Parser(Opcode.CMSG_VOICE_CHAT_LOGIN)]
        public static void HandleEmpty(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_VOICE_CHAT_JOIN_CHANNEL)]
        public static void HandleVoiceChatJoinChannel(Packet packet)
        {
            packet.ReadByte("ChannelID");
        }
    }
}
