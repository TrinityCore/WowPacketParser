using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("AreaTable")]
    public sealed class AreaTableEntry
    {
        [Index(true)]
        public uint ID;
        public string ZoneName;
        public string AreaName;
        public ushort ContinentID;
        public ushort ParentAreaID;
        public short AreaBit;
        public byte SoundProviderPref;
        public byte SoundProviderPrefUnderwater;
        public ushort AmbienceID;
        public ushort UwAmbience;
        public ushort ZoneMusic;
        public ushort UwZoneMusic;
        public ushort IntroSound;
        public uint UwIntroSound;
        public byte FactionGroupMask;
        public float AmbientMultiplier;
        public int MountFlags;
        public short PvpCombatWorldStateID;
        public byte WildBattlePetLevelMin;
        public byte WildBattlePetLevelMax;
        public byte WindSettingsID;
        public int ContentTuningID;
        [Cardinality(2)]
        public int[] Flags = new int[2];
        [Cardinality(4)]
        public ushort[] LiquidTypeID = new ushort[4];
    }
}
