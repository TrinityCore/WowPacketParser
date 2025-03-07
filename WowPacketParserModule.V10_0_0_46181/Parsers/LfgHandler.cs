using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.CMSG_DF_GET_SYSTEM_INFO, ClientVersionBuild.V10_1_7_51187)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.ReadBit("Player");
            if (packet.ReadBit())
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_DF_JOIN, ClientVersionBuild.V10_1_7_51187)]
        public static void HandleDFJoin(Packet packet)
        {
            packet.ReadBit("QueueAsGroup");
            var hasPartyIndex = packet.ReadBit();
            packet.ReadBit("Mercenary");

            packet.ResetBitReader();

            packet.ReadByteE<LfgRoleFlag>("Roles");
            var slotsCount = packet.ReadInt32();

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");

            for (var i = 0; i < slotsCount; ++i)
                packet.ReadUInt32("Slot", i);
        }

        [Parser(Opcode.CMSG_DF_SET_ROLES, ClientVersionBuild.V10_1_7_51187)]
        public static void HandleDFSetRoles(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit();
            packet.ReadByteE<LfgRoleFlag>("RolesDesired");
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHOSEN, ClientVersionBuild.V10_1_7_51187)]
        public static void HandleLfgRoleChosen(Packet packet)
        {
            packet.ReadGuid("Player");
            packet.ReadByteE<LfgRoleFlag>("RoleMask");
            packet.ReadBit("Accepted");
        }
    }
}
