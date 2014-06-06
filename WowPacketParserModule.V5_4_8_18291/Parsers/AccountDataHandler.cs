using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadBit("byte20");

            for (var i = 0; i < 8; ++i)
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");

            packet.ReadUInt32("dword16");
            packet.ReadTime("Server Time"); //24*4
        }
    }
}
