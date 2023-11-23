using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("expected_stat_mod")]
    public sealed record ExpectedStatModHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreatureHealthMod")]
        public float? CreatureHealthMod;

        [DBFieldName("PlayerHealthMod")]
        public float? PlayerHealthMod;

        [DBFieldName("CreatureAutoAttackDPSMod")]
        public float? CreatureAutoAttackDPSMod;

        [DBFieldName("CreatureArmorMod")]
        public float? CreatureArmorMod;

        [DBFieldName("PlayerManaMod")]
        public float? PlayerManaMod;

        [DBFieldName("PlayerPrimaryStatMod")]
        public float? PlayerPrimaryStatMod;

        [DBFieldName("PlayerSecondaryStatMod")]
        public float? PlayerSecondaryStatMod;

        [DBFieldName("ArmorConstantMod")]
        public float? ArmorConstantMod;

        [DBFieldName("CreatureSpellDamageMod")]
        public float? CreatureSpellDamageMod;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("expected_stat_mod")]
    public sealed record ExpectedStatModHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreatureHealthMod")]
        public float? CreatureHealthMod;

        [DBFieldName("PlayerHealthMod")]
        public float? PlayerHealthMod;

        [DBFieldName("CreatureAutoAttackDPSMod")]
        public float? CreatureAutoAttackDPSMod;

        [DBFieldName("CreatureArmorMod")]
        public float? CreatureArmorMod;

        [DBFieldName("PlayerManaMod")]
        public float? PlayerManaMod;

        [DBFieldName("PlayerPrimaryStatMod")]
        public float? PlayerPrimaryStatMod;

        [DBFieldName("PlayerSecondaryStatMod")]
        public float? PlayerSecondaryStatMod;

        [DBFieldName("ArmorConstantMod")]
        public float? ArmorConstantMod;

        [DBFieldName("CreatureSpellDamageMod")]
        public float? CreatureSpellDamageMod;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
