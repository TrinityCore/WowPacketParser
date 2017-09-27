using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct PartyInviteResponse
    {
        public byte PartyIndex;
        public bool Accept;
        public uint? RolesDesired; // Optional

        [Parser(Opcode.CMSG_PARTY_INVITE_RESPONSE)]
        public static void HandlePartyInviteResponse(Packet packet)
        {
            packet.ReadByte("PartyIndex");

            packet.ResetBitReader();

            packet.ReadBit("Accept");
            var hasRolesDesired = packet.ReadBit("HasRolesDesired");
            if (hasRolesDesired)
                packet.ReadInt32("RolesDesired");
        }
    }
}
