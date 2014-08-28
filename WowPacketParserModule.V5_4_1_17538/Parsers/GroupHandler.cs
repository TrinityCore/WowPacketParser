using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_GROUP_INVITE)]
        public static void HandleGroupInvite(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Unk Int32"); // Non-zero in cross realm parties (1383)
            packet.ReadInt32("Unk Int32"); // Always 0
            packet.ReadByte("unk");

            guid[2] = packet.ReadBit();
            var strLen = packet.ReadBits(9);
            guid[0] = packet.ReadBit();


            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var nameLen = packet.ReadBits(9);

            packet.ResetBitReader();

            packet.ReadXORByte(guid, 5);
            packet.ReadWoWString("Realm Name", strLen); // Non-empty in cross realm parties
            packet.ReadWoWString("Name", nameLen);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.WriteGuid("Guid", guid); // Non-zero in cross realm parties
        }
        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {

            var guid3 = new byte[8];
            var guid5 = new byte[8];
            var GroupGUID = new byte[8];

            var bit78 = false;
            var bit83 = false;

            packet.ReadInt32("Int68");
            packet.ReadByte("Byte88");
            packet.ReadByte("Byte20");
            packet.ReadByte("Byte10");
            packet.ReadInt32("Int48");
            guid3[3] = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            GroupGUID[6] = packet.ReadBit();
            GroupGUID[7] = packet.ReadBit();
            GroupGUID[2] = packet.ReadBit();
            GroupGUID[5] = packet.ReadBit();
            GroupGUID[3] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            var bit38 = packet.ReadBit();
            GroupGUID[4] = packet.ReadBit();
            var memberCount = packet.ReadBits("Member Count", 21);
            if (bit38)
            {
                guid5[4] = packet.ReadBit();
                guid5[6] = packet.ReadBit();
                guid5[5] = packet.ReadBit();
                guid5[7] = packet.ReadBit();
                guid5[0] = packet.ReadBit();
                guid5[1] = packet.ReadBit();
                guid5[2] = packet.ReadBit();
                guid5[3] = packet.ReadBit();
            }

            var guid7 = new byte[memberCount][];
            var bitsED = new uint[memberCount];

            for (var i = 0; i < memberCount; ++i)
            {
                guid7[i] = new byte[8];
                bitsED[i] = packet.ReadBits(6);
                packet.StartBitStream(guid7[i], 4, 3, 7, 0, 1, 2, 6, 5);
            }

            var bit84 = packet.ReadBit();
            GroupGUID[1] = packet.ReadBit();
            var bit64 = packet.ReadBit();
            guid3[4] = packet.ReadBit();
            GroupGUID[0] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            if (bit84)
            {
                bit83 = packet.ReadBit();
                bit78 = packet.ReadBit();
            }

            guid3[7] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            if (bit84)
            {
                packet.ReadInt32("Int74");
                packet.ReadByte("Byte79");
                packet.ReadByte("Byte80");
                packet.ReadByte("Byte82");
                packet.ReadByte("Byte81");
                packet.ReadByte("Byte6C");
                packet.ReadSingle("Float7C");
                packet.ReadInt32("Int70");
            }

            for (var i = 0; i < memberCount; ++i)
            {
                packet.ReadByte("Byte50");
                packet.ReadByte("Byte50");
                packet.ReadXORByte(guid7[i], 2);
                packet.ReadXORByte(guid7[i], 7);
                packet.ReadXORByte(guid7[i], 4);
                packet.ReadXORByte(guid7[i], 0);
                packet.ReadByte("Byte50");
                packet.ReadXORByte(guid7[i], 6);
                packet.ReadXORByte(guid7[i], 1);
                packet.ReadXORByte(guid7[i], 5);
                packet.ReadXORByte(guid7[i], 3);
                packet.ReadByte("Byte50");
                packet.ReadWoWString("Name", bitsED[i], i);
                packet.WriteGuid("Guid7", guid7[i]);
            }

            if (bit38)
            {
                packet.ReadByte("Byte31");
                packet.ReadXORByte(guid5, 5);
                packet.ReadXORByte(guid5, 4);
                packet.ReadByte("Byte30");
                packet.ReadXORByte(guid5, 3);
                packet.ReadXORByte(guid5, 1);
                packet.ReadXORByte(guid5, 0);
                packet.ReadXORByte(guid5, 6);
                packet.ReadXORByte(guid5, 2);
                packet.ReadXORByte(guid5, 7);
                packet.WriteGuid("Guid5", guid5);
            }

            packet.ReadXORByte(GroupGUID, 2);
            if (bit64)
            {
                packet.ReadInt32("Int60");
                packet.ReadInt32("Int5C");
            }

            packet.ReadXORByte(GroupGUID, 5);
            packet.ReadXORByte(GroupGUID, 3);
            packet.ReadXORByte(GroupGUID, 1);
            packet.ReadXORByte(GroupGUID, 0);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(GroupGUID, 7);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(GroupGUID, 6);
            packet.ReadXORByte(GroupGUID, 4);
            packet.ReadXORByte(guid3, 3);

            packet.WriteGuid("Guid3", guid3);
            packet.WriteGuid("GroupGUID", GroupGUID);
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var guid2 = new byte[8];

            var bits21 = 0;
            var bits122 = 0;
            var bits164 = 0;

            guid2[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            var bit161 = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            bits164 = (int)packet.ReadBits(22);
            bits122 = (int)packet.ReadBits(6);
            var bit160 = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            var bit18 = packet.ReadBit();
            var bit20 = packet.ReadBit();
            bits21 = (int)packet.ReadBits(9);
            guid2[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            packet.ReadInt32("Int174");
            packet.ReadXORByte(guid2, 6);
            packet.ReadWoWString("Realm Name", bits21);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 2);
            packet.ReadInt32("Int1C");
            packet.ReadWoWString("Invited", bits122);
            packet.ReadXORByte(guid2, 0);
            packet.ReadInt32("Int154");

            for (var i = 0; i < bits164; ++i)
            {
                packet.ReadInt32("IntEA");
            }

            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadInt64("Int158");
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 4);

            packet.WriteGuid("Guid2", guid2);
        }
    }
}