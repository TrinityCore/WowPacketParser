namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct AddBattlenetFriend
    {
        public uint RoleID;
        public ulong ClientToken;
        public bool VerifyOnly;
        public ulong TargetGUID;
        public string InvitationMsg;
    }
}
