using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureDisplayInfoTrn, HasIndexInData = false)]
    public class CreatureDisplayInfoTrnEntry
    {
        public int DstCreatureDisplayInfoID { get; set; }
        public uint DissolveEffectID { get; set; }
        public uint StartVisualKitID { get; set; }
        public float MaxTime { get; set; }
        public int FinishVisualKitID { get; set; }
        public int SrcCreatureDisplayInfoID { get; set; }
    }
}
