namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("AreaTable")]
    public sealed class AreaTableEntry
    {
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
        public sbyte ExplorationLevel;
        public ushort IntroSound;
        public uint UwIntroSound;
        public byte FactionGroupMask;
        public float AmbientMultiplier;
        public byte MountFlags;
        public short PvpCombatWorldStateID;
        public byte WildBattlePetLevelMin;
        public byte WildBattlePetLevelMax;
        public byte WindSettingsID;
        public int[] Flags = new int[2];
        public ushort[] LiquidTypeID = new ushort[4];
    }
}
