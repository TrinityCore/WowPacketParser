using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellTargetedHealPrediction
    {
        public ulong TargetGUID;
        public SpellHealPrediction Predict;
    }
}
