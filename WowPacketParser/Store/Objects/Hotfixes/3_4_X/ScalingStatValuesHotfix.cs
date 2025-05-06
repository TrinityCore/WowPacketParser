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
    [Hotfix]
    [DBTableName("scaling_stat_values")]
    public sealed record ScalingStatValuesHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Field44054377000")]
        public int? Field44054377000;

        [DBFieldName("Field44054377001")]
        public int? Field44054377001;

        [DBFieldName("Field44054377002")]
        public int? Field44054377002;

        [DBFieldName("Field44054377003")]
        public int? Field44054377003;

        [DBFieldName("Field44054377004")]
        public int? Field44054377004;

        [DBFieldName("Field44054377005")]
        public int? Field44054377005;

        [DBFieldName("Field44054377006")]
        public int? Field44054377006;

        [DBFieldName("Field44054377007")]
        public int? Field44054377007;

        [DBFieldName("Field44054377008")]
        public int? Field44054377008;

        [DBFieldName("Field44054377009")]
        public int? Field44054377009;

        [DBFieldName("Field44054377010")]
        public int? Field44054377010;

        [DBFieldName("Field44054377011")]
        public int? Field44054377011;

        [DBFieldName("Field44054377012")]
        public int? Field44054377012;

        [DBFieldName("Field44054377013")]
        public int? Field44054377013;

        [DBFieldName("Field44054377014")]
        public int? Field44054377014;

        [DBFieldName("Field44054377015")]
        public int? Field44054377015;

        [DBFieldName("Field44054377016")]
        public int? Field44054377016;

        [DBFieldName("Field44054377017")]
        public int? Field44054377017;

        [DBFieldName("Field44054377018")]
        public int? Field44054377018;

        [DBFieldName("Field44054377019")]
        public int? Field44054377019;

        [DBFieldName("Field44054377020")]
        public int? Field44054377020;

        [DBFieldName("Field44054377021")]
        public int? Field44054377021;

        [DBFieldName("Field44054377022")]
        public int? Field44054377022;

        [DBFieldName("Field44054377023")]
        public int? Field44054377023;

        [DBFieldName("Field44054377024")]
        public int? Field44054377024;

        [DBFieldName("Field44054377025")]
        public int? Field44054377025;

        [DBFieldName("Field44054377026")]
        public int? Field44054377026;

        [DBFieldName("Field44054377027")]
        public int? Field44054377027;

        [DBFieldName("Field44054377028")]
        public int? Field44054377028;

        [DBFieldName("Field44054377029")]
        public int? Field44054377029;

        [DBFieldName("Field44054377030", 4)]
        public int?[] Field44054377030;

        [DBFieldName("Field44054377031", 4)]
        public int?[] Field44054377031;

        [DBFieldName("Field44054377032", 4)]
        public int?[] Field44054377032;

        [DBFieldName("Field44054377033", 4)]
        public int?[] Field44054377033;

        [DBFieldName("Field44054377034", 4)]
        public int?[] Field44054377034;

        [DBFieldName("Field44054377035", 4)]
        public int?[] Field44054377035;

        [DBFieldName("Field44054377036", 4)]
        public int?[] Field44054377036;

        [DBFieldName("Field44054377037", 4)]
        public int?[] Field44054377037;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
