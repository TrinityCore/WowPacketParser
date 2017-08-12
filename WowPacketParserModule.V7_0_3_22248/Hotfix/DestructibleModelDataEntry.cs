using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.DestructibleModelData, HasIndexInData = false)]
    public class DestructibleModelDataEntry
    {
        public ushort StateDamagedDisplayID { get; set; }
        public ushort StateDestroyedDisplayID { get; set; }
        public ushort StateRebuildingDisplayID { get; set; }
        public ushort StateSmokeDisplayID { get; set; }
        public ushort HealEffectSpeed { get; set; }
        public byte StateDamagedImpactEffectDoodadSet { get; set; }
        public byte StateDamagedAmbientDoodadSet { get; set; }
        public byte StateDamagedNameSet { get; set; }
        public byte StateDestroyedDestructionDoodadSet { get; set; }
        public byte StateDestroyedImpactEffectDoodadSet { get; set; }
        public byte StateDestroyedAmbientDoodadSet { get; set; }
        public byte StateDestroyedNameSet { get; set; }
        public byte StateRebuildingDestructionDoodadSet { get; set; }
        public byte StateRebuildingImpactEffectDoodadSet { get; set; }
        public byte StateRebuildingAmbientDoodadSet { get; set; }
        public byte StateRebuildingNameSet { get; set; }
        public byte StateSmokeInitDoodadSet { get; set; }
        public byte StateSmokeAmbientDoodadSet { get; set; }
        public byte StateSmokeNameSet { get; set; }
        public byte EjectDirection { get; set; }
        public byte DoNotHighlight { get; set; }
        public byte HealEffect { get; set; }
    }
}