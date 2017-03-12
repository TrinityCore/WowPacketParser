using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDanceStudioCreateResult
    {
        public bool Enable;
        public fixed int Secrets[4];
    }
}
