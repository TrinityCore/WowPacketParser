using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemExtendedCost, HasIndexInData = false)]
    public class ItemExtendedCostEntry
    {
        public ushort RequiredArenaRating { get; set; }
        public sbyte ArenaBracket { get; set; }
        public byte Flags { get; set; }
        public byte MinFactionID { get; set; }
        public byte MinReputation { get; set; }
        public byte RequiredAchievement { get; set; }
        [HotfixArray(5)]
        public int[] ItemID { get; set; }
        [HotfixArray(5)]
        public ushort[] ItemCount { get; set; }
        [HotfixArray(5)]
        public ushort[] CurrencyID { get; set; }
        [HotfixArray(5)]
        public uint[] CurrencyCount { get; set; }
    }
}
