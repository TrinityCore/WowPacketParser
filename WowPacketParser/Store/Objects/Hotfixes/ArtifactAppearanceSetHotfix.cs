using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact_appearance_set")]
    public sealed record ArtifactAppearanceSetHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DisplayIndex")]
        public byte? DisplayIndex;

        [DBFieldName("UiCameraID")]
        public ushort? UiCameraID;

        [DBFieldName("AltHandUICameraID")]
        public ushort? AltHandUICameraID;

        [DBFieldName("ForgeAttachmentOverride")]
        public sbyte? ForgeAttachmentOverride;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ArtifactID")]
        public uint? ArtifactID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_appearance_set_locale")]
    public sealed record ArtifactAppearanceSetLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_appearance_set")]
    public sealed record ArtifactAppearanceSetHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DisplayIndex")]
        public byte? DisplayIndex;

        [DBFieldName("UiCameraID")]
        public ushort? UiCameraID;

        [DBFieldName("AltHandUICameraID")]
        public ushort? AltHandUICameraID;

        [DBFieldName("ForgeAttachmentOverride")]
        public sbyte? ForgeAttachmentOverride;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ArtifactID")]
        public int? ArtifactID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_appearance_set_locale")]
    public sealed record ArtifactAppearanceSetLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
