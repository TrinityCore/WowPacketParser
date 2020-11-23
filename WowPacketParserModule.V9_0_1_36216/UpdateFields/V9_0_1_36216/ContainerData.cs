using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class ContainerData : IContainerData
    {
        public WowGuid[] Slots { get; } = new WowGuid[36];
        public uint NumSlots { get; set; }
    }
}

