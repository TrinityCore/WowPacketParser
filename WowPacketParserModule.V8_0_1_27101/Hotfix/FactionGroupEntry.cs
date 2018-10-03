using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.FactionGroup)]
    public class FactionGroupEntry
    {
        public string InternalName { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public byte MaskID { get; set; }
        public int HonorCurrencyTextureFileID { get; set; }
        public int ConquestCurrencyTextureFileID { get; set; }
    }
}
