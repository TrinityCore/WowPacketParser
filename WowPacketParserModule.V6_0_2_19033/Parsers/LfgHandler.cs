using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class LfgHandler
    {
        public static void ReadRideTicket(Packet packet)
        {
            packet.ReadPackedGuid128("RequesterGuid");
            packet.ReadInt32("Id");
            packet.ReadInt32("Type");
            packet.ReadTime("Time");
        }

        [Parser(Opcode.CMSG_LFG_LIST_GET_STATUS)]
        public static void HandleLfgZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var int16 = packet.ReadInt32("DungeonCount");

            // LFGBlackList
            packet.ResetBitReader();
            var bit16 = packet.ReadBit("HasPlayerGuid");
            var int24 = packet.ReadInt32("LFGBlackListCount");

            if (bit16)
                packet.ReadPackedGuid128("PlayerGuid");

            for (var i = 0; i < int24; ++i)
            {
                packet.ReadInt32("Slot", i);
                packet.ReadInt32("Reason", i);
                packet.ReadInt32("SubReason1", i);
                packet.ReadInt32("SubReason2", i);
            }

            // LfgPlayerDungeonInfo
            for (var i = 0; i < int16; ++i)
            {
                packet.ReadInt32("Slot", i);
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
                packet.ReadInt32("CompletedMask", i);

                var int64 = packet.ReadInt32("ShortageRewardCount", i);

                // Rewards
                packet.ReadUInt32("Mask", i);
                packet.ReadInt32("RewardMoney", i);
                packet.ReadInt32("RewardXP", i);

                var int20 = packet.ReadInt32("ItemCount", i);
                var int36 = packet.ReadInt32("CurrencyCount", i);
                var int52 = packet.ReadInt32("BonusCurrencyCount", i);

                // Item
                for (var j = 0; j < int20; ++j)
                {
                    packet.ReadInt32("ItemID", i, j);
                    packet.ReadInt32("Quantity", i, j);
                }

                // Currency
                for (var j = 0; j < int36; ++j)
                {
                    packet.ReadInt32("CurrencyID", i, j);
                    packet.ReadInt32("Quantity", i, j);
                }

                // BonusCurrency
                for (var j = 0; j < int52; ++j)
                {
                    packet.ReadInt32("CurrencyID", i, j);
                    packet.ReadInt32("Quantity", i, j);
                }

                packet.ResetBitReader();

                var bit3 = packet.ReadBit("hasBit3", i);
                if (bit3)
                    packet.ReadInt32("Unk 1", i);

                // ShortageReward
                for (var j = 0; j < int64; ++j)
                {
                    // Rewards
                    packet.ReadInt32("Mask", i, j);
                    packet.ReadInt32("RewardMoney", i, j);
                    packet.ReadInt32("RewardXP", i, j);

                    var int200 = packet.ReadInt32("ItemCount", i, j);
                    var int360 = packet.ReadInt32("CurrencyCount", i, j);
                    var int520 = packet.ReadInt32("QuantityCount", i, j);

                    // Item
                    for (var k = 0; k < int200; ++k)
                    {
                        packet.ReadInt32("ItemID", i, j, k);
                        packet.ReadInt32("Quantity", i, j, k);
                    }

                    // Currency
                    for (var k = 0; k < int360; ++k)
                    {
                        packet.ReadInt32("CurrencyID", i, j, k);
                        packet.ReadInt32("Quantity", i, j, k);
                    }

                    // BonusCurrency
                    for (var k = 0; k < int520; ++k)
                    {
                        packet.ReadInt32("CurrencyID", i, j, k);
                        packet.ReadInt32("Quantity", i, j, k);
                    }

                    packet.ResetBitReader();

                    var bit30 = packet.ReadBit("HasBit30", i, j);
                    if (bit30)
                        packet.ReadInt32("Unk 2", i, j);
                }

                packet.ResetBitReader();

                packet.ReadBit("FirstReward", i);
                packet.ReadBit("ShortageEligible", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            ReadRideTicket(packet);

            packet.ReadByte("Result");
            packet.ReadByte("ResultDetail");

            var int16 = packet.ReadInt32("BlackListCount");
            for (int i = 0; i < int16; i++)
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
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_STATUS)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            ReadRideTicket(packet);

            packet.ReadByte("SubType");
            packet.ReadByte("Reason");

            for (int i = 0; i < 3; i++)
                packet.ReadByte("Needs", i);

            var int8 = packet.ReadInt32("SlotsCount");
            packet.ReadInt32("RequestedRoles");
            var int4 = packet.ReadInt32("SuspendedPlayersCount");

            for (int i = 0; i < int8; i++)
                packet.ReadInt32("Slots", i);

            for (int i = 0; i < int4; i++)
                packet.ReadPackedGuid128("SuspendedPlayers", i);

            packet.ResetBitReader();

            packet.ReadBit("IsParty");
            packet.ReadBit("NotifyUI");
            packet.ReadBit("Joined");
            packet.ReadBit("LfgJoined");
            packet.ReadBit("Queued");

            var bits56 = packet.ReadBits(8);
            packet.ReadWoWString("Comment", bits56);
        }

        [Parser(Opcode.CMSG_LFG_LOCK_INFO_REQUEST)]
        public static void HandleLFGLockInfoRequest(Packet packet)
        {
            packet.ReadBit("Player");
            packet.ReadByte("PartyIndex");
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS)]
        public static void HandleLfgQueueStatusUpdate434(Packet packet)
        {
            ReadRideTicket(packet);

            packet.ReadInt32("Slot");
            packet.ReadInt32("AvgWaitTime");
            packet.ReadInt32("QueuedTime");

            for (int i = 0; i < 3; i++)
            {
                packet.ReadInt32("AvgWaitTimeByRole", i);
                packet.ReadByte("LastNeeded", i);
            }

            packet.ReadInt32("AvgWaitTimeMe");
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            ReadRideTicket(packet);

            packet.ReadInt64("InstanceID");

            packet.ReadInt32("ProposalID");
            packet.ReadInt32("Slot");

            packet.ReadByte("State");

            packet.ReadInt32("CompletedMask");
            var int68 = packet.ReadInt32("PlayersCount");
            for (int i = 0; i < int68; i++)
            {
                packet.ReadInt32("Roles", i);

                packet.ResetBitReader();

                packet.ReadBit("Me", i);
                packet.ReadBit("SameParty", i);
                packet.ReadBit("MyParty", i);
                packet.ReadBit("Responded", i);
                packet.ReadBit("Accepted", i);
            }

            packet.ResetBitReader();

            packet.ReadBit("ValidCompletedMask");
            packet.ReadBit("ProposalSilent");
        }

        public static void ReadLFGPlayerRewards(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("RewardItem", indexes);
            packet.ReadUInt32("RewardItemQuantity", indexes);
            packet.ReadInt32("BonusCurrency", indexes);
            packet.ReadBitBoolean("IsCurrency", indexes);
        }

        [Parser(Opcode.SMSG_LFG_PLAYER_REWARD)]
        public static void HandleLfgPlayerReward(Packet packet)
        {
            packet.ReadUInt32("ActualSlot"); // unconfirmed order
            packet.ReadUInt32("QueuedSlot"); // unconfirmed order
            packet.ReadInt32("RewardMoney");
            packet.ReadInt32("AddedXP");

            var count = packet.ReadInt32("RewardsCount");
            for (var i = 0; i < count; ++i)
                ReadLFGPlayerRewards(packet, i);
        }

        [Parser(Opcode.CMSG_DF_JOIN)]
        public static void HandleDFJoin(Packet packet)
        {
            packet.ReadBitBoolean("QueueAsGroup");
            var commentLength = packet.ReadBits("UnkBits8", 8);

            packet.ResetBitReader();

            packet.ReadByte("PartyIndex");
            packet.ReadEnum<LfgRoleFlag>("Roles", TypeCode.Int32);
            var slotsCount = packet.ReadInt32();

            for (var i = 0; i < 3; ++i) // Needs
                packet.ReadUInt32("Need", i);

            packet.ReadWoWString("Comment", commentLength);

            for (var i = 0; i < slotsCount; ++i) // Slots
                packet.ReadUInt32("Slot", i);
        }

        public static void ReadLFGRoleCheckUpdateMember(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadEnum<LfgRoleFlag>("RolesDesired", TypeCode.UInt32, idx);
            packet.ReadByte("Level", idx);
            packet.ReadBitBoolean("RoleCheckComplete", idx);

            packet.ResetBitReader();
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadEnum<LfgRoleCheckStatus>("RoleCheckStatus", TypeCode.Byte);
            var joinSlotsCount = packet.ReadInt32("JoinSlotsCount");
            packet.ReadUInt64("BgQueueID");
            packet.ReadInt32("ActivityID"); // NC
            var membersCount = packet.ReadInt32("MembersCount");

            for (var i = 0; i < joinSlotsCount; ++i) // JoinSlots
                packet.ReadUInt32("JoinSlot", i);

            for (var i = 0; i < membersCount; ++i) // Members
                ReadLFGRoleCheckUpdateMember(packet, i);

            packet.ReadBitBoolean("IsBeginning");
            packet.ReadBitBoolean("ShowRoleCheck"); // NC
        }
    }
}
