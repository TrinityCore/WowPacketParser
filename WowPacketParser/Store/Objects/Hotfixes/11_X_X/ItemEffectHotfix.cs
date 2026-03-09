using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_effect")]
    public sealed record ItemEffectHotfix1100: IDataModel
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
    public sealed record ItemEffectHotfix1127 : IDataModel
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

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
