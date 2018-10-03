using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ResearchBranch, HasIndexInData = false)]
    public class ResearchBranchEntry
    {
        public string Name { get; set; }
        public byte ResearchFieldID { get; set; }
        public ushort CurrencyID { get; set; }
        public int TextureFileID { get; set; }
        public int BigTextureFileID { get; set; }
        public int ItemID { get; set; }
    }
}
