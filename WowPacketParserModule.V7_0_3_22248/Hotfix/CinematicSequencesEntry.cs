using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CinematicSequences, HasIndexInData = false)]
    public class CinematicSequencesEntry
    {
        public ushort SoundID { get; set; }
        [HotfixArray(8)]
        public ushort[] Camera { get; set; }
    }
}
