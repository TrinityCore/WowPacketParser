using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_disenchant_loot")]
    public sealed record ItemDisenchantLootHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Subclass")]
        public sbyte? Subclass;

        [DBFieldName("Quality")]
        public byte? Quality;

        [DBFieldName("MinLevel")]
        public ushort? MinLevel;

        [DBFieldName("MaxLevel")]
        public ushort? MaxLevel;

        [DBFieldName("SkillRequired")]
        public ushort? SkillRequired;

        [DBFieldName("ExpansionID")]
        public sbyte? ExpansionID;

        [DBFieldName("Class")]
        public uint? Class;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("item_disenchant_loot")]
    public sealed record ItemDisenchantLootHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Subclass")]
        public sbyte? Subclass;

        [DBFieldName("Quality")]
        public byte? Quality;

        [DBFieldName("MinLevel")]
        public ushort? MinLevel;

        [DBFieldName("MaxLevel")]
        public ushort? MaxLevel;

        [DBFieldName("SkillRequired")]
        public ushort? SkillRequired;

        [DBFieldName("ExpansionID")]
        public sbyte? ExpansionID;

        [DBFieldName("Class")]
        public int? Class;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
