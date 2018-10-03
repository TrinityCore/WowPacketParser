using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DeviceDefaultSettings, HasIndexInData = false)]
    public class DeviceDefaultSettingsEntry
    {
        public ushort VendorID { get; set; }
        public ushort DeviceID { get; set; }
        public sbyte DefaultSetting { get; set; }
    }
}
