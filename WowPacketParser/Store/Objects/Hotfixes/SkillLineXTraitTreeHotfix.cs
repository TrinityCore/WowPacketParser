using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("skill_line_x_trait_tree")]
    public sealed record SkillLineXTraitTreeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SkillLineID")]
        public int? SkillLineID;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("skill_line_x_trait_tree")]
    public sealed record SkillLineXTraitTreeHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SkillLineID")]
        public int? SkillLineID;

        [DBFieldName("TraitTreeID")]
        public int? TraitTreeID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
