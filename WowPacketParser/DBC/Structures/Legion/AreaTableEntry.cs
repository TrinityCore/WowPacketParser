using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("AreaTable")]
    public sealed class AreaTableEntry
    {
        public uint[] Flags;
        public string ZoneName;
        public float AmbientMultiplier;
        public string AreaName;
        public ushort MapID;
        public ushort ParentAreaID;
        public short AreaBit;
        public ushort AmbienceID;
        public ushort ZoneMusic;
        public ushort IntroSound;
        public ushort[] LiquidTypeID;
        public ushort UWZoneMusic;
        public ushort UWAmbience;
        public ushort PvPCombatWorldStateID;
        public byte SoundProviderPref;
        public byte SoundProviderPrefUnderwater;
        public byte ExplorationLevel;
        public byte FactionGroupMask;
        public byte MountFlags;
        public byte WildBattlePetLevelMin;
        public byte WildBattlePetLevelMax;
        public byte WindSettingsID;
        public uint UWIntroSound;
    }
}
