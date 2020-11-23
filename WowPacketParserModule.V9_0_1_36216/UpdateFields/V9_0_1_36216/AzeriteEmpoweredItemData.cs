using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class AzeriteEmpoweredItemData : IAzeriteEmpoweredItemData
    {
        public int[] Selections { get; } = new int[5];
    }
}

