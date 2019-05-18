using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class AzeriteItemData : IAzeriteItemData
    {
        public ulong Xp { get; set; }
        public uint Level { get; set; }
        public uint AuraLevel { get; set; }
        public uint KnowledgeLevel { get; set; }
        public uint DEBUGknowledgeWeek { get; set; }
    }
}

