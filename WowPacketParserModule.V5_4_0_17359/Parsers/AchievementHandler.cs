using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaUpdate(Packet packet)
        {
            var counter = new byte[8];
            var guid = new byte[8];

            counter[5] = packet.ReadBit();

            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete

            counter[3] = packet.ReadBit();
            counter[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            counter[4] = packet.ReadBit();
            counter[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            counter[6] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            counter[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            counter[7] = packet.ReadBit();
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(counter, 6);
            packet.ReadXORByte(guid, 1);

            packet.ReadUInt32("Timer 1");

            packet.ReadXORByte(counter, 2);

            packet.ReadInt32("Criteria ID");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(counter, 0);
            packet.ReadXORByte(counter, 5);

            packet.ResetBitReader();

            packet.ReadPackedTime("Time");

            packet.ReadXORByte(counter, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);

            packet.ReadUInt32("Timer 2");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(counter, 7);
            packet.ReadXORByte(counter, 3);
            packet.ReadXORByte(counter, 1);

            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_CRITERIA_UNKNOWN)]
        public static void HandleCriteriaUnknow(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Criteria ID");
            packet.ReadInt32("Flags");

            packet.ResetBitReader();

            packet.ReadPackedTime("Time");

            packet.ReadInt32("Timer 1");
            packet.ReadInt64("Counter");
            packet.ReadInt32("Timer 2");

            packet.StartBitStream(guid, 2, 4, 1, 5, 3, 6, 7, 0);
            packet.ParseBitStream(guid, 7, 0, 6, 5, 2, 1, 4, 3);

            packet.WriteGuid("Guid", guid);
        }
    }
}
