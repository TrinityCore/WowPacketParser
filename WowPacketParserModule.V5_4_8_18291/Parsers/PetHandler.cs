using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.CMSG_PET_NAME_QUERY)]
        public static void HandlePetNameQuery(Packet packet)
        {
            var guid = new byte[8];
            var number = new byte[8];

            number[0] = packet.ReadBit();
            number[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            number[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            number[3] = packet.ReadBit();
            number[6] = packet.ReadBit();
            number[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            number[1] = packet.ReadBit();
            number[4] = packet.ReadBit();

            packet.ReadXORByte(number, 2);
            packet.ReadXORByte(number, 1);
            packet.ReadXORByte(number, 0);
            packet.ReadXORByte(number, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(number, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(number, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(number, 3);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(number, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);

            var GUID = new Guid(BitConverter.ToUInt64(number, 0));
            var Number = BitConverter.ToUInt64(number, 0);
            packet.WriteGuid("Guid", guid);
            packet.WriteLine("Pet Number: {0}", Number);

            // Store temporary name (will be replaced in SMSG_PET_NAME_QUERY_RESPONSE)
            StoreGetters.AddName(GUID, Number.ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_PET_NAME_QUERY_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            var hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt64("Pet number");
                return;
            }

            packet.ReadBit("Declined");

            const int maxDeclinedNameCases = 5;
            var declinedNameLen = new int[maxDeclinedNameCases];
            for (var i = 0; i < maxDeclinedNameCases; ++i)
                declinedNameLen[i] = (int)packet.ReadBits(7);

            var len = packet.ReadBits(8);

            for (var i = 0; i < maxDeclinedNameCases; ++i)
                if (declinedNameLen[i] != 0)
                    packet.ReadWoWString("Declined name", declinedNameLen[i], i);

            var petName = packet.ReadWoWString("Pet name", len);
            packet.ReadTime("Time");
            var number = packet.ReadUInt64("Pet number");

            var guidArray = (from pair in StoreGetters.NameDict where Equals(pair.Value, number) select pair.Key).ToList();
            foreach (var guid in guidArray)
                StoreGetters.NameDict[guid] = petName;
        }

        [Parser(Opcode.SMSG_PET_SPELLS)]
        public static void HandlePetSpells(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var bits44 = (int)packet.ReadBits(21);
            var bits28 = (int)packet.ReadBits(22);
            guid[2] = packet.ReadBit();
            var bits10 = (int)packet.ReadBits(20);
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            const int maxCreatureSpells = 10;
            var spells = new List<uint>(maxCreatureSpells);
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet/vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadByte();
                var spellId = spell16 + (spell8 << 16);
                var slot = packet.ReadByte();

                var s = new StringBuilder("[");
                s.Append(i).Append("] ").Append("Spell/Action: ");
                if (spellId <= 4)
                    s.Append(spellId);
                else
                    s.Append(StoreGetters.GetName(StoreNameType.Spell, spellId));
                s.Append(" slot: ").Append(slot);
                packet.WriteLine(s.ToString());
            }

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt16("Int14", i);
                packet.ReadInt32("Int14", i);
            }

            packet.ReadXORByte(guid, 2);
            for (var i = 0; i < bits28; ++i)
                packet.ReadInt32("IntED", i);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadInt16("Int42");

            for (var i = 0; i < bits44; ++i)
            {
                packet.ReadInt32("Int48", i);
                packet.ReadByte("Byte48", i);
                packet.ReadInt32("Int48", i);
            }

            packet.ReadInt16("Int40");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadInt32("Int24");
            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("Int20");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadUInt32("Data");
            packet.ReadSingle("Y");
            packet.ReadSingle("Z");
            packet.ReadSingle("X");

            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[1] = packet.ReadBit();

            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 0);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }
    }
}