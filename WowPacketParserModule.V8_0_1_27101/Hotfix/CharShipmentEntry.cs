using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharShipment, HasIndexInData = false)]
    public class CharShipmentEntry
    {
        public ushort ContainerID { get; set; }
        public int DummyItemID { get; set; }
        public uint TreasureID { get; set; }
        public int SpellID { get; set; }
        public int OnCompleteSpellID { get; set; }
        public uint Duration { get; set; }
        public byte MaxShipments { get; set; }
        public ushort GarrFollowerID { get; set; }
        public byte Flags { get; set; }
    }
}
