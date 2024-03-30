using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class MawPower : IMawPower
    {
        public int SpellID { get; set; }
        public int MawPowerID { get; set; }
        public int Stacks { get; set; }
    }
}

