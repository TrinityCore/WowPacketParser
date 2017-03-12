using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAllGuildAchievements
    {
        public List<EarnedAchievement> Earned;

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var cnt = packet.ReadUInt32("Count");
            for (var i = 0; i < cnt; ++i)
                packet.ReadPackedTime("Date", i);

            for (var i = 0; i < cnt; ++i)
                packet.ReadUInt32<AchievementId>("Achievement Id", i);
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleGuildAchievementData430(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedTime("Date", i);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
            }
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleGuildAchievementData540(Packet packet)
        {
            var count = packet.ReadBits("Achievement count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 4, 6, 3, 7, 0, 2, 5, 1);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 6);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadInt32("Unk 1", i);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadInt32("Unk 2", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadPackedTime("Date", i);
                packet.ReadXORByte(guid[i], 2);

                packet.WriteGuid("GUID", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildAchievementData542(Packet packet)
        {
            var count = packet.ReadBits("Achievement count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 7, 5, 3, 4, 0, 6, 2, 1);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32<AchievementId>("Achievement Id", i);

                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 3);

                packet.ReadInt32("Unk 1", i);

                packet.ReadXORByte(guid[i], 0);
                packet.ReadXORByte(guid[i], 1);

                packet.ReadPackedTime("Time", i);

                packet.ReadXORByte(guid[i], 4);

                packet.ReadInt32("Unk 2", i);

                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 5);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildAchievementData547(Packet packet)
        {
            var count = packet.ReadBits("Criteria count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 3, 5, 4, 7, 2, 1, 0, 6);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadInt32("Unk 1", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadInt32("Unk 2", i);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadPackedTime("Time", i);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildAchievementData6(Packet packet)
        {
            var count = packet.ReadUInt32();
            for (var i = 0; i < count; ++i)
            {
                EarnedAchievement.ReadEarnedAchievement602(packet, "Earned", i);
            }
        }
    }
}
