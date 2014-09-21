using System;

namespace WowPacketParser.Enums
{
    // TODO: use when reading ITEM_FIELD_FLAGS
    [Flags]
    public enum ItemFieldFlags : uint
    {
        Souldbound   = 0x00000001, // Item is soulbound and cannot be traded
        Unk1         = 0x00000002, // ?
        Unlocked     = 0x00000004, // Item had lock but can be opened now
        Wrapped      = 0x00000008, // Item is wrapped and contains another item
        Unk2         = 0x00000010, // ?
        Unk3         = 0x00000020, // ?
        Unk4         = 0x00000040, // ?
        Unk5         = 0x00000080, // ?
        BOPTradeable = 0x00000100, // Allows trading soulbound items
        Readble      = 0x00000200, // Opens text page when right clicked
        Unk6         = 0x00000400, // ?
        Unk7         = 0x00000800, // ?
        Refundable   = 0x00001000, // Item can be returned to vendor for its original cost (extended cost)
        Unk8         = 0x00002000, // ?
        Unk9         = 0x00004000, // ?
        Unk10        = 0x00008000, // ?
        Unk11        = 0x00010000, // ?
        Unk12        = 0x00020000, // ?
        Unk13        = 0x00040000, // ?
        Unk14        = 0x00080000, // ?
        Unk15        = 0x00100000, // ?
        Unk16        = 0x00200000, // ?
        Unk17        = 0x00400000, // ?
        Unk18        = 0x00800000, // ?
        Unk19        = 0x01000000, // ?
        Unk20        = 0x02000000, // ?
        Unk21        = 0x04000000, // ?
        Unk22        = 0x08000000, // ?
        Unk23        = 0x10000000, // ?
        Unk24        = 0x20000000, // ?
        Unk25        = 0x40000000, // ?
        Unk26        = 0x80000000  // ?
    }
}
