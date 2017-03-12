using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessagePartyLeader // not confirmed
    {
        public string Text;
        public int Language;

        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY_LEADER)]
        public static void HandleMessageChatParty(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            packet.ReadCString("Message");
        }
    }
}
