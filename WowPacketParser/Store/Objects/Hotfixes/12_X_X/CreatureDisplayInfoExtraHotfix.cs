using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("creature_display_info_extra")]
    public sealed record CreatureDisplayInfoExtraHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DisplayRaceID")]
        public sbyte? DisplayRaceID;

        [DBFieldName("DisplaySexID")]
        public sbyte? DisplaySexID;

        [DBFieldName("DisplayClassID")]
        public sbyte? DisplayClassID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("BakeMaterialResourcesID")]
        public int? BakeMaterialResourcesID;

        [DBFieldName("HDBakeMaterialResourcesID")]
        public int? HDBakeMaterialResourcesID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
