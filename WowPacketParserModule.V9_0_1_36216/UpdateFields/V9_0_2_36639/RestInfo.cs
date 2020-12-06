using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_2_36639
{
    public class RestInfo : IRestInfo
    {
        public uint Threshold { get; set; }
        public byte StateID { get; set; }
    }
}

