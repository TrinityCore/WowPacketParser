using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual")]
    public sealed record SpellVisualHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MissileCastOffset", 3)]
        public float?[] MissileCastOffset;

        [DBFieldName("MissileImpactOffset", 3)]
        public float?[] MissileImpactOffset;

        [DBFieldName("AnimEventSoundID")]
        public uint? AnimEventSoundID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("MissileAttachment")]
        public sbyte? MissileAttachment;

        [DBFieldName("MissileDestinationAttachment")]
        public sbyte? MissileDestinationAttachment;

        [DBFieldName("MissileCastPositionerID")]
        public uint? MissileCastPositionerID;

        [DBFieldName("MissileImpactPositionerID")]
        public uint? MissileImpactPositionerID;

        [DBFieldName("MissileTargetingKit")]
        public int? MissileTargetingKit;

        [DBFieldName("HostileSpellVisualID")]
        public uint? HostileSpellVisualID;

        [DBFieldName("CasterSpellVisualID")]
        public uint? CasterSpellVisualID;

        [DBFieldName("SpellVisualMissileSetID")]
        public ushort? SpellVisualMissileSetID;

        [DBFieldName("DamageNumberDelay")]
        public ushort? DamageNumberDelay;

        [DBFieldName("LowViolenceSpellVisualID")]
        public uint? LowViolenceSpellVisualID;

        [DBFieldName("RaidSpellVisualMissileSetID")]
        public uint? RaidSpellVisualMissileSetID;

        [DBFieldName("ReducedUnexpectedCameraMovementSpellVisualID")]
        public int? ReducedUnexpectedCameraMovementSpellVisualID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_visual")]
    public sealed record SpellVisualHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MissileCastOffset", 3)]
        public float?[] MissileCastOffset;

        [DBFieldName("MissileImpactOffset", 3)]
        public float?[] MissileImpactOffset;

        [DBFieldName("AnimEventSoundID")]
        public uint? AnimEventSoundID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("MissileAttachment")]
        public sbyte? MissileAttachment;

        [DBFieldName("MissileDestinationAttachment")]
        public sbyte? MissileDestinationAttachment;

        [DBFieldName("MissileCastPositionerID")]
        public uint? MissileCastPositionerID;

        [DBFieldName("MissileImpactPositionerID")]
        public uint? MissileImpactPositionerID;

        [DBFieldName("MissileTargetingKit")]
        public int? MissileTargetingKit;

        [DBFieldName("HostileSpellVisualID")]
        public uint? HostileSpellVisualID;

        [DBFieldName("CasterSpellVisualID")]
        public uint? CasterSpellVisualID;

        [DBFieldName("SpellVisualMissileSetID")]
        public ushort? SpellVisualMissileSetID;

        [DBFieldName("DamageNumberDelay")]
        public ushort? DamageNumberDelay;

        [DBFieldName("LowViolenceSpellVisualID")]
        public uint? LowViolenceSpellVisualID;

        [DBFieldName("RaidSpellVisualMissileSetID")]
        public uint? RaidSpellVisualMissileSetID;

        [DBFieldName("ReducedUnexpectedCameraMovementSpellVisualID")]
        public int? ReducedUnexpectedCameraMovementSpellVisualID;

        [DBFieldName("AreaModel")]
        public ushort? AreaModel;

        [DBFieldName("HasMissile")]
        public sbyte? HasMissile;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
