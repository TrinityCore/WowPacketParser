namespace WowPacketParserModule.V5_4_8_18414.Enums
{
    public enum TradeStatus
    {
        TRADE_STATUS_FAILED                = 0,
        TRADE_STATUS_TARGET_STUNNED        = 1,
        TRADE_STATUS_INITIATED             = 2,
        TRADE_STATUS_CURRENCY_NOT_TRADABLE = 3,
        TRADE_STATUS_PLAYER_NOT_FOUND      = 4,
        TRADE_STATUS_TOO_FAR_AWAY          = 5,
        TRADE_STATUS_ACCEPTED              = 6,
        TRADE_STATUS_DEAD                  = 7,
        TRADE_STATUS_STATE_CHANGED         = 9,
        TRADE_STATUS_WRONG_FACTION         = 10,
        TRADE_STATUS_ALREADY_TRADING       = 11,
        TRADE_STATUS_RESTRICTED_ACCOUNT    = 13,
        TRADE_STATUS_COMPLETE              = 14,
        TRADE_STATUS_LOGGING_OUT           = 15,
        TRADE_STATUS_PLAYER_IGNORED        = 16,
        TRADE_STATUS_TARGET_LOGGING_OUT    = 17,
        TRADE_STATUS_PETITION              = 18,
        TRADE_STATUS_STUNNED               = 20,
        TRADE_STATUS_PLAYER_BUSY           = 21,
        TRADE_STATUS_WRONG_REALM           = 22, // Вы можете передавать игрокам из других миров только сотворенные предметы
        TRADE_STATUS_NOT_ENOUGH_CURRENCY   = 23,
        TRADE_STATUS_PROPOSED              = 24,
        TRADE_STATUS_UNACCEPTED            = 27,
        TRADE_STATUS_TARGET_DEAD           = 28,
        TRADE_STATUS_CANCELLED             = 30, // Сделка отменена
        TRADE_STATUS_NOT_ON_TAPLIST        = 31, // Вы можете передавать персональные предметы только тем игрокам, которые могли подобрать их сами
    }
}
