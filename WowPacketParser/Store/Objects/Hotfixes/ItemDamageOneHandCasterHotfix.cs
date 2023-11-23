using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_damage_one_hand_caster")]
    public sealed record ItemDamageOneHandCasterHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("Quality", 7)]
        public float?[] Quality;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_damage_one_hand_caster")]
    public sealed record ItemDamageOneHandCasterHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemLevel")]
        public ushort? ItemLevel;

        [DBFieldName("Quality", 7)]
        public float?[] Quality;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
