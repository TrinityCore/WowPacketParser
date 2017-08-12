using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.BroadcastText, HasIndexInData = false)]
    public class BroadcastTextEntry : IBroadcastTextEntry
    {
        public string MaleText { get; set; }
        public string FemaleText { get; set; }
        [HotfixArray(3)]
        public ushort[] EmoteID { get; set; }
        [HotfixArray(3)]
        public ushort[] EmoteDelay { get; set; }
        public ushort UnkEmoteID { get; set; }
        public byte Language { get; set; }
        public byte Type { get; set; }
        [HotfixArray(2)]
        public uint[] SoundID { get; set; }
        public uint PlayerConditionID { get; set; }
    }
}