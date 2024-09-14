using System.Linq;
using System.Xml;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class PetHandler
    {
        public static void ReadPetFlags(Packet packet, params object[] idx)
        {
            var petModeFlag = packet.ReadUInt16();
            var reactState = packet.ReadByte();
            var flag = petModeFlag >> 16;
            var commandState = (petModeFlag & flag);

            packet.AddValue("ReactState", (ReactState)reactState, idx);
            packet.AddValue("CommandState", (CommandState)commandState, idx);
            packet.AddValue("Flag", flag, idx);
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

        public static void ReadPetAction(Packet packet, params object[] indexes)
        {
            var action = packet.ReadUInt32();
            var value = action & 0x7FFFFF;
            var type = (action >> 23) & 0x1F;
            var flags = action & 0xF800000;

            switch (type)
            {
                case 1:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                {
                    packet.AddValue("SpellID", StoreGetters.GetName(StoreNameType.Spell, (int)value), indexes);
                    packet.AddValue("Flags", (PetModeFlags)flags, indexes);
                    break;
                }
                case 6:
                {
                    packet.AddValue("ReactState", (ReactState)value, indexes);
                    packet.AddValue("Flags", (PetModeFlags)flags, indexes);
                    break;
                }
                case 7:
                {
                    packet.AddValue("CommandState", (CommandState)value, indexes);
                    packet.AddValue("Flags", (PetModeFlags)flags, indexes);
                    break;
                }
            }
        }

        [Parser(Opcode.CMSG_QUERY_PET_NAME)]
        public static void HandlePetNameQuery(Packet packet)
        {
            packet.ReadPackedGuid128("PetID");
        }

        [Parser(Opcode.SMSG_PET_STABLE_RESULT)]
        public static void HandlePetStableResult(Packet packet)
        {
            packet.ReadByteE<PetStableResult>("Result");
        }

        [Parser(Opcode.SMSG_PET_ACTION_FEEDBACK)]
        public static void HandlePetActionFeedback(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadByteE<PetFeedback>("Response");
        }

        [Parser(Opcode.SMSG_PET_ACTION_SOUND)]
        public static void HandlePetSound(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("Action");
        }

        [Parser(Opcode.SMSG_PET_LEARNED_SPELLS)]
        [Parser(Opcode.SMSG_PET_UNLEARNED_SPELLS)]
        public static void HandlePetSpellsLearnedRemoved(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadPetFlags(packet, "PetModeAndOrders");
        }

        [Parser(Opcode.SMSG_PET_NAME_INVALID)]
        public static void HandlePetNameInvalid(Packet packet)
        {
            packet.ReadByte("Result");
            ReadPetRenameData(packet);
        }

        [Parser(Opcode.SMSG_PET_TAME_FAILURE)]
        public static void HandlePetTameFailure(Packet packet)
        {
            packet.ReadByteE<PetTameFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_SET_PET_SPECIALIZATION)]
        public static void HandleSetPetSpecialization(Packet packet)
        {
            packet.ReadUInt16("SpecID");
        }

        [Parser(Opcode.CMSG_DISMISS_CRITTER)]
        public static void HandleDismissCritter(Packet packet)
        {
            packet.ReadPackedGuid128("CritterGUID");
        }

        [Parser(Opcode.CMSG_PET_ABANDON)]
        public static void HandlePetAbandon(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");

            ReadPetAction(packet, "Action");

            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadVector3("ActionPosition");
        }

        [Parser(Opcode.CMSG_PET_SET_ACTION)]
        public static void HandlePetSetAction(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadUInt32("Index");

            ReadPetAction(packet, "Action");

            var unkBit = packet.ReadBit("UnkBit");

            if (unkBit)
            {
                packet.ReadUInt32("Unk440_1");
                packet.ReadUInt32("Unk440_2");
            }
        }
    }
}
