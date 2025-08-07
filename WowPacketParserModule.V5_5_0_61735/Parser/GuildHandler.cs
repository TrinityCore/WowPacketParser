using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class GuildHandler
    {
        public static void ReadPetitionSignature(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("Signer", indexes);
            packet.ReadInt32("Choice", indexes);
        }

        [Parser(Opcode.SMSG_PETITION_ALREADY_SIGNED)]
        public static void HandlePetitionAlreadySigned(Packet packet)
        {
            packet.ReadPackedGuid128("SignerGUID");
        }

        [Parser(Opcode.SMSG_OFFER_PETITION_ERROR)]
        public static void HandlePetitionError(Packet packet)
        {
            packet.ReadPackedGuid128("PetitionGUID");
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_LIST)]
        public static void HandlePetitionShowList(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            var counter = packet.ReadUInt32("Counter");
            for (var i = 0; i < counter; i++)
            {
                packet.ReadUInt32("Index");
                packet.ReadUInt32("CharterCost");
                packet.ReadUInt32("CharterEntry");
                packet.ReadUInt32("Unk440");
                packet.ReadUInt32("RequiredSigns");
            }
        }

        [Parser(Opcode.SMSG_PETITION_SHOW_SIGNATURES)]
        public static void HandlePetitionShowSignatures(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadPackedGuid128("Owner");
            packet.ReadPackedGuid128("OwnerWoWAccount");
            packet.ReadInt32("PetitionID");

            var signaturesCount = packet.ReadInt32("SignaturesCount");
            for (int i = 0; i < signaturesCount; i++)
                ReadPetitionSignature(packet, i, "PetitionSignature");
        }
    }
}
