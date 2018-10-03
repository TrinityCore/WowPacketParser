using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetAbilityTurn)]
    public class BattlePetAbilityTurnEntry
    {
        public int ID { get; set; }
        public ushort BattlePetAbilityID { get; set; }
        public byte OrderIndex { get; set; }
        public byte TurnTypeEnum { get; set; }
        public sbyte EventTypeEnum { get; set; }
        public ushort BattlePetVisualID { get; set; }
    }
}
