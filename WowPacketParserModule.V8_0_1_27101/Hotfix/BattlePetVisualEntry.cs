using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetVisual, HasIndexInData = false)]
    public class BattlePetVisualEntry
    {
        public string SceneScriptFunction { get; set; }
        public uint SpellVisualID { get; set; }
        public ushort CastMilliSeconds { get; set; }
        public ushort ImpactMilliSeconds { get; set; }
        public sbyte RangeTypeEnum { get; set; }
        public byte Flags { get; set; }
        public ushort SceneScriptPackageID { get; set; }
    }
}
