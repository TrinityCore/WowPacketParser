using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum NPCFlags : ulong
    {
        None                = 0x0000000000,
        Gossip              = 0x0000000001,
        QuestGiver          = 0x0000000002,
        Unknown1            = 0x0000000004,
        Unknown2            = 0x0000000008,
        Trainer             = 0x0000000010,
        ClassTrainer        = 0x0000000020,
        ProfessionTrainer   = 0x0000000040,
        Vendor              = 0x0000000080,
        AmmoVendor          = 0x0000000100,
        FoodVendor          = 0x0000000200,
        PoisonVendor        = 0x0000000400,
        ReagentVendor       = 0x0000000800,
        Repair              = 0x0000001000,
        FlightMaster        = 0x0000002000,
        SpiritHealer        = 0x0000004000,
        SpiritGuide         = 0x0000008000,
        InnKeeper           = 0x0000010000,
        Banker              = 0x0000020000,
        Petitioner          = 0x0000040000,
        TabardDesigner      = 0x0000080000,
        BattleMaster        = 0x0000100000,
        Auctioneer          = 0x0000200000,
        StableMaster        = 0x0000400000,
        GuildBanker         = 0x0000800000,
        SpellClick          = 0x0001000000,
        PlayerVehicle       = 0x0002000000,
        MailObject          = 0x0004000000,
        ForgeMaster         = 0x0008000000,
        Transmogrifier      = 0x0010000000,
        Vaultkeeper         = 0x0020000000,
        BlackMarket         = 0x0080000000,
        ItemUpgradeMaster   = 0x0100000000,
        GarrisonArchitect   = 0x0200000000,
        Steering            = 0x0400000000,
        ShipmentCrafter     = 0x1000000000,
        GarrisonMissionNpc  = 0x2000000000,
        TradeskillNpc       = 0x4000000000,
        BlackMarketView     = 0x8000000000
    }
}
