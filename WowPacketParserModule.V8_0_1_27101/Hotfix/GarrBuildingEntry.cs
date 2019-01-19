using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrBuilding, HasIndexInData = false)]
    public class GarrBuildingEntry
    {
        public string HordeName { get; set; }
        public string AllianceName { get; set; }
        public string Description { get; set; }
        public string Tooltip { get; set; }
        public byte GarrTypeID { get; set; }
        public byte BuildingType { get; set; }
        public int HordeGameObjectID { get; set; }
        public int AllianceGameObjectID { get; set; }
        public byte GarrSiteID { get; set; }
        public byte UpgradeLevel { get; set; }
        public int BuildSeconds { get; set; }
        public ushort CurrencyTypeID { get; set; }
        public int CurrencyQty { get; set; }
        public ushort HordeUiTextureKitID { get; set; }
        public ushort AllianceUiTextureKitID { get; set; }
        public int IconFileDataID { get; set; }
        public ushort AllianceSceneScriptPackageID { get; set; }
        public ushort HordeSceneScriptPackageID { get; set; }
        public int MaxAssignments { get; set; }
        public byte ShipmentCapacity { get; set; }
        public ushort GarrAbilityID { get; set; }
        public ushort BonusGarrAbilityID { get; set; }
        public ushort GoldCost { get; set; }
        public byte Flags { get; set; }
    }
}
