namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerChoiceResponse
    {
        public int ResponseID;
        public string Answer;
        public string Description;
        public int ChoiceArtFileID;
        public PlayerChoiceResponseReward? Reward; // Optional
    }
}
