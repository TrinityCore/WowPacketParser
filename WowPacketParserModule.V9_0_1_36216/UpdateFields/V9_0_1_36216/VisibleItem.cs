using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class VisibleItem : IMutableVisibleItem
    {
        public int? ItemID { get; set; }
        public int SecondaryItemModifiedAppearanceID { get; set; }
        public ushort? ItemAppearanceModID { get; set; }
        public ushort? ItemVisual { get; set; }
    }
}

