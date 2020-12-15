using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpeciesState, HasIndexInData = false)]
    public class BattlePetSpeciesStateEntry
    {
        public ushort BattlePetStateID { get; set; }
        public int Value { get; set; }
        public ushort BattlePetSpeciesID { get; set; }
    }
}
