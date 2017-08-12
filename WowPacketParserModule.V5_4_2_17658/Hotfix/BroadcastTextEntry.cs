using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Hotfix
{
    [HotfixStructure(DB2Hash.BroadcastText)]
    public class BroadcastTextEntry : IBroadcastTextEntry
    {
        public int ID { get; set; }
        public int Language { get; set; }
        public string MaleText { get; set; }
        public string FemaleText { get; set; }
        [HotfixArray(3)]
        public int[] EmoteID { get; set; }
        [HotfixArray(3)]
        public uint[] EmoteDelay { get; set; }
        public uint SoundID { get; set; }
        public uint UnkEmoteID { get; set; }
        public uint UnkType { get; set; }
    }
}
