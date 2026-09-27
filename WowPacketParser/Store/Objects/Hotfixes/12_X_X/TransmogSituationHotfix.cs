using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_situation")]
    public sealed record TransmogSituationHotfix1200: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SituationEnum")]
        public sbyte? SituationEnum;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("TransmogSituationGroupID")]
        public uint? TransmogSituationGroupID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transmog_situation_locale")]
    public sealed record TransmogSituationLocaleHotfix1200: IDataModel
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
