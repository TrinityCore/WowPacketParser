using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            var charsCount = packet.ReadUInt32("Characters Count");
            var restrictionsCount = packet.ReadUInt32("FactionChangeRestrictionsCount");

            for (uint i = 0; i < charsCount; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);

                packet.ReadByte("ListPosition", i);
                packet.ReadEnum<Race>("RaceID", TypeCode.Byte, i);
                packet.ReadEnum<Class>("ClassID", TypeCode.Byte, i);
                packet.ReadByte("SexID", i);
                packet.ReadByte("SkinID", i);
                packet.ReadByte("FaceID", i);
                packet.ReadByte("HairStyle", i);
                packet.ReadByte("HairColor", i);
                packet.ReadByte("FacialHairStyle", i);
                packet.ReadByte("ExperienceLevel", i);
                packet.ReadUInt32("ZoneID", i);
                packet.ReadUInt32("MapID", i);
                
                packet.ReadVector3("PreloadPos", i);

                packet.ReadPackedGuid128("GuildGUID", i);

                packet.ReadUInt32("Flags", i);
                packet.ReadUInt32("Flags2", i);
                packet.ReadUInt32("Flags3", i);
                packet.ReadUInt32("PetCreatureDisplayID", i);
                packet.ReadUInt32("PetExperienceLevel", i);
                packet.ReadUInt32("PetCreatureFamilyID", i);

                packet.ReadUInt32("ProfessionIDs", i, 0);
                packet.ReadUInt32("ProfessionIDs", i, 1);

                for (uint j = 0; j < 23; ++j)
                {
                    packet.ReadUInt32("InventoryItem DisplayID", i, j);
                    packet.ReadUInt32("InventoryItem DisplayEnchantID", i, j);
                    packet.ReadEnum<InventoryType>("InventoryItem InvType", TypeCode.Byte, i, j);
                }

                packet.ResetBitReader();
                var nameLength = packet.ReadBits("Character Name Length", 6, i);
                packet.ReadBit("FirstLogin", i);
                packet.ReadBit("BoostInProgress", i);
                packet.ReadWoWString("Character Name", nameLength, i);
            }

            for (var i = 0; i < restrictionsCount; ++i)
            {
                packet.ReadUInt32("FactionChangeRestriction Mask", i);
                packet.ReadByte("FactionChangeRestriction RaceID", i);
            }
        }
    }
}
