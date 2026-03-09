using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("criteria")]
    public sealed record CriteriaHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type")]
        public short? Type;

        [DBFieldName("Asset")]
        public int? Asset;

        [DBFieldName("ModifierTreeId")]
        public uint? ModifierTreeId;

        [DBFieldName("StartEvent")]
        public int? StartEvent;

        [DBFieldName("StartAsset")]
        public int? StartAsset;

        [DBFieldName("StartTimer")]
        public ushort? StartTimer;

        [DBFieldName("FailEvent")]
        public int? FailEvent;

        [DBFieldName("FailAsset")]
        public int? FailAsset;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("EligibilityWorldStateID")]
        public short? EligibilityWorldStateID;

        [DBFieldName("EligibilityWorldStateValue")]
        public sbyte? EligibilityWorldStateValue;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
