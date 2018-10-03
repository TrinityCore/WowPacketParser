using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundKitFallback, HasIndexInData = false)]
    public class SoundKitFallbackEntry
    {
        public uint SoundKitID { get; set; }
        public uint FallbackSoundKitID { get; set; }
    }
}
