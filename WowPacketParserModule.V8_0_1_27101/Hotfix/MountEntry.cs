using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Mount)]
    public class MountEntry
    {
        public string Name { get; set; }
        public string SourceText { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        public ushort MountTypeID { get; set; }
        public ushort Flags { get; set; }
        public sbyte SourceTypeEnum { get; set; }
        public int SourceSpellID { get; set; }
        public uint PlayerConditionID { get; set; }
        public float MountFlyRideHeight { get; set; }
        public int UiModelSceneID { get; set; }
    }
}
