using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundEnvelope, HasIndexInData = false)]
    public class SoundEnvelopeEntry
    {
        public int SoundKitID { get; set; }
        public byte EnvelopeType { get; set; }
        public uint Flags { get; set; }
        public int CurveID { get; set; }
        public ushort DecayIndex { get; set; }
        public ushort SustainIndex { get; set; }
        public ushort ReleaseIndex { get; set; }
    }
}
