using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE_ACCOUNT)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            var accountId = new byte[8];
            var counter = new byte[8];

            accountId[1] = packet.ReadBit();
            counter[5] = packet.ReadBit();
            accountId[0] = packet.ReadBit();
            counter[4] = packet.ReadBit();

            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete

            counter[3] = packet.ReadBit();
            counter[7] = packet.ReadBit();
            counter[2] = packet.ReadBit();
            accountId[3] = packet.ReadBit();
            counter[1] = packet.ReadBit();
            counter[6] = packet.ReadBit();
            accountId[2] = packet.ReadBit();
            accountId[6] = packet.ReadBit();
            counter[0] = packet.ReadBit();
            accountId[4] = packet.ReadBit();
            accountId[5] = packet.ReadBit();
            accountId[7] = packet.ReadBit();

            packet.ReadXORByte(accountId, 0);
            packet.ReadXORByte(counter, 1);
            packet.ReadXORByte(counter, 3);
            packet.ReadUInt32("Timer 1");
            packet.ReadXORByte(accountId, 4);
            packet.ReadXORByte(counter, 0);
            packet.ReadXORByte(accountId, 7);
            packet.ReadXORByte(accountId, 6);
            packet.ReadXORByte(counter, 2);
            packet.ReadUInt32("Timer 2");
            packet.ReadXORByte(counter, 7);
            packet.ReadXORByte(accountId, 5);
            packet.ReadXORByte(accountId, 1);
            packet.ReadXORByte(counter, 5);
            packet.ReadXORByte(accountId, 3);
            packet.ReadInt32("Criteria ID");
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(accountId, 2);
            packet.ReadXORByte(counter, 4);
            packet.ReadXORByte(counter, 6);

            packet.WriteLine("Account: {0}", BitConverter.ToUInt64(accountId, 0));
            packet.WriteLine("Counter: {0}", BitConverter.ToInt64(counter, 0));
        }
    }
}
