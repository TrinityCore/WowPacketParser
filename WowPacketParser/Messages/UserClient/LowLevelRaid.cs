using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct LowLevelRaid
    {
        public bool Enable;

        [Parser(Opcode.CMSG_LOW_LEVEL_RAID1)]
        [Parser(Opcode.CMSG_LOW_LEVEL_RAID2)]
        public static void HandleLowLevelRaidPackets(Packet packet)
        {
            packet.ReadBool("Allow");
        }
    }
}
