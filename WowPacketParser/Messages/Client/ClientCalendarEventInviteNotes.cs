namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventInviteNotes
    {
        public ulong InviteGUID;
        public bool ClearPending;
        public string Notes;
        public ulong EventID;
    }
}
