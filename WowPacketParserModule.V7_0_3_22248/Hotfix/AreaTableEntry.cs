using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTable, HasIndexInData = false)]
    public class AreaTableEntry
    {
        [HotfixArray(2)]
        public uint[] Flags { get; set; }
        public string ZoneName { get; set; }
        public float AmbientMultiplier { get; set; }
        public string AreaName { get; set; }
        public ushort MapID { get; set; }
        public ushort ParentAreaID { get; set; }
        public short AreaBit { get; set; }
        public ushort AmbienceID { get; set; }
        public ushort ZoneMusic { get; set; }
        public ushort IntroSound { get; set; }
        [HotfixArray(4)]
        public ushort[] LiquidTypeID { get; set; }
        public ushort UWZoneMusic { get; set; }
        public ushort UWAmbience { get; set; }
        public ushort PvPCombatWorldStateID { get; set; }
        public byte SoundProviderPref { get; set; }
        public byte SoundProviderPrefUnderwater { get; set; }
        public byte ExplorationLevel { get; set; }
        public byte FactionGroupMask { get; set; }
        public byte MountFlags { get; set; }
        public byte WildBattlePetLevelMin { get; set; }
        public byte WildBattlePetLevelMax { get; set; }
        public byte WindSettingsID { get; set; }
        public uint UWIntroSound { get; set; }
    }
}