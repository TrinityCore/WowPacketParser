using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.VehicleUIIndSeat, HasIndexInData = false)]
    public class VehicleUIIndSeatEntry
    {
        public byte VirtualSeatIndex { get; set; }
        public float XPos { get; set; }
        public float YPos { get; set; }
        public ushort VehicleUIIndicatorID { get; set; }
    }
}
