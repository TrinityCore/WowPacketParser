using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WbCertWhitelist, HasIndexInData = false)]
    public class WbCertWhitelistEntry
    {
        public string Domain { get; set; }
        public byte GrantAccess { get; set; }
        public byte RevokeAccess { get; set; }
        public byte WowEditInternal { get; set; }
    }
}
