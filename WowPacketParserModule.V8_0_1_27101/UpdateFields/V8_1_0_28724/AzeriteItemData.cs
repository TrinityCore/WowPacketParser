using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_0_28724
{
    public class AzeriteItemData : IAzeriteItemData
    {
        public ulong Xp { get; set; }
        public uint Level { get; set; }
        public uint AuraLevel { get; set; }
        public uint KnowledgeLevel { get; set; }
        public int DEBUGknowledgeWeek { get; set; }
    }
}

