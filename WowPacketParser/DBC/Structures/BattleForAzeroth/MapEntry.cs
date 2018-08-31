using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
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
        public float[] Corpse;
        public byte MapType;
        public sbyte InstanceType;
        public byte ExpansionID;
        public ushort AreaTableID;
        public short LoadingScreenID;
        public short TimeOfDayOverride;
        public short ParentMapID;
        public short CosmeticParentMapID;
        public byte TimeOffset;
        public float MinimapIconScale;
        public short CorpseMapID;
        public byte MaxPlayers;
        public short WindSettingsID;
        public int ZmpFileDataID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Flags;
    }
}
