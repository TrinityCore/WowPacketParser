using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrFollower)]
    public class GarrFollowerEntry
    {
        public string HordeSourceText { get; set; }
        public string AllianceSourceText { get; set; }
        public string TitleName { get; set; }
        public int ID { get; set; }
        public byte GarrTypeID { get; set; }
        public byte GarrFollowerTypeID { get; set; }
        public int HordeCreatureID { get; set; }
        public int AllianceCreatureID { get; set; }
        public byte HordeGarrFollRaceID { get; set; }
        public byte AllianceGarrFollRaceID { get; set; }
        public byte HordeGarrClassSpecID { get; set; }
        public byte AllianceGarrClassSpecID { get; set; }
        public byte Quality { get; set; }
        public byte FollowerLevel { get; set; }
        public ushort ItemLevelWeapon { get; set; }
        public ushort ItemLevelArmor { get; set; }
        public sbyte HordeSourceTypeEnum { get; set; }
        public sbyte AllianceSourceTypeEnum { get; set; }
        public int HordeIconFileDataID { get; set; }
        public int AllianceIconFileDataID { get; set; }
        public ushort HordeGarrFollItemSetID { get; set; }
        public ushort AllianceGarrFollItemSetID { get; set; }
        public ushort HordeUITextureKitID { get; set; }
        public ushort AllianceUITextureKitID { get; set; }
        public byte Vitality { get; set; }
        public byte HordeFlavorGarrStringID { get; set; }
        public byte AllianceFlavorGarrStringID { get; set; }
        public uint HordeSlottingBroadcastTextID { get; set; }
        public uint AllySlottingBroadcastTextID { get; set; }
        public byte ChrClassID { get; set; }
        public byte Flags { get; set; }
        public byte Gender { get; set; }
    }
}
