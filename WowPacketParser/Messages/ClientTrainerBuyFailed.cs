using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTrainerBuyFailed
    {
        public ulong TrainerGUID;
        public int TrainerFailedReason;
        public int SpellID;
    }
}
