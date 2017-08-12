using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.KeyChain)]
    public class KeyChainEntry
    {
        public int KeychainID { get; set; }
        [HotfixArray(32)]
        public byte[] Key { get; set; }
    }
}
