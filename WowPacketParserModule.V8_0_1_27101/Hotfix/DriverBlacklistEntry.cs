using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DriverBlacklist, HasIndexInData = false)]
    public class DriverBlacklistEntry
    {
        public ushort VendorID { get; set; }
        public byte DeviceID { get; set; }
        public uint DriverVersionHi { get; set; }
        public uint DriverVersionLow { get; set; }
        public byte OsVersion { get; set; }
        public byte OsBits { get; set; }
        public byte Flags { get; set; }
    }
}
