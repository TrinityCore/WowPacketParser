using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundFilter, HasIndexInData = false)]
    public class SoundFilterEntry
    {
        public string Name { get; set; }
    }
}
