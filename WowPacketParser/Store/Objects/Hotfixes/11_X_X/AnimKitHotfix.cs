using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("anim_kit")]
    public sealed record AnimKitHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OneShotDuration")]
        public uint? OneShotDuration;

        [DBFieldName("OneShotStopAnimKitID")]
        public ushort? OneShotStopAnimKitID;

        [DBFieldName("LowDefAnimKitID")]
        public ushort? LowDefAnimKitID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
