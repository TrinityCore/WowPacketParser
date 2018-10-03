using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LfgRoleRequirement, HasIndexInData = false)]
    public class LfgRoleRequirementEntry
    {
        public sbyte RoleType { get; set; }
        public uint PlayerConditionID { get; set; }
        public ushort LfgDungeonsID { get; set; }
    }
}
