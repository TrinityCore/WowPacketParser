using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TerrainType, HasIndexInData = false)]
    public class TerrainTypeEntry
    {
        public string TerrainDesc { get; set; }
        public ushort FootstepSprayRun { get; set; }
        public ushort FootstepSprayWalk { get; set; }
        public byte SoundID { get; set; }
        public byte Flags { get; set; }
    }
}
