using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGenerateRandomCharacterNameResult
    {
        public string Name;
        public bool Success;
    }
}
