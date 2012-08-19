using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum BagFamilyMask
    {
        None = 0x00000000,
        Arrows = 0x00000001,
        Bullets = 0x00000002,
        Soulshards = 0x00000004,
        LeatherworkingSupplies = 0x00000008,
        InscriptionSupplies = 0x00000010,
        Herbs = 0x00000020,
        EnchantingSupplies = 0x00000040,
        EngineeringSupplies = 0x00000080,
        Keys = 0x00000100,
        Gems = 0x00000200,
        MiningSupplies = 0x00000400,
        SoulboundEquipment = 0x00000800,
        VanityPets = 0x00001000,
        CurrencyTokens = 0x00002000,
        QuestItems = 0x00004000
    }
}
