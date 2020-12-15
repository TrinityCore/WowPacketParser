using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.PowerType)]
    public class PowerTypeEntry
    {
        public string NameGlobalStringTag { get; set; }
        public string CostGlobalStringTag { get; set; }
        public uint ID { get; set; }
        public sbyte PowerTypeEnum { get; set; }
        public sbyte MinPower { get; set; }
        public short MaxBasePower { get; set; }
        public sbyte CenterPower { get; set; }
        public sbyte DefaultPower { get; set; }
        public sbyte DisplayModifier { get; set; }
        public short RegenInterruptTimeMS { get; set; }
        public float RegenPeace { get; set; }
        public float RegenCombat { get; set; }
        public short Flags { get; set; }
    }
}
