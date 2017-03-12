using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct JSONBnetChallengeForm
    {
        public string Type;
        public string Prompt;
        public List<string> Errors;
        public List<JSONBnetChallengeFormInput> Inputs;
    }
}
