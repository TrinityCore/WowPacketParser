using WowPacketParser.Misc;

namespace WowPacketParser.Enums.Version
{
    public static partial class Opcodes
    {
        public static string GetOpcodeName(int opcodeId)
        {
            return GetOpcodeName(opcodeId, ClientVersion.Version);
        }

        public static string GetOpcodeName(int opcodeId, ClientVersionBuild versionBuild)
        {
            var opcodeName = string.Empty;
            var found = false;
            switch (versionBuild)
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
                case ClientVersionBuild.V3_3_5a_12340:
                {
                    foreach (var pair in _V3_3_5_opcodes)
                        if (pair.Value == opcodeId)
                        {
                            opcodeName = pair.Key.ToString();
                            found = true;
                            break; // We've found what we want
                        }
                    break;
                }
                case ClientVersionBuild.V4_0_6_13596:
                case ClientVersionBuild.V4_0_6_13623:
                {
                    foreach (var pair in _V4_0_6_opcodes)
                        if (pair.Value == opcodeId)
                        {
                            opcodeName = pair.Key.ToString();
                            found = true;
                            break;
                        }
                    break;
                }
            }

            if (found == false)
                opcodeName = opcodeId.ToString();

            return opcodeName;
        }

        public static int GetOpcode(Opcode opcode)
        {
            return GetOpcode(opcode, ClientVersion.Version);
        }

        public static int GetOpcode(Opcode opcode, ClientVersionBuild versionBuild)
        {
            var opcodeId = 0;
            switch (versionBuild)
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
                case ClientVersionBuild.V3_3_5a_12340:
                {
                    _V3_3_5_opcodes.TryGetValue(opcode, out opcodeId);
                    break;
                }

                case ClientVersionBuild.V4_0_6_13596:
                case ClientVersionBuild.V4_0_6_13623:
                {
                    _V4_0_6_opcodes.TryGetValue(opcode, out opcodeId);
                    break;
                }
            }

            return opcodeId;
        }
    }
}
