
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_DO_COUNTDOWN, ClientVersionBuild.V10_1_7_51187)]
        public static void HandleDoCountdown(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadInt32("TotalTime");
            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_CHANGE_PLAYER_DIFFICULTY_RESULT)]
        public static void HandleChangePlayerDifficultyResult(Packet packet)
        {
            var result = packet.ReadBitsE<DifficultyChangeResult>("Result", 4);

            switch (result)
            {
                case DifficultyChangeResult.Cooldown:
                    packet.ReadBit("InCombat");
                    packet.ReadInt64("Cooldown");
                    break;
                case DifficultyChangeResult.LoadingScreenEnable:
                    packet.ReadBit("Unused");
                    packet.ReadInt64("NextDifficultyChangeTime");
                    break;
                case DifficultyChangeResult.MapDifficultyConditionNotSatisfied:
                    packet.ReadInt32E<MapDifficulty>("MapDifficultyID");
                    break;
                case DifficultyChangeResult.PlayerAlreadyLockedToDifferentInstance:
                    packet.ReadPackedGuid128("PlayerGUID");
                    break;
                case DifficultyChangeResult.Success:
                    packet.ReadInt32<MapId>("MapID");
                    packet.ReadInt32<DifficultyId>("DifficultyID");
                    break;
                default:
                    break;
            }
        }

        [Parser(Opcode.CMSG_PARTY_INVITE_RESPONSE, ClientVersionBuild.V10_1_7_51187)]
        public static void HandlePartyInviteResponse(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            packet.ReadBit("Accept");
            var hasRolesDesired = packet.ReadBit("HasRolesDesired");

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");

            if (hasRolesDesired)
                packet.ReadByte("RolesDesired");
        }
    }
}
