using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientChangeGdfPVPRating
    {
        public float Variance;
        public float Rating;
        public int Bracket;
    }
}
