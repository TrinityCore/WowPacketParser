using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Storing;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class PetHandler
    {
        static class Constants
        {
            public const int MaxDeclinedNameCases = 5;
            public const int CreatureMaxSpells = 8;
            public const int PetSpellsOffset = 8;
        }

        [Parser(Opcode.SMSG_PET_SPELLS)]
        public static void HandlePetSpells(Packet packet)
        {
            var guid64 = packet.ReadUInt64();
            if (guid64 == 0) // Sent when player leaves vehicle - empty packet
                return;

            var guid = new Guid(guid64);
            Console.WriteLine("GUID: " + guid);

            var family = packet.ReadInt16();
            Console.WriteLine("Pet Family: " + family);

            var unk1 = packet.ReadUInt32();
            Console.WriteLine("Unknown 1: " + unk1);

            var reactState = packet.ReadSByte();
            Console.WriteLine("React state: " + reactState);

            var commandState = packet.ReadSByte();
            Console.WriteLine("Command state: " + commandState);

            var unk2 = packet.ReadUInt16();
            Console.WriteLine("Unknow 2: " + unk1);

            for (var i = 1; i <= Constants.CreatureMaxSpells + 1; i++) // Read vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadSByte();
                var spellid = spell16 | spell8;
                var slotid = packet.ReadSByte();
                slotid -= Constants.PetSpellsOffset;
                Console.WriteLine("Spell " + slotid + ": " + spellid);
            }

            var unk3 = packet.ReadSByte(); // always 0?
            Console.WriteLine("Unknown 3: " + unk3);

            var intcount = packet.ReadSByte();
            Console.WriteLine("Int32 count: " + intcount);

            for (var i = 0; i < intcount; i++)
            {
                var unk4 = packet.ReadUInt32();
                Console.WriteLine("Unknown 4 " + i + ": " + unk4);
            }

            var cdcount = packet.ReadUInt16();
            Console.WriteLine("Cooldown count: " + cdcount);

            // not finished
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
            Console.WriteLine("Declined? " + (declined ? "yes" : "no"));

            if (declined)
                for (int i = 0; i < Constants.MaxDeclinedNameCases; i++)
                    Console.WriteLine("Declined name " + i + ": " + packet.ReadCString());
        }
    }
}
