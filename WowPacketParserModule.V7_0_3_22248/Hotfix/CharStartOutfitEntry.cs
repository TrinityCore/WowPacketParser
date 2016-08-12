using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CharStartOutfit, HasIndexInData = false)]
    public class CharStartOutfitEntry
    {
        [HotfixArray(24)]
        public int[] ItemID { get; set; }
        public uint PetDisplayID { get; set; }
        public byte RaceID { get; set; }
        public byte ClassID { get; set; }
        public byte GenderID { get; set; }
        public byte OutfitID { get; set; }
        public byte PetFamilyID { get; set; }
    }
}