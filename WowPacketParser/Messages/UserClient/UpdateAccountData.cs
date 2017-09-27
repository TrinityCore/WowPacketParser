using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UpdateAccountData
    {
        public UnixTime Time;
        public uint Size;
        public byte DataType;
        public Data CompressedData;

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleClientUpdateAccountData(Packet packet)
        {
            readUpdateAccountDataBlock(packet);
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleClientUpdateAccountData530(Packet packet)
        {
            packet.ReadTime("Login Time");

            var decompCount = packet.ReadInt32();
            var compCount = packet.ReadInt32();
            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);
            pkt.ClosePacket();

            packet.AddValue("Account Data", data);
            packet.ReadBitsE<AccountDataType>("Data Type", 3);
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientUpdateAccountData547(Packet packet)
        {
            var decompCount = packet.ReadInt32();
            packet.ReadTime("Login Time");
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);
            var data = pkt.ReadWoWString(decompCount);
            pkt.ClosePacket();

            packet.ReadBitsE<AccountDataType>("Data Type", 3);
            packet.AddValue("Account Data", data);
        }

        [Parser(Opcode.CMSG_UPDATE_ACCOUNT_DATA, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientUpdateAccountData602(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime("Time");

            var decompCount = packet.ReadInt32();
            packet.ResetBitReader();
            packet.ReadBitsE<AccountDataType>("Data Type", 3);
            var compCount = packet.ReadInt32();

            var pkt = packet.Inflate(compCount, decompCount, false);

            var data = pkt.ReadWoWString(decompCount);

            packet.AddValue("CompressedData", data);
        }

        private static void readUpdateAccountDataBlock(Packet packet)
        {
            packet.ReadInt32E<AccountDataType>("Data Type");

            packet.ReadTime("Login Time");

            var decompCount = packet.ReadInt32();
            var pkt = packet.Inflate(decompCount, false);
            pkt.ReadWoWString("Account Data", decompCount);
            pkt.ClosePacket(false);
        }
    }
}
