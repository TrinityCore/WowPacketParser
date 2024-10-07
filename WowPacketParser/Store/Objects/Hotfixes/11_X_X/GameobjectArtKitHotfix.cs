using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("gameobject_art_kit")]
    public sealed record GameobjectArtKitHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("AttachModelFileID")]
        public int? AttachModelFileID;

        [DBFieldName("TextureVariationFileID", 3)]
        public int?[] TextureVariationFileID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
