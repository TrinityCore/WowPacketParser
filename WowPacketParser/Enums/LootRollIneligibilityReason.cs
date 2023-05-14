
namespace WowPacketParser.Enums
{
    public enum LootRollIneligibilityReason : uint
    {
        None                    = 0,
        UnusableByClass         = 1, // Your class may not roll need on this item.
        MaxUniqueItemCount      = 2, // You already have the maximum amount of this item.
        CannotBeDisenchanted    = 3, // This item may not be disenchanted.
        EnchantingSkillTooLow   = 4, // You do not have an Enchanter of skill %d in your group.
        NeedDisabled            = 5, // Need rolls are disabled for this item.
        OwnBetterItem           = 6  // You already have a powerful version of this item.
    };
}
