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
            packet.Translator.ReadBit("Unk Bit");

            for (var i = 0; i < 8; ++i)
            {
                packet.Translator.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }

            packet.Translator.ReadUInt32("unk24");
            packet.Translator.ReadTime("Server Time");
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBitsE<AccountDataType>("Data Type", 3);

            packet.Translator.StartBitStream(guid, 5, 1, 3, 7, 0, 4, 2, 6);

            packet.Translator.ReadXORBytes(guid, 3, 1, 5);

            var decompCount = packet.Translator.ReadInt32();
            var compCount = packet.Translator.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.Translator.ReadWoWString(decompCount);
            pkt.ClosePacket(false);

            packet.AddValue("Account Data", data);

            packet.Translator.ReadXORBytes(guid, 7, 4, 0, 6, 2);

            packet.Translator.ReadTime("Login Time");

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA)]
        public static void HandleRequestAccountData(Packet packet)
        {
            packet.Translator.ReadBitsE<AccountDataType>("Data Type", 3);
        }

    }
}