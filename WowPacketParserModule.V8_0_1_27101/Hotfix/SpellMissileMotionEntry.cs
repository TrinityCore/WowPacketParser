using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellMissileMotion, HasIndexInData = false)]
    public class SpellMissileMotionEntry
    {
        public string Name { get; set; }
        public string ScriptBody { get; set; }
        public byte Flags { get; set; }
        public byte MissileCount { get; set; }
    }
}
