using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GroundEffectTexture, HasIndexInData = false)]
    public class GroundEffectTextureEntry
    {
        public uint Density { get; set; }
        public byte Sound { get; set; }
        [HotfixArray(4)]
        public ushort[] DoodadID { get; set; }
        [HotfixArray(4)]
        public sbyte[] DoodadWeight { get; set; }
    }
}
