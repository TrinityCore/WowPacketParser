using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct JSONBnetChallengeSubmission
    {
        public List<JSONBnetChallengeSubmissionInput> Inputs;
    }
}
