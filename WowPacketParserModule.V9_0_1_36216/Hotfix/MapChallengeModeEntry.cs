using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.MapChallengeMode)]
    public class MapChallengeModeEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public ushort MapID { get; set; }
        public byte Flags { get; set; }
        public uint ExpansionLevel { get; set; }
        [HotfixArray(3)]
        public short[] CriteriaCount { get; set; }
    }
}
