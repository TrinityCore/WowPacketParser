using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureMovementInfo, HasIndexInData = false)]
    public class CreatureMovementInfoEntry
    {
        public float SmoothFacingChaseRate { get; set; }
    }
}
