namespace WowPacketParser.Enums
{
    public enum LfgUpdateType
    {
        None               = 0,
        Unknown1           = 1, // Related to Group leader
        RoleCheckAborted   = 4,
        JoinProposal       = 5,
        RoleCheckFailed    = 6,
        RemovedFromQueue   = 7,
        ProposalFailed     = 8,
        ProposalDeclined   = 9,
        GroupFound         = 10,
        Unknown11          = 11,
        AddedToQueue       = 12,
        ProposalBegin      = 13,
        UpdateStatus       = 14,
        GroupMemberOffline = 15,
        Unknown16          = 16, // Appears with Group Disband
        JoinedRaidBrowser  = 18
    }
}
