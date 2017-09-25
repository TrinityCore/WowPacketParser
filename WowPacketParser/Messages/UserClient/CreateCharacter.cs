using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct CreateCharacter
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

        [Parser(Opcode.CMSG_CREATE_CHARACTER, ClientVersionBuild.Zero, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadByteE<Race>("Race");
            packet.ReadByteE<Class>("Class");
            packet.ReadByteE<Gender>("Gender");
            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
            packet.ReadByte("Outfit Id");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleClientCharCreate530(Packet packet)
        {
            packet.ReadByte("Hair Style");
            packet.ReadByte("Face");
            packet.ReadByte("Facial Hair");
            packet.ReadByte("Hair Color");
            packet.ReadByteE<Race>("Race");
            packet.ReadByteE<Class>("Class");
            packet.ReadByte("Skin");
            packet.ReadByteE<Gender>("Gender");
            packet.ReadByte("Outfit Id");

            var nameLength = packet.ReadBits(6);
            var unk = packet.ReadBit("unk");
            packet.ReadWoWString("Name", (int)nameLength);
            if (unk)
                packet.ReadUInt32("unk20");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientCharCreate542(Packet packet)
        {
            packet.ReadByte("Hair Style");
            packet.ReadByteE<Gender>("Gender");
            packet.ReadByte("Skin");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
            packet.ReadByteE<Class>("Class");
            packet.ReadByteE<Race>("Race");
            packet.ReadByte("Face");
            packet.ReadByte("Outfit Id");

            var unk = packet.ReadBit("unk");
            var nameLength = packet.ReadBits(6);
            packet.ReadWoWString("Name", (int)nameLength);
            if (unk)
                packet.ReadUInt32("unk20");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleClientCharCreate602(Packet packet)
        {
            var bits29 = packet.ReadBits(6);
            var bit24 = packet.ReadBit();

            packet.ReadByteE<Race>("RaceID");
            packet.ReadByteE<Class>("ClassID");
            packet.ReadByteE<Gender>("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("FaceID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("OutfitID");

            packet.ReadWoWString("Name", bits29);

            if (bit24)
                packet.ReadInt32("TemplateSetID");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER, ClientVersionBuild.V7_0_3_22248)]
        public static void HandleClientCharCreate703(Packet packet)
        {
            var nameLen = packet.ReadBits(6);
            var hasTemplateSet = packet.ReadBit();

            packet.ReadByteE<Race>("RaceID");
            packet.ReadByteE<Class>("ClassID");
            packet.ReadByteE<Gender>("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("FaceID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("OutfitID");

            for (uint i = 0; i < 3; ++i)
                packet.ReadByte("CustomDisplay", i);

            packet.ReadWoWString("Name", nameLen);

            if (hasTemplateSet)
                packet.ReadInt32("TemplateSetID");
        }
    }
}
