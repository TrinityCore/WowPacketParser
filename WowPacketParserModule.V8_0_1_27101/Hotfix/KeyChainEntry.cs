using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Keychain, HasIndexInData = false)]
    public class KeyChainEntry
    {
        [HotfixArray(32)]
        public byte[] Key { get; set; }
    }
}
