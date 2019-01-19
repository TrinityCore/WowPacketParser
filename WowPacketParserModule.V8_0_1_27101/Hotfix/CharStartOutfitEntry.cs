using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharStartOutfit, HasIndexInData = false)]
    public class CharStartOutfitEntry
    {
        public byte ClassID { get; set; }
        public byte SexID { get; set; }
        public byte OutfitID { get; set; }
        public uint PetDisplayID { get; set; }
        public byte PetFamilyID { get; set; }
        [HotfixArray(24)]
        public int[] ItemID { get; set; }
        public byte RaceID { get; set; }
    }
}
