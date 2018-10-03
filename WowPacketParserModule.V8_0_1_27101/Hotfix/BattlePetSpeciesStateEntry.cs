using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpeciesState, HasIndexInData = false)]
    public class BattlePetSpeciesStateEntry
    {
        public byte BattlePetStateID { get; set; }
        public int Value { get; set; }
        public ushort BattlePetSpeciesID { get; set; }
    }
}
