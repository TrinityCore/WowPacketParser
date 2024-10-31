using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual")]
    public sealed record SpellVisualHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MissileCastOffsetX")]
        public float? MissileCastOffsetX;

        [DBFieldName("MissileCastOffsetY")]
        public float? MissileCastOffsetY;

        [DBFieldName("MissileCastOffsetZ")]
        public float? MissileCastOffsetZ;

        [DBFieldName("MissileImpactOffsetX")]
        public float? MissileImpactOffsetX;

        [DBFieldName("MissileImpactOffsetY")]
        public float? MissileImpactOffsetY;

        [DBFieldName("MissileImpactOffsetZ")]
        public float? MissileImpactOffsetZ;

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
