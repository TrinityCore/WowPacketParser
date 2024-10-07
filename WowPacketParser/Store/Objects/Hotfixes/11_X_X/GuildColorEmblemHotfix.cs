using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("guild_color_emblem")]
    public sealed record GuildColorEmblemHotfix1100: IDataModel
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
