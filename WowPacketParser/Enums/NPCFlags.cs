using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum NPCFlags
    {
        None              = 0x00000000,
        Gossip            = 0x00000001,
        QuestGiver        = 0x00000002,
        Unknown1          = 0x00000004,
        Unknown2          = 0x00000008,
        Trainer           = 0x00000010,
        ClassTrainer      = 0x00000020,
        ProfessionTrainer = 0x00000040,
        Vendor            = 0x00000080,
        AmmoVendor        = 0x00000100,
        FoodVendor        = 0x00000200,
        PoisonVendor      = 0x00000400,
        ReagentVendor     = 0x00000800,
        Repair            = 0x00001000,
        FlightMaster      = 0x00002000,
        SpiritHealer      = 0x00004000,
        SpiritGuide       = 0x00008000,
        InnKeeper         = 0x00010000,
        Banker            = 0x00020000,
        Petitioner        = 0x00040000,
        TabardDesigner    = 0x00080000,
        BattleMaster      = 0x00100000,
        Auctioneer        = 0x00200000,
        StableMaster      = 0x00400000,
        GuildBanker       = 0x00800000,
        SpellClick        = 0x01000000,
        PlayerVehicle     = 0x02000000,
        MailObject        = 0x04000000,
        ForgeMaster       = 0x08000000,
        Transmogrifier    = 0x10000000,
        Vaultkeeper       = 0x20000000
    }
}
