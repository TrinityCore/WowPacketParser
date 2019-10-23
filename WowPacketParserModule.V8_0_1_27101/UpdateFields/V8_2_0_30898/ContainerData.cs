using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_0_30898
{
    public class ContainerData : IContainerData
    {
        public WowGuid[] Slots { get; } = new WowGuid[36];
        public uint NumSlots { get; set; }
    }
}

