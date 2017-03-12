namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSetEncounterProfiler
    {
        public bool Enable;
        public int EncounterID;
        public string MailTo;
    }
}
