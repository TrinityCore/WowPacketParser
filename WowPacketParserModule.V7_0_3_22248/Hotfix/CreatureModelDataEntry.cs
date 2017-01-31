using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureModelData, HasIndexInData = false)]
    public class CreatureModelDataEntry
    {
        public float ModelScale { get; set; }
        public float FootprintTextureLength { get; set; }
        public float FootprintTextureWidth { get; set; }
        public float FootprintParticleScale { get; set; }
        public float CollisionWidth { get; set; }
        public float CollisionHeight { get; set; }
        public float MountHeight { get; set; }
        [HotfixArray(3)]
        public float[] GeoBoxMin { get; set; }
        [HotfixArray(3)]
        public float[] GeoBoxMax { get; set; }
        public float WorldEffectScale { get; set; }
        public float AttachedEffectScale { get; set; }
        public float MissileCollisionRadius { get; set; }
        public float MissileCollisionPush { get; set; }
        public float MissileCollisionRaise { get; set; }
        public float OverrideLootEffectScale { get; set; }
        public float OverrideNameScale { get; set; }
        public float OverrideSelectionRadius { get; set; }
        public float TamedPetBaseScale { get; set; }
        public float HoverHeight { get; set; }
        public uint Flags { get; set; }
        public uint FileDataID { get; set; }
        public uint SizeClass { get; set; }
        public uint BloodID { get; set; }
        public uint FootprintTextureID { get; set; }
        public uint FoleyMaterialID { get; set; }
        public uint FootstepEffectID { get; set; }
        public uint DeathThudEffectID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_1_0_22900, true)]
        public uint FootstepShakeSize { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_1_0_22900, true)]
        public uint DeathThudShakeSize { get; set; }
        public uint SoundID { get; set; }
        public uint CreatureGeosetDataID { get; set; }
    }
}