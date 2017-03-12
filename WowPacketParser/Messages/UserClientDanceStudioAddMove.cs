using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDanceStudioAddMove
    {
        public ulong ImageBase;
        public string PseudoFilename;
        public ulong Caller;
        public string Code;
    }
}
