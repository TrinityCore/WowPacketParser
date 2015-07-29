using System.Globalization;
using WowPacketParser.Enums.Version.V3_3_5a_12340;
using WowPacketParser.Enums.Version.V4_0_3_13329;
using WowPacketParser.Enums.Version.V4_0_6_13596;
using WowPacketParser.Enums.Version.V4_1_0_13914;
using WowPacketParser.Enums.Version.V4_2_0_14480;
using WowPacketParser.Enums.Version.V4_2_2_14545;
using WowPacketParser.Enums.Version.V4_3_0_15005;
using WowPacketParser.Enums.Version.V4_3_2_15211;
using WowPacketParser.Enums.Version.V4_3_3_15354;
using WowPacketParser.Enums.Version.V4_3_4_15595;
using WowPacketParser.Enums.Version.V5_0_4_16016;
using WowPacketParser.Enums.Version.V5_0_5_16048;
using WowPacketParser.Enums.Version.V5_1_0_16309;
using WowPacketParser.Enums.Version.V5_2_0_16650;
using WowPacketParser.Enums.Version.V5_3_0_16981;
using WowPacketParser.Enums.Version.V5_4_0_17359;
using WowPacketParser.Enums.Version.V5_4_1_17538;
using WowPacketParser.Enums.Version.V5_4_2_17658;
using WowPacketParser.Enums.Version.V5_4_7_17898;
using WowPacketParser.Enums.Version.V5_4_8_18291;
using WowPacketParser.Enums.Version.V6_0_2_19033;
using WowPacketParser.Enums.Version.V6_0_3_19103;
using WowPacketParser.Enums.Version.V6_1_0_19678;
using WowPacketParser.Enums.Version.V6_1_2_19802;
using WowPacketParser.Enums.Version.V6_2_0_20173;
using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static class Opcodes
    {
        private static BiDictionary<Opcode, int> ServerDict = GetOpcodeDictionary(ClientVersion.Build, Direction.ServerToClient);
        private static BiDictionary<Opcode, int> ClientDict = GetOpcodeDictionary(ClientVersion.Build, Direction.ClientToServer);
        private static BiDictionary<Opcode, int> MiscDict = GetOpcodeDictionary(ClientVersion.Build, Direction.Bidirectional);

        public static void InitializeOpcodeDictionary()
        {
            ServerDict = GetOpcodeDictionary(ClientVersion.Build, Direction.ServerToClient);
            ClientDict = GetOpcodeDictionary(ClientVersion.Build, Direction.ClientToServer);
            MiscDict = GetOpcodeDictionary(ClientVersion.Build, Direction.Bidirectional);
        }

        public static BiDictionary<Opcode, int> GetOpcodeDictionary(ClientVersionBuild build, Direction direction)
        {
            switch (build)
            {
                case ClientVersionBuild.V2_4_3_8606:
                case ClientVersionBuild.V3_0_2_9056:
                case ClientVersionBuild.V3_0_3_9183:
                case ClientVersionBuild.V3_0_8_9464:
                case ClientVersionBuild.V3_0_8a_9506:
                case ClientVersionBuild.V3_0_9_9551:
                case ClientVersionBuild.V3_1_0_9767:
                case ClientVersionBuild.V3_1_1_9806:
                case ClientVersionBuild.V3_1_1a_9835:
                case ClientVersionBuild.V3_1_2_9901:
                case ClientVersionBuild.V3_1_3_9947:
                case ClientVersionBuild.V3_2_0_10192:
                case ClientVersionBuild.V3_2_0a_10314:
                case ClientVersionBuild.V3_2_2_10482:
                case ClientVersionBuild.V3_2_2a_10505:
                case ClientVersionBuild.V3_3_0_10958:
                case ClientVersionBuild.V3_3_0a_11159:
                case ClientVersionBuild.V3_3_3_11685:
                case ClientVersionBuild.V3_3_3a_11723:
                case ClientVersionBuild.V3_3_5a_12340:
                {
                    return Opcodes_3_3_5.Opcodes(direction);
                }
                case ClientVersionBuild.V4_0_3_13329:
                {
                    return Opcodes_4_0_3.Opcodes(direction);
                }
                case ClientVersionBuild.V4_0_6_13596:
                case ClientVersionBuild.V4_0_6a_13623:
                {
                    return Opcodes_4_0_6.Opcodes(direction);
                }
                case ClientVersionBuild.V4_1_0_13914:
                case ClientVersionBuild.V4_1_0a_14007:
                {
                    return Opcodes_4_1_0.Opcodes(direction);
                }
                case ClientVersionBuild.V4_2_0_14333:
                case ClientVersionBuild.V4_2_0a_14480:
                {
                    return Opcodes_4_2_0.Opcodes(direction);
                }
                case ClientVersionBuild.V4_2_2_14545:
                {
                    return Opcodes_4_2_2.Opcodes(direction);
                }
                case ClientVersionBuild.V4_3_0_15005:
                case ClientVersionBuild.V4_3_0a_15050:
                {
                    return Opcodes_4_3_0.Opcodes(direction);
                }
                case ClientVersionBuild.V4_3_2_15211:
                {
                    return Opcodes_4_3_2.Opcodes(direction);
                }
                case ClientVersionBuild.V4_3_3_15354:
                {
                    return Opcodes_4_3_3.Opcodes(direction);
                }
                case ClientVersionBuild.V4_3_4_15595:
                {
                    return Opcodes_4_3_4.Opcodes(direction);
                }
                case ClientVersionBuild.V5_0_4_16016:
                {
                    return Opcodes_5_0_4.Opcodes(direction);
                }
                case ClientVersionBuild.V5_0_5_16048:
                case ClientVersionBuild.V5_0_5a_16057:
                case ClientVersionBuild.V5_0_5b_16135:
                {
                    return Opcodes_5_0_5.Opcodes(direction);
                }
                case ClientVersionBuild.V5_1_0_16309:
                case ClientVersionBuild.V5_1_0a_16357:
                {
                    return Opcodes_5_1_0.Opcodes(direction);
                }
                case ClientVersionBuild.V5_2_0_16650:
                case ClientVersionBuild.V5_2_0_16669:
                case ClientVersionBuild.V5_2_0_16683:
                case ClientVersionBuild.V5_2_0_16685:
                case ClientVersionBuild.V5_2_0_16701:
                case ClientVersionBuild.V5_2_0_16709:
                case ClientVersionBuild.V5_2_0_16716:
                case ClientVersionBuild.V5_2_0_16733:
                case ClientVersionBuild.V5_2_0_16769:
                case ClientVersionBuild.V5_2_0_16826:
                {
                    return Opcodes_5_2_0.Opcodes(direction);
                }
                case ClientVersionBuild.V5_3_0_16981:
                case ClientVersionBuild.V5_3_0_16983:
                case ClientVersionBuild.V5_3_0_16992:
                case ClientVersionBuild.V5_3_0_17055:
                case ClientVersionBuild.V5_3_0_17116:
                case ClientVersionBuild.V5_3_0_17128:
                {
                    return Opcodes_5_3_0.Opcodes(direction);
                }
                case ClientVersionBuild.V5_4_0_17359:
                case ClientVersionBuild.V5_4_0_17371:
                case ClientVersionBuild.V5_4_0_17399:
                {
                    return Opcodes_5_4_0.Opcodes(direction);
                }
                case ClientVersionBuild.V5_4_1_17538:
                {
                    return Opcodes_5_4_1.Opcodes(direction);
                }
                case ClientVersionBuild.V5_4_2_17658:
                case ClientVersionBuild.V5_4_2_17688:
                {
                    return Opcodes_5_4_2.Opcodes(direction);
                }
                case ClientVersionBuild.V5_4_7_17898:
                case ClientVersionBuild.V5_4_7_17930:
                case ClientVersionBuild.V5_4_7_17956:
                case ClientVersionBuild.V5_4_7_18019:
                {
                    return Opcodes_5_4_7.Opcodes(direction);
                }
                case ClientVersionBuild.V5_4_8_18291:
                case ClientVersionBuild.V5_4_8_18414:
                {
                    return Opcodes_5_4_8.Opcodes(direction);
                }
                case ClientVersionBuild.V6_0_2_19033:
                case ClientVersionBuild.V6_0_2_19034:
                {
                    return Opcodes_6_0_2.Opcodes(direction);
                }
                case ClientVersionBuild.V6_0_3_19103:
                case ClientVersionBuild.V6_0_3_19116:
                case ClientVersionBuild.V6_0_3_19243:
                case ClientVersionBuild.V6_0_3_19342:
                {
                    return Opcodes_6_0_3.Opcodes(direction);
                }
                case ClientVersionBuild.V6_1_0_19678:
                case ClientVersionBuild.V6_1_0_19702:
                {
                    return Opcodes_6_1_0.Opcodes(direction);
                }
                case ClientVersionBuild.V6_1_2_19802:
                case ClientVersionBuild.V6_1_2_19831:
                case ClientVersionBuild.V6_1_2_19865:
                {
                    return Opcodes_6_1_2.Opcodes(direction);
                }
                case ClientVersionBuild.V6_2_0_20173:
                case ClientVersionBuild.V6_2_0_20182:
                case ClientVersionBuild.V6_2_0_20201:
                case ClientVersionBuild.V6_2_0_20216:
                case ClientVersionBuild.V6_2_0_20253:
                case ClientVersionBuild.V6_2_0_20338:
                {
                    return Opcodes_6_2_0.Opcodes(direction);
                }
                default:
                {
                    return Opcodes_3_3_5.Opcodes(direction);
                }
            }
        }

        public static Opcode GetOpcode(int opcodeId, Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                    return ClientDict.GetBySecond(opcodeId);
                case Direction.ServerToClient:
                    return ServerDict.GetBySecond(opcodeId);
                case Direction.Bidirectional:
                    return MiscDict.GetBySecond(opcodeId);
            }
            return default(Opcode); // Can never be called, anyway.
        }

        public static int GetOpcode(Opcode opcodeId, Direction direction)
        {
            switch (direction)
            {
                case Direction.ClientToServer:
                    return ClientDict.GetByFirst(opcodeId);
                case Direction.ServerToClient:
                    return ServerDict.GetByFirst(opcodeId);
                case Direction.Bidirectional:
                    return MiscDict.GetByFirst(opcodeId);
            }

            return 0;
        }

        public static string GetOpcodeName(int opcodeId, Direction direction, bool hex = true)
        {
            var opc = GetOpcode(opcodeId, direction);

            if (opc != 0)
                return opc.ToString();

            if (hex)
                return "0x" + opcodeId.ToString("X4", CultureInfo.InvariantCulture);
            return opcodeId.ToString();
        }
    }
}
