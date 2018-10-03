using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DeathThudLookups, HasIndexInData = false)]
    public class DeathThudLookupsEntry
    {
        public byte SizeClass { get; set; }
        public byte TerrainTypeSoundID { get; set; }
        public uint SoundEntryID { get; set; }
        public uint SoundEntryIDWater { get; set; }
    }
}
