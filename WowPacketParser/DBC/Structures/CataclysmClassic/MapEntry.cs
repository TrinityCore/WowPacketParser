using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("Map")]
    public sealed class MapEntry
    {
        [Index(true)]
        public uint ID;
        public string Directory;
        public string MapName;
        public string MapDescription0;
        public string MapDescription1;
        public string PvpShortDescription;
        public string PvpLongDescription;
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
        public int RaidOffset;
        public short CorpseMapID;
        public byte MaxPlayers;
        public short WindSettingsID;
        public int ZmpFileDataID;
        public int Unknown1154;
        [Cardinality(3)]
        public int[] Flags = new int[3];
    }
}
