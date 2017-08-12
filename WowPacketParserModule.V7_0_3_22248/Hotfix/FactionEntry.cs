using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Faction, HasIndexInData = false)]
    public class FactionEntry
    {
        [HotfixArray(4)]
        public uint[] ReputationRaceMask { get; set; }
        [HotfixArray(4)]
        public int[] ReputationBase { get; set; }
        public float ParentFactionModIn { get; set; }
        public float ParentFactionModOut { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [HotfixArray(4)]
        public uint[] ReputationMax { get; set; }
        public short ReputationIndex { get; set; }
        [HotfixArray(4)]
        public ushort[] ReputationClassMask { get; set; }
        [HotfixArray(4)]
        public ushort[] ReputationFlags { get; set; }
        public ushort ParentFactionID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public ushort ParagonFactionID { get; set; }
        public byte ParentFactionCapIn { get; set; }
        public byte ParentFactionCapOut { get; set; }
        public byte Expansion { get; set; }
        public byte Flags { get; set; }
        public byte FriendshipRepID { get; set; }
    }
}