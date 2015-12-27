using WowPacketParser.Enums;

namespace WowPacketParser.DBC.Structures
{
    public sealed class MapEntry
    {
        public uint ID;
        public string Directory;
        public uint   InstanceType;
        public uint   Flags;
        public uint   MapType;
        public uint   unk5;
        public string MapName_lang;
        public uint   AreaTableID;
        public string MapDescription0_lang;
        public string MapDescription1_lang;
        public uint   LoadingScreenID;
        public float  MinimapIconScale;
        public int    CorpseMapID;
        public float  CorpsePosX;
        public float  CorpsePosY;
        public uint   TimeOfDayOverride;
        public uint   ExpansionID;
        public uint   RaidOffset;
        public uint   MaxPlayers;
        public int    ParentMapID;
        public int    CosmeticParentMapID;
        public uint   TimeOffset;
    }
}
