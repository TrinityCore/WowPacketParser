using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("scaling_stat_values")]
    public sealed record ScalingStatValuesHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Charlevel")]
        public int? Charlevel;

        [DBFieldName("WeaponDPS1H")]
        public int? WeaponDPS1H;

        [DBFieldName("WeaponDPS2H")]
        public int? WeaponDPS2H;

        [DBFieldName("SpellcasterDPS1H")]
        public int? SpellcasterDPS1H;

        [DBFieldName("SpellcasterDPS2H")]
        public int? SpellcasterDPS2H;

        [DBFieldName("RangedDPS")]
        public int? RangedDPS;

        [DBFieldName("WandDPS")]
        public int? WandDPS;

        [DBFieldName("SpellPower")]
        public int? SpellPower;

        [DBFieldName("ShoulderBudget")]
        public int? ShoulderBudget;

        [DBFieldName("TrinketBudget")]
        public int? TrinketBudget;

        [DBFieldName("WeaponBudget1H")]
        public int? WeaponBudget1H;

        [DBFieldName("PrimaryBudget")]
        public int? PrimaryBudget;

        [DBFieldName("RangedBudget")]
        public int? RangedBudget;

        [DBFieldName("TertiaryBudget")]
        public int? TertiaryBudget;

        [DBFieldName("ClothShoulderArmor")]
        public int? ClothShoulderArmor;

        [DBFieldName("LeatherShoulderArmor")]
        public int? LeatherShoulderArmor;

        [DBFieldName("MailShoulderArmor")]
        public int? MailShoulderArmor;

        [DBFieldName("PlateShoulderArmor")]
        public int? PlateShoulderArmor;

        [DBFieldName("ClothCloakArmor")]
        public int? ClothCloakArmor;

        [DBFieldName("ClothChestArmor")]
        public int? ClothChestArmor;

        [DBFieldName("LeatherChestArmor")]
        public int? LeatherChestArmor;

        [DBFieldName("MailChestArmor")]
        public int? MailChestArmor;

        [DBFieldName("PlateChestArmor")]
        public int? PlateChestArmor;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
