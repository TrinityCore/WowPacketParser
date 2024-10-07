using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_set")]
    public sealed record TransmogSetHotfix1100: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("TrackingQuestID")]
        public int? TrackingQuestID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("TransmogSetGroupID")]
        public int? TransmogSetGroupID;

        [DBFieldName("ItemNameDescriptionID")]
        public int? ItemNameDescriptionID;

        [DBFieldName("ParentTransmogSetID")]
        public uint? ParentTransmogSetID;

        [DBFieldName("Unknown810")]
        public int? Unknown810;

        [DBFieldName("ExpansionID")]
        public int? ExpansionID;

        [DBFieldName("PatchID")]
        public int? PatchID;

        [DBFieldName("UiOrder")]
        public int? UiOrder;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transmog_set_locale")]
    public sealed record TransmogSetLocaleHotfix1100: IDataModel
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
