using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.MapDifficulty, HasIndexInData = false)]
    public class MapDifficultyEntry
    {
        public string Message { get; set; }
        public ushort MapID { get; set; }
        public byte DifficultyID { get; set; }
        public byte RaidDurationType { get; set; }
        public byte MaxPlayers { get; set; }
        public byte LockID { get; set; }
        public byte ItemBonusTreeModID { get; set; }
        public uint Context { get; set; }
    }
}