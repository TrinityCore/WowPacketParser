using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_7_18019.Enums;
using Guid = WowPacketParser.Misc.WowGuid;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.CMSG_PET_NAME_QUERY)]
        public static void HandlePetNameQuery(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[1] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Pet Guid", guid1);
            packet.WriteGuid("Pet Number", guid2);

            var PetGuid = new Guid(BitConverter.ToUInt64(guid1, 0));
            var PetNumberGuid = new Guid(BitConverter.ToUInt64(guid2, 0));
            var PetNumber = PetNumberGuid.GetEntry().ToString(CultureInfo.InvariantCulture); // Not sure about this.

            // Store temporary name from Pet Number GUID (will be retrieved as uint64 in SMSG_PET_NAME_QUERY_RESPONSE)
            StoreGetters.AddName(PetGuid, PetNumber);
        }

        [Parser(Opcode.SMSG_PET_NAME_QUERY_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
