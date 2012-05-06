using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static partial class Opcodes
    {
        private static BiDictionary<Opcode, int> GetOpcodeDictionary(ClientVersionBuild build)
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
                    return _V3_3_5_opcodes;
                }
                case ClientVersionBuild.V4_0_3_13329:
                {
                    return _V4_0_3_opcodes;
                }
                case ClientVersionBuild.V4_0_6_13596:
                case ClientVersionBuild.V4_0_6a_13623:
                {
                    return _V4_0_6_opcodes;
                }
                case ClientVersionBuild.V4_1_0_13914:
                case ClientVersionBuild.V4_1_0a_14007:
                {
                    return _V4_1_0_opcodes;
                }
                case ClientVersionBuild.V4_2_0_14333:
                case ClientVersionBuild.V4_2_0a_14480:
                {
                    return _V4_2_0_opcodes;
                }
                case ClientVersionBuild.V4_2_2_14545:
                {
                    return _V4_2_2_opcodes;
                }
                case ClientVersionBuild.V4_3_0_15005:
                case ClientVersionBuild.V4_3_0_15050:
                {
                    return _V4_3_0_opcodes;
                }
                case ClientVersionBuild.V4_3_2_15211:
                {
                    return _V4_3_2_opcodes;
                }
                case ClientVersionBuild.V4_3_3_15354:
                {
                    return _V4_3_3_opcodes;
                }
                case ClientVersionBuild.V4_3_4_15595:
                {
                    return _V4_3_4_opcodes;
                }
            }
            return _V3_3_5_opcodes; // Default case
        }

        public static Opcode GetOpcode(int opcodeId)
        {
            return GetOpcode(opcodeId, ClientVersion.Build);
        }

        private static Opcode GetOpcode(int opcodeId, ClientVersionBuild build)
        {
            return GetOpcodeDictionary(build).GetBySecond(opcodeId);
        }

        public static int GetOpcode(Opcode opcode)
        {
            return GetOpcode(opcode, ClientVersion.Build);
        }

        private static int GetOpcode(Opcode opcode, ClientVersionBuild build)
        {
            return GetOpcodeDictionary(build).GetByFirst(opcode);
        }

        public static string GetOpcodeName(int opcodeId)
        {
            return GetOpcode(opcodeId, ClientVersion.Build).ToString();
        }
    }
}
