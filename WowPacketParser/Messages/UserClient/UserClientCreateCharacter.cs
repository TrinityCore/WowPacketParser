namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCreateCharacter
    {
        public byte FacialHairStyleID;
        public byte FaceID;
        public byte SexID;
        public byte HairStyleID;
        public byte SkinID;
        public byte RaceID;
        public byte ClassID;
        public byte OutfitID;
        public byte HairColorID;
        public int? TemplateSetID; // Optional
        public string Name;
    }
}
