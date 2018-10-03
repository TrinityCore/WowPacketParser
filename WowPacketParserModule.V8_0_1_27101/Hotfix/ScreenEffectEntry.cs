using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ScreenEffect, HasIndexInData = false)]
    public class ScreenEffectEntry
    {
        public string Name { get; set; }
        [HotfixArray(4)]
        public int[] Param { get; set; }
        public sbyte Effect { get; set; }
        public uint FullScreenEffectID { get; set; }
        public ushort LightParamsID { get; set; }
        public ushort LightParamsFadeIn { get; set; }
        public ushort LightParamsFadeOut { get; set; }
        public uint SoundAmbienceID { get; set; }
        public uint ZoneMusicID { get; set; }
        public short TimeOfDayOverride { get; set; }
        public sbyte EffectMask { get; set; }
        public byte LightFlags { get; set; }
    }
}
