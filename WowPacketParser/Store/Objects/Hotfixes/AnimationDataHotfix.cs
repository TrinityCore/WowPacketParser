using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("animation_data")]
    public sealed record AnimationDataHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Fallback")]
        public ushort? Fallback;

        [DBFieldName("BehaviorTier")]
        public byte? BehaviorTier;

        [DBFieldName("BehaviorID")]
        public int? BehaviorID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("animation_data")]
    public sealed record AnimationDataHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Fallback")]
        public ushort? Fallback;

        [DBFieldName("BehaviorTier")]
        public byte? BehaviorTier;

        [DBFieldName("BehaviorID")]
        public int? BehaviorID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
