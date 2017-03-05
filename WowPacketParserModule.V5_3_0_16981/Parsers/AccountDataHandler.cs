using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
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

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            packet.Translator.ReadTime("Login Time");

            var decompCount = packet.Translator.ReadInt32();
            var compCount = packet.Translator.ReadInt32();
            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.Translator.ReadWoWString(decompCount);
            pkt.ClosePacket();

            packet.AddValue("Account Data", data);
            packet.Translator.ReadBitsE<AccountDataType>("Data Type", 3);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            var guid = new byte[8];

            var decompCount = packet.Translator.ReadInt32();
            var compCount = packet.Translator.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.Translator.ReadWoWString(decompCount);
            pkt.ClosePacket();
            packet.AddValue("Account Data", data);

            packet.Translator.ReadTime("Login Time");
            guid[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBitsE<AccountDataType>("Data Type", 3);
            packet.Translator.StartBitStream(guid, 3, 6, 1, 5, 0, 4, 2);
            packet.Translator.ReadXORBytes(guid, 6, 7, 4, 1, 5, 0, 3, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}
