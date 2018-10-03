using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiTextureKit, HasIndexInData = false)]
    public class UiTextureKitEntry
    {
        public string KitPrefix { get; set; }
    }
}
