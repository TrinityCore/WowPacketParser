using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlaySound
    {
        public ulong SourceObjectGUID;
        public int SoundKitID;
    }
}
