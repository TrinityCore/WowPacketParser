using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_4_0_17359.Hotfix
{
    [HotfixStructure(DB2Hash.KeyChain)]
    public class KeyChainEntry
    {
        public int KeychainID { get; set; }
        [HotfixArray(32)]
        public byte[] Key { get; set; }
    }
}
