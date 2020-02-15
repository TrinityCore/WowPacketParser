using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_32861
{
    public class QuestLog : IQuestLog
    {
        public int QuestID { get; set; }
        public uint StateFlags { get; set; }
        public uint EndTime { get; set; }
        public uint AcceptTime { get; set; }
        public uint Field_10 { get; set; }
        public short[] ObjectiveProgress { get; } = new short[24];
    }
}

