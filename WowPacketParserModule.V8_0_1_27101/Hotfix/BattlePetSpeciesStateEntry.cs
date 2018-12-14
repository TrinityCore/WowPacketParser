using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetSpeciesState, HasIndexInData = false)]
    public class BattlePetSpeciesStateEntry
    {
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public ushort BattlePetStateID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, true)]
        public byte BattlePetStateId { get; set; }
        public int Value { get; set; }
        public ushort BattlePetSpeciesID { get; set; }
    }
}
