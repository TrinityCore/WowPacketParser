using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.ChallengeMode
{
    public unsafe struct RequestLeadersResult
    {
        public int MapID;


        [Parser(Opcode.SMSG_CHALLENGE_MODE_REQUEST_LEADERS_RESULT)]
        public static void HandleChallengeModeRequestLeadersResult(Packet packet)
        {
            packet.ReadInt32("MapID");
            packet.ReadTime("LastGuildUpdate");
            packet.ReadTime("LastRealmUpdate");

            var int4 = packet.ReadInt32("GuildLeadersCount");
            var int9 = packet.ReadInt32("RealmLeadersCount");

            for (int i = 0; i < int4; i++)
                ReadChallengeModeAttempt(packet, i, "GuildLeaders");

            for (int i = 0; i < int9; i++)
                ReadChallengeModeAttempt(packet, i, "RealmLeaders");
        }

        public static void ReadChallengeModeAttempt(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("InstanceRealmAddress", indexes);
            packet.ReadInt32("AttemptID", indexes);
            packet.ReadInt32("CompletionTime", indexes);
            packet.ReadPackedTime("CompletionDate", indexes);
            packet.ReadInt32("MedalEarned", indexes);

            var int12 = packet.ReadInt32("MembersCount", indexes);
            for (int i = 0; i < int12; i++)
            {
                packet.ReadInt32("VirtualRealmAddress", indexes, i);
                packet.ReadInt32("NativeRealmAddress", indexes, i);
                packet.ReadPackedGuid128("Guid", indexes, i);
                packet.ReadInt32("SpecializationID", indexes, i);
            }
        }
    }
}
