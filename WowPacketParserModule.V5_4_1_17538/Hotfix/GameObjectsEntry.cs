using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_1_17538.Hotfix
{
    [HotfixStructure(DB2Hash.Gameobjects)]
    public class GameObjectsEntry
    {
        public int ID { get; set; }
        public int MapID { get; set; }
        public int DisplayID { get; set; }
        [HotfixArray(3)]
        public float[] Position { get; set; }
        [HotfixArray(4)]
        public float[] Rotation { get; set; }
        public float Scale { get; set; }
        public int Type { get; set; }
        [HotfixArray(4)]
        public int[] Data { get; set; }
        public string Name { get; set; }
    }
}
