using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DecalProperties)]
    public class DecalPropertiesEntry
    {
        public int ID { get; set; }
        public int FileDataID { get; set; }
        public int TopTextureBlendSetID { get; set; }
        public int BotTextureBlendSetID { get; set; }
        public float ModX { get; set; }
        public float InnerRadius { get; set; }
        public float OuterRadius { get; set; }
        public float Rim { get; set; }
        public float Gain { get; set; }
        public int Flags { get; set; }
        public float Scale { get; set; }
        public float FadeIn { get; set; }
        public float FadeOut { get; set; }
        public byte Priority { get; set; }
        public byte BlendMode { get; set; }
        public int GameFlags { get; set; }
        public int CasterDecalPropertiesID { get; set; }
        public float ArbitraryBoxHeight { get; set; }
    }
}
