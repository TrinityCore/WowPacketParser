namespace WowPacketParser.Enums
{
    public enum ItemSpellTriggerType : sbyte
    {
        OnUse        = 0,
        OnEquip      = 1,
        ChanceOnHit  = 2,
        Soulstone    = 4,
        OnNoDelayUse = 5,
        LearnSpellId = 6
    }
}
