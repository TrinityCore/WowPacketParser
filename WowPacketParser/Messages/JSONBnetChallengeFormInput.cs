using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct JSONBnetChallengeFormInput
    {
        public string Input_id;
        public string Type;
        public string Label;
        public uint Max_length;
        public List<string> Errors;
    }
}
