using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Gameobjects)]
    public class GameObjectsEntry
    {
        [HotfixArray(3)]
        public float[] Position { get; set; }
        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float RotationZ { get; set; }
        public float RotationW { get; set; }
        public float Size { get; set; }
        [HotfixArray(8)]
        public int[] Data { get; set; }
        public string Name { get; set; }
        public ushort MapID { get; set; }
        public ushort DisplayID { get; set; }
        public ushort PhaseID { get; set; }
        public ushort PhaseGroupID { get; set; }
        public byte PhaseUseFlags { get; set; }
        public byte Type { get; set; }
        public uint ID { get; set; }
    }
}