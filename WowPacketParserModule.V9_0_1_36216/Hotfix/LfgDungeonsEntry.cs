using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.LfgDungeons, HasIndexInData = false)]
    public class LFGDungeonsEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte TypeID { get; set; }
        public byte Subtype { get; set; }
        public sbyte Faction { get; set; }
        public int IconTextureFileID { get; set; }
        public int RewardsBgTextureFileID { get; set; }
        public int PopupBgTextureFileID { get; set; }
        public byte ExpansionLevel { get; set; }
        public short MapID { get; set; }
        public byte DifficultyID { get; set; }
        public float MinGear { get; set; }
        public byte GroupID { get; set; }
        public byte OrderIndex { get; set; }
        public uint RequiredPlayerConditionId { get; set; }
        public ushort RandomID { get; set; }
        public ushort ScenarioID { get; set; }
        public ushort FinalEncounterID { get; set; }
        public byte CountTank { get; set; }
        public byte CountHealer { get; set; }
        public byte CountDamage { get; set; }
        public byte MinCountTank { get; set; }
        public byte MinCountHealer { get; set; }
        public byte MinCountDamage { get; set; }
        public ushort BonusReputationAmount { get; set; }
        public ushort MentorItemLevel { get; set; }
        public byte MentorCharLevel { get; set; }
        public int ContentTuningID { get; set; }
        [HotfixArray(2)]
        public int[] Flags { get; set; }
    }
}
