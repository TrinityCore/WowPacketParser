using System.Collections.Generic;
using WowPacketParser.Messages.Player;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDisplayPlayerChoice
    {
        public int ChoiceID;
        public string Question;
        public List<PlayerChoiceResponse> Responses;
    }
}
