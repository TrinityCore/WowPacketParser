using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("animation_data")]
    public sealed record AnimationDataHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Fallback")]
        public ushort? Fallback;

        [DBFieldName("BehaviorTier")]
        public sbyte? BehaviorTier;

        [DBFieldName("BehaviorID")]
        public short? BehaviorID;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
