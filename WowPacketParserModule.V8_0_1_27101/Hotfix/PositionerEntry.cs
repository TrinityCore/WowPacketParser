using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Positioner, HasIndexInData = false)]
    public class PositionerEntry
    {
        public ushort FirstStateID { get; set; }
        public byte Flags { get; set; }
        public float StartLife { get; set; }
        public byte StartLifePercent { get; set; }
    }
}
