using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("path_property")]
    public sealed record PathPropertyHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PathID")]
        public ushort? PathID;

        [DBFieldName("PropertyIndex")]
        public byte? PropertyIndex;

        [DBFieldName("Value")]
        public int? Value;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
