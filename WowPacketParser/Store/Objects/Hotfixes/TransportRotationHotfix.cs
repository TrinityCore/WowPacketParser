using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transport_rotation")]
    public sealed record TransportRotationHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Rot", 4)]
        public float?[] Rot;

        [DBFieldName("TimeIndex")]
        public uint? TimeIndex;

        [DBFieldName("GameObjectsID")]
        public uint? GameObjectsID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("transport_rotation")]
    public sealed record TransportRotationHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Rot", 4)]
        public float?[] Rot;

        [DBFieldName("TimeIndex")]
        public uint? TimeIndex;

        [DBFieldName("GameObjectsID")]
        public int? GameObjectsID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
