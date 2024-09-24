using System.Linq;
using System.Xml;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL.Builders;
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

        public static (uint slot, uint spellID) ReadPetAction(Packet packet, params object[] indexes)
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

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE)]
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

                // pets do not have npc entry available in sniff - skip
                if (petGuid.GetHighType() == HighGuidType.Pet)
                    continue;

                var operationName = "";
                if (slot == 7 && spellId == 2)
                    operationName = "Attack";
                else
                    operationName = StoreGetters.GetName(StoreNameType.Spell, (int)spellId, false);

                var potentialKey = (int)(petGuid.GetEntry() * 100 + 5 + CoreParsers.MovementHandler.CurrentDifficultyID);
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

        [Parser(Opcode.CMSG_PET_CANCEL_AURA)]
        public static void HandlePetCancelAura(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.CMSG_PET_RENAME)]
        public static void HandlePetRename(Packet packet)
        {
            ReadPetRenameData(packet);
        }

        [Parser(Opcode.CMSG_PET_SPELL_AUTOCAST)]
        public static void HandlePetSpellAutocast(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ResetBitReader();
            packet.ReadBit("AutocastEnabled");
        }

        [Parser(Opcode.CMSG_REQUEST_STABLED_PETS)]
        public static void HandleRequestStabledPets(Packet packet)
        {
            packet.ReadPackedGuid128("StableMaster");
        }

        [Parser(Opcode.CMSG_SET_PET_SLOT)]
        public static void HandleSetPetSlot(Packet packet)
        {
            packet.ReadUInt32("PetNumber");
            packet.ReadByte("DestSlot");
            packet.ReadPackedGuid128("StableMaster");
        }

        [Parser(Opcode.CMSG_REQUEST_PET_INFO)]
        public static void HandlePetNull(Packet packet)
        {
        }
    }
}
