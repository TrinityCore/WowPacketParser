namespace WowPacketParser.Enums
{
    public enum MailErrorType
    {
        Ok                    = 0,
        Equip                 = 1,
        CannotSendToSelf      = 2,
        NotEnoughMoney        = 3,
        RecipientNotFound     = 4,
        NotYourTeam           = 5,
        InternalError         = 6,
        DisabledForTrial      = 14,
        RecipientCapReached   = 15,
        CantSendWrappedCOD    = 16,
        MailAndChatSuspended  = 17,
        TooManyAttachMents    = 18,
        MailAttachmentInvalid = 19,
        ItemHasExpired        = 21
    }
}
