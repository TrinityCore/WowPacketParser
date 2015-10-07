namespace WowPacketParserModule.BattleNet.V37165.Enums
{
    public enum WowAuthResult : byte
    {
        Success = 0,
        FailBanned = 3,
        FailUnknownAccount = 4,
        FailIncorrectPassword = 5,
        FailAlreadyOnline = 6,
        FailNoTime = 7,
        FailDbBusy = 8,
        FailVersionInvalid = 9,
        FailVersionUpdate = 10,
        FailInvalidServer = 11,
        FailSuspended = 12,
        FailFailNoAccess = 13,
        SuccessSurvey = 14,
        FailParentcontrol = 15,
        FailLockedEnforced = 16,
        FailTrialEnded = 17,
        FailOvermindConverted = 18,
        FailAntiIndulgence = 19,
        FailExpired = 20,
        FailNoGameAccount = 21,
        FailBillingLock = 22,
        FailIgrWithoutBnet = 23,
        FailAaLock = 24,
        FailUnlockableLock = 25,
        FailMustUseBnet = 26,
        FailOther = 255
    };
}
