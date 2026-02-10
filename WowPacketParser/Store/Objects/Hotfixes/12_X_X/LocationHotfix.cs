using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("location")]
    public sealed record LocationHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PosX")]
        public float? PosX;

        [DBFieldName("PosY")]
        public float? PosY;

        [DBFieldName("PosZ")]
        public float? PosZ;

        [DBFieldName("Rot", 3)]
        public float?[] Rot;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
