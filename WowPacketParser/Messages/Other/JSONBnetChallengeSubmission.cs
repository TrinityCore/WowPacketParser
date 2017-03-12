using System.Collections.Generic;

namespace WowPacketParser.Messages.Other
{
    public unsafe struct JSONBnetChallengeSubmission
    {
        public List<JSONBnetChallengeSubmissionInput> Inputs;
    }
}
