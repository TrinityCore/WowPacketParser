using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct JSONBnetChallengeResponse
    {
        public string Error_code;
        public string Challenge_id;
        public JSONBnetChallengeForm Challenge;
        public bool Done;
    }
}
