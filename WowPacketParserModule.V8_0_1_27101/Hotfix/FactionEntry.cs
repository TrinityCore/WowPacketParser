using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Faction)]
    public class FactionEntry
    {
        [HotfixArray(4)]
        public ulong[] ReputationRaceMask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public short ReputationIndex { get; set; }
        public ushort ParentFactionID { get; set; }
        public byte Expansion { get; set; }
        public byte FriendshipRepID { get; set; }
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
