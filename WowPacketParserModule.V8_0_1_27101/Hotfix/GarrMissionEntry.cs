using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrMission)]
    public class GarrMissionEntry
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        [HotfixArray(2)]
        public float[] MapPos { get; set; }
        [HotfixArray(2)]
        public float[] WorldPos { get; set; }
        public int ID { get; set; }
        public byte GarrTypeID { get; set; }
        public byte GarrMissionTypeID { get; set; }
        public byte GarrFollowerTypeID { get; set; }
        public byte MaxFollowers { get; set; }
        public uint MissionCost { get; set; }
        public ushort MissionCostCurrencyTypesID { get; set; }
        public byte OfferedGarrMissionTextureID { get; set; }
        public ushort UiTextureKitID { get; set; }
        public uint EnvGarrMechanicID { get; set; }
        public byte EnvGarrMechanicTypeID { get; set; }
        public uint PlayerConditionID { get; set; }
        public sbyte TargetLevel { get; set; }
        public ushort TargetItemLevel { get; set; }
        public int MissionDuration { get; set; }
        public int TravelDuration { get; set; }
        public uint OfferDuration { get; set; }
        public byte BaseCompletionChance { get; set; }
        public uint BaseFollowerXP { get; set; }
        public uint OvermaxRewardPackID { get; set; }
        public byte FollowerDeathChance { get; set; }
        public uint AreaID { get; set; }
        public uint Flags { get; set; }
        public int GarrMissionSetID { get; set; }
    }
}
