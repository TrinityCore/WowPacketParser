using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_0_28724.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpeciesState, ClientVersionBuild.V8_1_0_28724, HasIndexInData = false)]
    public class BattlePetSpeciesStateEntry
    {
        public ushort BattlePetStateID { get; set; }
        public int Value { get; set; }
        public ushort BattlePetSpeciesID { get; set; }
    }
}
