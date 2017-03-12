using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAllAccountCriteria
    {
        public List<CriteriaProgress> Progress;

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleAllAchievementCriteriaDataAccount542(Packet packet)
        {
            var count = packet.ReadBits("Criteria count", 19);

            var counter = new byte[count][];
            var accountId = new byte[count][];
            var flags = new byte[count];

            for (var i = 0; i < count; ++i)
            {
                counter[i] = new byte[8];
                accountId[i] = new byte[8];

                accountId[i][6] = packet.ReadBit();

                flags[i] = (byte)(packet.ReadBits(4) & 0xFFu);

                counter[i][1] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                accountId[i][2] = packet.ReadBit();
                accountId[i][7] = packet.ReadBit();
                accountId[i][4] = packet.ReadBit();
                counter[i][4] = packet.ReadBit();
                accountId[i][3] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                counter[i][2] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                accountId[i][5] = packet.ReadBit();
                accountId[i][0] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                accountId[i][1] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(counter[i], 6);
                packet.ReadXORByte(accountId[i], 1);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(accountId[i], 4);
                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(counter[i], 3);

                packet.ReadPackedTime("Time", i);

                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(accountId[i], 7);

                packet.ReadInt32("Criteria ID", i);
                packet.ReadUInt32("Timer 1", i);

                packet.ReadXORByte(accountId[i], 2);
                packet.ReadXORByte(accountId[i], 0);

                packet.ReadUInt32("Timer 2", i);

                packet.ReadXORByte(counter[i], 5);
                packet.ReadXORByte(counter[i], 1);
                packet.ReadXORByte(accountId[i], 5);
                packet.ReadXORByte(accountId[i], 6);
                packet.ReadXORByte(counter[i], 0);
                packet.ReadXORByte(accountId[i], 3);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.AddValue("Account", i, BitConverter.ToUInt64(accountId[i], 0), i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleAllAchievementCriteriaDataAccount547(Packet packet)
        {
            var count = packet.ReadBits("Criteria count", 19);

            var counter = new byte[count][];
            var accountId = new byte[count][];
            var flags = new byte[count];

            for (var i = 0; i < count; ++i)
            {
                counter[i] = new byte[8];
                accountId[i] = new byte[8];

                counter[i][2] = packet.ReadBit();
                accountId[i][0] = packet.ReadBit();
                counter[i][0] = packet.ReadBit();
                accountId[i][1] = packet.ReadBit();

                flags[i] = (byte)(packet.ReadBits(4) & 0xFFu);

                accountId[i][5] = packet.ReadBit();
                accountId[i][7] = packet.ReadBit();
                counter[i][7] = packet.ReadBit();
                counter[i][6] = packet.ReadBit();
                accountId[i][4] = packet.ReadBit();
                accountId[i][2] = packet.ReadBit();
                counter[i][4] = packet.ReadBit();
                accountId[i][3] = packet.ReadBit();
                accountId[i][6] = packet.ReadBit();
                counter[i][3] = packet.ReadBit();
                counter[i][5] = packet.ReadBit();
                counter[i][1] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(accountId[i], 0);
                packet.ReadXORByte(counter[i], 4);
                packet.ReadXORByte(accountId[i], 2);
                packet.ReadXORByte(counter[i], 0);
                packet.ReadPackedTime("Time", i);
                packet.ReadXORByte(accountId[i], 6);
                packet.ReadInt32("Criteria ID", i);
                packet.ReadXORByte(accountId[i], 5);
                packet.ReadXORByte(counter[i], 1);
                packet.ReadXORByte(counter[i], 2);
                packet.ReadXORByte(counter[i], 3);
                packet.ReadXORByte(accountId[i], 4);
                packet.ReadXORByte(accountId[i], 1);
                packet.ReadXORByte(accountId[i], 7);
                packet.ReadXORByte(counter[i], 5);
                packet.ReadUInt32("Timer 1", i);
                packet.ReadXORByte(counter[i], 7);
                packet.ReadXORByte(accountId[i], 3);
                packet.ReadUInt32("Timer 2", i);
                packet.ReadXORByte(counter[i], 6);

                packet.AddValue("Criteria Flags", flags[i], i);
                packet.AddValue("Criteria Counter", BitConverter.ToUInt64(counter[i], 0), i);
                packet.AddValue("Account", i, BitConverter.ToUInt64(accountId[i], 0), i);
            }
        }

        [Parser(Opcode.SMSG_ALL_ACCOUNT_CRITERIA, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAllAchievementCriteriaDataAccount6(Packet packet)
        {
            var count = packet.ReadUInt32("ProgressCount");

            for (var i = 0; i < count; ++i)
                CriteriaProgress.Read6(packet, "Progress", i);
        }
    }
}
