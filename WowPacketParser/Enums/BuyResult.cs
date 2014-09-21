using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum BuyResult
    {
        CantFindItem       = 0,
        AlreadySold        = 1,
        NotEnoughMoney     = 2,
        VendorHatesYou     = 4,
        VendorTooFar       = 5,
        ItemSoldOut        = 7,
        CantCarryMore      = 8,
        RequiresRank       = 11,
        RequiresReputation = 12
    }
}
