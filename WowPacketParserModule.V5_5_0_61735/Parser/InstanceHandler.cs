using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadUInt32<MapId>("MapID");
            packet.ReadUInt32("DifficultyID");
            packet.ReadInt32("TimeLeft");
            var messageLen = packet.ReadBits(8);
            packet.ReadBit("Locked");
            packet.ReadBit("Extended");
            packet.ReadWoWString("Message", messageLen);
        }

        [Parser(Opcode.SMSG_BOSS_KILL)]
        public static void HandleBossKill(Packet packet)
        {
            packet.ReadUInt32("DungeonEncounterID");
        }
    }
}
