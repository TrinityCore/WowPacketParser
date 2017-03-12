namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCharCustomize
    {
        public byte HairStyleID;
        public byte FaceID;
        public ulong CharGUID;
        public byte SexID;
        public string CharName;
        public byte HairColorID;
        public byte FacialHairStyleID;
        public byte SkinID;
    }
}
