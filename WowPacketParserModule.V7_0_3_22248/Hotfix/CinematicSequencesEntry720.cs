using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.CinematicSequences, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class CinematicSequencesEntry
    {
        public uint SoundID { get; set; }
        [HotfixArray(8)]
        public ushort[] Camera { get; set; }
    }
}