using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_5_31921
{
    public class SelectedAzeriteEssences : ISelectedAzeriteEssences
    {
        public uint[] AzeriteEssenceID { get; } = new uint[3];
        public uint SpecializationID { get; set; }
        public uint Enabled { get; set; }
    }
}

