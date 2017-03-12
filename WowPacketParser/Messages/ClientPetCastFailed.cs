using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetCastFailed
    {
        public int Reason;
        public int FailedArg2;
        public int FailedArg1;
        public int SpellID;
        public byte CastID;
    }
}
