using System;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL.Builders;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V6_0_2_19033.Parsers;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class PetHandler
    {
        public static (uint slot, uint spellID) ReadPetAction(Packet packet, params object[] indexes)
        {
            var action = packet.ReadUInt32();
            var spellID = action & 0x7FFFFF;
            var slot = (action >> 23);

            packet.AddValue("Action", slot, indexes);
            packet.AddValue("SpellID", StoreGetters.GetName(StoreNameType.Spell, (int)spellID), indexes);

            return (slot, spellID);
        }

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

        public static void ReadPetFlags344(Packet packet, params object[] idx)
        {
            packet.ReadByteE<CommandState>("CommandState");
            packet.ReadByte("Flag");
            packet.ReadByteE<ReactState>("ReactState");
        }

        public static void ReadPetSpellCooldownData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SpellID", idx);
            packet.ReadInt32("Duration", idx);
            packet.ReadInt32("CategoryDuration", idx);
            packet.ReadSingle("ModRate", idx);
            packet.ReadInt16("Category", idx);
        }

        public static void ReadPetSpellHistoryData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("CategoryID", idx);
            packet.ReadInt32("RecoveryTime", idx);
            packet.ReadSingle("ChargeModRate", idx);
            packet.ReadSByte("ConsumedCharges", idx);
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

        public static (uint slot, uint spellID) ReadPetAction344(Packet packet, params object[] indexes)
        {
            var action = packet.ReadUInt32();
            var value = action & 0x007FFFFF;
            var slot = (action & 0xFF800000) >> 23;

            switch (slot)
            {
                case 0:
                {
                    packet.AddValue("SpellID", value, indexes);
                    break;
                }
                case 0x1:
                {
                    packet.AddValue("PassiveSpell", StoreGetters.GetName(StoreNameType.Spell, (int)value), indexes);
                    break;
                }
                case 0x101:
                {
                    packet.AddValue("ManualSpell", StoreGetters.GetName(StoreNameType.Spell, (int)value), indexes);
                    break;
                }
                case 0x181:
                {
                    packet.AddValue("AutoCastSpell", StoreGetters.GetName(StoreNameType.Spell, (int)value), indexes);
                    break;
                }
                case 6:
                {
                    packet.AddValue("ReactState", (ReactState)value, indexes);
                    break;
                }
                case 7:
                {
                    packet.AddValue("CommandState", (CommandState)value, indexes);
                    break;
                }
                default:
                {
                    packet.AddValue("UnknonwCase", value, indexes);
                    break;
                }
            }

            return (slot, value);
        }

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetSpells(Packet packet)
        {
            var petGuid = packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt16("CreatureFamily");
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
                V6_0_2_19033.Parsers.PetHandler.ReadPetAction(packet, i, "Actions");

            for (int i = 0; i < cooldownsCount; i++)
                ReadPetSpellCooldownData(packet, i, "PetSpellCooldown");

            for (int i = 0; i < spellHistoryCount; i++)
                ReadPetSpellHistoryData(packet, i, "PetSpellHistory");
        }

        [Parser(Opcode.CMSG_QUERY_PET_NAME, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetNameQuery(Packet packet)
        {
            packet.ReadPackedGuid128("PetID");
        }

        [Parser(Opcode.SMSG_PET_STABLE_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetStableResult(Packet packet)
        {
            packet.ReadUInt32E<PetStableResult>("Result");
        }

        [Parser(Opcode.SMSG_PET_ACTION_FEEDBACK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetActionFeedback(Packet packet)
        {
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadByteE<PetFeedback>("Response");
        }

        [Parser(Opcode.SMSG_PET_ACTION_SOUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetSound(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("Action");
        }

        [Parser(Opcode.SMSG_PET_LEARNED_SPELLS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_PET_UNLEARNED_SPELLS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetSpellsLearnedRemoved(Packet packet)
        {
            var count = packet.ReadUInt32("Spell Count");

            for (var i = 0; i < count; ++i)
                packet.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_PET_MODE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            ReadPetFlags344(packet, "PetModeAndOrders");
        }

        [Parser(Opcode.SMSG_PET_NAME_INVALID, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetNameInvalid(Packet packet)
        {
            packet.ReadByte("Result");
            ReadPetRenameData(packet);
        }

        [Parser(Opcode.SMSG_PET_TAME_FAILURE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetTameFailure(Packet packet)
        {
            packet.ReadByteE<PetTameFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_SET_PET_SPECIALIZATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetPetSpecialization(Packet packet)
        {
            packet.ReadUInt16("SpecID");
        }

        [Parser(Opcode.CMSG_DISMISS_CRITTER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleDismissCritter(Packet packet)
        {
            packet.ReadPackedGuid128("CritterGUID");
        }

        [Parser(Opcode.CMSG_PET_ABANDON, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetAbandon(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
        }

        [Parser(Opcode.CMSG_PET_ACTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetAction(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");

            ReadPetAction344(packet, "Action");

            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadVector3("ActionPosition");
        }

        [Parser(Opcode.CMSG_PET_SET_ACTION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetSetAction(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadUInt32("Index");

            ReadPetAction344(packet, "Action");

            var unkBit = packet.ReadBit("UnkBit");

            if (unkBit)
            {
                packet.ReadUInt32("Unk440_1");
                packet.ReadUInt32("Unk440_2");
            }
        }

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetSpells344(Packet packet)
        {
            var petGuid = packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt16("CreatureFamily");
            packet.ReadInt16("Specialization");
            packet.ReadInt32("TimeLimit");

            ReadPetFlags344(packet, "PetModeAndOrders");

            const int maxCreatureSpells = 10;
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet / vehicle spell ids
            {
                var (slot, spellId) = ReadPetAction344(packet, "ActionButtons", i);

                if (spellId == 0)
                    continue;

                if (slot == 7 && spellId != 2 || slot == 6 && spellId < 10)
                    continue;

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
                ReadPetAction344(packet, i, "Actions");

            for (int i = 0; i < cooldownsCount; i++)
                ReadPetSpellCooldownData(packet, i, "PetSpellCooldown");


            for (int i = 0; i < spellHistoryCount; i++)
                ReadPetSpellHistoryData(packet, i, "PetSpellHistory");
        }

        [Parser(Opcode.CMSG_PET_CANCEL_AURA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetCancelAura(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.CMSG_PET_RENAME, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetRename(Packet packet)
        {
            ReadPetRenameData(packet);
        }

        [Parser(Opcode.CMSG_PET_SPELL_AUTOCAST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetSpellAutocast(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ResetBitReader();
            packet.ReadBit("AutocastEnabled");
        }

        [Parser(Opcode.CMSG_REQUEST_STABLED_PETS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRequestStabledPets(Packet packet)
        {
            packet.ReadPackedGuid128("StableMaster");
        }

        [Parser(Opcode.CMSG_SET_PET_SLOT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleSetPetSlot(Packet packet)
        {
            packet.ReadUInt32("PetNumber");
            packet.ReadByte("DestSlot");
            packet.ReadPackedGuid128("StableMaster");
        }

        [Parser(Opcode.SMSG_PET_DISMISS_SOUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetDismissSound(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("CreatureDisplayInfoID");
            packet.ReadVector3("ModelPosition");
        }

        [Parser(Opcode.SMSG_PET_GUIDS, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetGuids(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadPackedGuid128("PetGUID", i);
        }

        [Parser(Opcode.SMSG_PET_NEWLY_TAMED)]
        public static void HandlePetNewlyTamed(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ResetBitReader();
            packet.ReadBit("SuppressLevelUpAnim");
        }

        [Parser(Opcode.CMSG_REQUEST_PET_INFO, ClientVersionBuild.V3_4_4_59817)]
        public static void HandlePetNull(Packet packet)
        {
        }
    }
}
