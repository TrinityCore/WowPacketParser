using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct WardenData
    {
        public Data Packet;

        [Parser(Opcode.CMSG_WARDEN_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleWardenData(Packet packet)
        {
            var opcode = packet.ReadByteE<WardenClientOpcode>("Warden Client Opcode");

            switch (opcode)
            {
                case WardenClientOpcode.CheatCheckResults:
                    {
                        var length = packet.ReadUInt16("Check Result Length");
                        packet.ReadInt32("Check Result Checksum");
                        packet.ReadBytes("Check Results", length);

                        break;
                    }
                case WardenClientOpcode.TransformedSeed:
                    {
                        packet.ReadBytes("SHA1 Seed", 20);
                        break;
                    }
            }
        }


        [Parser(Opcode.CMSG_WARDEN_DATA, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleWardenData540(Packet packet)
        {
            var opcode = packet.ReadInt32E<WardenServerOpcode>("Warden Opcode");

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_WARDEN_DATA, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleWardenData547(Packet packet)
        {
            var len = packet.ReadInt32();

            packet.ReadBytes(len);
        }
    }
}
