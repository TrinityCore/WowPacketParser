using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_price_base")]
    public sealed record ItemPriceBaseHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("Armor")]
        public float? Armor;

        [DBFieldName("Weapon")]
        public float? Weapon;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
