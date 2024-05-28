using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_set")]
    public sealed record TransmogSetHotfix440: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("TrackingQuestID")]
        public uint? TrackingQuestID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("TransmogSetGroupID")]
        public uint? TransmogSetGroupID;

        [DBFieldName("ItemNameDescriptionID")]
        public int? ItemNameDescriptionID;

        [DBFieldName("ParentTransmogSetID")]
        public ushort? ParentTransmogSetID;

        [DBFieldName("ExpansionID")]
        public byte? ExpansionID;

        [DBFieldName("UiOrder")]
        public short? UiOrder;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transmog_set_locale")]
    public sealed record TransmogSetLocaleHotfix440: IDataModel
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
