using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.Mount, ClientVersionBuild.V7_2_0_23826)]
    public class MountEntry
    {
        public uint SpellId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceDescription { get; set; }
        public float CameraPivotMultiplier { get; set; }
        public ushort MountTypeId { get; set; }
        public ushort Flags { get; set; }
        public byte Source { get; set; }
        public uint ID { get; set; }
        public uint PlayerConditionId { get; set; }
        public int UiModelSceneID { get; set; }
    }
}