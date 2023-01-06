using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Keychain, HasIndexInData = false)]
    public class KeychainEntry
    {
        [HotfixArray(32)]
        public byte[] Key { get; set; }
    }
}
