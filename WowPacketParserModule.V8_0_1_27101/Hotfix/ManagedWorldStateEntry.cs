using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ManagedWorldState)]
    public class ManagedWorldStateEntry
    {
        public int ID { get; set; }
        public int CurrentStageWorldStateID { get; set; }
        public int ProgressWorldStateID { get; set; }
        public uint UpTimeSecs { get; set; }
        public uint DownTimeSecs { get; set; }
        public int AccumulationStateTargetValue { get; set; }
        public int DepletionStateTargetValue { get; set; }
        public int AccumulationAmountPerMinute { get; set; }
        public int DepletionAmountPerMinute { get; set; }
        [HotfixArray(4)]
        public int[] OccurrencesWorldStateID { get; set; }
    }
}
