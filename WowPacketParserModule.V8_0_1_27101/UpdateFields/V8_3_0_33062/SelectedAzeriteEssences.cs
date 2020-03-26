using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_33062
{
    public class SelectedAzeriteEssences : ISelectedAzeriteEssences
    {
        public uint[] AzeriteEssenceID { get; } = new uint[4];
        public uint SpecializationID { get; set; }
        public uint Enabled { get; set; }
    }
}

