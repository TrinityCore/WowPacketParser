using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("path_node")]
    public sealed record PathNodeHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PathID")]
        public ushort? PathID;

        [DBFieldName("Sequence")]
        public short? Sequence;

        [DBFieldName("LocationID")]
        public int? LocationID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
