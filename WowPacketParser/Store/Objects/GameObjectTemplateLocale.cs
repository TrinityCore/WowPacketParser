using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template_locale")]
    public sealed record GameObjectTemplateLocale : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? Entry;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("name", nullable: true)]
        public string Name;

        [DBFieldName("castBarCaption", nullable: true)]
        public string OpeningText;

        [DBFieldName("unk1", nullable: true)]
        public string ClosingText;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
