using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
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
        public ushort OwnerID { get; set; }
        public ushort DisplayID { get; set; }
        public float Scale { get; set; }
        public byte TypeID { get; set; }
        public byte PhaseUseFlags { get; set; }
        public ushort PhaseID { get; set; }
        public ushort PhaseGroupID { get; set; }
        [HotfixArray(8)]
        public int[] PropValue { get; set; }
    }
}
