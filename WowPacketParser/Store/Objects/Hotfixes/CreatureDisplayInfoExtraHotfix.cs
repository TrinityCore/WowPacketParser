using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("creature_display_info_extra")]
    public sealed record CreatureDisplayInfoExtraHotfix1000: IDataModel
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
        public sbyte? Flags;

        [DBFieldName("BakeMaterialResourcesID")]
        public int? BakeMaterialResourcesID;

        [DBFieldName("HDBakeMaterialResourcesID")]
        public int? HDBakeMaterialResourcesID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("creature_display_info_extra")]
    public sealed record CreatureDisplayInfoExtraHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DisplayRaceID")]
        public sbyte? DisplayRaceID;

        [DBFieldName("DisplaySexID")]
        public sbyte? DisplaySexID;

        [DBFieldName("DisplayClassID")]
        public sbyte? DisplayClassID;

        [DBFieldName("SkinID")]
        public sbyte? SkinID;

        [DBFieldName("FaceID")]
        public sbyte? FaceID;

        [DBFieldName("HairStyleID")]
        public sbyte? HairStyleID;

        [DBFieldName("HairColorID")]
        public sbyte? HairColorID;

        [DBFieldName("FacialHairID")]
        public sbyte? FacialHairID;

        [DBFieldName("Flags")]
        public sbyte? Flags;

        [DBFieldName("BakeMaterialResourcesID")]
        public int? BakeMaterialResourcesID;

        [DBFieldName("HDBakeMaterialResourcesID")]
        public int? HDBakeMaterialResourcesID;

        [DBFieldName("CustomDisplayOption", 3)]
        public byte?[] CustomDisplayOption;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
