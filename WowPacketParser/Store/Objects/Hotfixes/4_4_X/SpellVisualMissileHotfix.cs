using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual_missile")]
    public sealed record SpellVisualMissileHotfix440: IDataModel
    {
        [DBFieldName("CastOffset", 3)]
        public float?[] CastOffset;

        [DBFieldName("ImpactOffset", 3)]
        public float?[] ImpactOffset;

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
}
