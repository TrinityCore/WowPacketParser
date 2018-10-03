using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ModelAnimCloakDampening, HasIndexInData = false)]
    public class ModelAnimCloakDampeningEntry
    {
        public uint AnimationDataID { get; set; }
        public uint CloakDampeningID { get; set; }
        public uint FileDataID { get; set; }
    }
}
