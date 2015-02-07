namespace WowPacketParser.Enums
{
    public enum SellResult
    {
        Ok             = 0,
        CantFindItem   = 1,
        CantSellItem   = 2,
        CantFindVendor = 3,
        DoNotOwnItem   = 4,
        Unk            = 5,
        OnlyEmptyBag   = 6
    }
}
