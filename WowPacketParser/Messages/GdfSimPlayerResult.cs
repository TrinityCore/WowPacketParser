using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GdfSimPlayerResult
    {
        public ulong Guid;
        public bool Team;
        public int PreRating;
        public int PostRating;
        public int RatingChange;
        public float PreGdf;
        public float PostGdf;
        public float GdfChange;
        public int PreGdfAsELO;
        public int PostGdfAsELO;
        public int GdfChangeAsELO;
        public float PreVariance;
        public float PostVariance;
        public float VarianceChange;
    }
}
