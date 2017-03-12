using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAreaSpiritHealerTime
    {
        public ulong HealerGuid;
        public int TimeLeft;

        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAreaSpiritHealerTime(Packet packet)
        {
            packet.ReadGuid("HealerGuid");
            packet.ReadUInt32("TimeLeft");
        }

        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAreaSpiritHealerTime6(Packet packet)
        {
            packet.ReadPackedGuid128("HealerGuid");
            packet.ReadUInt32("TimeLeft");
        }
    }
}
