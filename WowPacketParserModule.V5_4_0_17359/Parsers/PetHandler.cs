using System;
using System.Globalization;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.SMSG_PET_NAME_QUERY_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            var number = packet.ReadUInt64("Pet number");
            var hasData = packet.ReadBit();
            if (!hasData)
                return;

            const int maxDeclinedNameCases = 5;
            var declinedNameLen = new int[maxDeclinedNameCases];
            for (var i = 0; i < maxDeclinedNameCases; ++i)
                declinedNameLen[i] = (int)packet.ReadBits(7);

            packet.ReadBit("Declined?");
            var len = packet.ReadBits(8);

            for (var i = 0; i < maxDeclinedNameCases; ++i)
                if (declinedNameLen[i] != 0)
                    packet.ReadWoWString("Declined name", declinedNameLen[i], i);

            packet.ReadTime("Time");
            var petName = packet.ReadWoWString("Pet name", len);

            var guidArray = (from pair in StoreGetters.NameDict where Equals(pair.Value, number) select pair.Key).ToList();
            foreach (var guid in guidArray)
                StoreGetters.NameDict[guid] = petName;
        }
    }
}
