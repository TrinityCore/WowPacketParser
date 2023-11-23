using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("creature_display_info")]
    public sealed record CreatureDisplayInfoHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ModelID")]
        public ushort? ModelID;

        [DBFieldName("SoundID")]
        public ushort? SoundID;

        [DBFieldName("SizeClass")]
        public sbyte? SizeClass;

        [DBFieldName("CreatureModelScale")]
        public float? CreatureModelScale;

        [DBFieldName("CreatureModelAlpha")]
        public byte? CreatureModelAlpha;

        [DBFieldName("BloodID")]
        public byte? BloodID;

        [DBFieldName("ExtendedDisplayInfoID")]
        public int? ExtendedDisplayInfoID;

        [DBFieldName("NPCSoundID")]
        public ushort? NPCSoundID;

        [DBFieldName("ParticleColorID")]
        public ushort? ParticleColorID;

        [DBFieldName("PortraitCreatureDisplayInfoID")]
        public int? PortraitCreatureDisplayInfoID;

        [DBFieldName("PortraitTextureFileDataID")]
        public int? PortraitTextureFileDataID;

        [DBFieldName("ObjectEffectPackageID")]
        public ushort? ObjectEffectPackageID;

        [DBFieldName("AnimReplacementSetID")]
        public ushort? AnimReplacementSetID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("StateSpellVisualKitID")]
        public int? StateSpellVisualKitID;

        [DBFieldName("PlayerOverrideScale")]
        public float? PlayerOverrideScale;

        [DBFieldName("PetInstanceScale")]
        public float? PetInstanceScale;

        [DBFieldName("UnarmedWeaponType")]
        public sbyte? UnarmedWeaponType;

        [DBFieldName("MountPoofSpellVisualKitID")]
        public int? MountPoofSpellVisualKitID;

        [DBFieldName("DissolveEffectID")]
        public int? DissolveEffectID;

        [DBFieldName("Gender")]
        public sbyte? Gender;

        [DBFieldName("DissolveOutEffectID")]
        public int? DissolveOutEffectID;

        [DBFieldName("CreatureModelMinLod")]
        public sbyte? CreatureModelMinLod;

        [DBFieldName("TextureVariationFileDataID", 4)]
        public int?[] TextureVariationFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("creature_display_info")]
    public sealed record CreatureDisplayInfoHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ModelID")]
        public ushort? ModelID;

        [DBFieldName("SoundID")]
        public ushort? SoundID;

        [DBFieldName("SizeClass")]
        public sbyte? SizeClass;

        [DBFieldName("CreatureModelScale")]
        public float? CreatureModelScale;

        [DBFieldName("CreatureModelAlpha")]
        public byte? CreatureModelAlpha;

        [DBFieldName("BloodID")]
        public byte? BloodID;

        [DBFieldName("ExtendedDisplayInfoID")]
        public int? ExtendedDisplayInfoID;

        [DBFieldName("NPCSoundID")]
        public ushort? NPCSoundID;

        [DBFieldName("ParticleColorID")]
        public ushort? ParticleColorID;

        [DBFieldName("PortraitCreatureDisplayInfoID")]
        public int? PortraitCreatureDisplayInfoID;

        [DBFieldName("PortraitTextureFileDataID")]
        public int? PortraitTextureFileDataID;

        [DBFieldName("ObjectEffectPackageID")]
        public ushort? ObjectEffectPackageID;

        [DBFieldName("AnimReplacementSetID")]
        public ushort? AnimReplacementSetID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("StateSpellVisualKitID")]
        public int? StateSpellVisualKitID;

        [DBFieldName("PlayerOverrideScale")]
        public float? PlayerOverrideScale;

        [DBFieldName("PetInstanceScale")]
        public float? PetInstanceScale;

        [DBFieldName("UnarmedWeaponType")]
        public sbyte? UnarmedWeaponType;

        [DBFieldName("MountPoofSpellVisualKitID")]
        public int? MountPoofSpellVisualKitID;

        [DBFieldName("DissolveEffectID")]
        public int? DissolveEffectID;

        [DBFieldName("Gender")]
        public sbyte? Gender;

        [DBFieldName("DissolveOutEffectID")]
        public int? DissolveOutEffectID;

        [DBFieldName("CreatureModelMinLod")]
        public sbyte? CreatureModelMinLod;

        [DBFieldName("TextureVariationFileDataID", 3)]
        public int?[] TextureVariationFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("creature_display_info")]
    public sealed record CreatureDisplayInfoHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ModelID")]
        public ushort? ModelID;

        [DBFieldName("SoundID")]
        public ushort? SoundID;

        [DBFieldName("SizeClass")]
        public sbyte? SizeClass;

        [DBFieldName("CreatureModelScale")]
        public float? CreatureModelScale;

        [DBFieldName("CreatureModelAlpha")]
        public byte? CreatureModelAlpha;

        [DBFieldName("BloodID")]
        public byte? BloodID;

        [DBFieldName("ExtendedDisplayInfoID")]
        public int? ExtendedDisplayInfoID;

        [DBFieldName("NPCSoundID")]
        public ushort? NPCSoundID;

        [DBFieldName("ParticleColorID")]
        public ushort? ParticleColorID;

        [DBFieldName("PortraitCreatureDisplayInfoID")]
        public int? PortraitCreatureDisplayInfoID;

        [DBFieldName("PortraitTextureFileDataID")]
        public int? PortraitTextureFileDataID;

        [DBFieldName("ObjectEffectPackageID")]
        public ushort? ObjectEffectPackageID;

        [DBFieldName("AnimReplacementSetID")]
        public ushort? AnimReplacementSetID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("StateSpellVisualKitID")]
        public int? StateSpellVisualKitID;

        [DBFieldName("PlayerOverrideScale")]
        public float? PlayerOverrideScale;

        [DBFieldName("PetInstanceScale")]
        public float? PetInstanceScale;

        [DBFieldName("UnarmedWeaponType")]
        public sbyte? UnarmedWeaponType;

        [DBFieldName("MountPoofSpellVisualKitID")]
        public int? MountPoofSpellVisualKitID;

        [DBFieldName("DissolveEffectID")]
        public int? DissolveEffectID;

        [DBFieldName("Gender")]
        public sbyte? Gender;

        [DBFieldName("DissolveOutEffectID")]
        public int? DissolveOutEffectID;

        [DBFieldName("CreatureModelMinLod")]
        public sbyte? CreatureModelMinLod;

        [DBFieldName("TextureVariationFileDataID", 4)]
        public int?[] TextureVariationFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
