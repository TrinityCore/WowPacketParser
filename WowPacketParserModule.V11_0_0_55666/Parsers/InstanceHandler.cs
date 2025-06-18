using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_INSTANCE_INFO, ClientVersionBuild.V11_1_7_61491)]
        public static void HandleInstanceInfo(Packet packet)
        {
            var count = packet.ReadInt32("LocksCount");
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt64("InstanceID", i);
                packet.ReadInt32<MapId>("MapID", i);
                packet.ReadInt32<DifficultyId>("DifficultyID", i);
                packet.ReadInt32("TimeRemaining", i);
                packet.ReadInt32("Completed_mask", i);

                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                packet.ReadBit("Extended", i);
            }
        }

        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY, ClientVersionBuild.V11_1_7_61491)]
        [Parser(Opcode.SMSG_RAID_DIFFICULTY_SET, ClientVersionBuild.V11_1_7_61491)]
        public static void HandleSetRaidDifficulty(Packet packet)
        {
            packet.ReadInt32("Legacy");
            packet.ReadInt32<DifficultyId>("DifficultyID");
        }
    }
}
