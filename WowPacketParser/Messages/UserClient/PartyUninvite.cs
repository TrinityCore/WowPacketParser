using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct PartyUninvite
    {
        public string Reason;
        public byte PartyIndex;
        public ulong TargetGuid;

        [Parser(Opcode.CMSG_PARTY_UNINVITE)]
        public static void HandlePartyUninvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("TargetGuid");

            var len = packet.ReadBits(8);
            packet.ReadWoWString("Reason", len);
        }
    }
}
