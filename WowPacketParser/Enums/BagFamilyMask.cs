using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum BagFamilyMask
    {
        None                   = 0x00000000,
        Arrows                 = 0x00000001,
        Bullets                = 0x00000002,
        Soulshards             = 0x00000004,
        LeatherworkingSupplies = 0x00000008,
        InscriptionSupplies    = 0x00000010,
        Herbs                  = 0x00000020,
        EnchantingSupplies     = 0x00000040,
        EngineeringSupplies    = 0x00000080,
        Keys                   = 0x00000100,
        Gems                   = 0x00000200,
        MiningSupplies         = 0x00000400,
        SoulboundEquipment     = 0x00000800,
        VanityPets             = 0x00001000,
        CurrencyTokens         = 0x00002000,
        QuestItems             = 0x00004000,
        FishingSupplies        = 0x00008000,
        CookingSupplies        = 0x00010000,
        Toys                   = 0x00020000,
        Archaeology            = 0x00040000,
        Alchemy                = 0x00080000,
        Blacksmithing          = 0x00100000,
        FirstAid               = 0x00200000,
        Jewelcrafting          = 0x00400000,
        Skinning               = 0x00800000,
        Tailoring              = 0x01000000
    }
}
