namespace WowPacketParser.Messages.Other
{
    public unsafe struct JSONBnetChallengeResponse
    {
        public string Error_code;
        public string Challenge_id;
        public JSONBnetChallengeForm Challenge;
        public bool Done;
    }
}
