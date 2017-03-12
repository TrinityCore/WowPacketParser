namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCharCustomize
    {
        public byte Result;
        public byte HairStyleID;
        public byte SexID;
        public byte FaceID;
        public string CharName;
        public byte HairColorID;
        public byte SkinID;
        public byte FacialHairStyleID;
        public ulong CharGUID;
    }
}
