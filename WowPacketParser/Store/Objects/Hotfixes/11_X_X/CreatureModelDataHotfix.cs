using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("creature_model_data")]
    public sealed record CreatureModelDataHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("GeoBox", 6)]
        public float?[] GeoBox;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("FileDataID")]
        public uint? FileDataID;

        [DBFieldName("WalkSpeed")]
        public float? WalkSpeed;

        [DBFieldName("RunSpeed")]
        public float? RunSpeed;

        [DBFieldName("BloodID")]
        public uint? BloodID;

        [DBFieldName("FootprintTextureID")]
        public uint? FootprintTextureID;

        [DBFieldName("FootprintTextureLength")]
        public float? FootprintTextureLength;

        [DBFieldName("FootprintTextureWidth")]
        public float? FootprintTextureWidth;

        [DBFieldName("FootprintParticleScale")]
        public float? FootprintParticleScale;

        [DBFieldName("FoleyMaterialID")]
        public uint? FoleyMaterialID;

        [DBFieldName("FootstepCameraEffectID")]
        public uint? FootstepCameraEffectID;

        [DBFieldName("DeathThudCameraEffectID")]
        public uint? DeathThudCameraEffectID;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("SizeClass")]
        public uint? SizeClass;

        [DBFieldName("CollisionWidth")]
        public float? CollisionWidth;

        [DBFieldName("CollisionHeight")]
        public float? CollisionHeight;

        [DBFieldName("WorldEffectScale")]
        public float? WorldEffectScale;

        [DBFieldName("CreatureGeosetDataID")]
        public uint? CreatureGeosetDataID;

        [DBFieldName("HoverHeight")]
        public float? HoverHeight;

        [DBFieldName("AttachedEffectScale")]
        public float? AttachedEffectScale;

        [DBFieldName("ModelScale")]
        public float? ModelScale;

        [DBFieldName("MissileCollisionRadius")]
        public float? MissileCollisionRadius;

        [DBFieldName("MissileCollisionPush")]
        public float? MissileCollisionPush;

        [DBFieldName("MissileCollisionRaise")]
        public float? MissileCollisionRaise;

        [DBFieldName("MountHeight")]
        public float? MountHeight;

        [DBFieldName("OverrideLootEffectScale")]
        public float? OverrideLootEffectScale;

        [DBFieldName("OverrideNameScale")]
        public float? OverrideNameScale;

        [DBFieldName("OverrideSelectionRadius")]
        public float? OverrideSelectionRadius;

        [DBFieldName("TamedPetBaseScale")]
        public float? TamedPetBaseScale;

        [DBFieldName("MountScaleOtherIndex")]
        public sbyte? MountScaleOtherIndex;

        [DBFieldName("MountScaleSelf")]
        public float? MountScaleSelf;

        [DBFieldName("Unknown1100")]
        public ushort? Unknown1100;

        [DBFieldName("MountScaleOther", 2)]
        public float?[] MountScaleOther;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
