using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct CharCustomize
    {
        public byte HairStyleID;
        public byte FaceID;
        public ulong CharGUID;
        public byte SexID;
        public string CharName;
        public byte HairColorID;
        public byte FacialHairStyleID;
        public byte SkinID;

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("New Name");
            packet.ReadByteE<Gender>("Gender");
            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientCharCustomize602(Packet packet)
        {
            packet.ReadPackedGuid128("CharGUID");

            packet.ReadByte("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");

            packet.ResetBitReader();

            var bits19 = packet.ReadBits(6);
            packet.ReadWoWString("CharName", bits19);
        }
    }
}
