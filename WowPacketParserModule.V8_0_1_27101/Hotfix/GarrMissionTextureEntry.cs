using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrMissionTexture, HasIndexInData = false)]
    public class GarrMissionTextureEntry
    {
        [HotfixArray(2)]
        public float[] Pos { get; set; }
        public ushort UiTextureKitID { get; set; }
    }
}
