using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class SkillInfo : ISkillInfo
    {
        public ushort[] SkillLineID { get; } = new ushort[256];
        public ushort[] SkillStep { get; } = new ushort[256];
        public ushort[] SkillRank { get; } = new ushort[256];
        public ushort[] SkillStartingRank { get; } = new ushort[256];
        public ushort[] SkillMaxRank { get; } = new ushort[256];
        public short[] SkillTempBonus { get; } = new short[256];
        public ushort[] SkillPermBonus { get; } = new ushort[256];
    }
}

