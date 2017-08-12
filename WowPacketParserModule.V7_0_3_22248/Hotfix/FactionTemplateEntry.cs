using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.FactionTemplate, HasIndexInData = false)]
    public class FactionTemplateEntry
    {
        public ushort Faction { get; set; }
        public ushort Flags { get; set; }
        [HotfixArray(4)]
        public ushort[] Enemies { get; set; }
        [HotfixArray(4)]
        public ushort[] Friends { get; set; }
        public byte Mask { get; set; }
        public byte FriendMask { get; set; }
        public byte EnemyMask { get; set; }
    }
}