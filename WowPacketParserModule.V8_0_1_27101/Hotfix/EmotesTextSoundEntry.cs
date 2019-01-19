using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.EmotesTextSound, HasIndexInData = false)]
    public class EmotesTextSoundEntry
    {
        public byte RaceID { get; set; }
        public byte ClassID { get; set; }
        public byte SexID { get; set; }
        public uint SoundID { get; set; }
        public ushort EmotesTextID { get; set; }
    }
}
