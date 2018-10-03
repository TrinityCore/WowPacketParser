using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaFarClipOverride, HasIndexInData = false)]
    public class AreaFarClipOverrideEntry
    {
        public int AreaID { get; set; }
        public float MinFarClip { get; set; }
        public float MinHorizonStart { get; set; }
        public int Flags { get; set; }
    }
}
