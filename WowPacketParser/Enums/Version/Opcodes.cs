using System.Globalization;
using PacketParser.Enums.Version.V3_3_5a_12340;
using PacketParser.Enums.Version.V4_0_3_13329;
using PacketParser.Enums.Version.V4_0_6_13596;
using PacketParser.Enums.Version.V4_1_0_13914;
using PacketParser.Enums.Version.V4_2_0_14480;
using PacketParser.Enums.Version.V4_2_2_14545;
using PacketParser.Enums.Version.V4_3_0_15005;
using PacketParser.Enums.Version.V4_3_2_15211;
using PacketParser.Enums.Version.V4_3_3_15354;
using PacketParser.Enums.Version.V4_3_4_15595;
using PacketParser.Misc;
using System;

namespace PacketParser.Enums.Version
{
    public static class Opcodes
    {
        [ThreadStatic]
        private static BiDictionary<Opcode, int> Dict;

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
                    return Opcodes_3_3_5.Opcodes();
                }
                case ClientVersionBuild.V4_0_3_13329:
                {
                    return Opcodes_4_0_3.Opcodes();
                }
                case ClientVersionBuild.V4_0_6_13596:
                case ClientVersionBuild.V4_0_6a_13623:
                {
                    return Opcodes_4_0_6.Opcodes();
                }
                case ClientVersionBuild.V4_1_0_13914:
                case ClientVersionBuild.V4_1_0a_14007:
                {
                    return Opcodes_4_1_0.Opcodes();
                }
                case ClientVersionBuild.V4_2_0_14333:
                case ClientVersionBuild.V4_2_0a_14480:
                {
                    return Opcodes_4_2_0.Opcodes();
                }
                case ClientVersionBuild.V4_2_2_14545:
                {
                    return Opcodes_4_2_2.Opcodes();
                }
                case ClientVersionBuild.V4_3_0_15005:
                case ClientVersionBuild.V4_3_0_15050:
                {
                    return Opcodes_4_3_0.Opcodes();
                }
                case ClientVersionBuild.V4_3_2_15211:
                {
                    return Opcodes_4_3_2.Opcodes();
                }
                case ClientVersionBuild.V4_3_3_15354:
                {
                    return Opcodes_4_3_3.Opcodes();
                }
                case ClientVersionBuild.V4_3_4_15595:
                {
                    return Opcodes_4_3_4.Opcodes();
                }
                default:
                {
                    return Opcodes_3_3_5.Opcodes();
                }
            }
        }

        public static void InitForClientVersion()
        {
            Dict = GetOpcodeDictionary(ClientVersion.Build);
        }

        public static Opcode GetOpcode(int opcodeId)
        {
            return Dict.GetBySecond(opcodeId);
        }

        public static int GetOpcode(Opcode opcode)
        {
            return Dict.GetByFirst(opcode);
        }

        public static string GetOpcodeName(int opcodeId)
        {
            var opc = GetOpcode(opcodeId);
            return opc == 0 ? opcodeId.ToString() : Enum<Opcode>.ToString(opc);
        }
    }
}
