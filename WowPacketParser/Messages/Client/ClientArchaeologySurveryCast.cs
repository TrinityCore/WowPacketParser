using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientArchaeologySurveryCast
    {
        public int ResearchBranchID;
        public bool SuccessfulFind;
        public uint NumFindsCompleted;
        public uint TotalFinds;

        [Parser(Opcode.SMSG_ARCHAEOLOGY_SURVERY_CAST, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleArchaelogySurveryCast6(Packet packet)
        {
            packet.ReadUInt32("TotalFinds");
            packet.ReadUInt32("NumFindsCompleted");
            packet.ReadInt32("ResearchBranchID");
            packet.ReadBit("SuccessfulFind");
        }
    }
}
