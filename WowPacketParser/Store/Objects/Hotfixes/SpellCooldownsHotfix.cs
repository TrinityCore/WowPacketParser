using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_cooldowns")]
    public sealed record SpellCooldownsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("CategoryRecoveryTime")]
        public int? CategoryRecoveryTime;

        [DBFieldName("RecoveryTime")]
        public int? RecoveryTime;

        [DBFieldName("StartRecoveryTime")]
        public int? StartRecoveryTime;

        [DBFieldName("AuraSpellID")]
        public int? AuraSpellID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_cooldowns")]
    public sealed record SpellCooldownsHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("CategoryRecoveryTime")]
        public int? CategoryRecoveryTime;

        [DBFieldName("RecoveryTime")]
        public int? RecoveryTime;

        [DBFieldName("StartRecoveryTime")]
        public int? StartRecoveryTime;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
