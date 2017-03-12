using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GdfSimPlayer
    {
        public ulong Guid;
        public float GdfRating;
        public float GdfVariance;
        public int PersonalRating;
        public bool Team;
    }
}
