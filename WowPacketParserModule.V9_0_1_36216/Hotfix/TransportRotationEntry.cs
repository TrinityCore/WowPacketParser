using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TransportRotation, HasIndexInData = false)]
    public class TransportRotationEntry
    {
        [HotfixArray(4)]
        public float[] Rot { get; set; }
        public uint TimeIndex { get; set; }
        public uint GameObjectsID { get; set; }
    }
}
