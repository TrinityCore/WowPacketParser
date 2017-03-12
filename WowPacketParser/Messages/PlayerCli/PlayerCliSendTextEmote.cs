namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSendTextEmote
    {
        public ulong Target;
        public int EmoteID;
        public int SoundIndex;
    }
}
