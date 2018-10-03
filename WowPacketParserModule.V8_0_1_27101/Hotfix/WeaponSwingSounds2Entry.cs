using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WeaponSwingSounds2, HasIndexInData = false)]
    public class WeaponSwingSounds2Entry
    {
        public byte SwingType { get; set; }
        public byte Crit { get; set; }
        public uint SoundID { get; set; }
    }
}
