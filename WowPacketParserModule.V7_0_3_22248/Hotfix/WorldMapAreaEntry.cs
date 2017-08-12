using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.WorldMapArea)]
    public class WorldMapAreaEntry
    {
        public string AreaName { get; set; }
        public float LocLeft { get; set; }
        public float LocRight { get; set; }
        public float LocTop { get; set; }
        public float LocBottom { get; set; }
        public ushort MapID { get; set; }
        public ushort AreaID { get; set; }
        public short DisplayMapID { get; set; }
        public short DefaultDungeonFloor { get; set; }
        public ushort ParentWorldMapID { get; set; }
        public ushort Flags { get; set; }
        public byte LevelRangeMin { get; set; }
        public byte LevelRangeMax { get; set; }
        public byte BountySetID { get; set; }
        public byte BountyBoardLocation { get; set; }
        public uint ID { get; set; }
        public uint PlayerConditionID { get; set; }
    }
}