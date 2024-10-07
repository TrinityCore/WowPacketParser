using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("power_type")]
    public sealed record PowerTypeHotfix1100: IDataModel
    {
        [DBFieldName("NameGlobalStringTag")]
        public string NameGlobalStringTag;

        [DBFieldName("CostGlobalStringTag")]
        public string CostGlobalStringTag;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PowerTypeEnum")]
        public sbyte? PowerTypeEnum;

        [DBFieldName("MinPower")]
        public int? MinPower;

        [DBFieldName("MaxBasePower")]
        public int? MaxBasePower;

        [DBFieldName("CenterPower")]
        public int? CenterPower;

        [DBFieldName("DefaultPower")]
        public int? DefaultPower;

        [DBFieldName("DisplayModifier")]
        public int? DisplayModifier;

        [DBFieldName("RegenInterruptTimeMS")]
        public int? RegenInterruptTimeMS;

        [DBFieldName("RegenPeace")]
        public float? RegenPeace;

        [DBFieldName("RegenCombat")]
        public float? RegenCombat;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
