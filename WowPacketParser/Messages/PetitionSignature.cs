using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetitionSignature
    {
        public ulong Signer;
        public int Choice;
    }
}
