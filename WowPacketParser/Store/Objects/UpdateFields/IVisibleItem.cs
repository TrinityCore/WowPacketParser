
namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IVisibleItem
    {
        int? ItemID { get; }
        ushort? ItemAppearanceModID { get; }
        ushort? ItemVisual { get; }
    }

    public interface IMutableVisibleItem : IVisibleItem
    {
        new int? ItemID { get; set; }
        new ushort? ItemAppearanceModID { get; set; }
        new ushort? ItemVisual { get; set; }
    }

    public static partial class Extensions
    {
        public static void UpdateData(this IMutableVisibleItem data, IVisibleItem update)
        {
            data.ItemID = update.ItemID ?? data.ItemID;
            data.ItemAppearanceModID = update.ItemAppearanceModID ?? data.ItemAppearanceModID;
            data.ItemVisual = update.ItemVisual ?? data.ItemVisual;
        }
    }
}
