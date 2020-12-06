using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_2_36639
{
    public class SelectedAzeriteEssences : ISelectedAzeriteEssences
    {
        public uint[] AzeriteEssenceID { get; } = new uint[4];
        public uint SpecializationID { get; set; }
        public uint Enabled { get; set; }
    }
}

