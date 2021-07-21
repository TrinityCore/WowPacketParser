using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.MountCapability, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class MountCapabilityEntry
    {
        public byte Flags { get; set; }
        public ushort ReqRidingSkill { get; set; }
        public ushort ReqAreaID { get; set; }
        public uint ReqSpellAuraID { get; set; }
        public int ReqSpellKnownID { get; set; }
        public int ModSpellAuraID { get; set; }
        public short ReqMapID { get; set; }
        public int PlayerConditionID { get; set; }
    }
}
