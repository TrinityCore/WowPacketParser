using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MapDifficulty, HasIndexInData = false)]
    public class MapDifficultyEntry
    {
        public string Message { get; set; }
        public uint ItemContextPickerID { get; set; }
        public int ContentTuningID { get; set; }
        public byte DifficultyID { get; set; }
        public byte LockID { get; set; }
        public byte ResetInterval { get; set; }
        public byte MaxPlayers { get; set; }
        public byte ItemContext { get; set; }
        public byte Flags { get; set; }
        public ushort MapID { get; set; }
    }
}
