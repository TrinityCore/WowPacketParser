using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("AreaTable")]
    public sealed class AreaTableEntry
    {
        public string ZoneName;
        public string AreaName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] Flags;
        public float AmbientMultiplier;
        public ushort ContinentID;
        public ushort ParentAreaID;
        public short AreaBit;
        public ushort AmbienceID;
        public ushort ZoneMusic;
        public ushort IntroSound;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] LiquidTypeID;
        public ushort UwZoneMusic;
        public ushort UwAmbience;
        public short PvpCombatWorldStateID;
        public byte SoundProviderPref;
        public byte SoundProviderPrefUnderwater;
        public sbyte ExplorationLevel;
        public byte FactionGroupMask;
        public byte MountFlags;
        public byte WildBattlePetLevelMin;
        public byte WildBattlePetLevelMax;
        public byte WindSettingsID;
        public uint UwIntroSound;
    }
}
