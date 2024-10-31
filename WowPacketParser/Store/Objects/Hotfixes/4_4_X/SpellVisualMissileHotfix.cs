using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual_missile")]
    public sealed record SpellVisualMissileHotfix440: IDataModel
    {
        [DBFieldName("CastOffsetX")]
        public float? CastOffsetX;

        [DBFieldName("CastOffsetY")]
        public float? CastOffsetY;

        [DBFieldName("CastOffsetZ")]
        public float? CastOffsetZ;

        [DBFieldName("ImpactOffsetX")]
        public float? ImpactOffsetX;

        [DBFieldName("ImpactOffsetY")]
        public float? ImpactOffsetY;

        [DBFieldName("ImpactOffsetZ")]
        public float? ImpactOffsetZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellVisualEffectNameID")]
        public ushort? SpellVisualEffectNameID;

        [DBFieldName("SoundEntriesID")]
        public uint? SoundEntriesID;

        [DBFieldName("Attachment")]
        public sbyte? Attachment;

        [DBFieldName("DestinationAttachment")]
        public sbyte? DestinationAttachment;

        [DBFieldName("CastPositionerID")]
        public ushort? CastPositionerID;

        [DBFieldName("ImpactPositionerID")]
        public ushort? ImpactPositionerID;

        [DBFieldName("FollowGroundHeight")]
        public int? FollowGroundHeight;

        [DBFieldName("FollowGroundDropSpeed")]
        public uint? FollowGroundDropSpeed;

        [DBFieldName("FollowGroundApproach")]
        public ushort? FollowGroundApproach;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("SpellMissileMotionID")]
        public ushort? SpellMissileMotionID;

        [DBFieldName("AnimKitID")]
        public uint? AnimKitID;

        [DBFieldName("SpellVisualMissileSetID")]
        public int? SpellVisualMissileSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("spell_visual_missile")]
    public sealed record SpellVisualMissileHotfix441: IDataModel
    {
        [DBFieldName("CastOffsetX")]
        public float? CastOffsetX;

        [DBFieldName("CastOffsetY")]
        public float? CastOffsetY;

        [DBFieldName("CastOffsetZ")]
        public float? CastOffsetZ;

        [DBFieldName("ImpactOffsetX")]
        public float? ImpactOffsetX;

        [DBFieldName("ImpactOffsetY")]
        public float? ImpactOffsetY;

        [DBFieldName("ImpactOffsetZ")]
        public float? ImpactOffsetZ;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellVisualEffectNameID")]
        public ushort? SpellVisualEffectNameID;

        [DBFieldName("SoundEntriesID")]
        public uint? SoundEntriesID;

        [DBFieldName("Attachment")]
        public sbyte? Attachment;

        [DBFieldName("DestinationAttachment")]
        public sbyte? DestinationAttachment;

        [DBFieldName("CastPositionerID")]
        public ushort? CastPositionerID;

        [DBFieldName("ImpactPositionerID")]
        public ushort? ImpactPositionerID;

        [DBFieldName("FollowGroundHeight")]
        public int? FollowGroundHeight;

        [DBFieldName("FollowGroundDropSpeed")]
        public uint? FollowGroundDropSpeed;

        [DBFieldName("FollowGroundApproach")]
        public ushort? FollowGroundApproach;

        [DBFieldName("Flags")]
        public uint? Flags;

        [DBFieldName("SpellMissileMotionID")]
        public ushort? SpellMissileMotionID;

        [DBFieldName("AnimKitID")]
        public uint? AnimKitID;

        [DBFieldName("Field115456400015")]
        public ushort? Field115456400015;

        [DBFieldName("SpellVisualMissileSetID")]
        public int? SpellVisualMissileSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
