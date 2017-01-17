using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PowerType, HasIndexInData = false)]
    public class PowerTypeEntry
    {
        public string PowerTypeToken { get; set; }
        public string PowerCostToken { get; set; }
        public float RegenerationPeace { get; set; }
        public float RegenerationCombat { get; set; }
        public short MaxPower { get; set; }
        public ushort RegenerationDelay { get; set; }
        public ushort Flags { get; set; }
        public byte PowerTypeEnum { get; set; }
        public sbyte RegenerationMin { get; set; }
        public sbyte RegenerationCenter { get; set; }
        public sbyte RegenerationMax { get; set; }
        public byte UIModifier { get; set; }
    }
}
