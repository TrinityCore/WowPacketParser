using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_interrupts")]
    public sealed record SpellInterruptsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("InterruptFlags")]
        public short? InterruptFlags;

        [DBFieldName("AuraInterruptFlags", 2)]
        public int?[] AuraInterruptFlags;

        [DBFieldName("ChannelInterruptFlags", 2)]
        public int?[] ChannelInterruptFlags;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_interrupts")]
    public sealed record SpellInterruptsHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("InterruptFlags")]
        public short? InterruptFlags;

        [DBFieldName("AuraInterruptFlags", 2)]
        public int?[] AuraInterruptFlags;

        [DBFieldName("ChannelInterruptFlags", 2)]
        public int?[] ChannelInterruptFlags;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
