namespace WowPacketParser.Enums
{
    public enum PetStableResult
    {
        STABLE_ERR_MONEY        = 0x01,                         // "you don't have enough money"
        STABLE_ERR_INVALID_SLOT = 0x03,                         // "That slot is locked"
        STABLE_SUCCESS_STABLE   = 0x08,                         // stable success
        STABLE_SUCCESS_UNSTABLE = 0x09,                         // unstable/swap success
        STABLE_SUCCESS_BUY_SLOT = 0x0A,                         // buy slot success
        STABLE_ERR_EXOTIC       = 0x0B,                         // "you are unable to control exotic creatures"
        STABLE_ERR_STABLE       = 0x0C,                         // "Internal pet error"
    }
}
