using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_effect")]
    public sealed record ItemEffectHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LegacySlotIndex")]
        public byte? LegacySlotIndex;

        [DBFieldName("TriggerType")]
        public sbyte? TriggerType;

        [DBFieldName("Charges")]
        public short? Charges;

        [DBFieldName("CoolDownMSec")]
        public int? CoolDownMSec;

        [DBFieldName("CategoryCoolDownMSec")]
        public int? CategoryCoolDownMSec;

        [DBFieldName("SpellCategoryID")]
        public ushort? SpellCategoryID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("ChrSpecializationID")]
        public ushort? ChrSpecializationID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_effect")]
    public sealed record ItemEffectHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LegacySlotIndex")]
        public byte? LegacySlotIndex;

        [DBFieldName("TriggerType")]
        public sbyte? TriggerType;

        [DBFieldName("Charges")]
        public short? Charges;

        [DBFieldName("CoolDownMSec")]
        public int? CoolDownMSec;

        [DBFieldName("CategoryCoolDownMSec")]
        public int? CategoryCoolDownMSec;

        [DBFieldName("SpellCategoryID")]
        public ushort? SpellCategoryID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("ChrSpecializationID")]
        public ushort? ChrSpecializationID;

        [DBFieldName("ParentItemID")]
        public int? ParentItemID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
