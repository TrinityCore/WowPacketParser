using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct PartyInvite
    {
        public ulong TargetGuid;
        public uint ProposedRoles;
        public string TargetName;
        public byte PartyIndex;
        public string TargetRealm;
        public uint TargetCfgRealmID;

        [Parser(Opcode.CMSG_PARTY_INVITE, CClientVersionBuild.Zero, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadInt32("ProposedRoles");
            packet.ReadPackedGuid128("TargetGuid");
            packet.ReadInt32("TargetCfgRealmID");

            packet.ResetBitReader();

            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);

            packet.ReadWoWString("TargetName", lenTargetName);
            packet.ReadWoWString("TargetRealm", lenTargetRealm);
        }

        [Parser(Opcode.CMSG_PARTY_INVITE, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleClientPartyInvite701(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadInt32("ProposedRoles");
            packet.ReadPackedGuid128("TargetGuid");

            packet.ResetBitReader();

            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);

            packet.ReadWoWString("TargetName", lenTargetName);
            packet.ReadWoWString("TargetRealm", lenTargetRealm);
        }
    }
}
