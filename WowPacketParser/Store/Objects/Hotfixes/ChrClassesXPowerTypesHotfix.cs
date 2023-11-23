using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_classes_x_power_types")]
    public sealed record ChrClassesXPowerTypesHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PowerType")]
        public sbyte? PowerType;

        [DBFieldName("ClassID")]
        public uint? ClassID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_classes_x_power_types")]
    public sealed record ChrClassesXPowerTypesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PowerType")]
        public sbyte? PowerType;

        [DBFieldName("ClassID")]
        public int? ClassID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
