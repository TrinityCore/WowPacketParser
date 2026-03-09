using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_INSTANCE_INFO)]
        public static void HandleInstanceInfo(Packet packet)
        {
            var count = packet.ReadInt32("LocksCount");
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32<MapId>("MapID", i);
                packet.ReadInt16<DifficultyId>("DifficultyID", i);
                packet.ReadInt64("InstanceID", i);
                packet.ReadInt32("TimeRemaining", i);
                packet.ReadInt32("Completed_mask", i);

                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                packet.ReadBit("Extended", i);
            }
        }

        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.SMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.ReadInt16<DifficultyId>("DifficultyID");
        }

        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_RAID_DIFFICULTY_SET)]
        public static void HandleSetRaidDifficulty(Packet packet)
        {
            packet.ReadInt32("Legacy");
            packet.ReadInt16<DifficultyId>("DifficultyID");
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            packet.ReadInt32("Type");
            packet.ReadUInt32<MapId>("MapID");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadInt32("TimeLeft");

            packet.ResetBitReader();
            var warningMessageLength = packet.ReadBits(8);
            packet.ReadBit("Locked");
            packet.ReadBit("Extended");

            packet.ReadWoWString("WarningMessage", warningMessageLength);
        }
    }
}
