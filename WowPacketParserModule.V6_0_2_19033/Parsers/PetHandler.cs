using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.CMSG_PET_NAME_QUERY)]
        public static void HandlePetNameQuery(Packet packet)
        {
            packet.ReadPackedGuid128("PetID");
        }

        [Parser(Opcode.SMSG_PET_NAME_QUERY_RESPONSE)]
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


        [Parser(Opcode.SMSG_PET_SPELLS)]
        public static void HandlePetSpells(Packet packet)
        {
            packet.ReadPackedGuid128("PetGUID");
            packet.ReadInt16("CreatureFamily");
            packet.ReadInt16("Specialization");
            packet.ReadInt32("TimeLimit");
            packet.ReadInt32("PetModeAndOrders");

            // ActionButtons
            const int maxCreatureSpells = 10;
            var spells = new List<uint>(maxCreatureSpells);
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet/vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadByte();
                var spellId = spell16 + (spell8 << 16);
                var slot = packet.ReadByte();

                if (spellId <= 4)
                    packet.AddValue("Action", spellId, i);
                else
                    packet.AddValue("Spell", StoreGetters.GetName(StoreNameType.Spell, spellId), i);
                packet.AddValue("Slot", slot, i);
            }

            var int28 = packet.ReadInt32("ActionsCount");
            var int44 = packet.ReadUInt32("CooldownsCount");
            var int60 = packet.ReadUInt32("SpellHistoryCount");

            // Actions
            for (int i = 0; i < int28; i++)
                packet.ReadInt32("Actions", i);

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
    }
}
