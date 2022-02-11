using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;


namespace WowPacketParserModule.V2_5_1_38707.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_PARTY_INVITE, ClientVersionBuild.V2_5_2_39570)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ResetBitReader();

            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);

            packet.ReadInt32("ProposedRoles");
            packet.ReadPackedGuid128("TargetGuid");

            packet.ReadWoWString("TargetName", lenTargetName);
            packet.ReadWoWString("TargetRealm", lenTargetRealm);
        }
    }
}
