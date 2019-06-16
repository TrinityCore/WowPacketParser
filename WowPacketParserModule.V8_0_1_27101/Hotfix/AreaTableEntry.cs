using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTable, HasIndexInData = false)]
    public class AreaTableEntry
    {
        public string ZoneName { get; set; }
        public string AreaName { get; set; }
        public ushort ContinentID { get; set; }
        public ushort ParentAreaID { get; set; }
        public short AreaBit { get; set; }
        public byte SoundProviderPref { get; set; }
        public byte SoundProviderPrefUnderwater { get; set; }
        public ushort AmbienceID { get; set; }
        public ushort UwAmbience { get; set; }
        public ushort ZoneMusic { get; set; }
        public ushort UwZoneMusic { get; set; }
        public sbyte ExplorationLevel { get; set; }
        public ushort IntroSound { get; set; }
        public uint UwIntroSound { get; set; }
        public byte FactionGroupMask { get; set; }
        public float AmbientMultiplier { get; set; }
        public byte MountFlags { get; set; }
        public short PvpCombatWorldStateID { get; set; }
        public byte WildBattlePetLevelMin { get; set; }
        public byte WildBattlePetLevelMax { get; set; }
        public byte WindSettingsID { get; set; }
        [HotfixArray(2)]
        public int[] Flags { get; set; }
        [HotfixArray(4)]
        public ushort[] LiquidTypeID { get; set; }
    }
}
