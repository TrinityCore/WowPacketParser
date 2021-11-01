using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.JournalInstance, HasIndexInData = false)]
    public class JournalInstanceEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ushort MapID { get; set; }
        public int BackgroundFileDataID { get; set; }
        public int ButtonFileDataID { get; set; }
        public int ButtonSmallFileDataID { get; set; }
        public int LoreFileDataID { get; set; }
        public byte OrderIndex { get; set; }
        public byte Flags { get; set; }
        public ushort AreaID { get; set; }
    }
}
