using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("expected_stat")]
    public sealed record ExpectedStatHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ExpansionID")]
        public int? ExpansionID;

        [DBFieldName("CreatureHealth")]
        public float? CreatureHealth;

        [DBFieldName("PlayerHealth")]
        public float? PlayerHealth;

        [DBFieldName("CreatureAutoAttackDps")]
        public float? CreatureAutoAttackDps;

        [DBFieldName("CreatureArmor")]
        public float? CreatureArmor;

        [DBFieldName("PlayerMana")]
        public float? PlayerMana;

        [DBFieldName("PlayerPrimaryStat")]
        public float? PlayerPrimaryStat;

        [DBFieldName("PlayerSecondaryStat")]
        public float? PlayerSecondaryStat;

        [DBFieldName("ArmorConstant")]
        public float? ArmorConstant;

        [DBFieldName("CreatureSpellDamage")]
        public float? CreatureSpellDamage;

        [DBFieldName("Lvl")]
        public uint? Lvl;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("expected_stat")]
    public sealed record ExpectedStatHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ExpansionID")]
        public int? ExpansionID;

        [DBFieldName("CreatureHealth")]
        public float? CreatureHealth;

        [DBFieldName("PlayerHealth")]
        public float? PlayerHealth;

        [DBFieldName("CreatureAutoAttackDps")]
        public float? CreatureAutoAttackDps;

        [DBFieldName("CreatureArmor")]
        public float? CreatureArmor;

        [DBFieldName("PlayerMana")]
        public float? PlayerMana;

        [DBFieldName("PlayerPrimaryStat")]
        public float? PlayerPrimaryStat;

        [DBFieldName("PlayerSecondaryStat")]
        public float? PlayerSecondaryStat;

        [DBFieldName("ArmorConstant")]
        public float? ArmorConstant;

        [DBFieldName("CreatureSpellDamage")]
        public float? CreatureSpellDamage;

        [DBFieldName("Lvl")]
        public int? Lvl;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
