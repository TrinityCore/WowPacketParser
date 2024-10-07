using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node")]
    public sealed record TraitNodeHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitTreeID")]
        public uint? TraitTreeID;

        [DBFieldName("PosX")]
        public int? PosX;

        [DBFieldName("PosY")]
        public int? PosY;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("TraitSubTreeID")]
        public int? TraitSubTreeID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
