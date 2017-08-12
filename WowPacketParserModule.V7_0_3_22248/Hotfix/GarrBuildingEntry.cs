using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GarrBuilding, HasIndexInData = false)]
    public class GarrBuildingEntry
    {
        public uint HordeGameObjectID { get; set; }
        public uint AllianceGameObjectID { get; set; }
        public string NameAlliance { get; set; }
        public string NameHorde { get; set; }
        public string Description { get; set; }
        public string Tooltip { get; set; }
        public uint IconFileDataID { get; set; }
        public ushort CostCurrencyID { get; set; }
        public ushort HordeTexPrefixKitID { get; set; }
        public ushort AllianceTexPrefixKitID { get; set; }
        public ushort AllianceActivationScenePackageID { get; set; }
        public ushort HordeActivationScenePackageID { get; set; }
        public ushort FollowerRequiredGarrAbilityID { get; set; }
        public ushort FollowerGarrAbilityEffectID { get; set; }
        public short CostMoney { get; set; }
        public byte Unknown { get; set; }
        public byte Type { get; set; }
        public byte Level { get; set; }
        public byte Flags { get; set; }
        public byte MaxShipments { get; set; }
        public byte GarrTypeID { get; set; }
        public uint BuildDuration { get; set; }
        public int CostCurrencyAmount { get; set; }
        public uint BonusAmount { get; set; }
    }
}