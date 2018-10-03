using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CommunityIcon)]
    public class CommunityIconEntry
    {
        public int ID { get; set; }
        public int IconFileDataID { get; set; }
        public int OrderIndex { get; set; }
    }
}
