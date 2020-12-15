using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetBreedState, HasIndexInData = false)]
    public class BattlePetBreedStateEntry
    {
        public int BattlePetStateID { get; set; }
        public ushort Value { get; set; }
        public byte BattlePetBreedID { get; set; }
    }
}
