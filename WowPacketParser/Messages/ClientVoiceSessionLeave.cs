using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientVoiceSessionLeave
    {
        public ulong ClientGUID;
        public ulong SessionGUID;
    }
}
