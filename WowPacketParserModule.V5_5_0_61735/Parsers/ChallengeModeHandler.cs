using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ChallengeModeHandler
    {
        public static void ReadChallengeModeAttempt(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("InstanceRealmAddress", indexes);
            packet.ReadUInt32("AttemptID", indexes);
            packet.ReadInt32("CompletionTime", indexes);
            packet.ReadPackedTime("CompletionDate", indexes);
            packet.ReadUInt32("MedalEarned", indexes);

            var int12 = packet.ReadUInt32("MembersCount", indexes);
            for (int i = 0; i < int12; i++)
            {
                packet.ReadUInt32("VirtualRealmAddress", indexes, i);
                packet.ReadUInt32("NativeRealmAddress", indexes, i);
                packet.ReadPackedGuid128("Guid", indexes, i);
                packet.ReadInt32("SpecializationID", indexes, i);
            }
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_UPDATE_DEATH_COUNT)]
        public static void ChallengeModeUpdateDeathCount(Packet packet)
        {
            packet.ReadInt32("NewDeathCount");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_RESET)]
        public static void HandleChallengeModeReset(Packet packet)
        {
            packet.ReadUInt32<MapId>("MapID");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REQUEST_LEADERS_RESULT)]
        public static void HandleChallengeModeRequestLeadersResult(Packet packet)
        {
            packet.ReadInt32("MapID");
            packet.ReadInt32("Unk");
            packet.ReadTime64("LastGuildUpdate");
            packet.ReadTime64("LastRealmUpdate");

            var int4 = packet.ReadInt32("GuildLeadersCount");
            var int9 = packet.ReadInt32("RealmLeadersCount");

            for (int i = 0; i < int4; i++)
               ReadChallengeModeAttempt(packet, i, "GuildLeaders");

            for (int i = 0; i < int9; i++)
                ReadChallengeModeAttempt(packet, i, "RealmLeaders");
        }
    }
}
