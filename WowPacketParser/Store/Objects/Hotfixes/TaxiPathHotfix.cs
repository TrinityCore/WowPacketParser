using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("taxi_path")]
    public sealed record TaxiPathHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FromTaxiNode")]
        public ushort? FromTaxiNode;

        [DBFieldName("ToTaxiNode")]
        public ushort? ToTaxiNode;

        [DBFieldName("Cost")]
        public uint? Cost;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("taxi_path")]
    public sealed record TaxiPathHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FromTaxiNode")]
        public ushort? FromTaxiNode;

        [DBFieldName("ToTaxiNode")]
        public ushort? ToTaxiNode;

        [DBFieldName("Cost")]
        public uint? Cost;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
