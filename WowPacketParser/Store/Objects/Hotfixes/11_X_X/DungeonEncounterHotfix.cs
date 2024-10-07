using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("dungeon_encounter")]
    public sealed record DungeonEncounterHotfix1100: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("DifficultyID")]
        public int? DifficultyID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("CompleteWorldStateID")]
        public int? CompleteWorldStateID;

        [DBFieldName("Bit")]
        public sbyte? Bit;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SpellIconFileID")]
        public int? SpellIconFileID;

        [DBFieldName("Faction")]
        public int? Faction;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("dungeon_encounter_locale")]
    public sealed record DungeonEncounterLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
