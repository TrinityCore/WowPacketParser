using System;

namespace WowPacketParser.Enums
{
    public enum UnitSheathState
    {
        SHEATH_STATE_UNARMED = 0,
        SHEATH_STATE_MELEE   = 1,
        SHEATH_STATE_RANGED  = 2
    }

    [Flags]
    public enum UnitPVPStateFlags
    {
        UNIT_BYTE2_FLAG_PVP       = 0x01,
        UNIT_BYTE2_FLAG_UNK1      = 0x02,
        UNIT_BYTE2_FLAG_FFA_PVP   = 0x04,
        UNIT_BYTE2_FLAG_SANCTUARY = 0x08,
        UNIT_BYTE2_FLAG_UNK4      = 0x10,
        UNIT_BYTE2_FLAG_UNK5      = 0x20,
        UNIT_BYTE2_FLAG_UNK6      = 0x40,
        UNIT_BYTE2_FLAG_UNK7      = 0x80
    }

    [Flags]
    enum UnitRename
    {
        UNIT_CAN_BE_RENAMED   = 0x01,
        UNIT_CAN_BE_ABANDONED = 0x02
    }

    enum ShapeshiftForm
    {
        FORM_NONE                      = 0,
        FORM_CAT_FORM                  = 1,
        FORM_TREE_OF_LIFE              = 2,
        FORM_TRAVEL_FORM               = 3,
        FORM_AQUATIC_FORM              = 4,
        FORM_BEAR_FORM                 = 5,
        FORM_AMBIENT                   = 6,
        FORM_GHOUL                     = 7,
        FORM_DIRE_BEAR_FORM            = 8,
        FORM_CRANE_STANCE              = 9,
        FORM_THARONJA_SKELETON         = 10,
        FORM_DARKMOON_TEST_OF_STRENGTH = 11,
        FORM_BLB_PLAYER                = 12,
        FORM_SHADOW_DANCE              = 13,
        FORM_CREATURE_BEAR             = 14,
        FORM_CREATURE_CAT              = 15,
        FORM_GHOST_WOLF                = 16,
        FORM_BATTLE_STANCE             = 17,
        FORM_DEFENSIVE_STANCE          = 18,
        FORM_BERSERKER_STANCE          = 19,
        FORM_SERPENT_STANCE            = 20,
        FORM_ZOMBIE                    = 21,
        FORM_METAMORPHOSIS             = 22,
        FORM_OX_STANCE                 = 23,
        FORM_TIGER_STANCE              = 24,
        FORM_UNDEAD                    = 25,
        FORM_FRENZY                    = 26,
        FORM_FLIGHT_FORM_EPIC          = 27,
        FORM_SHADOWFORM                = 28,
        FORM_FLIGHT_FORM               = 29,
        FORM_STEALTH                   = 30,
        FORM_MOONKIN_FORM              = 31,
        FORM_SPIRIT_OF_REDEMPTION      = 32,
        FORM_GLADIATOR_STANCE          = 33,
        FORM_METAMORPHOSIS_2           = 34,
        FORM_MOONKIN_FORM_RESTORATION  = 35,
        FORM_TREANT_FORM               = 36,
        FORM_SPIRIT_OWL_FORM           = 37,
        FORM_SPIRIT_OWL_FORM_2         = 38,
        FORM_WISP_FORM                 = 39,
        FORM_WISP_FORM_2               = 40,
    }
}
