using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("mount_capability")]
    public sealed record MountCapabilityHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ReqRidingSkill")]
        public ushort? ReqRidingSkill;

        [DBFieldName("ReqAreaID")]
        public ushort? ReqAreaID;

        [DBFieldName("ReqSpellAuraID")]
        public uint? ReqSpellAuraID;

        [DBFieldName("ReqSpellKnownID")]
        public int? ReqSpellKnownID;

        [DBFieldName("ModSpellAuraID")]
        public int? ModSpellAuraID;

        [DBFieldName("ReqMapID")]
        public short? ReqMapID;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("FlightCapabilityID")]
        public int? FlightCapabilityID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
