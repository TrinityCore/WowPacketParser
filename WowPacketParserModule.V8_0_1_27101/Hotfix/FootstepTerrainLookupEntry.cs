using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.FootstepTerrainLookup, HasIndexInData = false)]
    public class FootstepTerrainLookupEntry
    {
        public ushort CreatureFootstepID { get; set; }
        public sbyte TerrainSoundID { get; set; }
        public uint SoundID { get; set; }
        public uint SoundIDSplash { get; set; }
    }
}
