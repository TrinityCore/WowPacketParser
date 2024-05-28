using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact_appearance")]
    public sealed record ArtifactAppearanceHotfix440: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ArtifactAppearanceSetID")]
        public ushort? ArtifactAppearanceSetID;

        [DBFieldName("DisplayIndex")]
        public byte? DisplayIndex;

        [DBFieldName("UnlockPlayerConditionID")]
        public uint? UnlockPlayerConditionID;

        [DBFieldName("ItemAppearanceModifierID")]
        public byte? ItemAppearanceModifierID;

        [DBFieldName("UiSwatchColor")]
        public int? UiSwatchColor;

        [DBFieldName("UiModelSaturation")]
        public float? UiModelSaturation;

        [DBFieldName("UiModelOpacity")]
        public float? UiModelOpacity;

        [DBFieldName("OverrideShapeshiftFormID")]
        public byte? OverrideShapeshiftFormID;

        [DBFieldName("OverrideShapeshiftDisplayID")]
        public uint? OverrideShapeshiftDisplayID;

        [DBFieldName("UiItemAppearanceID")]
        public uint? UiItemAppearanceID;

        [DBFieldName("UiAltItemAppearanceID")]
        public uint? UiAltItemAppearanceID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("UiCameraID")]
        public ushort? UiCameraID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_appearance_locale")]
    public sealed record ArtifactAppearanceLocaleHotfix440: IDataModel
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
