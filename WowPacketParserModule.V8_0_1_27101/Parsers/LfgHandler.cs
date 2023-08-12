using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class LfgHandler
    {
        public static void ReadLFGPlayerRewards801(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            var hasRewardItem = packet.ReadBit();
            var hasRewardCurrency = packet.ReadBit();
            if (hasRewardItem)
                Substructures.ItemHandler.ReadItemInstance(packet, indexes);
            packet.ReadUInt32("Quantity", indexes);
            packet.ReadInt32("BonusQuantity", indexes);
            if (hasRewardCurrency)
                packet.ReadInt32("RewardCurrency", indexes);
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet);

            packet.ReadByte("Result");
            packet.ReadByte("ResultDetail");

            var blackListCount = packet.ReadInt32("BlackListCount");
            var blackListNamesCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                blackListNamesCount = packet.ReadUInt32("BlackListNamesCount");

            for (int i = 0; i < blackListCount; i++)
            {
                packet.ReadPackedGuid128("Guid", i);

                var int160 = packet.ReadInt32("SlotsCount", i);

                for (int j = 0; j < int160; j++)
                {
                    packet.ReadInt32("Slot", i, j);
                    packet.ReadInt32("Reason", i, j);
                    packet.ReadInt32("SubReason1", i, j);
                    packet.ReadInt32("SubReason2", i, j);
                }
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                int[] blackListNamesLengths = new int[blackListNamesCount];
                for (int i = 0; i < blackListNamesCount; i++)
                {
                    blackListNamesLengths[i] = (int) packet.ReadBits(24);
                }

                for (int i = 0; i < blackListNamesCount; i++)
                {
                    packet.ReadDynamicString(blackListNamesLengths[i]);
                }
            }
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByteE<LfgRoleCheckStatus>("RoleCheckStatus");
            var joinSlotsCount = packet.ReadUInt32("JoinSlotsCount");
            var bgQueueIdsCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                bgQueueIdsCount = packet.ReadUInt32("BgQueueIDCount");
            else
                V6_0_2_19033.Parsers.BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32("GroupFinderActivityID");
            var membersCount = packet.ReadUInt32("MembersCount");

            for (var i = 0; i < joinSlotsCount; ++i)
                packet.ReadUInt32("JoinSlot", i);

            for (var i = 0; i < bgQueueIdsCount; i++)
                V6_0_2_19033.Parsers.BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);

            packet.ResetBitReader();
            packet.ReadBit("IsBeginning");
            packet.ReadBit("ShowRoleCheck");

            for (var i = 0; i < membersCount; ++i)
                V6_0_2_19033.Parsers.LfgHandler.ReadLFGRoleCheckUpdateMember(packet, i, "Members");
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD)]
        public static void HandleLfgPlayerReward(Packet packet)
        {
            packet.ReadUInt32("QueuedSlot");
            packet.ReadUInt32("ActualSlot");
            packet.ReadInt32("RewardMoney");
            packet.ReadInt32("AddedXP");

            var count = packet.ReadInt32("RewardsCount");
            for (var i = 0; i < count; ++i)
                ReadLFGPlayerRewards801(packet, i, "LFGPlayerRewards");
        }
    }
}
