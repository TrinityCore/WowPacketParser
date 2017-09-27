using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UndeleteCharacter
    {
        public ulong CharacterGuid;
        public int ClientToken;

        [Parser(Opcode.CMSG_UNDELETE_CHARACTER)]
        public static void HandleUndeleteCharacter(Packet packet)
        {
            packet.ReadInt32("ClientToken");
            packet.ReadPackedGuid128("CharacterGuid");
        }
    }
}
