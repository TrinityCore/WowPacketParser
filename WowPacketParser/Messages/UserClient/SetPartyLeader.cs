using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetPartyLeader
    {
        public ulong Target;
        public byte PartyIndex;

        [Parser(Opcode.CMSG_SET_PARTY_LEADER)]
        public static void HandleSetPartyLeader(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("Target");
        }
    }
}
