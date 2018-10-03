using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetAbilityEffect)]
    public class BattlePetAbilityEffectEntry
    {
        public int ID { get; set; }
        public ushort BattlePetAbilityTurnID { get; set; }
        public byte OrderIndex { get; set; }
        public ushort BattlePetEffectPropertiesID { get; set; }
        public ushort AuraBattlePetAbilityID { get; set; }
        public ushort BattlePetVisualID { get; set; }
        [HotfixArray(6)]
        public short[] Param { get; set; }
    }
}
