using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisual, HasIndexInData = false)]
    public class SpellVisualEntry
    {
        [HotfixArray(3)]
        public float[] MissileCastOffset { get; set; }
        [HotfixArray(3)]
        public float[] MissileImpactOffset { get; set; }
        public uint AnimEventSoundID { get; set; }
        public int Flags { get; set; }
        public sbyte MissileAttachment { get; set; }
        public sbyte MissileDestinationAttachment { get; set; }
        public uint MissileCastPositionerID { get; set; }
        public uint MissileImpactPositionerID { get; set; }
        public int MissileTargetingKit { get; set; }
        public uint HostileSpellVisualID { get; set; }
        public uint CasterSpellVisualID { get; set; }
        public ushort SpellVisualMissileSetID { get; set; }
        public ushort DamageNumberDelay { get; set; }
        public uint LowViolenceSpellVisualID { get; set; }
        public uint RaidSpellVisualMissileSetID { get; set; }
    }
}
