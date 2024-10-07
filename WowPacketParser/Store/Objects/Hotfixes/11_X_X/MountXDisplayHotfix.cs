using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("mount_x_display")]
    public sealed record MountXDisplayHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CreatureDisplayInfoID")]
        public int? CreatureDisplayInfoID;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("Unknown1100")]
        public ushort? Unknown1100;

        [DBFieldName("MountID")]
        public uint? MountID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
