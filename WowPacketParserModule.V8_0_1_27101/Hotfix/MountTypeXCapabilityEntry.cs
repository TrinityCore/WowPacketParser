using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MountTypeXCapability, HasIndexInData = false)]
    public class MountTypeXCapabilityEntry
    {
        public ushort MountTypeID { get; set; }
        public ushort MountCapabilityID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
