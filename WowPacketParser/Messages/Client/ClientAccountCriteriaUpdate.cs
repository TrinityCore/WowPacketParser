using System;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAccountCriteriaUpdate
    {
        public CriteriaProgress Progress;

        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleCriteriaUpdateAccount542(Packet packet)
        {
            var counter = new byte[8];
            var accountId = new byte[8];

            accountId[0] = packet.ReadBit();
            accountId[4] = packet.ReadBit();
            counter[7] = packet.ReadBit();
            accountId[6] = packet.ReadBit();
            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete
            accountId[3] = packet.ReadBit();
            counter[0] = packet.ReadBit();
            counter[5] = packet.ReadBit();
            accountId[1] = packet.ReadBit();
            counter[6] = packet.ReadBit();
            counter[3] = packet.ReadBit();
            counter[1] = packet.ReadBit();
            counter[4] = packet.ReadBit();
            counter[2] = packet.ReadBit();
            accountId[2] = packet.ReadBit();
            accountId[7] = packet.ReadBit();
            accountId[5] = packet.ReadBit();
            packet.ReadUInt32("Timer 1");
            packet.ReadUInt32("Timer 2");
            packet.ReadXORByte(accountId, 6);
            packet.ReadXORByte(counter, 3);
            packet.ReadXORByte(counter, 6);
            packet.ReadXORByte(accountId, 1);
            packet.ReadXORByte(counter, 5);
            packet.ReadXORByte(counter, 2);
            packet.ReadXORByte(accountId, 7);
            packet.ReadXORByte(accountId, 2);
            packet.ReadXORByte(accountId, 4);
            packet.ReadXORByte(counter, 4);
            packet.ReadXORByte(accountId, 3);
            packet.ReadXORByte(counter, 1);
            packet.ReadXORByte(accountId, 0);
            packet.ReadInt32("Criteria ID");
            packet.ReadXORByte(counter, 0);
            packet.ReadXORByte(counter, 7);

            packet.ReadPackedTime("Time");

            packet.ReadXORByte(accountId, 5);

            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
            packet.AddValue("Account", BitConverter.ToUInt64(accountId, 0));
        }

        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleCriteriaUpdateAccount547(Packet packet)
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

            packet.AddValue("Account", BitConverter.ToUInt64(accountId, 0));
            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
        }

        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleCriteriaUpdateAccount548(Packet packet)
        {
            var counter = new byte[8];
            var accountId = new byte[8];

            counter[4] = packet.ReadBit();
            accountId[2] = packet.ReadBit();
            counter[2] = packet.ReadBit();
            accountId[4] = packet.ReadBit();
            counter[0] = packet.ReadBit();
            counter[5] = packet.ReadBit();
            accountId[3] = packet.ReadBit();
            counter[3] = packet.ReadBit();
            accountId[6] = packet.ReadBit();
            counter[6] = packet.ReadBit();
            accountId[1] = packet.ReadBit();
            accountId[7] = packet.ReadBit();
            counter[1] = packet.ReadBit();

            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete

            accountId[5] = packet.ReadBit();
            counter[7] = packet.ReadBit();
            accountId[0] = packet.ReadBit();

            packet.ReadXORByte(accountId, 7);
            packet.ReadUInt32("Timer 2");
            packet.ReadInt32("Criteria ID");
            packet.ReadXORByte(counter, 7);
            packet.ReadUInt32("Timer 1");
            packet.ReadXORByte(accountId, 4);
            packet.ReadXORByte(accountId, 3);
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(counter, 0);
            packet.ReadXORByte(counter, 1);
            packet.ReadXORByte(counter, 2);
            packet.ReadXORByte(counter, 3);
            packet.ReadXORByte(accountId, 1);
            packet.ReadXORByte(counter, 4);
            packet.ReadXORByte(counter, 5);
            packet.ReadXORByte(accountId, 5);
            packet.ReadXORByte(accountId, 2);
            packet.ReadXORByte(counter, 6);
            packet.ReadXORByte(accountId, 0);
            packet.ReadXORByte(accountId, 6);

            packet.AddValue("Account", BitConverter.ToUInt64(accountId, 0));
            packet.AddValue("Counter", BitConverter.ToInt64(counter, 0));
        }

        [Parser(Opcode.SMSG_ACCOUNT_CRITERIA_UPDATE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleCriteriaUpdateAccount602(Packet packet)
        {
            CriteriaProgress.Read6(packet, "Progress");
        }
    }
}
