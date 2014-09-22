using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.CMSG_REALM_NAME_QUERY)]
        public static void HandleRealmNameQuery(Packet packet)
        {
            packet.ReadUInt32("RealmID");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadUInt32("dword10");
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");

            packet.ReadBit("byte18");
        }
    }
}
