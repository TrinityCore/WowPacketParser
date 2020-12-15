using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.CharacterLoadout)]
    public class CharacterLoadoutEntry
    {
        public long RaceMask { get; set; }
        public uint ID { get; set; }
        public sbyte ChrClassID { get; set; }
        public sbyte Purpose { get; set; }
    }
}
