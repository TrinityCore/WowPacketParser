namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PeriodicAuraLogEffect
    {
        public int Effect;
        public int Amount;
        public int OverHealOrKill;
        public int SchoolMaskOrPower;
        public int AbsorbedOrAmplitude;
        public int Resisted;
        public bool Crit;
        public bool Multistrike;
        public PeriodicAuraLogEffectDebugInfo? DebugInfo; // Optional
    }
}
