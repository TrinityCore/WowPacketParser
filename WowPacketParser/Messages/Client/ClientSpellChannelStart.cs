using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellChannelStart
    {
        public int SpellID;
        public SpellChannelStartInterruptImmunities? InterruptImmunities; // Optional
        public ulong CasterGUID;
        public SpellTargetedHealPrediction? HealPrediction; // Optional
        public uint ChannelDuration;
    }
}
