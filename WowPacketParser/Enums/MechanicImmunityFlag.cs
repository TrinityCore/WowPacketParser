using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum MechanicImmunityFlag : ulong
    {
        None                = 0x000000000,
        Charm               = 0x000000002,
        Disoriented         = 0x000000004,
        Disarm              = 0x000000008,
        Distract            = 0x000000010,
        Fear                = 0x000000020,
        Grip                = 0x000000040,
        Root                = 0x000000080,
        SlowAttack          = 0x000000100,
        Silence             = 0x000000200,
        Sleep               = 0x000000400,
        Snare               = 0x000000800,
        Stun                = 0x000001000,
        Freeze              = 0x000002000,
        Knockout            = 0x000004000,
        Bleed               = 0x000008000,
        Bandage             = 0x000010000,
        Polymorph           = 0x000020000,
        Banish              = 0x000040000,
        Shield              = 0x000080000,
        Shackle             = 0x000100000,
        Mount               = 0x000200000,
        Infected            = 0x000400000,
        Turn                = 0x000800000,
        Horror              = 0x001000000,
        Invulnerability     = 0x002000000,
        Interrupt           = 0x004000000,
        Daze                = 0x008000000,
        Discovery           = 0x010000000,
        ImmuneShield        = 0x020000000,
        Sapped              = 0x040000000,
        Enraged             = 0x080000000,
        Wounded             = 0x100000000
    }
}
