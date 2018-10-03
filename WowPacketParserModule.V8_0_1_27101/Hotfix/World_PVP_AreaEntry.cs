using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.World_PVP_Area, HasIndexInData = false)]
    public class World_PVP_AreaEntry
    {
        public ushort AreaID { get; set; }
        public ushort NextTimeWorldstate { get; set; }
        public ushort GameTimeWorldstate { get; set; }
        public ushort BattlePopulateTime { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public short MapID { get; set; }
    }
}
