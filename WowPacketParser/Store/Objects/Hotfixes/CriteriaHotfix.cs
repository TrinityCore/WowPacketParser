using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("criteria")]
    public sealed record CriteriaHotfix1000: IDataModel
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
        public byte? StartEvent;

        [DBFieldName("StartAsset")]
        public int? StartAsset;

        [DBFieldName("StartTimer")]
        public ushort? StartTimer;

        [DBFieldName("FailEvent")]
        public byte? FailEvent;

        [DBFieldName("FailAsset")]
        public int? FailAsset;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("EligibilityWorldStateID")]
        public short? EligibilityWorldStateID;

        [DBFieldName("EligibilityWorldStateValue")]
        public sbyte? EligibilityWorldStateValue;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("criteria")]
    public sealed record CriteriaHotfix1015 : IDataModel
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

    [Hotfix]
    [DBTableName("criteria")]
    public sealed record CriteriaHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type")]
        public short? Type;

        [DBFieldName("Asset")]
        public int? Asset;

        [DBFieldName("ModifierTreeID")]
        public uint? ModifierTreeID;

        [DBFieldName("StartEvent")]
        public byte? StartEvent;

        [DBFieldName("StartAsset")]
        public int? StartAsset;

        [DBFieldName("StartTimer")]
        public ushort? StartTimer;

        [DBFieldName("FailEvent")]
        public byte? FailEvent;

        [DBFieldName("FailAsset")]
        public int? FailAsset;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("EligibilityWorldStateID")]
        public short? EligibilityWorldStateID;

        [DBFieldName("EligibilityWorldStateValue")]
        public sbyte? EligibilityWorldStateValue;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("criteria")]
    public sealed record CriteriaHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Type")]
        public short? Type;

        [DBFieldName("Asset")]
        public int? Asset;

        [DBFieldName("ModifierTreeID")]
        public uint? ModifierTreeID;

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
