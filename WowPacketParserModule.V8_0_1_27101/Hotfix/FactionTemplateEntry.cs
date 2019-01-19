using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.FactionTemplate, HasIndexInData = false)]
    public class FactionTemplateEntry
    {
        public ushort Faction { get; set; }
        public ushort Flags { get; set; }
        public byte FactionGroup { get; set; }
        public byte FriendGroup { get; set; }
        public byte EnemyGroup { get; set; }
        [HotfixArray(4)]
        public ushort[] Enemies { get; set; }
        [HotfixArray(4)]
        public ushort[] Friend { get; set; }
    }
}
