using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("liquid_type")]
    public sealed record LiquidTypeHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Texture", 6)]
        public string[] Texture;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("SoundBank")]
        public byte? SoundBank;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("MaxDarkenDepth")]
        public float? MaxDarkenDepth;

        [DBFieldName("FogDarkenIntensity")]
        public float? FogDarkenIntensity;

        [DBFieldName("AmbDarkenIntensity")]
        public float? AmbDarkenIntensity;

        [DBFieldName("DirDarkenIntensity")]
        public float? DirDarkenIntensity;

        [DBFieldName("LightID")]
        public ushort? LightID;

        [DBFieldName("ParticleScale")]
        public float? ParticleScale;

        [DBFieldName("ParticleMovement")]
        public byte? ParticleMovement;

        [DBFieldName("ParticleTexSlots")]
        public byte? ParticleTexSlots;

        [DBFieldName("MaterialID")]
        public byte? MaterialID;

        [DBFieldName("MinimapStaticCol")]
        public int? MinimapStaticCol;

        [DBFieldName("FrameCountTexture", 6)]
        public byte?[] FrameCountTexture;

        [DBFieldName("Color", 2)]
        public int?[] Color;

        [DBFieldName("Float", 18)]
        public float?[] Float;

        [DBFieldName("Int", 4)]
        public uint?[] Int;

        [DBFieldName("Coefficient", 4)]
        public float?[] Coefficient;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("liquid_type")]
    public sealed record LiquidTypeHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Texture", 6)]
        public string[] Texture;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("SoundBank")]
        public byte? SoundBank;

        [DBFieldName("SoundID")]
        public uint? SoundID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("MaxDarkenDepth")]
        public float? MaxDarkenDepth;

        [DBFieldName("FogDarkenIntensity")]
        public float? FogDarkenIntensity;

        [DBFieldName("AmbDarkenIntensity")]
        public float? AmbDarkenIntensity;

        [DBFieldName("DirDarkenIntensity")]
        public float? DirDarkenIntensity;

        [DBFieldName("LightID")]
        public ushort? LightID;

        [DBFieldName("ParticleScale")]
        public float? ParticleScale;

        [DBFieldName("ParticleMovement")]
        public byte? ParticleMovement;

        [DBFieldName("ParticleTexSlots")]
        public byte? ParticleTexSlots;

        [DBFieldName("MaterialID")]
        public byte? MaterialID;

        [DBFieldName("MinimapStaticCol")]
        public int? MinimapStaticCol;

        [DBFieldName("FrameCountTexture", 6)]
        public byte?[] FrameCountTexture;

        [DBFieldName("Color", 2)]
        public int?[] Color;

        [DBFieldName("Float", 18)]
        public float?[] Float;

        [DBFieldName("Int", 4)]
        public uint?[] Int;

        [DBFieldName("Coefficient", 4)]
        public float?[] Coefficient;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
