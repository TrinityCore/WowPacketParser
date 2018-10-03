using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TransportRotation, HasIndexInData = false)]
    public class TransportRotationEntry
    {
        [HotfixArray(4)]
        public float[] Rot { get; set; }
        public uint TimeIndex { get; set; }
        public int GameObjectsID { get; set; }
    }
}
