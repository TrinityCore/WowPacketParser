using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundAmbience, HasIndexInData = false)]
    public class SoundAmbienceEntry
    {
        public byte Flags { get; set; }
        public uint SoundFilterID { get; set; }
        public uint FlavorSoundFilterID { get; set; }
        [HotfixArray(2)]
        public uint[] AmbienceID { get; set; }
        [HotfixArray(2)]
        public uint[] AmbienceStartID { get; set; }
        [HotfixArray(2)]
        public uint[] AmbienceStopID { get; set; }
    }
}
