using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.Faction, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class FactionEntry
    {
        [HotfixArray(4)]
        public long[] ReputationRaceMask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short ReputationIndex { get; set; }
        public ushort ParentFactionID { get; set; }
        public byte Expansion { get; set; }
        public uint FriendshipRepID { get; set; }
        public byte Flags { get; set; }
        public ushort ParagonFactionID { get; set; }
        [HotfixArray(4)]
        public short[] ReputationClassMask { get; set; }
        [HotfixArray(4)]
        public ushort[] ReputationFlags { get; set; }
        [HotfixArray(4)]
        public int[] ReputationBase { get; set; }
        [HotfixArray(4)]
        public int[] ReputationMax { get; set; }
        [HotfixArray(2)]
        public float[] ParentFactionMod { get; set; }
        [HotfixArray(2)]
        public byte[] ParentFactionCap { get; set; }
    }
}
