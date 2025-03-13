using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("warband_scene")]
    public sealed record WarbandSceneHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("PositionZ")]
        public float? PositionZ;

        [DBFieldName("LookAtX")]
        public float? LookAtX;

        [DBFieldName("LookAtY")]
        public float? LookAtY;

        [DBFieldName("LookAtZ")]
        public float? LookAtZ;

        [DBFieldName("MapID")]
        public uint? MapID;

        [DBFieldName("Fov")]
        public float? Fov;

        [DBFieldName("TimeOfDay")]
        public int? TimeOfDay;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("warband_scene")]
    public sealed record WarbandSceneHotfix1110 : IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Source")]
        public string Source;

        [DBFieldName("PositionX")]
        public float? PositionX;

        [DBFieldName("PositionY")]
        public float? PositionY;

        [DBFieldName("PositionZ")]
        public float? PositionZ;

        [DBFieldName("LookAtX")]
        public float? LookAtX;

        [DBFieldName("LookAtY")]
        public float? LookAtY;

        [DBFieldName("LookAtZ")]
        public float? LookAtZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public uint? MapID;

        [DBFieldName("Fov")]
        public float? Fov;

        [DBFieldName("TimeOfDay")]
        public int? TimeOfDay;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("SoundAmbienceID")]
        public int? SoundAmbienceID;

        [DBFieldName("Quality")]
        public sbyte? Quality;

        [DBFieldName("TextureKit")]
        public int? TextureKit;

        [DBFieldName("DefaultScenePriority")]
        public int? DefaultScenePriority;

        [DBFieldName("SourceType")]
        public sbyte? SourceType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("warband_scene_locale")]
    public sealed record WarbandSceneLocaleHotfix1110 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Source_lang")]
        public string SourceLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
