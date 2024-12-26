using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
                packet.ReadTime64("Timestamp");
            else
                packet.ReadTime("Timestamp");

            packet.ReadWoWString("Pet name", len);
        }

        public static (uint slot, uint spellID) ReadPetAction(Packet packet, params object[] indexes)
        {
            var action = packet.ReadUInt32();
            var spellID = action & 0xFFFFFF;
            var slot = (action >> 24);

            packet.AddValue("Action", slot, indexes);
            packet.AddValue("SpellID", StoreGetters.GetName(StoreNameType.Spell, (int)spellID), indexes);

            return (slot, spellID);
        }

        public static void ReadPetFlags(Packet packet, params object[] indexes)
        {
            var petModeFlag = packet.ReadUInt32("PetModeAndOrders");
            var reactState = (petModeFlag >> 0) & 0xFF;
            var commandState = (petModeFlag >> 8) & 0xFF;
            var flag = petModeFlag >> 16;

            packet.AddValue("ReactState", (ReactState)reactState, indexes);
            packet.AddValue("CommandState", (CommandState)commandState, indexes);
            packet.AddValue("Flag", flag, indexes);
        }

        public static void ReadPetSpellCooldownData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpellID", idx);
            packet.ReadInt32("Duration", idx);
            packet.ReadInt32("CategoryDuration", idx);
            packet.ReadInt16("Category", idx);
        }

        public static void ReadPetSpellHistoryData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("CategoryID", idx);
            packet.ReadInt32("RecoveryTime", idx);
            packet.ReadSByte("ConsumedCharges", idx);
        }

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE)]
        public static void HandlePetSpells(Packet packet)
        {
            var petGuid = packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt16<CreatureFamilyId>("CreatureFamily");
            packet.ReadInt16("Specialization");
            packet.ReadInt32("TimeLimit");
            ReadPetFlags(packet, "PetModeAndOrders");

            const int maxCreatureSpells = 10;
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet / vehicle spell ids
            {
                var (slot, spellId) = ReadPetAction(packet, "ActionButtons", i);

                if (spellId == 0)
                    continue;

                if (slot == 7 && spellId != 2 || slot == 6 && spellId < 10)
                    continue;

                var creatureTemplateSpell = new CreatureTemplateSpell
                {
                    CreatureID = petGuid.GetEntry(),
                    Index = (byte)(i - 1),
                    Spell = spellId
                };
                Storage.CreatureTemplateSpells.Add(creatureTemplateSpell);

                // pets do not have npc entry available in sniff - skip
                if (petGuid.GetHighType() == HighGuidType.Pet)
                    continue;

                var operationName = "";
                if (slot == 7 && spellId == 2)
                    operationName = "Attack";
                else
                    operationName = StoreGetters.GetName(StoreNameType.Spell, (int)spellId, false);

                var potentialKey = (int)(petGuid.GetEntry() * 100 + CreatureSpellList.ConvertDifficultyToIdx(CoreParsers.MovementHandler.CurrentDifficultyID));
                if (Storage.CreatureSpellLists.Where(p => p.Item1.Id == potentialKey && p.Item1.SpellId == spellId).SingleOrDefault() == null)
                    Storage.CreatureSpellLists.Add(new CreatureSpellList
                    {
                        Id = potentialKey,
                        Position = i,
                        SpellId = (int)spellId,
                        Comments = StoreGetters.GetName(StoreNameType.Unit, (int)petGuid.GetEntry(), false) + " - " + operationName
                    });
            }
            var actionsCount = packet.ReadInt32("ActionsCount");
            var cooldownsCount = packet.ReadUInt32("CooldownsCount");
            var spellHistoryCount = packet.ReadUInt32("SpellHistoryCount");

            for (int i = 0; i < actionsCount; i++)
                ReadPetAction(packet, i, "Actions");

            for (int i = 0; i < cooldownsCount; i++)
                ReadPetSpellCooldownData(packet, i, "PetSpellCooldown");

            for (int i = 0; i < spellHistoryCount; i++)
                ReadPetSpellHistoryData(packet, i, "PetSpellHistory");
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");

            ReadPetAction(packet, "Action");

            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadVector3("ActionPosition");
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
            ReadPetFlags(packet, "PetMode");
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

            ReadPetAction(packet, "Action");
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

        [Parser(Opcode.CMSG_SET_PET_SLOT)]
        public static void HandleSetPetSlot(Packet packet)
        {
            packet.ReadInt32("PetNumber");
            packet.ReadByte("DestSlot");
            packet.ReadPackedGuid128("StableMaster");
        }

        [Parser(Opcode.SMSG_PET_SLOT_UPDATED)]
        public static void HandlePetSlotUpdated(Packet packet)
        {
            packet.ReadInt32("PetNumberA");
            packet.ReadInt32("PetSlotA");
            packet.ReadInt32("PetNumberB");
            packet.ReadInt32("PetSlotB");
        }

        [Parser(Opcode.SMSG_PET_STABLE_RESULT)]
        public static void HandlePetStableResult(Packet packet)
        {
            packet.ReadByteE<PetStableResult>("Result");
        }

        [Parser(Opcode.CMSG_REQUEST_STABLED_PETS)]
        public static void HandleRequestStabledPets(Packet packet)
        {
            packet.ReadPackedGuid128("StableMaster");
        }

        [Parser(Opcode.SMSG_SET_PET_SPECIALIZATION)]
        public static void HandleSetPetSpecialization(Packet packet)
        {
            packet.ReadUInt16("SpecID");
        }
    }
}
