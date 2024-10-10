using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transport_rotation")]
    public sealed record TransportRotationHotfix1100: IDataModel
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
}
