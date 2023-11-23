using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("adventure_map_poi")]
    public sealed record AdventureMapPoiHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("WorldPositionX")]
        public float? WorldPositionX;

        [DBFieldName("WorldPositionY")]
        public float? WorldPositionY;

        [DBFieldName("Type")]
        public sbyte? Type;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("QuestID")]
        public uint? QuestID;

        [DBFieldName("LfgDungeonID")]
        public uint? LfgDungeonID;

        [DBFieldName("RewardItemID")]
        public int? RewardItemID;

        [DBFieldName("UiTextureAtlasMemberID")]
        public uint? UiTextureAtlasMemberID;

        [DBFieldName("UiTextureKitID")]
        public uint? UiTextureKitID;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("AreaTableID")]
        public uint? AreaTableID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("adventure_map_poi_locale")]
    public sealed record AdventureMapPoiLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("adventure_map_poi")]
    public sealed record AdventureMapPOIHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("WorldPositionX")]
        public float? WorldPositionX;

        [DBFieldName("WorldPositionY")]
        public float? WorldPositionY;

        [DBFieldName("Type")]
        public sbyte? Type;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("QuestID")]
        public uint? QuestID;

        [DBFieldName("LfgDungeonID")]
        public uint? LfgDungeonID;

        [DBFieldName("RewardItemID")]
        public int? RewardItemID;

        [DBFieldName("UiTextureAtlasMemberID")]
        public uint? UiTextureAtlasMemberID;

        [DBFieldName("UiTextureKitID")]
        public uint? UiTextureKitID;

        [DBFieldName("MapID")]
        public int? MapID;

        [DBFieldName("AreaTableID")]
        public uint? AreaTableID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("adventure_map_poi_locale")]
    public sealed record AdventureMapPOILocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
