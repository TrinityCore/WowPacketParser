using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_power_set_member")]
    public sealed record AzeritePowerSetMemberHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AzeritePowerSetID")]
        public uint? AzeritePowerSetID;

        [DBFieldName("AzeritePowerID")]
        public int? AzeritePowerID;

        [DBFieldName("Class")]
        public sbyte? Class;

        [DBFieldName("Tier")]
        public byte? Tier;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
