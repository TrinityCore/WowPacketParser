using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetState, HasIndexInData = false)]
    public class BattlePetStateEntry
    {
        public string LuaName { get; set; }
        public ushort Flags { get; set; }
        public ushort BattlePetVisualID { get; set; }
    }
}
