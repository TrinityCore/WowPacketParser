using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessageBattleground // not confirmed
    {
        public int Language;
        public string Text;

        [Parser(Opcode.CMSG_CHAT_MESSAGE_BATTLEGROUND, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMessageChatBattleground434(Packet packet)
        {
            packet.ReadInt32E<Language>("Language"); // not confirmed
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Message", len);
        }
    }
}
