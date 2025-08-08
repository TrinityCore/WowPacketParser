using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;


namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class PetHandler
    {
        public static void ReadPetFlags(Packet packet, params object[] idx)
        {
            packet.ReadByteE<CommandState>("CommandState");
            packet.ReadByte("Flag");
            packet.ReadByteE<ReactState>("ReactState");
        }

        public static void ReadPetRenameData(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt32("PetNumber");

            packet.ResetBitReader();
            var bits20 = packet.ReadBits(8);

            var bit149 = packet.ReadBit("HasDeclinedNames");
            if (bit149)
            {
                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("DeclinedNames", count[i], i);
            }

            packet.ReadWoWString("NewName", bits20);
        }

        [Parser(Opcode.SMSG_PET_NEWLY_TAMED)]
        public static void HandlePetNewlyTamed(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ResetBitReader();
            packet.ReadBit("SuppressLevelUpAnim");
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadPetFlags(packet, "PetModeAndOrders");
        }

        [Parser(Opcode.SMSG_PET_STABLE_RESULT)]
        public static void HandlePetStableResult(Packet packet)
        {
            packet.ReadUInt32E<PetStableResult>("Result");
        }

        [Parser(Opcode.SMSG_SET_PET_SPECIALIZATION)]
        public static void HandleSetPetSpecialization(Packet packet)
        {
            packet.ReadUInt16("SpecID");
        }

        [Parser(Opcode.SMSG_PET_ACTION_SOUND)]
        public static void HandlePetSound(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("Action");
        }

        [Parser(Opcode.SMSG_PET_DISMISS_SOUND)]
        public static void HandlePetDismissSound(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("CreatureDisplayInfoID");
            packet.ReadVector3("ModelPosition");
        }

        [Parser(Opcode.SMSG_PET_TAME_FAILURE)]
        public static void HandlePetTameFailure(Packet packet)
        {
            packet.ReadByteE<PetTameFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_PET_NAME_INVALID)]
        public static void HandlePetNameInvalid(Packet packet)
        {
            packet.ReadByte("Result");
            ReadPetRenameData(packet);
        }

        [Parser(Opcode.SMSG_PET_GUIDS)]
        public static void HandlePetGuids(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadPackedGuid128("PetGUID", i);
        }
    }
}
