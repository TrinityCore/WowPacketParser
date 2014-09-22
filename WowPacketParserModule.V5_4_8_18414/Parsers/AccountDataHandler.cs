using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.CMSG_REALM_NAME_QUERY)]
        public static void HandleRealmNameQuery(Packet packet)
        {
            packet.ReadUInt32("RealmID");
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA)]
        public static void HandleRequestAccountData(Packet packet)
        {
            var t = packet.ReadBits(3);
            packet.WriteLine("Data Type: " + (AccountDataType)t);
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            var decompCount = packet.ReadInt32();
            packet.ReadTime("Login Time");
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);

            packet.ReadEnum<AccountDataType>("Data Type", 3);
            packet.WriteLine("Account Data : {0}", data);
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            packet.ReadBit("byte20");

            for (var i = 0; i < 8; i++)
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");

            packet.ReadUInt32("dword16");
            packet.ReadTime("Server Time"); //24*4
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            var guid = new byte[8];

            var t = packet.ReadBits(3);
            packet.WriteLine("Data Type: " + (AccountDataType)t);

            packet.StartBitStream(guid, 5, 1, 3, 7, 0, 4, 2, 6);

            packet.ReadXORBytes(guid, 3, 1, 5);

            var decompCount = packet.ReadInt32();
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);

            packet.WriteLine("Account Data {0}", data);

            packet.ReadXORBytes(guid, 7, 4, 0, 6, 2);

            packet.ReadTime("Login Time");

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_LOGOUT_CANCEL)]
        [Parser(Opcode.CMSG_LOGOUT_REQUEST)]
        [Parser(Opcode.SMSG_LOGOUT_CANCEL_ACK)]
        public static void HandleAccountNull(Packet packet)
        {
        }
    }
}
