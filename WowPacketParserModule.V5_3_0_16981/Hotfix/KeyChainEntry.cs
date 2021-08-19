using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V5_3_0_16981.Hotfix
{
    [HotfixStructure(DB2Hash.KeyChain)]
    public class KeyChainEntry
    {
        public int KeychainID { get; set; }
        [HotfixArray(32)]
        public byte[] Key { get; set; }
    }
}
