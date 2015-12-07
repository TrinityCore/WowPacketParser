using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("key_chain")]
    public sealed class KeyChain : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? ID;

        [DBFieldName("Key", 32)]
        public byte?[] Key;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
