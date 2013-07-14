using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum ItemProtoFlags : uint
    {
        None                 = 0x00000000,
        Unk1                 = 0x00000001, // ?
        Conjured             = 0x00000002, // Conjured item
        Openable             = 0x00000004, // Item can be right clicked to open for loot
        Heroic               = 0x00000008, // Makes green "Heroic" text appear on item
        Deprecated           = 0x00000010, // Cannot equip or use
        Indestructible       = 0x00000020, // Item can not be destroyed, except by using spell (item can be reagent for spell)
        Unk2                 = 0x00000040, // ?
        NoEquipCooldown      = 0x00000080, // No default 30 seconds cooldown when equipped
        Unk3                 = 0x00000100, // ?
        Wrapper              = 0x00000200, // Item can wrap other items
        Unk4                 = 0x00000400, // ?
        PartyLoot            = 0x00000800, // Looting this item does not remove it from available loot
        Refundable           = 0x00001000, // Item can be returned to vendor for its original cost (extended cost)
        Charter              = 0x00002000, // Item is guild or arena charter
        Unk5                 = 0x00004000, // Only readable items have this (but not all)
        Unk6                 = 0x00008000, // ?
        Unk7                 = 0x00010000, // ?
        Unk8                 = 0x00020000, // ?
        Prospectable         = 0x00040000, // Item can be prospected
        UniqueEquipped       = 0x00080000, // You can only equip one of these
        Unk9                 = 0x00100000, // ?
        UseableInArena       = 0x00200000, // Item can be used during arena match
        Throwable            = 0x00400000, // Some Thrown weapons have it (and only Thrown) but not all
      UsableWhenShapeshifted = 0x00800000, // Item can be used in shapeshift forms
        Unk10                = 0x01000000, // ?
        SmartLoot            = 0x02000000, // Profession recipes: can only be looted if you meet requirements and don't already know it
        NotUseableInArena    = 0x04000000, // Item cannot be used in arena
        BindToAccount        = 0x08000000, // Item binds to account and can be sent only to your own characters
        TriggeredCast        = 0x10000000, // Spell is cast with triggered flag
        Millable             = 0x20000000, // Item can be milled
        Unk11                = 0x40000000, // ?
        Unk12                = 0x80000000  // ?
    }
}
