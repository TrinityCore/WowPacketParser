using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClassRaceSex, HasIndexInData = false)]
    public class ChrClassRaceSexEntry
    {
        public sbyte ClassID { get; set; }
        public sbyte RaceID { get; set; }
        public sbyte Sex { get; set; }
        public int Flags { get; set; }
        public uint SoundID { get; set; }
        public uint VoiceSoundFilterID { get; set; }
    }
}
