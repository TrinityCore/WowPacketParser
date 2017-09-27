using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetEveryoneIsAssistant
    {
        public byte PartyIndex;
        public bool EveryoneIsAssistant;
        [Parser(Opcode.CMSG_SET_EVERYONE_IS_ASSISTANT)]
        public static void HandleEveryoneIsAssistant(Packet packet)
        {
            packet.ReadBit("Active");
        }
    }
}
