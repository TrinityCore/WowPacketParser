using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellSpecialUnitEffect, HasIndexInData = false)]
    public class SpellSpecialUnitEffectEntry
    {
        public ushort SpellVisualEffectNameID { get; set; }
        public uint PositionerID { get; set; }
    }
}
