using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("pvp_talent_slot_unlock")]
    public sealed record PvpTalentSlotUnlockHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Slot")]
        public sbyte? Slot;

        [DBFieldName("LevelRequired")]
        public int? LevelRequired;

        [DBFieldName("DeathKnightLevelRequired")]
        public int? DeathKnightLevelRequired;

        [DBFieldName("DemonHunterLevelRequired")]
        public int? DemonHunterLevelRequired;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("pvp_talent_slot_unlock")]
    public sealed record PvpTalentSlotUnlockHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Slot")]
        public sbyte? Slot;

        [DBFieldName("LevelRequired")]
        public int? LevelRequired;

        [DBFieldName("DeathKnightLevelRequired")]
        public int? DeathKnightLevelRequired;

        [DBFieldName("DemonHunterLevelRequired")]
        public int? DemonHunterLevelRequired;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
