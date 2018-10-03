using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MapChallengeMode)]
    public class MapChallengeModeEntry
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public ushort MapID { get; set; }
        public byte Flags { get; set; }
        [HotfixArray(3)]
        public short[] CriteriaCount { get; set; }
    }
}
