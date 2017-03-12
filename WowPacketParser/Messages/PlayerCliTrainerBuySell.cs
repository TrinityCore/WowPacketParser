using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliTrainerBuySell
    {
        public ulong TrainerGUID;
        public int TrainerID;
        public int SpellID;
    }
}
