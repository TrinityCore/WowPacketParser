using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.Mount)]
    public class MountEntry
    {
        public int ID { get; set; }
        public int MountTypeId { get; set; }
        public uint DisplayId { get; set; }
        public int Flags { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceDescription { get; set; }
        public float CameraPivotMultiplier { get; set; }
        public int Source { get; set; }
        public uint SpellId { get; set; }
        public ushort PlayerConditionId { get; set; }
    }
}