using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Occluder)]
    public class OccluderEntry
    {
        public int ID { get; set; }
        public int MapID { get; set; }
        public byte Type { get; set; }
        public byte SplineType { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Alpha { get; set; }
        public byte Flags { get; set; }
    }
}
