using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlayObjectSound
    {
        public ulong TargetObjectGUID;
        public ulong SourceObjectGUID;
        public int SoundKitID;
    }
}
