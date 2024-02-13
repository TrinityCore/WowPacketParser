using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.SQL.Builders;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class PetHandler
    {
        public static (uint, uint) ReadPetAction(Packet packet, WowGuid guid, params object[] indexes)
        {
            bool isPet = guid.GetHighType() == HighGuidType.Pet;
            bool isVehicle = guid.GetHighType() == HighGuidType.Vehicle;
            bool isMinion = guid.GetHighType() == HighGuidType.Creature;

            var packedData = packet.ReadUInt32();
            var spellId = packedData & 0x7FFFFF;
            var slot = ((packedData >> 23) & 0x1F) - 8;

            packet.AddValue("Slot", slot, indexes);

            if (spellId <= 4)
                packet.AddValue("Action", spellId, indexes);
            else
                packet.AddValue("Spell", StoreGetters.GetName(StoreNameType.Spell, (int)spellId), indexes);

            if (!isPet && (isVehicle || (isMinion && slot >= 8)))
                return (slot, spellId);

            return (0, 0);
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
                var (slot, spellId) = ReadPetAction(packet, petGuid, "ActionButtons", i);

                if (spellId == 0)
                    continue;

                var creatureTemplateSpell = new CreatureTemplateSpell
                {
                    CreatureID = petGuid.GetEntry(),
                    Index = (byte)slot,
                    Spell = spellId
                };
                Storage.CreatureTemplateSpells.Add(creatureTemplateSpell);
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
    }
}
