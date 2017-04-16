using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAchievementEarned
    {
        public uint EarnerNativeRealm;
        public bool Initial;
        public ulong Earner;
        public Data Time;
        public int AchievementID;
        public uint EarnerVirtualRealm;
        public ulong Sender;

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleAchievementEarned(Packet packet)
        {
            packet.ReadPackedGuid("Player GUID");
            packet.ReadInt32<AchievementId>("Achievement Id");
            packet.ReadPackedTime("Time");
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAchievementEarned548(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            packet.ReadBit("unk");
            guid2[7] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadPackedTime("Time");
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadInt32<AchievementId>("Achievement Id");
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadInt32("Realm Id");
            packet.ReadInt32("Realm Id");
            packet.ReadXORByte(guid2, 2);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid1);
        }

        [Parser(Opcode.SMSG_ACHIEVEMENT_EARNED, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAchievementEarned6(Packet packet)
        {
            packet.ReadPackedGuid128("Sender");
            packet.ReadPackedGuid128("Earner");
            packet.ReadUInt32<AchievementId>("AchievementID");
            packet.ReadPackedTime("Time");
            packet.ReadUInt32("EarnerNativeRealm");
            packet.ReadUInt32("EarnerVirtualRealm");
            packet.ReadBit("Initial");
        }
    }
}
