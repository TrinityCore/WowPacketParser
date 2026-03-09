using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_creation_context")]
    public sealed record ItemCreationContextHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemContext")]
        public byte? ItemContext;

        [DBFieldName("ItemCreationContextGroupID")]
        public uint? ItemCreationContextGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
