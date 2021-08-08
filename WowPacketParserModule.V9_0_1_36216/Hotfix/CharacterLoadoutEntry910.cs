using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.CharacterLoadout, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class CharacterLoadoutEntry
    {
        public long RaceMask { get; set; }
        public sbyte ChrClassID { get; set; }
        public sbyte Purpose { get; set; }
        public sbyte Unused910 { get; set; }
    }
}
