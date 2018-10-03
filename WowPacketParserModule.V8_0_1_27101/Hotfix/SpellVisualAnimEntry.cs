using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellVisualAnim, HasIndexInData = false)]
    public class SpellVisualAnimEntry
    {
        public int InitialAnimID { get; set; }
        public int LoopAnimID { get; set; }
        public ushort AnimKitID { get; set; }
    }
}
