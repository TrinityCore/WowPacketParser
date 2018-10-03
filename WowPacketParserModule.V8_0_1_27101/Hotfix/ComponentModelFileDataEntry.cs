using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ComponentModelFileData, HasIndexInData = false)]
    public class ComponentModelFileDataEntry
    {
        public byte GenderIndex { get; set; }
        public byte ClassID { get; set; }
        public byte RaceID { get; set; }
        public sbyte PositionIndex { get; set; }
    }
}
