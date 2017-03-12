namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientVoiceSessionRosterUpdateMember
    {
        public ulong MemberGUID;
        public byte NetworkId;
        public byte Priority;
        public byte Flags;
    }
}
