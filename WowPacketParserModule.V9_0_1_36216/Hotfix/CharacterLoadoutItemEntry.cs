using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CharacterLoadoutItem, HasIndexInData = false)]
    public class CharacterLoadoutItemEntry
    {
        public ushort CharacterLoadoutID { get; set; }
        public uint ItemID { get; set; }
    }
}
