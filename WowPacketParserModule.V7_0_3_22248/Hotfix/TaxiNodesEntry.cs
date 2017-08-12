using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiNodes)]
    public class TaxiNodesEntry
    {
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        public string Name { get; set; }
        [HotfixArray(2)]
        public uint[] MountCreatureID { get; set; }
        [HotfixArray(2)]
        public float[] MapOffset { get; set; }
        public ushort MapID { get; set; }
        public ushort ConditionID { get; set; }
        public ushort LearnableIndex { get; set; }
        public byte Flags { get; set; }
        public uint ID { get; set; }
    }
}