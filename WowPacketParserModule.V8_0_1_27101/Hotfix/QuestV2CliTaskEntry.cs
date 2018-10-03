using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestV2CliTask)]
    public class QuestV2CliTaskEntry
    {
        public ulong FiltRaces { get; set; }
        public string QuestTitle { get; set; }
        public string BulletText { get; set; }
        public uint ID { get; set; }
        public ushort UniqueBitFlag { get; set; }
        public uint ConditionID { get; set; }
        public uint FiltActiveQuest { get; set; }
        public short FiltClasses { get; set; }
        public uint FiltCompletedQuestLogic { get; set; }
        public uint FiltMaxFactionID { get; set; }
        public uint FiltMaxFactionValue { get; set; }
        public uint FiltMaxLevel { get; set; }
        public uint FiltMinFactionID { get; set; }
        public uint FiltMinFactionValue { get; set; }
        public uint FiltMinLevel { get; set; }
        public uint FiltMinSkillID { get; set; }
        public uint FiltMinSkillValue { get; set; }
        public uint FiltNonActiveQuest { get; set; }
        public uint BreadCrumbID { get; set; }
        public int StartItem { get; set; }
        public short WorldStateExpressionID { get; set; }
        public uint QuestInfoID { get; set; }
        public int ContentTuningID { get; set; }
        [HotfixArray(3)]
        public uint[] FiltCompletedQuest { get; set; }
    }
}
