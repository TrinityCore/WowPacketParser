using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
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
        public ushort[] EmoteDelay { get; set; }
        public uint SoundID { get; set; }
        public uint UnkEmoteID { get; set; }
        public uint UnkType { get; set; }
    }
}
