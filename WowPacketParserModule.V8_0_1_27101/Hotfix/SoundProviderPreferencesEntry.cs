using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundProviderPreferences, HasIndexInData = false)]
    public class SoundProviderPreferencesEntry
    {
        public string Description { get; set; }
        public sbyte EAXEnvironmentSelection { get; set; }
        public float EAXDecayTime { get; set; }
        public float EAX2EnvironmentSize { get; set; }
        public float EAX2EnvironmentDiffusion { get; set; }
        public short EAX2Room { get; set; }
        public short EAX2RoomHF { get; set; }
        public float EAX2DecayHFRatio { get; set; }
        public short EAX2Reflections { get; set; }
        public float EAX2ReflectionsDelay { get; set; }
        public short EAX2Reverb { get; set; }
        public float EAX2ReverbDelay { get; set; }
        public float EAX2RoomRolloff { get; set; }
        public float EAX2AirAbsorption { get; set; }
        public sbyte EAX3RoomLF { get; set; }
        public float EAX3DecayLFRatio { get; set; }
        public float EAX3EchoTime { get; set; }
        public float EAX3EchoDepth { get; set; }
        public float EAX3ModulationTime { get; set; }
        public float EAX3ModulationDepth { get; set; }
        public float EAX3HFReference { get; set; }
        public float EAX3LFReference { get; set; }
        public ushort Flags { get; set; }
    }
}
