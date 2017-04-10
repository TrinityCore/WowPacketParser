using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CinematicSequences, ClientVersionBuild.V7_0_3_22248, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class CinematicSequencesEntry
    {
        public ushort SoundID { get; set; }
        [HotfixArray(8)]
        public ushort[] Camera { get; set; }
    }
}