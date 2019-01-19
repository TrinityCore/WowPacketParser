using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DestructibleModelData, HasIndexInData = false)]
    public class DestructibleModelDataEntry
    {
        public sbyte State0ImpactEffectDoodadSet { get; set; }
        public byte State0AmbientDoodadSet { get; set; }
        public ushort State1Wmo { get; set; }
        public sbyte State1DestructionDoodadSet { get; set; }
        public sbyte State1ImpactEffectDoodadSet { get; set; }
        public byte State1AmbientDoodadSet { get; set; }
        public ushort State2Wmo { get; set; }
        public sbyte State2DestructionDoodadSet { get; set; }
        public sbyte State2ImpactEffectDoodadSet { get; set; }
        public byte State2AmbientDoodadSet { get; set; }
        public ushort State3Wmo { get; set; }
        public byte State3InitDoodadSet { get; set; }
        public byte State3AmbientDoodadSet { get; set; }
        public byte EjectDirection { get; set; }
        public byte DoNotHighlight { get; set; }
        public ushort State0Wmo { get; set; }
        public byte HealEffect { get; set; }
        public ushort HealEffectSpeed { get; set; }
        public sbyte State0NameSet { get; set; }
        public sbyte State1NameSet { get; set; }
        public sbyte State2NameSet { get; set; }
        public sbyte State3NameSet { get; set; }
    }
}
