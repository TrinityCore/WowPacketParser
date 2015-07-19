using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("toy")]
    public sealed class Toy
    {
        [DBFieldName("ItemID")]
        public uint ItemID;

        [DBFieldName("Flags")]
        public uint Flags;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("SourceType")]
        public int SourceType;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
