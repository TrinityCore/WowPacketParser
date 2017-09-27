using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SetRole
    {
        public ulong ChangedUnit;
        public uint Role;
        public byte PartyIndex;

        [Parser(Opcode.CMSG_SET_ROLE)]
        public static void HandleSetRole(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("ChangedUnit");
            packet.ReadInt32("Role");
        }
    }
}
