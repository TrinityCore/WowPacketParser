using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.LfgDungeons, ClientVersionBuild.V7_0_3_22248, ClientVersionBuild.V7_2_0_23826)]
    public class LfgDungeonsEntry
    {
        public string Name { get; set; }
        public uint Flags { get; set; }
        public string TextureFilename { get; set; }
        public string Description { get; set; }
        public uint PlayerConditionID { get; set; }
        public float MinItemLevel { get; set; }
        public ushort MaxLevel { get; set; }
        public ushort TargetLevelMax { get; set; }
        public short MapID { get; set; }
        public ushort RandomID { get; set; }
        public ushort ScenarioID { get; set; }
        public ushort LastBossJournalEncounterID { get; set; }
        public ushort BonusReputationAmount { get; set; }
        public ushort MentorItemLevel { get; set; }
        public byte MinLevel { get; set; }
        public byte TargetLevel { get; set; }
        public byte TargetLevelMin { get; set; }
        public byte DifficultyID { get; set; }
        public byte Type { get; set; }
        public byte Faction { get; set; }
        public byte Expansion { get; set; }
        public byte OrderIndex { get; set; }
        public byte GroupID { get; set; }
        public byte CountTank { get; set; }
        public byte CountHealer { get; set; }
        public byte CountDamage { get; set; }
        public byte MinCountTank { get; set; }
        public byte MinCountHealer { get; set; }
        public byte MinCountDamage { get; set; }
        public byte SubType { get; set; }
        public byte MentorCharLevel { get; set; }
        public uint ID { get; set; }
    }
}