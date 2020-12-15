using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.GameObjects)]
    public class GameObjectsEntry
    {
        public string Name { get; set; }
        [HotfixArray(3, true)]
        public float[] Pos { get; set; }
        [HotfixArray(4)]
        public float[] Rot { get; set; }
        public uint ID { get; set; }
        public int OwnerID { get; set; }
        public int DisplayID { get; set; }
        public float Scale { get; set; }
        public int TypeID { get; set; }
        public int PhaseUseFlags { get; set; }
        public int PhaseID { get; set; }
        public int PhaseGroupID { get; set; }
        [HotfixArray(8)]
        public int[] PropValue { get; set; }
    }
}
