using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_32861
{
    public class AzeriteItemData : IAzeriteItemData
    {
        public ulong Xp { get; set; }
        public uint Level { get; set; }
        public uint AuraLevel { get; set; }
        public uint KnowledgeLevel { get; set; }
        public int DEBUGknowledgeWeek { get; set; }
        public DynamicUpdateField<IUnlockedAzeriteEssence> UnlockedEssences { get; } = new DynamicUpdateField<IUnlockedAzeriteEssence>();
        public DynamicUpdateField<uint> UnlockedEssenceMilestones { get; } = new DynamicUpdateField<uint>();
        public DynamicUpdateField<ISelectedAzeriteEssences> SelectedEssences { get; } = new DynamicUpdateField<ISelectedAzeriteEssences>();
    }
}

