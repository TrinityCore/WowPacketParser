using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_power_set_member")]
    public sealed record AzeritePowerSetMemberHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AzeritePowerSetID")]
        public int? AzeritePowerSetID;

        [DBFieldName("AzeritePowerID")]
        public int? AzeritePowerID;

        [DBFieldName("Class")]
        public int? Class;

        [DBFieldName("Tier")]
        public sbyte? Tier;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("azerite_power_set_member")]
    public sealed record AzeritePowerSetMemberHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AzeritePowerSetID")]
        public int? AzeritePowerSetID;

        [DBFieldName("AzeritePowerID")]
        public int? AzeritePowerID;

        [DBFieldName("Class")]
        public int? Class;

        [DBFieldName("Tier")]
        public byte? Tier;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
