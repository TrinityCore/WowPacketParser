using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("artifact")]
    public sealed record ArtifactHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("UiNameColor")]
        public int? UiNameColor;

        [DBFieldName("UiBarOverlayColor")]
        public int? UiBarOverlayColor;

        [DBFieldName("UiBarBackgroundColor")]
        public int? UiBarBackgroundColor;

        [DBFieldName("ChrSpecializationID")]
        public ushort? ChrSpecializationID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ArtifactCategoryID")]
        public byte? ArtifactCategoryID;

        [DBFieldName("UiModelSceneID")]
        public uint? UiModelSceneID;

        [DBFieldName("SpellVisualKitID")]
        public uint? SpellVisualKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_locale")]
    public sealed record ArtifactLocaleHotfix1000: IDataModel
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

    [Hotfix]
    [DBTableName("artifact")]
    public sealed record ArtifactHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("UiTextureKitID")]
        public ushort? UiTextureKitID;

        [DBFieldName("UiNameColor")]
        public int? UiNameColor;

        [DBFieldName("UiBarOverlayColor")]
        public int? UiBarOverlayColor;

        [DBFieldName("UiBarBackgroundColor")]
        public int? UiBarBackgroundColor;

        [DBFieldName("ChrSpecializationID")]
        public ushort? ChrSpecializationID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ArtifactCategoryID")]
        public byte? ArtifactCategoryID;

        [DBFieldName("UiModelSceneID")]
        public uint? UiModelSceneID;

        [DBFieldName("SpellVisualKitID")]
        public uint? SpellVisualKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("artifact_locale")]
    public sealed record ArtifactLocaleHotfix340: IDataModel
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
