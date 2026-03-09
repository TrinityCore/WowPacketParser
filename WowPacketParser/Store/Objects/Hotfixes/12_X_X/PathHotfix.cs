using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("path")]
    public sealed record PathHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("SplineType")]
        public byte? SplineType;

        [DBFieldName("Red")]
        public byte? Red;

        [DBFieldName("Green")]
        public byte? Green;

        [DBFieldName("Blue")]
        public byte? Blue;

        [DBFieldName("Alpha")]
        public byte? Alpha;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
