using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDisplayPlayerChoice
    {
        public int ChoiceID;
        public string Question;
        public List<PlayerChoiceResponse> Responses;
    }
}
