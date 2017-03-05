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

            packet.Translator.ReadInt32("Unk Int32"); // Non-zero in cross realm parties (1383)
            packet.Translator.ReadInt32("Unk Int32"); // Always 0
            packet.Translator.ReadByte("unk");

            guid[2] = packet.Translator.ReadBit();
            var strLen = packet.Translator.ReadBits(9);
            guid[0] = packet.Translator.ReadBit();


            guid[3] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            var nameLen = packet.Translator.ReadBits(9);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadWoWString("Realm Name", strLen); // Non-empty in cross realm parties
            packet.Translator.ReadWoWString("Name", nameLen);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.WriteGuid("Guid", guid); // Non-zero in cross realm parties
        }
        [Parser(Opcode.SMSG_GROUP_LIST)]
        public static void HandleGroupList(Packet packet)
        {

            var guid3 = new byte[8];
            var guid5 = new byte[8];
            var GroupGUID = new byte[8];

            var bit78 = false;
            var bit83 = false;

            packet.Translator.ReadInt32("Int68");
            packet.Translator.ReadByte("Byte88");
            packet.Translator.ReadByte("Byte20");
            packet.Translator.ReadByte("Byte10");
            packet.Translator.ReadInt32("Int48");
            guid3[3] = packet.Translator.ReadBit();
            guid3[6] = packet.Translator.ReadBit();
            GroupGUID[6] = packet.Translator.ReadBit();
            GroupGUID[7] = packet.Translator.ReadBit();
            GroupGUID[2] = packet.Translator.ReadBit();
            GroupGUID[5] = packet.Translator.ReadBit();
            GroupGUID[3] = packet.Translator.ReadBit();
            guid3[0] = packet.Translator.ReadBit();
            guid3[5] = packet.Translator.ReadBit();
            var bit38 = packet.Translator.ReadBit();
            GroupGUID[4] = packet.Translator.ReadBit();
            var memberCount = packet.Translator.ReadBits("Member Count", 21);
            if (bit38)
            {
                guid5[4] = packet.Translator.ReadBit();
                guid5[6] = packet.Translator.ReadBit();
                guid5[5] = packet.Translator.ReadBit();
                guid5[7] = packet.Translator.ReadBit();
                guid5[0] = packet.Translator.ReadBit();
                guid5[1] = packet.Translator.ReadBit();
                guid5[2] = packet.Translator.ReadBit();
                guid5[3] = packet.Translator.ReadBit();
            }

            var guid7 = new byte[memberCount][];
            var bitsED = new uint[memberCount];

            for (var i = 0; i < memberCount; ++i)
            {
                guid7[i] = new byte[8];
                bitsED[i] = packet.Translator.ReadBits(6);
                packet.Translator.StartBitStream(guid7[i], 4, 3, 7, 0, 1, 2, 6, 5);
            }

            var bit84 = packet.Translator.ReadBit();
            GroupGUID[1] = packet.Translator.ReadBit();
            var bit64 = packet.Translator.ReadBit();
            guid3[4] = packet.Translator.ReadBit();
            GroupGUID[0] = packet.Translator.ReadBit();
            guid3[2] = packet.Translator.ReadBit();
            if (bit84)
            {
                bit83 = packet.Translator.ReadBit();
                bit78 = packet.Translator.ReadBit();
            }

            guid3[7] = packet.Translator.ReadBit();
            guid3[1] = packet.Translator.ReadBit();
            if (bit84)
            {
                packet.Translator.ReadInt32("Int74");
                packet.Translator.ReadByte("Byte79");
                packet.Translator.ReadByte("Byte80");
                packet.Translator.ReadByte("Byte82");
                packet.Translator.ReadByte("Byte81");
                packet.Translator.ReadByte("Byte6C");
                packet.Translator.ReadSingle("Float7C");
                packet.Translator.ReadInt32("Int70");
            }

            for (var i = 0; i < memberCount; ++i)
            {
                packet.Translator.ReadByte("Byte50");
                packet.Translator.ReadByte("Byte50");
                packet.Translator.ReadXORByte(guid7[i], 2);
                packet.Translator.ReadXORByte(guid7[i], 7);
                packet.Translator.ReadXORByte(guid7[i], 4);
                packet.Translator.ReadXORByte(guid7[i], 0);
                packet.Translator.ReadByte("Byte50");
                packet.Translator.ReadXORByte(guid7[i], 6);
                packet.Translator.ReadXORByte(guid7[i], 1);
                packet.Translator.ReadXORByte(guid7[i], 5);
                packet.Translator.ReadXORByte(guid7[i], 3);
                packet.Translator.ReadByte("Byte50");
                packet.Translator.ReadWoWString("Name", bitsED[i], i);
                packet.Translator.WriteGuid("Guid7", guid7[i]);
            }

            if (bit38)
            {
                packet.Translator.ReadByte("Byte31");
                packet.Translator.ReadXORByte(guid5, 5);
                packet.Translator.ReadXORByte(guid5, 4);
                packet.Translator.ReadByte("Byte30");
                packet.Translator.ReadXORByte(guid5, 3);
                packet.Translator.ReadXORByte(guid5, 1);
                packet.Translator.ReadXORByte(guid5, 0);
                packet.Translator.ReadXORByte(guid5, 6);
                packet.Translator.ReadXORByte(guid5, 2);
                packet.Translator.ReadXORByte(guid5, 7);
                packet.Translator.WriteGuid("Guid5", guid5);
            }

            packet.Translator.ReadXORByte(GroupGUID, 2);
            if (bit64)
            {
                packet.Translator.ReadInt32("Int60");
                packet.Translator.ReadInt32("Int5C");
            }

            packet.Translator.ReadXORByte(GroupGUID, 5);
            packet.Translator.ReadXORByte(GroupGUID, 3);
            packet.Translator.ReadXORByte(GroupGUID, 1);
            packet.Translator.ReadXORByte(GroupGUID, 0);
            packet.Translator.ReadXORByte(guid3, 7);
            packet.Translator.ReadXORByte(guid3, 2);
            packet.Translator.ReadXORByte(guid3, 0);
            packet.Translator.ReadXORByte(guid3, 1);
            packet.Translator.ReadXORByte(GroupGUID, 7);
            packet.Translator.ReadXORByte(guid3, 6);
            packet.Translator.ReadXORByte(guid3, 4);
            packet.Translator.ReadXORByte(guid3, 5);
            packet.Translator.ReadXORByte(GroupGUID, 6);
            packet.Translator.ReadXORByte(GroupGUID, 4);
            packet.Translator.ReadXORByte(guid3, 3);

            packet.Translator.WriteGuid("Guid3", guid3);
            packet.Translator.WriteGuid("GroupGUID", GroupGUID);
        }

        [Parser(Opcode.SMSG_GROUP_INVITE)]
        public static void HandleSmsgGroupInvite(Packet packet)
        {
            var guid2 = new byte[8];

            var bits21 = 0;
            var bits122 = 0;
            var bits164 = 0;

            guid2[3] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            var bit161 = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            bits164 = (int)packet.Translator.ReadBits(22);
            bits122 = (int)packet.Translator.ReadBits(6);
            var bit160 = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            var bit18 = packet.Translator.ReadBit();
            var bit20 = packet.Translator.ReadBit();
            bits21 = (int)packet.Translator.ReadBits(9);
            guid2[6] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            packet.Translator.ReadInt32("Int174");
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadWoWString("Realm Name", bits21);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadWoWString("Invited", bits122);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadInt32("Int154");

            for (var i = 0; i < bits164; ++i)
            {
                packet.Translator.ReadInt32("IntEA");
            }

            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadInt64("Int158");
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 4);

            packet.Translator.WriteGuid("Guid2", guid2);
        }
    }
}