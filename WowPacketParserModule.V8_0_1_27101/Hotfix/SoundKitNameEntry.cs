using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundKitName, HasIndexInData = false)]
    public class SoundKitNameEntry
    {
        public string Name { get; set; }
    }
}
