using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_outfit_slot_option")]
    public sealed record TransmogOutfitSlotOptionHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("OptionEnum")]
        public byte? OptionEnum;

        [DBFieldName("TransmogOutfitSlotInfoID")]
        public uint? TransmogOutfitSlotInfoID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SecondaryOptionID")]
        public int? SecondaryOptionID;

        [DBFieldName("ItemCostMultiplier")]
        public float? ItemCostMultiplier;

        [DBFieldName("IllusionCostMultiplier")]
        public float? IllusionCostMultiplier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transmog_outfit_slot_option_locale")]
    public sealed record TransmogOutfitSlotOptionLocaleHotfix1200: IDataModel
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
