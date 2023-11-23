using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("sound_kit")]
    public sealed record SoundKitHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SoundType")]
        public int? SoundType;

        [DBFieldName("VolumeFloat")]
        public float? VolumeFloat;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("MinDistance")]
        public float? MinDistance;

        [DBFieldName("DistanceCutoff")]
        public float? DistanceCutoff;

        [DBFieldName("EAXDef")]
        public byte? EAXDef;

        [DBFieldName("SoundKitAdvancedID")]
        public uint? SoundKitAdvancedID;

        [DBFieldName("VolumeVariationPlus")]
        public float? VolumeVariationPlus;

        [DBFieldName("VolumeVariationMinus")]
        public float? VolumeVariationMinus;

        [DBFieldName("PitchVariationPlus")]
        public float? PitchVariationPlus;

        [DBFieldName("PitchVariationMinus")]
        public float? PitchVariationMinus;

        [DBFieldName("DialogType")]
        public sbyte? DialogType;

        [DBFieldName("PitchAdjust")]
        public float? PitchAdjust;

        [DBFieldName("BusOverwriteID")]
        public ushort? BusOverwriteID;

        [DBFieldName("MaxInstances")]
        public byte? MaxInstances;

        [DBFieldName("SoundMixGroupID")]
        public uint? SoundMixGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("sound_kit")]
    public sealed record SoundKitHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SoundType")]
        public byte? SoundType;

        [DBFieldName("VolumeFloat")]
        public float? VolumeFloat;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("MinDistance")]
        public float? MinDistance;

        [DBFieldName("DistanceCutoff")]
        public float? DistanceCutoff;

        [DBFieldName("EAXDef")]
        public byte? EAXDef;

        [DBFieldName("SoundKitAdvancedID")]
        public uint? SoundKitAdvancedID;

        [DBFieldName("VolumeVariationPlus")]
        public float? VolumeVariationPlus;

        [DBFieldName("VolumeVariationMinus")]
        public float? VolumeVariationMinus;

        [DBFieldName("PitchVariationPlus")]
        public float? PitchVariationPlus;

        [DBFieldName("PitchVariationMinus")]
        public float? PitchVariationMinus;

        [DBFieldName("DialogType")]
        public sbyte? DialogType;

        [DBFieldName("PitchAdjust")]
        public float? PitchAdjust;

        [DBFieldName("BusOverwriteID")]
        public ushort? BusOverwriteID;

        [DBFieldName("MaxInstances")]
        public byte? MaxInstances;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("sound_kit")]
    public sealed record SoundKitHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SoundType")]
        public byte? SoundType;

        [DBFieldName("VolumeFloat")]
        public float? VolumeFloat;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("MinDistance")]
        public float? MinDistance;

        [DBFieldName("DistanceCutoff")]
        public float? DistanceCutoff;

        [DBFieldName("EAXDef")]
        public byte? EAXDef;

        [DBFieldName("SoundKitAdvancedID")]
        public uint? SoundKitAdvancedID;

        [DBFieldName("VolumeVariationPlus")]
        public float? VolumeVariationPlus;

        [DBFieldName("VolumeVariationMinus")]
        public float? VolumeVariationMinus;

        [DBFieldName("PitchVariationPlus")]
        public float? PitchVariationPlus;

        [DBFieldName("PitchVariationMinus")]
        public float? PitchVariationMinus;

        [DBFieldName("DialogType")]
        public sbyte? DialogType;

        [DBFieldName("PitchAdjust")]
        public float? PitchAdjust;

        [DBFieldName("BusOverwriteID")]
        public ushort? BusOverwriteID;

        [DBFieldName("MaxInstances")]
        public byte? MaxInstances;

        [DBFieldName("SoundMixGroupID")]
        public uint? SoundMixGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
