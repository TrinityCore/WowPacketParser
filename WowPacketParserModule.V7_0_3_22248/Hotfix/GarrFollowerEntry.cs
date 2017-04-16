using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GarrFollower)]
    public class GarrFollowerEntry
    {
        public uint HordeCreatureID { get; set; }
        public uint AllianceCreatureID { get; set; }
        public string HordeSourceText { get; set; }
        public string AllianceSourceText { get; set; }
        public uint HordePortraitIconID { get; set; }
        public uint AlliancePortraitIconID { get; set; }
        public uint HordeAddedBroadcastTextID { get; set; }
        public uint AllianceAddedBroadcastTextID { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public string Name { get; set; }
        public ushort HordeGarrFollItemSetID { get; set; }
        public ushort AllianceGarrFollItemSetID { get; set; }
        public ushort ItemLevelWeapon { get; set; }
        public ushort ItemLevelArmor { get; set; }
        public ushort HordeListPortraitTextureKitID { get; set; }
        public ushort AllianceListPortraitTextureKitID { get; set; }
        public byte FollowerTypeID { get; set; }
        public byte HordeUiAnimRaceInfoID { get; set; }
        public byte AllianceUiAnimRaceInfoID { get; set; }
        public byte Quality { get; set; }
        public byte HordeGarrClassSpecID { get; set; }
        public byte AllianceGarrClassSpecID { get; set; }
        public byte Level { get; set; }
        public byte Unknown1 { get; set; }
        public byte Flags { get; set; }
        public sbyte Unknown2 { get; set; }
        public sbyte Unknown3 { get; set; }
        public byte GarrTypeID { get; set; }
        public byte MaxDurability { get; set; }
        public byte Class { get; set; }
        public byte HordeFlavorTextGarrStringID { get; set; }
        public byte AllianceFlavorTextGarrStringID { get; set; }
        public uint ID { get; set; }
    }
}