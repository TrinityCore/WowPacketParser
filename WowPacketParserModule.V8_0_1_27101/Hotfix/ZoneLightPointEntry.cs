using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ZoneLightPoint, HasIndexInData = false)]
    public class ZoneLightPointEntry
    {
        [HotfixArray(2)]
        public float[] Pos { get; set; }
        public byte PointOrder { get; set; }
        public ushort ZoneLightID { get; set; }
    }
}
