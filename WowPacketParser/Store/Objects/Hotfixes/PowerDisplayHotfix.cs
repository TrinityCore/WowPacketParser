using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("power_display")]
    public sealed record PowerDisplayHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GlobalStringBaseTag")]
        public string GlobalStringBaseTag;

        [DBFieldName("ActualType")]
        public byte? ActualType;

        [DBFieldName("Red")]
        public byte? Red;

        [DBFieldName("Green")]
        public byte? Green;

        [DBFieldName("Blue")]
        public byte? Blue;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("power_display")]
    public sealed record PowerDisplayHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GlobalStringBaseTag")]
        public string GlobalStringBaseTag;

        [DBFieldName("ActualType")]
        public byte? ActualType;

        [DBFieldName("Red")]
        public byte? Red;

        [DBFieldName("Green")]
        public byte? Green;

        [DBFieldName("Blue")]
        public byte? Blue;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
