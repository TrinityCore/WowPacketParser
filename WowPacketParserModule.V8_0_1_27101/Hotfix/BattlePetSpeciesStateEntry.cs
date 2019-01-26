using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpeciesState, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_1_0_28724, HasIndexInData = false)]
    public class BattlePetSpeciesStateEntry
    {
        public byte BattlePetStateID { get; set; }
        public int Value { get; set; }
        public ushort BattlePetSpeciesID { get; set; }
    }
}
