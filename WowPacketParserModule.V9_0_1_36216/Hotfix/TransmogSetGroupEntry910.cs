using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogSetGroup, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class TransmogSetGroupEntry
    {
        public string Name { get; set; }
    }
}
