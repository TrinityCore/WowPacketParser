using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }

            packet.ReadUInt32("unk24");
            packet.ReadBit("Unk Bit");
        }
    }
}