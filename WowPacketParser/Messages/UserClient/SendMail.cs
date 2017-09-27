using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SendMail
    {
        public CliStructSendMail Info;

        [Parser(Opcode.CMSG_SEND_MAIL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSendMail(Packet packet)
        {
            packet.ReadGuid("Mailbox GUID");
            packet.ReadCString("Receiver");
            packet.ReadCString("Subject");
            packet.ReadCString("Body");
            packet.ReadUInt32("Stationery?");
            packet.ReadUInt32("Unk Uint32");
            var items = packet.ReadByte("Item Count");
            for (var i = 0; i < items; ++i)
            {
                packet.ReadByte("Slot", i);
                packet.ReadGuid("Item GUID", i);
            }
            packet.ReadUInt32("Money");
            packet.ReadUInt32("COD");
            packet.ReadUInt64("Unk Uint64");
            packet.ReadByte("Unk Byte");

        }

        [Parser(Opcode.CMSG_SEND_MAIL, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleSendMail434(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadInt32("Unk Int32"); // MailMessage.packageId ?
            packet.ReadInt32("Stationery?");
            packet.ReadInt64("COD");
            packet.ReadInt64("Money");
            var len2 = packet.ReadBits(12);
            var len1 = packet.ReadBits(9);
            var count = packet.ReadBits("Item Count", 5);
            guid[0] = packet.ReadBit();
            var guid2 = new byte[count][];
            for (var i = 0; i < count; i++)
            {
                guid2[i] = packet.StartBitStream(2, 6, 3, 7, 1, 0, 4, 5);
            }
            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var len3 = packet.ReadBits(7);
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < count; i++)
            {
                if (guid2[i][6] != 0) guid2[i][6] = packet.ReadByte();
                if (guid2[i][1] != 0) guid2[i][1] = packet.ReadByte();
                if (guid2[i][7] != 0) guid2[i][7] = packet.ReadByte();
                if (guid2[i][2] != 0) guid2[i][2] = packet.ReadByte();
                packet.ReadByte("Slot", i);
                if (guid2[i][3] != 0) guid2[i][3] = packet.ReadByte();
                if (guid2[i][0] != 0) guid2[i][0] = packet.ReadByte();
                if (guid2[i][4] != 0) guid2[i][4] = packet.ReadByte();
                if (guid2[i][5] != 0) guid2[i][5] = packet.ReadByte();
                packet.WriteGuid("Item Guid", guid2[i], i);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);

            packet.ReadWoWString("Subject", len1);
            packet.ReadWoWString("Receiver", len3);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);

            packet.ReadWoWString("Body", len2);

            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Mailbox Guid", guid);
        }

        [Parser(Opcode.CMSG_SEND_MAIL, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSendMail547(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("IntA20");
            packet.ReadInt64("IntA40");
            packet.ReadInt32("IntA1C");
            packet.ReadInt64("IntA38");

            var count = packet.ReadBits(5);

            guid[0] = packet.ReadBit();

            var ItemGUID = new byte[count][];
            for (var i = 0; i < count; ++i)
                packet.StartBitStream(ItemGUID[i], 6, 5, 1, 3, 0, 4, 7, 2);

            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var len1 = (int)packet.ReadBits(11);
            var len3 = (int)packet.ReadBits(9);
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var len2 = (int)packet.ReadBits(9);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(ItemGUID[i], 6);
                packet.ReadXORByte(ItemGUID[i], 3);
                packet.ReadXORByte(ItemGUID[i], 0);
                packet.ReadXORByte(ItemGUID[i], 5);
                packet.ReadXORByte(ItemGUID[i], 7);
                packet.ReadXORByte(ItemGUID[i], 4);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(ItemGUID[i], 1);
                packet.ReadXORByte(ItemGUID[i], 2);
                packet.WriteGuid("ItemGUID", ItemGUID[i]);
            }

            packet.ReadWoWString("Body", len2);
            packet.ReadXORByte(guid, 1);
            packet.ReadWoWString("Receiver", len3);
            packet.ReadXORByte(guid, 7);
            packet.ReadWoWString("Subject", len1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SEND_MAIL, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSendMail602(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            packet.ReadInt32("StationeryID");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.ReadInt32("PackageID");
            packet.ReadInt64("SendMoney");
            packet.ReadInt64("Cod");

            var nameLength = packet.ReadBits(9);
            var subjectLength = packet.ReadBits(9);
            var bodyLength = packet.ReadBits(11);
            var itemCount = packet.ReadBits(5);
            packet.ResetBitReader();

            packet.ReadWoWString("Target", nameLength);
            packet.ReadWoWString("Subject", subjectLength);
            packet.ReadWoWString("Body", bodyLength);

            for (var i = 0; i < itemCount; i++)
            {
                packet.ReadByte("AttachPosition", i);
                packet.ReadPackedGuid128("ItemGUID", i);
            }
        }

    }
}
