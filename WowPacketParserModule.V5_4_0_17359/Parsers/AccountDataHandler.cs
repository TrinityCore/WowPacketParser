using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.Translator.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
            {
                packet.Translator.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }

            packet.Translator.ReadUInt32("unk24");
            packet.Translator.ReadByte("Unk Byte");
        }
    }
}