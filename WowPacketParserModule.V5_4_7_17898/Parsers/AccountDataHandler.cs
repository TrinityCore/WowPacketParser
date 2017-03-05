using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.Translator.ReadUInt32("unk24");
            packet.Translator.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
                packet.Translator.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            packet.Translator.ReadBit("Unk Bit");
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            var decompCount = packet.Translator.ReadInt32();
            packet.Translator.ReadTime("Login Time");
            var compCount = packet.Translator.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.Translator.ReadWoWString(decompCount);
            pkt.ClosePacket();

            packet.Translator.ReadBitsE<AccountDataType>("Data Type", 3);
            packet.AddValue("Account Data", data);
        }
    }
}