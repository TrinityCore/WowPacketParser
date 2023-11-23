using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual_effect_name")]
    public sealed record SpellVisualEffectNameHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ModelFileDataID")]
        public int? ModelFileDataID;

        [DBFieldName("BaseMissileSpeed")]
        public float? BaseMissileSpeed;

        [DBFieldName("Scale")]
        public float? Scale;

        [DBFieldName("MinAllowedScale")]
        public float? MinAllowedScale;

        [DBFieldName("MaxAllowedScale")]
        public float? MaxAllowedScale;

        [DBFieldName("Alpha")]
        public float? Alpha;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("TextureFileDataID")]
        public int? TextureFileDataID;

        [DBFieldName("EffectRadius")]
        public float? EffectRadius;

        [DBFieldName("Type")]
        public uint? Type;

        [DBFieldName("GenericID")]
        public int? GenericID;

        [DBFieldName("RibbonQualityID")]
        public uint? RibbonQualityID;

        [DBFieldName("DissolveEffectID")]
        public int? DissolveEffectID;

        [DBFieldName("ModelPosition")]
        public int? ModelPosition;

        [DBFieldName("Unknown901")]
        public sbyte? Unknown901;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_visual_effect_name")]
    public sealed record SpellVisualEffectNameHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ModelFileDataID")]
        public int? ModelFileDataID;

        [DBFieldName("BaseMissileSpeed")]
        public float? BaseMissileSpeed;

        [DBFieldName("Scale")]
        public float? Scale;

        [DBFieldName("MinAllowedScale")]
        public float? MinAllowedScale;

        [DBFieldName("MaxAllowedScale")]
        public float? MaxAllowedScale;

        [DBFieldName("Alpha")]
        public float? Alpha;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("TextureFileDataID")]
        public int? TextureFileDataID;

        [DBFieldName("EffectRadius")]
        public float? EffectRadius;

        [DBFieldName("Type")]
        public uint? Type;

        [DBFieldName("GenericID")]
        public int? GenericID;

        [DBFieldName("RibbonQualityID")]
        public uint? RibbonQualityID;

        [DBFieldName("DissolveEffectID")]
        public int? DissolveEffectID;

        [DBFieldName("ModelPosition")]
        public int? ModelPosition;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
