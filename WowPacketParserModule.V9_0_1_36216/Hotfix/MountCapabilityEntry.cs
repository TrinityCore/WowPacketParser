using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.MountCapability, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class MountCapabilityEntry
    {
        public uint ID { get; set; }
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
