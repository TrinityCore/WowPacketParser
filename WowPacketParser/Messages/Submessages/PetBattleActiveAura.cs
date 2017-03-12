namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleActiveAura
    {
        public int AbilityID;
        public uint InstanceID;
        public int RoundsRemaining;
        public int CurrentRound;
        public byte CasterPBOID;
    }
}
