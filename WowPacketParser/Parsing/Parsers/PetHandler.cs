using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.SMSG_PET_SPELLS)]
        public static void HandlePetSpells(Packet packet)
        {
            var guid64 = packet.ReadUInt64();
            if (guid64 == 0) // Sent when player leaves vehicle - empty packet
                return;

            var guid = new Guid(guid64);
            Console.WriteLine("GUID: " + guid);
            var isPet = guid.GetHighType() == HighGuidType.Pet;

            var family = (CreatureFamily)packet.ReadUInt16();
            Console.WriteLine("Pet Family: " + family); // vehicles -> 0

            var unk1 = packet.ReadUInt32(); // 0
            Console.WriteLine("Unknown 1: " + unk1);

            // Following int8,int8,int16 is sent like int32
            var reactState = packet.ReadByte(); // 1
            Console.WriteLine("React state: " + reactState);

            var commandState = packet.ReadByte(); // 1
            Console.WriteLine("Command state: " + commandState);

            var unk2 = packet.ReadUInt16(); // pets -> 0, vehicles -> 0x800 (2048)
            Console.WriteLine("Unknown 2: " + unk2);

            for (var i = 1; i <= (int)MiscConstants.CreatureMaxSpells + 2; i++) // Read pet/vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadByte();
                var slotid = packet.ReadSByte();
                var spellId = spell16 | spell8;
                if (!isPet) // cleanup vehicle spells (start at 1 instead 8,
                {           // and do not print spells with id 0)
                    slotid -= (int) MiscConstants.PetSpellsOffset - 1;
                    if (spellId == 0)
                        continue;
                }
                Console.WriteLine("Spell " + slotid + ": " + Extensions.SpellLine(spellId));
            }

            var spellCount = packet.ReadByte(); // vehicles -> 0, pets -> != 0. Could this be auras?
            Console.WriteLine("Spell count: " + spellCount);

            for (var i = 0; i < spellCount; i++)
            {
                // Sent as int32
                var spellId = packet.ReadUInt16();
                var active = packet.ReadInt16();
                Console.WriteLine("Spell " + i + ": " + Extensions.SpellLine(spellId) + ", active: " + active);
            }

            var cdCount = packet.ReadByte();
            Console.WriteLine("Cooldown count: " + cdCount);

            for (var i = 0; i < cdCount; i++)
            {
                var spellId = packet.ReadInt32();
                var category = packet.ReadUInt16();
                var cooldown = packet.ReadUInt32();
                var categoryCooldown = packet.ReadUInt32();

                Console.WriteLine("Cooldown: Spell: " + Extensions.SpellLine(spellId) + " category: " + category +
                    " cooldown: " + cooldown + " category cooldown: " + categoryCooldown);
            }
        }

        [Parser(Opcode.SMSG_PET_NAME_QUERY_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            var petNumber = packet.ReadInt32();
            Console.WriteLine("Pet number: " + petNumber);

            var petName = packet.ReadCString();
            if (petName == string.Empty)
            {
                packet.ReadBytes(7); // 0s
                return;
            }
            Console.WriteLine("Pet name: " + petName);

            var time = packet.ReadTime();
            Console.WriteLine("Time: " + time);

            var declined = packet.ReadBoolean();
            Console.WriteLine("Declined: " + declined);

            if (declined)
                for (var i = 0; i < (int)MiscConstants.MaxDeclinedNameCases; i++)
                    Console.WriteLine("Declined name " + i + ": " + packet.ReadCString());
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadGuid("Guid");
            packet.ReadInt32("Unk int");
        }

        [Parser(Opcode.SMSG_PET_LEARNED_SPELL)]
        [Parser(Opcode.SMSG_PET_REMOVED_SPELL)]
        public static void HandlePetSpellsLearnedRemoved(Packet packet)
        {
            Console.WriteLine("Spell: " + Extensions.SpellLine(packet.ReadInt32()));
        }

        [Parser(Opcode.SMSG_PET_ACTION_FEEDBACK)]
        public static void HandlePetActionFeedback(Packet packet)
        {
            byte state = packet.ReadByte("Pet state");
            switch (state)
            {
                //case 1: Pet is dead
                case 2:
                    Console.WriteLine("Spell: " + Extensions.SpellLine(packet.ReadInt32()));
                    break;
                case 3:
                    // Invalid attack target
                    packet.ReadInt32("Unk int32");
                    break;
                //case 4: Charge has no path
                default:
                    Console.WriteLine("Unknown state");
                    break;
            }
        }

        [Parser(Opcode.SMSG_PET_UPDATE_COMBO_POINTS)]
        public static void HandlePetUpdateComboPoints(Packet packet)
        {
            packet.ReadGuid("Guid 1");
            packet.ReadGuid("Guid 2");
            packet.ReadByte("Combo points");
        }

        [Parser(Opcode.SMSG_PET_GUIDS)]
        public static void HandlePetGuids(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadGuid("Guid", i);
        }
    }
}
