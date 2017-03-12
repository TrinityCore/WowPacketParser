namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleInput
    {
        public byte MoveType;
        public sbyte NewFrontPet;
        public byte DebugFlags;
        public byte BattleInterrupted;
        public bool IgnoreAbandonPenalty;
        public int AbilityID;
        public int Round;
    }
}
