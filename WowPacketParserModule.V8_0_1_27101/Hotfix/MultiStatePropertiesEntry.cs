using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MultiStateProperties, HasIndexInData = false)]
    public class MultiStatePropertiesEntry
    {
        [HotfixArray(3)]
        public float[] Offset { get; set; }
        public int GameObjectID { get; set; }
        public byte StateIndex { get; set; }
        public int GameEventID { get; set; }
        public float Facing { get; set; }
        public int TransitionInID { get; set; }
        public int TransitionOutID { get; set; }
        public int CollisionHull { get; set; }
        public uint Flags { get; set; }
        public int SpellVisualKitID { get; set; }
        public int MultiPropertiesID { get; set; }
    }
}
