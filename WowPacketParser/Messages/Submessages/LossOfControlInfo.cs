namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LossOfControlInfo
    {
        public byte AuraSlot;
        public byte EffectIndex;
        public int Type;
        public int Mechanic;
    }
}
