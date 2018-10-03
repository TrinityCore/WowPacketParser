using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureImmunities, HasIndexInData = false)]
    public class CreatureImmunitiesEntry
    {
        public byte School { get; set; }
        public uint DispelType { get; set; }
        public byte MechanicsAllowed { get; set; }
        public byte EffectsAllowed { get; set; }
        public byte StatesAllowed { get; set; }
        public byte Flags { get; set; }
        [HotfixArray(2)]
        public int[] Mechanic { get; set; }
        [HotfixArray(9)]
        public uint[] Effect { get; set; }
        [HotfixArray(16)]
        public uint[] State { get; set; }
    }
}
