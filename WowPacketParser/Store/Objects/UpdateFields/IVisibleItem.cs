namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IVisibleItem
    {
        int ItemID { get; }
        ushort ItemAppearanceModID { get; }
        ushort ItemVisual { get; }
    }
}
