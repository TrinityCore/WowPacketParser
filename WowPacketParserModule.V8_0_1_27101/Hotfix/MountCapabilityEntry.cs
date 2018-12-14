using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MountCapability)]
    public class MountCapabilityEntry
    {
        public int ID { get; set; }
        public byte Flags { get; set; }
        public ushort ReqRidingSkill { get; set; }
        public ushort ReqAreaID { get; set; }
        public uint ReqSpellAuraID { get; set; }
        public uint ReqSpellKnownID { get; set; }
        public int ModSpellAuraID { get; set; }
        public ushort ReqMapID { get; set; }
    }
}
