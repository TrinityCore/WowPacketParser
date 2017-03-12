using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAccountDataTimes
    {
        public UnixTime ServerTime;
        public uint TimeBits;
        public fixed long AccountTimes[8];

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.Zero, ClientVersionBuild.V3_0_2_9056)]
        public static void HandleAccountDataTimes(Packet packet)
        {
            for (var i = 0; i < 32; i++)
                packet.ReadInt32("Unk Int32", i);
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V3_0_2_9056, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleAccountDataTimes2(Packet packet)
        {
            packet.ReadTime("Server Time");
            packet.ReadByte("Unk Byte");

            var mask = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                mask = packet.ReadInt32("Mask");

            for (var i = 0; i < 8; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                    if ((mask & (1 << i)) == 0)
                        continue;

                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleAccountDataTimes530(Packet packet)
        {
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }

            packet.ReadUInt32("unk24");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_1_17538)]
        public static void HandleAccountDataTimes540(Packet packet)
        {
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }

            packet.ReadUInt32("unk24");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleAccountDataTimes541(Packet packet)
        {
            packet.ReadTime("Server Time");
            packet.ReadUInt32("unk24");
            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }
            packet.ReadBit("Unk Byte");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleAccountDataTimes542(Packet packet)
        {
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }

            packet.ReadUInt32("unk24");
            packet.ReadBit("Unk Bit");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleAccountDataTimes547(Packet packet)
        {
            packet.ReadUInt32("unk24");
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            packet.ReadBit("Unk Bit");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAccountDataTimes548(Packet packet)
        {
            packet.ReadBit("Unk Bit");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }

            packet.ReadUInt32("unk24");
            packet.ReadTime("Server Time");
        }

        [Parser(Opcode.SMSG_ACCOUNT_DATA_TIMES, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAccountDataTimes602(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadTime("Server Time");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadTime("[" + (AccountDataType)i + "]" + " Time");
            }
        }
    }
}
