using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct CharRaceOrFactionChange
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

        [Parser(Opcode.CMSG_CHAR_RACE_OR_FACTION_CHANGE)]
        public static void HandleCharRaceOrFactionChange(Packet packet)
        {
            packet.ReadBit("FactionChange");

            var bits20 = packet.ReadBits(6);

            var bit93 = packet.ReadBit("HasSkinID");
            var bit96 = packet.ReadBit("HasHairColor");
            var bit89 = packet.ReadBit("HasHairStyleID");
            var bit17 = packet.ReadBit("HasFacialHairStyleID");
            var bit19 = packet.ReadBit("HasFaceID");

            packet.ReadPackedGuid128("Guid");
            packet.ReadByte("SexID");
            packet.ReadByte("RaceID");

            packet.ReadWoWString("Name", bits20);

            if (bit93)
                packet.ReadByte("SkinID");

            if (bit96)
                packet.ReadByte("HairColorID");

            if (bit89)
                packet.ReadByte("HairStyleID");

            if (bit17)
                packet.ReadByte("FacialHairStyleID");

            if (bit19)
                packet.ReadByte("FaceID");
        }
    }
}
