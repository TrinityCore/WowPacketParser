using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("durability_costs")]
    public sealed record DurabilityCostsHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("WeaponSubClassCost", 21)]
        public ushort?[] WeaponSubClassCost;

        [DBFieldName("ArmorSubClassCost", 8)]
        public ushort?[] ArmorSubClassCost;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
