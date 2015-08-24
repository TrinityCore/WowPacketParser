using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.CMSG_QUERY_PET_NAME)]
        public static void HandlePetNameQuery(Packet packet)
        {
            packet.ReadPackedGuid128("PetID");
        }

        [Parser(Opcode.SMSG_QUERY_PET_NAME_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PetID");

            var hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return;

            var len = packet.ReadBits(8);
            packet.ReadBit("HasDeclined");

            const int maxDeclinedNameCases = 5;
            var declinedNameLen = new int[maxDeclinedNameCases];
            for (var i = 0; i < maxDeclinedNameCases; ++i)
                declinedNameLen[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < maxDeclinedNameCases; ++i)
                packet.ReadWoWString("DeclinedNames", declinedNameLen[i], i);

            packet.ReadTime("Timestamp");
            packet.ReadWoWString("Pet name", len);
        }


        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE)]
        public static void HandlePetSpells(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt16("CreatureFamily");
            packet.ReadInt16("Specialization");
            packet.ReadInt32("TimeLimit");
            packet.ReadInt32("PetModeAndOrders");

            // ActionButtons
            const int maxCreatureSpells = 10;
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet/vehicle spell ids
            {
                var action = packet.ReadInt32();

                if (action >> 26 != 8)
                {
                    var spellID = action & 0xFFFFFF;
                    var slot = (action >> 24) & 0x3F;

                    if (slot >= 8 && slot <= 17)
                        packet.AddValue("Action", slot, i);

                    packet.AddValue("Spell", StoreGetters.GetName(StoreNameType.Spell, spellID), i);
                }
            }

            var int28 = packet.ReadInt32("ActionsCount");
            var int44 = packet.ReadUInt32("CooldownsCount");
            var int60 = packet.ReadUInt32("SpellHistoryCount");

            // Actions
            for (int i = 0; i < int28; i++)
            {
                var action = packet.ReadInt32();
                var spellID = action & 0xFFFFFF;
                var slot = (action >> 24) & 0x3F;

                if (slot == 1 || slot >= 8 && slot <= 17)
                    packet.AddValue("Slot", slot, i);

                packet.AddValue("Spell", StoreGetters.GetName(StoreNameType.Spell, spellID), i);
            }

            // PetSpellCooldown
            for (int i = 0; i < int44; i++)
            {
                packet.ReadInt32("SpellID", i);
                packet.ReadInt32("Duration", i);
                packet.ReadInt32("CategoryDuration", i);
                packet.ReadInt16("Category", i);
            }

            // PetSpellHistory
            for (int i = 0; i < int60; i++)
            {
                packet.ReadInt32("CategoryID", i);
                packet.ReadInt32("RecoveryTime", i);
                packet.ReadSByte("ConsumedCharges", i);
            }
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            var action = (uint)packet.ReadUInt16() + (packet.ReadByte() << 16);
            packet.AddValue("Action", action);
            packet.ReadByte("Slot");
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadVector3("ActionPosition");
        }

        public static void ReadPetRenameData(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt32("PetNumber");

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

        [Parser(Opcode.SMSG_PET_NAME_INVALID)]
        public static void HandlePetNameInvalid(Packet packet)
        {
            packet.ReadByte("Result");
            ReadPetRenameData(packet);
        }

        [Parser(Opcode.CMSG_PET_RENAME)]
        public static void HandlePetRename(Packet packet)
        {
            ReadPetRenameData(packet);
        }

        [Parser(Opcode.SMSG_PET_SPECIALIZATION)]
        public static void HandlePetSpecialization(Packet packet)
        {
             packet.ReadInt16("Specialization");
        }

        [Parser(Opcode.CMSG_LEARN_PET_SPECIALIZATION_GROUP)]
        public static void HandlePetSetSpecialization(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt32("SpecGroupId");
        }

        [Parser(Opcode.SMSG_PET_LEARNED_SPELLS)]
        [Parser(Opcode.SMSG_PET_UNLEARNED_SPELLS)]
        public static void HandlePetSpellsLearnedRemoved(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_PET_GUIDS)]
        public static void HandlePetGuids(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadPackedGuid128("PetGUID", i);
        }

        [Parser(Opcode.CMSG_DISMISS_CRITTER)]
        public static void HandleDismissCritter(Packet packet)
        {
            packet.ReadPackedGuid128("CritterGUID");
        }

        [Parser(Opcode.SMSG_PET_ACTION_SOUND)]
        public static void HandlePetSound(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("Action");
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            var petModeFlag = packet.ReadUInt32();
            packet.AddValue("React state", (ReactState)((petModeFlag >> 8) & 0xFF));
            packet.AddValue("Command state", (CommandState)((petModeFlag >> 16) & 0xFF));
            packet.AddValue("Flag", (petModeFlag & 0xFFFF0000), (PetModeFlags)(petModeFlag & 0xFFFF0000));
        }

        [Parser(Opcode.CMSG_PET_STOP_ATTACK)]
        public static void HandlePetStopAttack(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
        }

        [Parser(Opcode.CMSG_PET_SET_ACTION)]
        public static void HandlePetSetAction(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadUInt32("Index");

            //packet.ReadUInt32("Action");
            var action = (uint)packet.ReadUInt16() + (packet.ReadByte() << 16);
            packet.AddValue("Action", action);
            packet.ReadByte("Unk");
        }

        public static void ReadPetStableInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("PetSlot", indexes);
            packet.ReadInt32("PetNumber", indexes);
            packet.ReadInt32("CreatureID", indexes);
            packet.ReadInt32("DisplayID", indexes);
            packet.ReadInt32("ExperienceLevel", indexes);
            packet.ReadByte("PetFlags", indexes);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("PetName", len, indexes);
        }

        [Parser(Opcode.SMSG_PET_STABLE_LIST)]
        public static void HandlePetStableList(Packet packet)
        {
            packet.ReadPackedGuid128("StableMaster");
            var petCount = packet.ReadInt32("PetCount");
            for (int i = 0; i < petCount; i++)
                ReadPetStableInfo(packet, i, "PetStableInfo");
        }

        [Parser(Opcode.SMSG_PET_ADDED)]
        public static void HandlePetAdded(Packet packet)
        {
            ReadPetStableInfo(packet, "PetStableInfo");
        }

        [Parser(Opcode.SMSG_PET_ACTION_FEEDBACK)]
        public static void HandlePetActionFeedback(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadByteE<PetFeedback>("Response");
        }
    }
}
