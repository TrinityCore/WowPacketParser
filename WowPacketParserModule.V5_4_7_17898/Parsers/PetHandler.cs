using System;
using System.Globalization;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.CMSG_QUERY_PET_NAME)]
        public static void HandlePetNameQuery(Packet packet)
        {
            var guid = new byte[8];
            var number = new byte[8];

            number[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            number[3] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            number[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            number[0] = packet.Translator.ReadBit();
            number[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            number[5] = packet.Translator.ReadBit();
            number[2] = packet.Translator.ReadBit();
            number[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(number, 5);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(number, 1);
            packet.Translator.ReadXORByte(number, 7);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(number, 4);
            packet.Translator.ReadXORByte(number, 2);
            packet.Translator.ReadXORByte(number, 0);
            packet.Translator.ReadXORByte(number, 6);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(number, 3);

            var GUID = new WowGuid64(BitConverter.ToUInt64(number, 0));
            var Number = BitConverter.ToUInt64(number, 0);
            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Pet Number", Number);

            // Store temporary name (will be replaced in SMSG_QUERY_PET_NAME_RESPONSE)
            StoreGetters.AddName(GUID, Number.ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.SMSG_QUERY_PET_NAME_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            var hasData = packet.Translator.ReadBit();
            if (!hasData)
            {
                packet.Translator.ReadUInt64("Pet number");
                return;
            }

            packet.Translator.ReadBit("Declined");

            const int maxDeclinedNameCases = 5;
            var declinedNameLen = new int[maxDeclinedNameCases];
            for (var i = 0; i < maxDeclinedNameCases; ++i)
                declinedNameLen[i] = (int)packet.Translator.ReadBits(7);

            var len = packet.Translator.ReadBits(8);

            for (var i = 0; i < maxDeclinedNameCases; ++i)
                if (declinedNameLen[i] != 0)
                    packet.Translator.ReadWoWString("Declined name", declinedNameLen[i], i);

            var petName = packet.Translator.ReadWoWString("Pet name", len);
            packet.Translator.ReadTime("Time");
            var number = packet.Translator.ReadUInt64("Pet number");

            var guidArray = (from pair in StoreGetters.NameDict where Equals(pair.Value, number) select pair.Key).ToList();
            foreach (var guid in guidArray)
                StoreGetters.NameDict[guid] = petName;
        }
    }
}
