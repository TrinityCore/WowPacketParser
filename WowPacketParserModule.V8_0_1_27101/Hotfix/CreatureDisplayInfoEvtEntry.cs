using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfoEvt, HasIndexInData = false)]
    public class CreatureDisplayInfoEvtEntry
    {
        public int Fourcc { get; set; }
        public int SpellVisualKitID { get; set; }
        public sbyte Flags { get; set; }
        public int CreatureDisplayInfoID { get; set; }
    }
}
