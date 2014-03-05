using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes547(Packet packet)
        {
            packet.ReadUInt32("dword10");
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");

            packet.ReadBit("byte18");
        }
    }
}
