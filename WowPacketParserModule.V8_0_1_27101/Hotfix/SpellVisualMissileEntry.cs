using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualMissile)]
    public class SpellVisualMissileEntry
    {
        [HotfixArray(3)]
        public float[] CastOffset { get; set; }
        [HotfixArray(3)]
        public float[] ImpactOffset { get; set; }
        public int ID { get; set; }
        public ushort SpellVisualEffectNameID { get; set; }
        public uint SoundEntriesID { get; set; }
        public sbyte Attachment { get; set; }
        public sbyte DestinationAttachment { get; set; }
        public ushort CastPositionerID { get; set; }
        public ushort ImpactPositionerID { get; set; }
        public int FollowGroundHeight { get; set; }
        public uint FollowGroundDropSpeed { get; set; }
        public ushort FollowGroundApproach { get; set; }
        public uint Flags { get; set; }
        public ushort SpellMissileMotionID { get; set; }
        public uint AnimKitID { get; set; }
        public ushort SpellVisualMissileSetID { get; set; }
    }
}
