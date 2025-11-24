using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class LfgHandler
    {
        public static void ReadCliRideTicket(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("RequesterGuid", idx);
            packet.ReadInt32("Id", idx);
            packet.ReadInt32("Type", idx);
            packet.ReadTime64("Time", idx);
            packet.ResetBitReader();
            packet.ReadBit("Unknown925", idx);
        }

        public static void ReadLfgPlayerQuestReward(Packet packet, params object[] idx)
        {
            packet.ReadByte("Mask", idx);
            packet.ReadInt32("RewardMoney", idx);
            packet.ReadInt32("RewardXP", idx);

            var itemCount = packet.ReadUInt32("ItemCount", idx);
            var currencyCount = packet.ReadUInt32("CurrencyCount", idx);
            var bonusCurrencyCount = packet.ReadUInt32("BonusCurrency", idx);

            // Item
            for (var k = 0; k < itemCount; ++k)
            {
                packet.ReadInt32<ItemId>("ItemID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            // Currency
            for (var k = 0; k < currencyCount; ++k)
            {
                packet.ReadInt32("CurrencyID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            // BonusCurrency
            for (var k = 0; k < bonusCurrencyCount; ++k)
            {
                packet.ReadInt32("CurrencyID", idx, k);
                packet.ReadInt32("Quantity", idx, k);
            }

            packet.ResetBitReader();

            var hasRewardSpellId = packet.ReadBit("HasRewardSpellID", idx);
            var hasUnused1 = packet.ReadBit();
            var hasUnused2 = packet.ReadBit();
            var hasHonor = packet.ReadBit("HasHonor", idx);

            if (hasRewardSpellId)
                packet.ReadInt32("RewardSpellID", idx);

            if (hasUnused1)
                packet.ReadInt32("Unused1", idx);

            if (hasUnused2)
                packet.ReadUInt64("Unused2");

            if (hasHonor)
                packet.ReadInt32("Honor", idx);
        }

        public static void ReadLFGBlackList(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            var hasPlayerGuid = packet.ReadBit("HasPlayerGuid", idx);
            var lfglackListCount = packet.ReadUInt32("LFGBlackListCount", idx);

            if (hasPlayerGuid)
                packet.ReadPackedGuid128("PlayerGuid", idx);

            for (var i = 0; i < lfglackListCount; ++i)
            {
                packet.ReadUInt32("Slot", idx, i);
                packet.ReadUInt32("Reason", idx, i);
                packet.ReadInt32("SubReason1", idx, i);
                packet.ReadInt32("SubReason2", idx, i);
                packet.ReadUInt32("SoftLock", idx, i);
            }
        }

        public static void ReadLFGRoleCheckUpdateMember(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadByteE<LfgRoleFlag>("RolesDesired", idx);
            packet.ReadByte("Level", idx);
            packet.ReadBit("RoleCheckComplete", idx);
        }

        public static void ReadLFGListBlacklistEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ActivityID", indexes);
            packet.ReadInt32("Reason", indexes);
        }

        public static void ReadLfgBootInfo(Packet packet, params object[] idx)
        {
            packet.ReadBit("VoteInProgress", idx);
            packet.ReadBit("VotePassed", idx);
            packet.ReadBit("MyVoteCompleted", idx);
            packet.ReadBit("MyVote", idx);

            uint len = packet.ReadBits(9);
            packet.ReadPackedGuid128("Target", idx);
            packet.ReadUInt32("TotalVotes", idx);
            packet.ReadUInt32("BootVotes", idx);
            packet.ReadInt32("TimeLeft", idx);
            packet.ReadUInt32("VotesNeeded", idx);
            packet.ReadWoWString("Reason", len, idx);
        }

        public static void ReadLFGPlayerRewards(Packet packet, params object[] indexes)
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
            ReadCliRideTicket(packet, "RideTicket");

            packet.ReadInt32("Result");
            packet.ReadByte("ResultDetail");

            var blackListCount = packet.ReadInt32("BlackListCount");
            var blackListNamesCount = packet.ReadUInt32("BlackListNamesCount");

            for (int i = 0; i < blackListCount; i++)
                ReadLFGBlackList(packet, i, "LFGBlackList");


            int[] blackListNamesLengths = new int[blackListNamesCount];
            for (int i = 0; i < blackListNamesCount; i++)
            {
                blackListNamesLengths[i] = (int)packet.ReadBits(24);
            }

            for (int i = 0; i < blackListNamesCount; i++)
            {
                packet.ReadDynamicString(blackListNamesLengths[i]);
            }
        }

        [Parser(Opcode.SMSG_LFG_LIST_JOIN_RESULT)]
        public static void HandleLfgListJoinResult(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");
            packet.ReadInt32("ResultId");
            packet.ReadByte("Unk");
        }

        [Parser(Opcode.SMSG_LFG_LIST_SEARCH_STATUS)]
        public static void HandleLfgListSearchStatus(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");
            packet.ReadByte("Reason");

            packet.ResetBitReader();
            packet.ReadBit("UnkBit");
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");

            packet.ReadInt32("Slot");
            packet.ReadInt32("AvgWaitTimeMe");
            packet.ReadInt32("AvgWaitTime");

            for (int i = 0; i < 3; i++)
            {
                packet.ReadInt32("AvgWaitTimeByRole", i);
                packet.ReadByte("LastNeeded", i);
            }

            packet.ReadInt32("QueuedTime");
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByteE<LfgRoleCheckStatus>("RoleCheckStatus");
            var joinSlotsCount = packet.ReadUInt32("JoinSlotsCount");
            var bgQueueIdsCount = packet.ReadUInt32("BgQueueIDCount");
            packet.ReadInt32("GroupFinderActivityID");
            var membersCount = packet.ReadUInt32("MembersCount");

            for (var i = 0; i < joinSlotsCount; ++i)
                packet.ReadUInt32("JoinSlot", i);

            for (var i = 0; i < bgQueueIdsCount; i++)
                BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);

            packet.ResetBitReader();
            packet.ReadBit("IsBeginning");
            packet.ReadBit("ShowRoleCheck");

            for (var i = 0; i < membersCount; ++i)
                ReadLFGRoleCheckUpdateMember(packet, i, "Members");
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_STATUS)]
        public static void HandleLfgUpdateStatus(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");

            packet.ReadByte("SubType");
            packet.ReadByte("Reason");
            var slotsCount = packet.ReadInt32("SlotsCount");
            packet.ReadByte("RequestedRoles");
            var suspendedPlayersCount = packet.ReadInt32("SuspendedPlayersCount");
            packet.ReadUInt32<MapId>("QueueMapID");

            for (int i = 0; i < slotsCount; i++)
                packet.ReadInt32("Slots", i);

            for (int i = 0; i < suspendedPlayersCount; i++)
                packet.ReadPackedGuid128("SuspendedPlayers", i);

            packet.ResetBitReader();

            packet.ReadBit("IsParty");
            packet.ReadBit("NotifyUI");
            packet.ReadBit("Joined");
            packet.ReadBit("LfgJoined");
            packet.ReadBit("Queued");
            packet.ReadBit("Unused");
        }

        [Parser(Opcode.SMSG_LFG_INSTANCE_SHUTDOWN_COUNTDOWN)]
        public static void HandleLfgInstanceShutdownCountdown(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");
            packet.ReadUInt32("Timer");
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_EXPIRATION)]
        public static void HandleLfgListUpdateExpiration(Packet packet)
        {
            ReadCliRideTicket(packet, "Ticket");
            packet.ReadTime64("TimeoutTime");
            packet.ReadByte("Status");
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_BLACKLIST)]
        public static void HandleLFGListUpdateBlacklist(Packet packet)
        {
            var count = packet.ReadInt32("BlacklistEntryCount");
            for (int i = 0; i < count; i++)
                ReadLFGListBlacklistEntry(packet, i, "ListBlacklistEntry");
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            ReadCliRideTicket(packet);

            packet.ReadUInt64("InstanceID");
            packet.ReadUInt32("ProposalID");
            packet.ReadUInt32("Slot");
            packet.ReadSByte("State");
            packet.ReadUInt32("CompletedMask");
            packet.ReadUInt32("EncounterMask");

            var playerCount = packet.ReadUInt32("PlayersCount");
            packet.ReadByte("Unused");

            packet.ResetBitReader();
            packet.ReadBit("ValidCompletedMask");
            packet.ReadBit("ProposalSilent");
            packet.ReadBit("IsRequeue");

            for (var i = 0; i < playerCount; i++)
            {
                packet.ReadByte("Roles", i);

                packet.ResetBitReader();

                packet.ReadBit("Me", i);
                packet.ReadBit("SameParty", i);
                packet.ReadBit("MyParty", i);
                packet.ReadBit("Responded", i);
                packet.ReadBit("Accepted", i);
            }
        }

        [Parser(Opcode.SMSG_OPEN_LFG_DUNGEON_FINDER)]
        public static void HandleOpenLfgFinder(Packet packet)
        {
            packet.ReadLfgEntry("LfgEntry");
        }

        [Parser(Opcode.SMSG_LFG_TELEPORT_DENIED)]
        public static void HandleLFGTeleportDenied(Packet packet)
        {
            packet.ReadBits("Reason", 4);
        }

        [Parser(Opcode.SMSG_LFG_OFFER_CONTINUE)]
        public static void HandleLfgOfferContinue(Packet packet)
        {
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.SMSG_LFG_BOOT_PLAYER)]
        public static void HandleLfgBootPlayer(Packet packet)
        {
            ReadLfgBootInfo(packet);
        }

        [Parser(Opcode.SMSG_LFG_PARTY_INFO)]
        public static void HandleLfgPartyInfo(Packet packet)
        {
            var blackListCount = packet.ReadInt32("BlackListCount");
            for (var i = 0; i < blackListCount; i++)
                ReadLFGBlackList(packet, i);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var dungeonCount = packet.ReadInt32("DungeonCount");

            ReadLFGBlackList(packet, "LFGBlackList");

            // LfgPlayerDungeonInfo
            for (var i = 0; i < dungeonCount; ++i)
            {
                packet.ReadUInt32("Slot", i);
                packet.ReadInt32("CompletionQuantity", i);
                packet.ReadInt32("CompletionLimit", i);
                packet.ReadInt32("CompletionCurrencyID", i);
                packet.ReadInt32("SpecificQuantity", i);
                packet.ReadInt32("SpecificLimit", i);
                packet.ReadInt32("OverallQuantity", i);
                packet.ReadInt32("OverallLimit", i);
                packet.ReadInt32("PurseWeeklyQuantity", i);
                packet.ReadInt32("PurseWeeklyLimit", i);
                packet.ReadInt32("PurseQuantity", i);
                packet.ReadInt32("PurseLimit", i);
                packet.ReadInt32("Quantity", i);
                packet.ReadUInt32("CompletedMask", i);
                packet.ReadUInt32("EncounterMask", i);
                var shortageRewardCount = packet.ReadInt32("ShortageRewardCount", i);

                packet.ResetBitReader();

                packet.ReadBit("FirstReward", i);
                packet.ReadBit("ShortageEligible", i);

                ReadLfgPlayerQuestReward(packet, i, "Rewards");
                for (var j = 0; j < shortageRewardCount; ++j)
                    ReadLfgPlayerQuestReward(packet, i, j, "ShortageReward");
            }
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
                ReadLFGPlayerRewards(packet, i, "LFGPlayerRewards");
        }

        [Parser(Opcode.SMSG_ROLE_CHOSEN)]
        public static void HandleRoleChosen(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            packet.ReadByteE<LfgRoleFlag>("RoleMask");
            packet.ReadBit("Accepted");
        }

        [Parser(Opcode.SMSG_PARTY_NOTIFY_LFG_LEADER_CHANGE)]
        [Parser(Opcode.SMSG_LFG_DISABLED)]
        public static void HandleLfgZero(Packet packet)
        {
        }
    }
}
