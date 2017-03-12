namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GarrisonMission
    {
        public ulong DbID;
        public int MissionRecID;
        public UnixTime OfferTime;
        public UnixTime OfferDuration;
        public UnixTime StartTime;
        public UnixTime TravelDuration;
        public UnixTime MissionDuration;
        public int MissionState;
    }
}
