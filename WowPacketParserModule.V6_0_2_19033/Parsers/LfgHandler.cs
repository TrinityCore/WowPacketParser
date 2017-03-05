using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class LfgHandler
    {
        public static void ReadCliRideTicket(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("RequesterGuid", idx);
            packet.Translator.ReadInt32("Id", idx);
            packet.Translator.ReadInt32("Type", idx);
            packet.Translator.ReadTime("Time", idx);
        }

        public static void ReadLFGBlackList(Packet packet, params object[] idx)
        {
            packet.Translator.ResetBitReader();
            var bit16 = packet.Translator.ReadBit("HasPlayerGuid", idx);
            var int24 = packet.Translator.ReadInt32("LFGBlackListCount", idx);

            if (bit16)
                packet.Translator.ReadPackedGuid128("PlayerGuid", idx);

            for (var i = 0; i < int24; ++i)
            {
                packet.Translator.ReadUInt32("Slot", idx, i);
                packet.Translator.ReadUInt32("Reason", idx, i);
                packet.Translator.ReadInt32("SubReason1", idx, i);
                packet.Translator.ReadInt32("SubReason2", idx, i);
            }
        }

        public static void ReadLFGListBlacklistEntry(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("ActivityID", indexes);
            packet.Translator.ReadInt32("Reason", indexes);
        }

        public static void ReadLfgBootInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadBit("VoteInProgress", idx);
            packet.Translator.ReadBit("VotePassed", idx);
            packet.Translator.ReadBit("MyVoteCompleted", idx);
            packet.Translator.ReadBit("MyVote", idx);
            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadPackedGuid128("Target", idx);
            packet.Translator.ReadUInt32("TotalVotes", idx);
            packet.Translator.ReadUInt32("BootVotes", idx);
            packet.Translator.ReadInt32("TimeLeft", idx);
            packet.Translator.ReadUInt32("VotesNeeded", idx);
            packet.Translator.ReadWoWString("Reason", len, idx);
        }

        public static void ReadLFGListJoinRequest(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("ActivityID", idx);
            packet.Translator.ReadSingle("RequiredItemLevel", idx);

            packet.Translator.ResetBitReader();

            var lenName = packet.Translator.ReadBits(8);
            var lenComment = packet.Translator.ReadBits(11);
            var lenVoiceChat = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Name", lenName, idx);
            packet.Translator.ReadWoWString("Comment", lenComment, idx);
            packet.Translator.ReadWoWString("VoiceChat", lenVoiceChat, idx);
        }

        public static void ReadShortageReward(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Mask", idx);
            packet.Translator.ReadInt32("RewardMoney", idx);
            packet.Translator.ReadInt32("RewardXP", idx);

            var int200 = packet.Translator.ReadInt32("ItemCount", idx);
            var int360 = packet.Translator.ReadInt32("CurrencyCount", idx);
            var int520 = packet.Translator.ReadInt32("QuantityCount", idx);

            // Item
            for (var k = 0; k < int200; ++k)
            {
                packet.Translator.ReadInt32("ItemID", idx, k);
                packet.Translator.ReadInt32("Quantity", idx, k);
            }

            // Currency
            for (var k = 0; k < int360; ++k)
            {
                packet.Translator.ReadInt32("CurrencyID", idx, k);
                packet.Translator.ReadInt32("Quantity", idx, k);
            }

            // BonusCurrency
            for (var k = 0; k < int520; ++k)
            {
                packet.Translator.ReadInt32("CurrencyID", idx, k);
                packet.Translator.ReadInt32("Quantity", idx, k);
            }

            packet.Translator.ResetBitReader();

            var bit30 = packet.Translator.ReadBit("HasBit30", idx);
            if (bit30)
                packet.Translator.ReadInt32("Unk 2", idx);
        }

        [Parser(Opcode.CMSG_LFG_LIST_GET_STATUS)]
        [Parser(Opcode.CMSG_REQUEST_LFG_LIST_BLACKLIST)]
        public static void HandleLfgZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var int16 = packet.Translator.ReadInt32("DungeonCount");

            ReadLFGBlackList(packet, "LFGBlackList");

            // LfgPlayerDungeonInfo
            for (var i = 0; i < int16; ++i)
            {
                packet.Translator.ReadInt32("Slot", i);
                packet.Translator.ReadInt32("CompletionQuantity", i);
                packet.Translator.ReadInt32("CompletionLimit", i);
                packet.Translator.ReadInt32("CompletionCurrencyID", i);
                packet.Translator.ReadInt32("SpecificQuantity", i);
                packet.Translator.ReadInt32("SpecificLimit", i);
                packet.Translator.ReadInt32("OverallQuantity", i);
                packet.Translator.ReadInt32("OverallLimit", i);
                packet.Translator.ReadInt32("PurseWeeklyQuantity", i);
                packet.Translator.ReadInt32("PurseWeeklyLimit", i);
                packet.Translator.ReadInt32("PurseQuantity", i);
                packet.Translator.ReadInt32("PurseLimit", i);
                packet.Translator.ReadInt32("Quantity", i);
                packet.Translator.ReadInt32("CompletedMask", i);

                var int64 = packet.Translator.ReadInt32("ShortageRewardCount", i);

                ReadShortageReward(packet, i, "ShortageReward");

                // ShortageReward
                for (var j = 0; j < int64; ++j)
                    ReadShortageReward(packet, i ,j, "ShortageReward");

                packet.Translator.ResetBitReader();

                packet.Translator.ReadBit("FirstReward", i);
                packet.Translator.ReadBit("ShortageEligible", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            ReadCliRideTicket(packet);

            packet.Translator.ReadByte("Result");
            packet.Translator.ReadByte("ResultDetail");

            var int16 = packet.Translator.ReadInt32("BlackListCount");
            for (int i = 0; i < int16; i++)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);

                var int160 = packet.Translator.ReadInt32("SlotsCount", i);

                for (int j = 0; j < int160; j++)
                {
                    packet.Translator.ReadInt32("Slot", i, j);
                    packet.Translator.ReadInt32("Reason", i, j);
                    packet.Translator.ReadInt32("SubReason1", i, j);
                    packet.Translator.ReadInt32("SubReason2", i, j);
                }
            }
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_STATUS)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            ReadCliRideTicket(packet);

            packet.Translator.ReadByte("SubType");
            packet.Translator.ReadByte("Reason");

            for (int i = 0; i < 3; i++)
                packet.Translator.ReadByte("Needs", i);

            var int8 = packet.Translator.ReadInt32("SlotsCount");
            packet.Translator.ReadInt32("RequestedRoles");
            var int4 = packet.Translator.ReadInt32("SuspendedPlayersCount");

            for (int i = 0; i < int8; i++)
                packet.Translator.ReadInt32("Slots", i);

            for (int i = 0; i < int4; i++)
                packet.Translator.ReadPackedGuid128("SuspendedPlayers", i);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("IsParty");
            packet.Translator.ReadBit("NotifyUI");
            packet.Translator.ReadBit("Joined");
            packet.Translator.ReadBit("LfgJoined");
            packet.Translator.ReadBit("Queued");

            var bits56 = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Comment", bits56);
        }

        [Parser(Opcode.CMSG_DF_GET_SYSTEM_INFO)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.Translator.ReadBit("Player");
            packet.Translator.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS)]
        public static void HandleLfgQueueStatusUpdate434(Packet packet)
        {
            ReadCliRideTicket(packet);

            packet.Translator.ReadInt32("Slot");
            packet.Translator.ReadInt32("AvgWaitTime");
            packet.Translator.ReadInt32("QueuedTime");

            for (int i = 0; i < 3; i++)
            {
                packet.Translator.ReadInt32("AvgWaitTimeByRole", i);
                packet.Translator.ReadByte("LastNeeded", i);
            }

            packet.Translator.ReadInt32("AvgWaitTimeMe");
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            ReadCliRideTicket(packet);

            packet.Translator.ReadInt64("InstanceID");

            packet.Translator.ReadInt32("ProposalID");
            packet.Translator.ReadInt32("Slot");

            packet.Translator.ReadByte("State");

            packet.Translator.ReadInt32("CompletedMask");
            var int68 = packet.Translator.ReadInt32("PlayersCount");
            for (int i = 0; i < int68; i++)
            {
                packet.Translator.ReadInt32("Roles", i);

                packet.Translator.ResetBitReader();

                packet.Translator.ReadBit("Me", i);
                packet.Translator.ReadBit("SameParty", i);
                packet.Translator.ReadBit("MyParty", i);
                packet.Translator.ReadBit("Responded", i);
                packet.Translator.ReadBit("Accepted", i);
            }

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("ValidCompletedMask");
            packet.Translator.ReadBit("ProposalSilent");
        }

        public static void ReadLFGPlayerRewards(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("RewardItem", indexes);
            packet.Translator.ReadUInt32("RewardItemQuantity", indexes);
            packet.Translator.ReadInt32("BonusCurrency", indexes);
            packet.Translator.ReadBit("IsCurrency", indexes);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD)]
        public static void HandleLfgPlayerReward(Packet packet)
        {
            packet.Translator.ReadUInt32("ActualSlot"); // unconfirmed order
            packet.Translator.ReadUInt32("QueuedSlot"); // unconfirmed order
            packet.Translator.ReadInt32("RewardMoney");
            packet.Translator.ReadInt32("AddedXP");

            var count = packet.Translator.ReadInt32("RewardsCount");
            for (var i = 0; i < count; ++i)
                ReadLFGPlayerRewards(packet, i);
        }

        [Parser(Opcode.CMSG_DF_JOIN)]
        public static void HandleDFJoin(Packet packet)
        {
            packet.Translator.ReadBit("QueueAsGroup");
            var commentLength = packet.Translator.ReadBits("UnkBits8", 8);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadInt32E<LfgRoleFlag>("Roles");
            var slotsCount = packet.Translator.ReadInt32();

            for (var i = 0; i < 3; ++i) // Needs
                packet.Translator.ReadUInt32("Need", i);

            packet.Translator.ReadWoWString("Comment", commentLength);

            for (var i = 0; i < slotsCount; ++i) // Slots
                packet.Translator.ReadUInt32("Slot", i);
        }

        public static void ReadLFGRoleCheckUpdateMember(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("Guid", idx);
            packet.Translator.ReadUInt32E<LfgRoleFlag>("RolesDesired", idx);
            packet.Translator.ReadByte("Level", idx);
            packet.Translator.ReadBit("RoleCheckComplete", idx);

            packet.Translator.ResetBitReader();
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadByteE<LfgRoleCheckStatus>("RoleCheckStatus");
            var joinSlotsCount = packet.Translator.ReadInt32("JoinSlotsCount");
            packet.Translator.ReadUInt64("BgQueueID");
            packet.Translator.ReadInt32("ActivityID"); // NC
            var membersCount = packet.Translator.ReadInt32("MembersCount");

            for (var i = 0; i < joinSlotsCount; ++i) // JoinSlots
                packet.Translator.ReadUInt32("JoinSlot", i);

            for (var i = 0; i < membersCount; ++i) // Members
                ReadLFGRoleCheckUpdateMember(packet, i);

            packet.Translator.ReadBit("IsBeginning");
            packet.Translator.ReadBit("ShowRoleCheck"); // NC
        }

        [Parser(Opcode.SMSG_ROLE_CHOSEN)]
        public static void HandleRoleChosen(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Player");
            packet.Translator.ReadUInt32E<LfgRoleFlag>("RoleMask");
            packet.Translator.ReadBit("Accepted");
        }

        [Parser(Opcode.SMSG_LFG_PARTY_INFO)]
        public static void HandleLfgPartyInfo(Packet packet)
        {
            var blackListCount = packet.Translator.ReadInt32("BlackListCount");
            for (var i = 0; i < blackListCount; i++)
                ReadLFGBlackList(packet, i);
        }

        [Parser(Opcode.SMSG_LFG_BOOT_PLAYER)]
        public static void HandleLfgBootPlayer(Packet packet)
        {
            ReadLfgBootInfo(packet);
        }

        [Parser(Opcode.CMSG_DF_BOOT_PLAYER_VOTE)]
        public static void HandleDFBootPlayerVote(Packet packet)
        {
            packet.Translator.ReadBit("Vote");
        }

        [Parser(Opcode.CMSG_DF_PROPOSAL_RESPONSE)]
        public static void HandleDFProposalResponse(Packet packet)
        {
            ReadCliRideTicket(packet);
            packet.Translator.ReadInt64("InstanceID");
            packet.Translator.ReadInt32("ProposalID");
            packet.Translator.ReadBit("Accepted");
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_BLACKLIST)]
        public static void HandleLFGListUpdateBlacklist(Packet packet)
        {
            var count = packet.Translator.ReadInt32("BlacklistEntryCount");
            for (int i = 0; i < count; i++)
                ReadLFGListBlacklistEntry(packet, i, "ListBlacklistEntry");
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_STATUS)]
        public static void HandleLFGListUpdateStatus(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");
            ReadLFGListJoinRequest(packet, "LFGListJoinRequest");
            packet.Translator.ReadInt32("Unk");
            packet.Translator.ReadByte("Reason");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Listed");
        }

        [Parser(Opcode.SMSG_LFG_TELEPORT_DENIED)]
        public static void HandleLFGTeleportDenied(Packet packet)
        {
            packet.Translator.ReadBits("Reason", 4);
        }

        [Parser(Opcode.CMSG_LFG_LIST_INVITE_RESPONSE)]
        public static void HandleLFGListInviteResponse(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("Accept");
        }

        [Parser(Opcode.CMSG_DF_TELEPORT)]
        public static void HandleDFTeleport(Packet packet)
        {
            packet.Translator.ReadBit("TeleportOut");
        }

        [Parser(Opcode.CMSG_DF_SET_ROLES)]
        public static void HandleDFSetRoles(Packet packet)
        {
            packet.Translator.ReadUInt32("RolesDesired");
            packet.Translator.ReadByte("PartyIndex");
        }

        [Parser(Opcode.CMSG_DF_LEAVE)]
        public static void HandleDFLeave(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");
        }

        [Parser(Opcode.CMSG_LFG_LIST_JOIN)]
        public static void HandleLFGListJoin(Packet packet)
        {
            ReadLFGListJoinRequest(packet, "LFGListJoinRequest");
        }

        [Parser(Opcode.CMSG_LFG_LIST_LEAVE)]
        public static void HandleLFGListLeave(Packet packet)
        {
            ReadCliRideTicket(packet, "RideTicket");
        }

        [Parser(Opcode.CMSG_LFG_LIST_SEARCH)] // To-Do: Rename Unks
        public static void HandleLFGListSearch(Packet packet)
        {
            var len = packet.Translator.ReadBits(6);
            var bits92 = packet.Translator.ReadBits("Bits92", 7);

            packet.Translator.ReadInt32("Int64");
            packet.Translator.ReadInt32("Int68");
            packet.Translator.ReadInt32("Int72");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadInt32("Int76");
            var int72 = packet.Translator.ReadInt32("BlacklistEntryCount");

            packet.Translator.ReadWoWString("String", len);

            for (int i = 0; i < bits92; i++)
                packet.Translator.ReadPackedGuid128("SmartGuid96", i); // PartyMember?

            for (int i = 0; i < int72; i++)
                ReadLFGListBlacklistEntry(packet, i, "ListBlacklistEntry");
        }

        [Parser(Opcode.CMSG_SET_LFG_BONUS_FACTION_ID)]
        public static void HandleSetLFGBonusFactionID(Packet packet)
        {
            packet.Translator.ReadInt32("FactionID");
        }
    }
}
