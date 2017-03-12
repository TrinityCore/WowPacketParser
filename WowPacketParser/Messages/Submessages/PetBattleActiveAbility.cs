namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleActiveAbility
    {
        public int AbilityID;
        public short CooldownRemaining;
        public short LockdownRemaining;
        public sbyte AbilityIndex;
        public byte Pboid;
    }
}
