using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_node")]
    public sealed record TraitNodeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("PosX")]
        public int? PosX;

        [DBFieldName("PosY")]
        public int? PosY;

        [DBFieldName("Type")]
        public sbyte? Type;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_node")]
    public sealed record TraitNodeHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("PosX")]
        public int? PosX;

        [DBFieldName("PosY")]
        public int? PosY;

        [DBFieldName("Type")]
        public byte? Type;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
