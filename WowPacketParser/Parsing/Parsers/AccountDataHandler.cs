using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class AccountDataHandler
    {
        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.Zero, ClientVersionBuild.V3_0_2_9056)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            for (var i = 0; i < 32; i++)
                packet.ReadInt32("Unk Int32", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V3_0_2_9056)]
        public static void HandleAccountDataTimes2(Packet packet)
        {
            packet.ReadTime("Server Time");
            packet.ReadByte("Unk Byte");

            var mask = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                mask = packet.ReadInt32("Mask");

            for (var i = 0; i < 8; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                    if ((mask & (1 << i)) == 0)
                        continue;

                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }
        }

        [Parser(Opcode.CMSG_REQUEST_ACCOUNT_DATA)]
        public static void HandleRequestAccountData(Packet packet)
        {
            packet.ReadEnum<AccountDataType>("Data Type", TypeCode.Int32);
        }

        public static void ReadUpdateAccountDataBlock(ref Packet packet)
        {
            packet.ReadEnum<AccountDataType>("Data Type", TypeCode.Int32);

            packet.ReadTime("Login Time");

            var decompCount = packet.ReadInt32();
            packet = packet.Inflate(decompCount, false);

            packet.ReadWoWString("Account Data", decompCount);
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            ReadUpdateAccountDataBlock(ref packet);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA)]
        public static void HandleServerUpdateAccountData(Packet packet)
        {
            packet.ReadGuid("GUID");
            ReadUpdateAccountDataBlock(ref packet);
        }

        [Parser(Opcode.SMSG_UPDATE_ACCOUNT_DATA_COMPLETE)]
        [Parser(0x2015)] // 4.3.4
        public static void HandleUpdateAccountDataComplete(Packet packet)
        {
            packet.ReadEnum<AccountDataType>("Data Type", TypeCode.Int32);
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
