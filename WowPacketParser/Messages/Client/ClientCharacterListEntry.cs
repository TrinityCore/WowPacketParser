using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCharacterListEntry
    {
        public ulong Guid;
        public string Name;
        public byte ListPosition;
        public byte RaceID;
        public byte ClassID;
        public byte SexID;
        public byte SkinID;
        public byte FaceID;
        public byte HairStyle;
        public byte HairColor;
        public byte FacialHairStyle;
        public byte ExperienceLevel;
        public int ZoneID;
        public int MapID;
        public Vector3 PreloadPos;
        public ulong GuildGUID;
        public uint Flags;
        public uint Flags2;
        public uint Flags3;
        public bool FirstLogin;
        public uint PetCreatureDisplayID;
        public uint PetExperienceLevel;
        public uint PetCreatureFamilyID;
        public bool BoostInProgress;
        public fixed int ProfessionIDs[2];
        public ClientCharacterListItem[/*23*/] InventoryItems;
    }
}
