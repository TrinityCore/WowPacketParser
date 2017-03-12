using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct JSONBnetCreateChallengeResponse
    {
        public string Challenge_id;
        public string Challenge_url;
    }
}
