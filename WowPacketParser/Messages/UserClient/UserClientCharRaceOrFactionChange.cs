namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCharRaceOrFactionChange
    {
        public byte? HairColorID; // Optional
        public byte RaceID;
        public byte SexID;
        public byte? SkinID; // Optional
        public byte? FacialHairStyleID; // Optional
        public ulong Guid;
        public bool FactionChange;
        public string Name;
        public byte? FaceID; // Optional
        public byte? HairStyleID; // Optional
    }
}
