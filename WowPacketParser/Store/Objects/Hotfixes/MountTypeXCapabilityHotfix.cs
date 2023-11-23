using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("mount_type_x_capability")]
    public sealed record MountTypeXCapabilityHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MountTypeID")]
        public ushort? MountTypeID;

        [DBFieldName("MountCapabilityID")]
        public ushort? MountCapabilityID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("mount_type_x_capability")]
    public sealed record MountTypeXCapabilityHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MountTypeID")]
        public ushort? MountTypeID;

        [DBFieldName("MountCapabilityID")]
        public ushort? MountCapabilityID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
