using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("guild_color_border")]
    public sealed record GuildColorBorderHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Red")]
        public byte? Red;

        [DBFieldName("Blue")]
        public byte? Blue;

        [DBFieldName("Green")]
        public byte? Green;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("guild_color_border")]
    public sealed record GuildColorBorderHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Red")]
        public byte? Red;

        [DBFieldName("Blue")]
        public byte? Blue;

        [DBFieldName("Green")]
        public byte? Green;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
