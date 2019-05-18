using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class ArenaCooldown : IArenaCooldown
    {
        public int SpellID { get; set; }
        public int Charges { get; set; }
        public uint Flags { get; set; }
        public uint StartTime { get; set; }
        public uint EndTime { get; set; }
        public uint NextChargeTime { get; set; }
        public byte MaxCharges { get; set; }
    }
}

