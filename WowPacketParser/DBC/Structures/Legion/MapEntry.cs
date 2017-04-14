using  DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("Map")]
    public sealed class MapEntry
    {
        public string Directory;
        public uint[] Flags;
        public float MinimapIconScale;
        public float[] CorpsePos;
        public string MapName;
        public string MapDescription0;
        public string MapDescription1;
        public string ShortDescription;
        public string LongDescription;
        public ushort AreaTableID;
        public ushort LoadingScreenID;
        public short CorpseMapID;
        public ushort TimeOfDayOverride;
        public short ParentMapID;
        public short CosmeticParentMapID;
        public ushort WindSettingsID;
        public byte InstanceType;
        public byte unk5;
        public byte ExpansionID;
        public byte MaxPlayers;
        public byte TimeOffset;
    }
}
