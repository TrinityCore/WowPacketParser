using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("lock")]
    public sealed record LockHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Index", 8)]
        public int?[] Index;

        [DBFieldName("Skill", 8)]
        public ushort?[] Skill;

        [DBFieldName("Type", 8)]
        public byte?[] Type;

        [DBFieldName("Action", 8)]
        public byte?[] Action;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("lock")]
    public sealed record LockHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Index", 8)]
        public int?[] Index;

        [DBFieldName("Skill", 8)]
        public ushort?[] Skill;

        [DBFieldName("Type", 8)]
        public byte?[] Type;

        [DBFieldName("Action", 8)]
        public byte?[] Action;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
