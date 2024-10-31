using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("battlemaster_list")]
    public sealed record BattlemasterListHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("GameType")]
        public string GameType;

        [DBFieldName("ShortDescription")]
        public string ShortDescription;

        [DBFieldName("LongDescription")]
        public string LongDescription;

        [DBFieldName("InstanceType")]
        public sbyte? InstanceType;

        [DBFieldName("MinLevel")]
        public sbyte? MinLevel;

        [DBFieldName("MaxLevel")]
        public sbyte? MaxLevel;

        [DBFieldName("RatedPlayers")]
        public sbyte? RatedPlayers;

        [DBFieldName("MinPlayers")]
        public sbyte? MinPlayers;

        [DBFieldName("MaxPlayers")]
        public int? MaxPlayers;

        [DBFieldName("GroupsAllowed")]
        public sbyte? GroupsAllowed;

        [DBFieldName("MaxGroupSize")]
        public sbyte? MaxGroupSize;

        [DBFieldName("HolidayWorldState")]
        public short? HolidayWorldState;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("RequiredPlayerConditionID")]
        public int? RequiredPlayerConditionID;

        [DBFieldName("Unknown1153_0")]
        public int? Unknown1153_0;

        [DBFieldName("Unknown1153_1")]
        public int? Unknown1153_1;

        [DBFieldName("MapID", 16)]
        public short?[] MapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("battlemaster_list_locale")]
    public sealed record BattlemasterListLocaleHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("GameType_lang")]
        public string GameTypeLang;

        [DBFieldName("ShortDescription_lang")]
        public string ShortDescriptionLang;

        [DBFieldName("LongDescription_lang")]
        public string LongDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("battlemaster_list")]
    public sealed record BattlemasterListHotfix441: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("GameType")]
        public string GameType;

        [DBFieldName("ShortDescription")]
        public string ShortDescription;

        [DBFieldName("LongDescription")]
        public string LongDescription;

        [DBFieldName("InstanceType")]
        public sbyte? InstanceType;

        [DBFieldName("MinLevel")]
        public sbyte? MinLevel;

        [DBFieldName("MaxLevel")]
        public sbyte? MaxLevel;

        [DBFieldName("RatedPlayers")]
        public sbyte? RatedPlayers;

        [DBFieldName("MinPlayers")]
        public sbyte? MinPlayers;

        [DBFieldName("MaxPlayers")]
        public int? MaxPlayers;

        [DBFieldName("GroupsAllowed")]
        public sbyte? GroupsAllowed;

        [DBFieldName("MaxGroupSize")]
        public sbyte? MaxGroupSize;

        [DBFieldName("HolidayWorldState")]
        public short? HolidayWorldState;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("RequiredPlayerConditionID")]
        public int? RequiredPlayerConditionID;

        [DBFieldName("Unknown1153_0")]
        public int? Unknown1153_0;

        [DBFieldName("Unknown1153_1")]
        public int? Unknown1153_1;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("battlemaster_list_locale")]
    public sealed record BattlemasterListLocaleHotfix441: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("GameType_lang")]
        public string GameTypeLang;

        [DBFieldName("ShortDescription_lang")]
        public string ShortDescriptionLang;

        [DBFieldName("LongDescription_lang")]
        public string LongDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
