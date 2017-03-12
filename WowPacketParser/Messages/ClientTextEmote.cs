using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTextEmote
    {
        public ulong SourceGUID;
        public ulong TargetGUID;
        public int SoundIndex;
        public int EmoteID;
    }
}
