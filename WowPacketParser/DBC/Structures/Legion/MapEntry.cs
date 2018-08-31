using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("Map")]
    public sealed class MapEntry
    {
        public string Directory;
        public string MapName;
        public string MapDescription0;
        public string MapDescription1;
        public string PvpShortDescription;
        public string PvpLongDescription;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Flags;
        public float MinimapIconScale;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public float[] Corpse;
        public ushort AreaTableID;
        public ushort LoadingScreenID;
        public short CorpseMapID;
        public ushort TimeOfDayOverride;
        public short ParentMapID;
        public short CosmeticParentMapID;
        public ushort WindSettingsID;
        public byte InstanceType;
        public byte MapType;
        public byte ExpansionID;
        public byte MaxPlayers;
        public byte TimeOffset;
    }
}
