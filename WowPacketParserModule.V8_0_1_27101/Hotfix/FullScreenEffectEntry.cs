using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.FullScreenEffect, HasIndexInData = false)]
    public class FullScreenEffectEntry
    {
        public uint Flags { get; set; }
        public float Saturation { get; set; }
        public float GammaRed { get; set; }
        public float GammaGreen { get; set; }
        public float GammaBlue { get; set; }
        public float MaskOffsetY { get; set; }
        public float MaskSizeMultiplier { get; set; }
        public float MaskPower { get; set; }
        public float ColorMultiplyRed { get; set; }
        public float ColorMultiplyGreen { get; set; }
        public float ColorMultiplyBlue { get; set; }
        public float ColorMultiplyOffsetY { get; set; }
        public float ColorMultiplyMultiplier { get; set; }
        public float ColorMultiplyPower { get; set; }
        public float ColorAdditionRed { get; set; }
        public float ColorAdditionGreen { get; set; }
        public float ColorAdditionBlue { get; set; }
        public float ColorAdditionOffsetY { get; set; }
        public float ColorAdditionMultiplier { get; set; }
        public float ColorAdditionPower { get; set; }
        public int OverlayTextureFileDataID { get; set; }
        public float BlurIntensity { get; set; }
        public float BlurOffsetY { get; set; }
        public float BlurMultiplier { get; set; }
        public float BlurPower { get; set; }
        public uint EffectFadeInMs { get; set; }
        public uint EffectFadeOutMs { get; set; }
        public uint TextureBlendSetID { get; set; }
    }
}
