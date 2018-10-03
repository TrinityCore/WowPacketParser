using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrFollowerLevelXP, HasIndexInData = false)]
    public class GarrFollowerLevelXPEntry
    {
        public byte GarrFollowerTypeID { get; set; }
        public byte FollowerLevel { get; set; }
        public ushort XpToNextLevel { get; set; }
        public ushort ShipmentXP { get; set; }
    }
}
