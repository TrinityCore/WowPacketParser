using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WbAccessControlList, HasIndexInData = false)]
    public class WbAccessControlListEntry
    {
        public string URL { get; set; }
        public ushort GrantFlags { get; set; }
        public byte RevokeFlags { get; set; }
        public byte WowEditInternal { get; set; }
        public byte RegionID { get; set; }
    }
}
