using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.DF
{
    public unsafe struct SetRoles
    {
        public uint RolesDesired;
        public byte PartyIndex;

        [Parser(Opcode.CMSG_DF_SET_ROLES)]
        public static void HandleDFSetRoles(Packet packet)
        {
            packet.ReadUInt32("RolesDesired");
            packet.ReadByte("PartyIndex");
        }
    }
}
