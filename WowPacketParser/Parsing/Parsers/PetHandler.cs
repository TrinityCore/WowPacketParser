using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQLStore;
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

            var family = (CreatureFamily)packet.ReadUInt16();
            Console.WriteLine("Pet Family: " + family);

            var unk1 = packet.ReadUInt32();
            Console.WriteLine("Unknown 1: " + unk1);

            var reactState = packet.ReadByte();
            Console.WriteLine("React state: " + reactState);

            var commandState = packet.ReadByte();
            Console.WriteLine("Command state: " + commandState);

            var unk2 = packet.ReadUInt16();
            Console.WriteLine("Unknow 2: " + unk1);

            for (var i = 1; i <= (int)MiscConstants.CreatureMaxSpells + 2; i++) // Read vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadByte();
                var slotid = packet.ReadByte();
                var spellId = spell16 | spell8;
                slotid -= (int)MiscConstants.PetSpellsOffset;
                Console.WriteLine("Spell " + slotid + ": " + spellId);
            }

            var spellCount = packet.ReadByte(); // always 0?
            Console.WriteLine("Spell count: " + spellCount);

            for (var i = 0; i < spellCount; i++)
            {
                var spellId = packet.ReadUInt16();
                Console.WriteLine("Spell " + i + ": " + spellId);
                var active = packet.ReadUInt16();
                Console.WriteLine("Active: " + active);
            }

            var cdCount = packet.ReadByte();
            Console.WriteLine("Cooldown count: " + cdCount);

            for (var i = 0; i < cdCount; i++)
            {
                var spellId = packet.ReadUInt32();
                var category = packet.ReadUInt16();
                var cooldown = packet.ReadUInt32();
                var categoryCooldown = packet.ReadUInt32();

                Console.WriteLine("Cooldown: Spell: " + spellId + " category: " + category +
                    " cooldown: " + cooldown + " category cooldown: " + categoryCooldown);
            }
        }

        [Parser(Opcode.SMSG_PET_NAME_QUERY_RESPONSE)]
        public static void HandlePetNameQueryResponce(Packet packet)
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
                for (int i = 0; i < (int)MiscConstants.MaxDeclinedNameCases; i++)
                    Console.WriteLine("Declined name " + i + ": " + packet.ReadCString());
        }
    }
}
