using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item")]
    public sealed record ItemHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassID")]
        public byte? ClassID;

        [DBFieldName("SubclassID")]
        public byte? SubclassID;

        [DBFieldName("Material")]
        public byte? Material;

        [DBFieldName("InventoryType")]
        public sbyte? InventoryType;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("SoundOverrideSubclassID")]
        public sbyte? SoundOverrideSubclassID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("ItemGroupSoundsID")]
        public byte? ItemGroupSoundsID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("ModifiedCraftingReagentItemID")]
        public int? ModifiedCraftingReagentItemID;

        [DBFieldName("CraftingQualityID")]
        public int? CraftingQualityID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item")]
    public sealed record ItemHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassID")]
        public byte? ClassID;

        [DBFieldName("SubclassID")]
        public byte? SubclassID;

        [DBFieldName("Material")]
        public byte? Material;

        [DBFieldName("InventoryType")]
        public sbyte? InventoryType;

        [DBFieldName("RequiredLevel")]
        public int? RequiredLevel;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("RandomSelect")]
        public ushort? RandomSelect;

        [DBFieldName("ItemRandomSuffixGroupID")]
        public ushort? ItemRandomSuffixGroupID;

        [DBFieldName("SoundOverrideSubclassID")]
        public sbyte? SoundOverrideSubclassID;

        [DBFieldName("ScalingStatDistributionID")]
        public ushort? ScalingStatDistributionID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("ItemGroupSoundsID")]
        public byte? ItemGroupSoundsID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("MaxDurability")]
        public uint? MaxDurability;

        [DBFieldName("AmmunitionType")]
        public byte? AmmunitionType;

        [DBFieldName("DamageType", 5)]
        public byte?[] DamageType;

        [DBFieldName("Resistances", 7)]
        public short?[] Resistances;

        [DBFieldName("MinDamage", 5)]
        public ushort?[] MinDamage;

        [DBFieldName("MaxDamage", 5)]
        public ushort?[] MaxDamage;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("item")]
    public sealed record ItemHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassID")]
        public byte? ClassID;

        [DBFieldName("SubclassID")]
        public byte? SubclassID;

        [DBFieldName("Material")]
        public byte? Material;

        [DBFieldName("InventoryType")]
        public sbyte? InventoryType;

        [DBFieldName("RequiredLevel")]
        public int? RequiredLevel;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("RandomSelect")]
        public ushort? RandomSelect;

        [DBFieldName("ItemRandomSuffixGroupID")]
        public ushort? ItemRandomSuffixGroupID;

        [DBFieldName("SoundOverrideSubclassID")]
        public sbyte? SoundOverrideSubclassID;

        [DBFieldName("ScalingStatDistributionID")]
        public ushort? ScalingStatDistributionID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("ItemGroupSoundsID")]
        public byte? ItemGroupSoundsID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("MaxDurability")]
        public uint? MaxDurability;

        [DBFieldName("AmmunitionType")]
        public byte? AmmunitionType;

        [DBFieldName("ScalingStatValue")]
        public int? ScalingStatValue;

        [DBFieldName("DamageType", 5)]
        public byte?[] DamageType;

        [DBFieldName("Resistances", 7)]
        public short?[] Resistances;

        [DBFieldName("MinDamage", 5)]
        public ushort?[] MinDamage;

        [DBFieldName("MaxDamage", 5)]
        public ushort?[] MaxDamage;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
