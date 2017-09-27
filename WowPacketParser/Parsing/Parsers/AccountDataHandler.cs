using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AccountDataHandler
    {

        public static void ReadUpdateAccountDataBlock(Packet packet)
        {
            packet.ReadInt32E<AccountDataType>("Data Type");

            packet.ReadTime("Login Time");

            var decompCount = packet.ReadInt32();
            var pkt = packet.Inflate(decompCount, false);
            pkt.ReadWoWString("Account Data", decompCount);
            pkt.ClosePacket(false);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            packet.ReadGuid("GUID");
            ReadUpdateAccountDataBlock(packet);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE)]
        public static void HandleUpdateAccountDataComplete(Packet packet)
        {
            packet.ReadInt32E<AccountDataType>("Data Type");
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES)]
        [Parser(Opcode.CMSG_PLAYER_LOGOUT)]
        [Parser(Opcode.CMSG_LOGOUT_REQUEST)]
        [Parser(Opcode.CMSG_LOGOUT_CANCEL)]
        [Parser(Opcode.SMSG_LOGOUT_CANCEL_ACK)]
        public static void HandleAccountNull(Packet packet)
        {
        }
    }
}
