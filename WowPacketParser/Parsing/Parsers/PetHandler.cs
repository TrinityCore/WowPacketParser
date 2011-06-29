using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Storing;
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

            for (var i = 0; i < 10; i++) // Read vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadSByte();
                var spellid = spell16 | spell8;
                var slotid = packet.ReadSByte();
                slotid -= 8 - 1;
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
    }
}
