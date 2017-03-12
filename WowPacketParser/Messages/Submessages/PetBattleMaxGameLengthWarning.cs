namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleMaxGameLengthWarning
    {
        public UnixTime TimeRemaining;
        public int RoundsRemaining;
    }
}
