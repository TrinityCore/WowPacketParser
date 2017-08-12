using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            packet.ReadByte("Type");

            packet.ReadUInt32<MapId>("MapID");
            packet.ReadUInt32("DifficultyID");

            packet.ReadBit("Locked");
            packet.ReadBit("Extended");
        }
    }
}
