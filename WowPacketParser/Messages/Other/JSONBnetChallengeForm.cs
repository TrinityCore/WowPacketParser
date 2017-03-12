using System.Collections.Generic;

namespace WowPacketParser.Messages.Other
{
    public unsafe struct JSONBnetChallengeForm
    {
        public string Type;
        public string Prompt;
        public List<string> Errors;
        public List<JSONBnetChallengeFormInput> Inputs;
    }
}
