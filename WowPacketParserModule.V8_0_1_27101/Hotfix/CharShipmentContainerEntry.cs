using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharShipmentContainer, HasIndexInData = false)]
    public class CharShipmentContainerEntry
    {
        public string Description { get; set; }
        public string PendingText { get; set; }
        public ushort UiTextureKitID { get; set; }
        public byte GarrTypeID { get; set; }
        public byte GarrBuildingType { get; set; }
        public byte BaseCapacity { get; set; }
        public ushort SmallDisplayInfoID { get; set; }
        public ushort MediumDisplayInfoID { get; set; }
        public ushort LargeDisplayInfoID { get; set; }
        public ushort WorkingDisplayInfoID { get; set; }
        public uint WorkingSpellVisualID { get; set; }
        public uint CompleteSpellVisualID { get; set; }
        public byte MediumThreshold { get; set; }
        public byte LargeThreshold { get; set; }
        public sbyte Faction { get; set; }
        public ushort CrossFactionID { get; set; }
    }
}
