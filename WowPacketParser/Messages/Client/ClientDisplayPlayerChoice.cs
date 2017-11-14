using System.Collections.Generic;
using WowPacketParser.Messages.Player.Choice;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDisplayPlayerChoice
    {
        public int ChoiceID;
        public string Question;
        public List<Response> Responses;
    }
}
