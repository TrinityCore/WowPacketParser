using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.SMSG_LFG_PLAYER_INFO)]
        public static void HandleLfgPlayerLockInfoResponse(Packet packet)
        {
            var guid = new byte[8];

            var hasGuid = packet.ReadBit();
            if (hasGuid)
                packet.StartBitStream(guid, 0, 6, 7, 5, 2, 4, 1, 3);

            var dungeonsSize = packet.ReadBits(17);

            var bonusCurrencySize = new uint[dungeonsSize];
            var currencySize = new uint[dungeonsSize];
            var ShortageRewardSize = new uint[dungeonsSize];
            var bits1C = new uint[dungeonsSize][];
            var bits2C = new uint[dungeonsSize][];
            var ShortageRewardItemsSize = new uint[dungeonsSize][];
            var bit4 = new bool[dungeonsSize];
            var RewardItemsCount = new uint[dungeonsSize];
            var bit3C = new bool[dungeonsSize];

            for (var i = 0; i < dungeonsSize; i++)
            {
                bonusCurrencySize[i] = packet.ReadBits(21);
                currencySize[i] = packet.ReadBits(21);
                ShortageRewardSize[i] = packet.ReadBits(19);

                bits1C[i] = new uint[ShortageRewardSize[i]];
                bits2C[i] = new uint[ShortageRewardSize[i]];
                ShortageRewardItemsSize[i] = new uint[ShortageRewardSize[i]];

                for (var j = 0; j < ShortageRewardSize[i]; j++)
                {
                    bits1C[i][j] = packet.ReadBits(21);
                    bits2C[i][j] = packet.ReadBits(21);
                    ShortageRewardItemsSize[i][j] = packet.ReadBits(20);
                }

                bit3C[i] = packet.ReadBit();
                bit4[i] = packet.ReadBit();
                RewardItemsCount[i] = packet.ReadBits(20);
            }

            var bits20 = packet.ReadBits(20);

            for (var i = 0; i < dungeonsSize; i++)
            {

                for (var j = 0; j < ShortageRewardSize[i]; j++)
                {
                    for (var k = 0; k < ShortageRewardItemsSize[i][j]; k++)
                    {
                        packet.ReadInt32("ShortageRewardItemID", i, j, k);
                        packet.ReadInt32("ShortageRewardItemDisplayID", i, j, k);
                        packet.ReadInt32("ShortageRewardItemCount", i, j, k);
                    }

                    for (var k = 0; k < bits2C[i][j]; k++)
                    {
                        packet.ReadInt32("ShortageRewardCurrencyUnk1", i, j, k);
                        packet.ReadInt32("ShortageRewardCurrencyUnk2", i, j, k);
                    }

                    for (var k = 0; k < bits1C[i][j]; k++)
                    {
                        packet.ReadInt32("ShortageRewardCurrencyUnk3", i, j, k);
                        packet.ReadInt32("ShortageRewardCurrencyUnk4", i, j, k);
                    }

                    packet.ReadInt32("ShortageRewardXP", i, j);
                    packet.ReadInt32("ShortageRewardMask", i, j);
                    packet.ReadInt32("ShortageRewardMoney", i, j);
                }

                packet.ReadInt32("PurseLimit", i);
                packet.ReadInt32("CompletionLimit", i);

                for (var j = 0; j < bonusCurrencySize[i]; j++)
                {
                    packet.ReadInt32("BonusCurrencyCount", i, j);
                    packet.ReadInt32("BonusCurrencyID", i, j);
                }

                packet.ReadInt32("SpecificLimit", i);
                packet.ReadInt32("CompletionQuantity", i);
                packet.ReadInt32("PurseWeeklyLimit", i);

                for (var j = 0; j < RewardItemsCount[i]; j++)
                {
                    packet.ReadInt32("ItemDisplayId", i, j);
                    packet.ReadInt32("ItemQuantity", i, j);
                    packet.ReadInt32("ItemId", i, j);
                }

                for (var j = 0; j < currencySize[i]; j++)
                {
                    packet.ReadInt32("CurrencyId", i, j);
                    packet.ReadInt32("CurrencyQuantity", i, j);
                }

                packet.ReadInt32("Quantity", i);
                packet.ReadInt32("CompletedEncouterMask", i);
                packet.ReadInt32("PurseWeeklyQuantity", i);
                packet.ReadInt32("OverallLimit", i);
                packet.ReadInt32("RewardMoney", i);
                packet.ReadInt32("PurseQuantity", i);
                packet.ReadInt32("RewardXP", i);
                packet.ReadInt32("OverallQuantity", i);
                packet.ReadInt32("SpecificQuantity", i);
                packet.ReadInt32("CompletionCurrencyID", i);
                packet.ReadInt32("RewardMask", i);
                packet.ReadInt32("DungeonID", i);
            }

            if (hasGuid)
            {
                packet.ParseBitStream(guid, 6, 3, 0, 4, 5, 1, 2, 7);
                packet.WriteGuid("Guid", guid);
            }

            for (var i = 0; i < bits20; i++)
            {
                packet.ReadInt32("Slot", i);
                packet.ReadInt32("SubReason1", i);
                packet.ReadInt32("Reason", i);
                packet.ReadInt32("SubReason2", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_QUEUE_STATUS)]
        public static void HandleLfgQueueStatusUpdate434(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("TicketTime");
            packet.ReadInt32("TicketId");
            packet.ReadInt32("AvgWaitTimeMe");
            packet.ReadInt32("Slot");

            for (var i = 0; i < 3; ++i)
            {
                packet.ReadByte("LastNeeded", i);
                packet.ReadInt32("AvgWaitTimeByRole", i);

            }

            packet.ReadInt32("TicketType");
            packet.ReadInt32("QueuedTime");
            packet.ReadInt32("AvgWaitTime");

            packet.StartBitStream(guid, 2, 0, 6, 5, 1, 4, 7, 3);
            packet.ParseBitStream(guid, 6, 1, 2, 4, 7, 3, 5, 0);

            packet.WriteGuid("RequesterGuid", guid);
        }

        [Parser(Opcode.SMSG_LFG_PARTY_INFO)]
        public static void HandleLfgPartyInfo(Packet packet)
        {
            var playersSize = packet.ReadBits(22);

            var slotSizes = new uint[playersSize];
            var hasGuid = new bool[playersSize];
            var guids = new byte[playersSize][];

            for (var i = 0; i < playersSize; i++)
            {
                slotSizes[i] = packet.ReadBits(20);
                hasGuid[i] = packet.ReadBit();

                if (hasGuid[i])
                {
                    guids[i] = new byte[8];
                    packet.StartBitStream(guids[i], 2, 6, 1, 0, 7, 5, 4, 3);
                }
            }

            for (var i = 0; i < playersSize; i++)
            {
                if (hasGuid[i])
                    packet.ParseBitStream(guids[i], 7, 2, 4, 1, 5, 0, 6, 3);

                packet.WriteGuid("Guid", guids[i]);

                for (var s = 0; s < slotSizes[i]; ++s)
                {
                    packet.ReadUInt32("Reason", i, s);
                    packet.ReadInt32("SubReason2", i, s);
                    packet.ReadUInt32("Slot", i, s);
                    packet.ReadInt32("SubReason1", i, s);
                }
            }
        }

        [Parser(Opcode.SMSG_LFG_ROLE_CHECK_UPDATE)]
        public static void HandleLfgRoleCheck(Packet packet)
        {
            var bgQueueID = new byte[8];

            bgQueueID[0] = packet.ReadBit();
            bgQueueID[1] = packet.ReadBit();
            bgQueueID[6] = packet.ReadBit();
            var membersSize = packet.ReadBits(21);

            var membersGuid = new byte[membersSize][];

            for (var i = 0; i < membersSize; ++i)
            {
                membersGuid[i] = new byte[8];

                membersGuid[i][4] = packet.ReadBit();
                membersGuid[i][1] = packet.ReadBit();
                membersGuid[i][2] = packet.ReadBit();
                membersGuid[i][6] = packet.ReadBit();
                packet.ReadBit("MemberRoleCheckComplete", i);
                membersGuid[i][5] = packet.ReadBit();
                membersGuid[i][7] = packet.ReadBit();
                membersGuid[i][0] = packet.ReadBit();
                membersGuid[i][3] = packet.ReadBit();
            }

            bgQueueID[7] = packet.ReadBit();
            var joinSlotsSize = packet.ReadBits(22);
            packet.ReadBit("IsBeginning");
            bgQueueID[3] = packet.ReadBit();
            bgQueueID[5] = packet.ReadBit();
            bgQueueID[4] = packet.ReadBit();
            bgQueueID[2] = packet.ReadBit();

            for (var i = 0; i < membersSize; ++i)
            {
                packet.ReadUInt32("MemberRolesDesired", i);
                packet.ReadXORBytes(membersGuid[i], 0);
                packet.ReadXORBytes(membersGuid[i], 2);
                packet.ReadXORBytes(membersGuid[i], 5);
                packet.ReadXORBytes(membersGuid[i], 4);
                packet.ReadXORBytes(membersGuid[i], 7);
                packet.ReadXORBytes(membersGuid[i], 6);
                packet.ReadXORBytes(membersGuid[i], 1);
                packet.ReadXORBytes(membersGuid[i], 3);
                packet.ReadByte("MemberLevel", i);
            }

            packet.ReadXORBytes(bgQueueID, 4);

            for (var i = 0; i < joinSlotsSize; ++i)
                packet.ReadUInt32("JoinSlot", i);

            packet.ReadXORBytes(bgQueueID, 6);
            packet.ReadXORBytes(bgQueueID, 7);
            packet.ReadXORBytes(bgQueueID, 0);
            packet.ReadXORBytes(bgQueueID, 3);
            packet.ReadByte("PartyIndex");
            packet.ReadXORBytes(bgQueueID, 5);
            packet.ReadByteE<LfgRoleCheckStatus>("RoleCheckStatus");
            packet.ReadXORBytes(bgQueueID, 2);
            packet.ReadXORBytes(bgQueueID, 1);

            packet.WriteGuid("BgQueueID", bgQueueID);
        }

        [Parser(Opcode.SMSG_LFG_JOIN_RESULT)]
        public static void HandleLfgJoinResult(Packet packet)
        {
            packet.ReadUInt32("Ticket.Type");
            packet.ReadByte("Result");
            packet.ReadByte("ResultDetail");
            packet.ReadUInt32("Ticket.Time");
            packet.ReadUInt32("Ticket.Id");

            var requesterGuid = new byte[8];

            requesterGuid[4] = packet.ReadBit();
            requesterGuid[6] = packet.ReadBit();
            requesterGuid[2] = packet.ReadBit();
            requesterGuid[5] = packet.ReadBit();
            requesterGuid[0] = packet.ReadBit();
            requesterGuid[1] = packet.ReadBit();
            var int16 = packet.ReadBits("BlackListCount", 22);
            requesterGuid[3] = packet.ReadBit();

            var blacklistGuid = new byte[int16][];
            var slotsCount = new uint[int16];

            for (int i = 0; i < int16; i++)
            {
                blacklistGuid[i] = new byte[8];

                blacklistGuid[i][7] = packet.ReadBit();
                blacklistGuid[i][0] = packet.ReadBit();
                blacklistGuid[i][1] = packet.ReadBit();
                blacklistGuid[i][6] = packet.ReadBit();
                blacklistGuid[i][2] = packet.ReadBit();
                blacklistGuid[i][4] = packet.ReadBit();
                slotsCount[i] = packet.ReadBits(20);
                blacklistGuid[i][3] = packet.ReadBit();
                blacklistGuid[i][5] = packet.ReadBit();
            }

            requesterGuid[7] = packet.ReadBit();
            packet.ReadXORBytes(requesterGuid, 5);

            for (int i = 0; i < int16; i++)
            {
                for (int j = 0; j < slotsCount[i]; j++)
                {
                    packet.ReadInt32("SubReason1", i, j);
                    packet.ReadInt32("Reason", i, j);
                    packet.ReadInt32("Slot", i, j);
                    packet.ReadInt32("SubReason2", i, j);
                }

                foreach (var s in new byte[] { 1, 4, 7, 6, 3, 2, 0, 5 })
                    packet.ReadXORBytes(blacklistGuid[i], s);
            }

            packet.ReadXORBytes(requesterGuid, 0);
            packet.ReadXORBytes(requesterGuid, 4);
            packet.ReadXORBytes(requesterGuid, 3);
            packet.ReadXORBytes(requesterGuid, 7);
            packet.ReadXORBytes(requesterGuid, 2);
            packet.ReadXORBytes(requesterGuid, 6);
            packet.ReadXORBytes(requesterGuid, 1);

            packet.WriteGuid("RequesterGuid", requesterGuid);
        }

        [Parser(Opcode.CMSG_DF_LEAVE)]
        public static void HandleDFLeave(Packet packet)
        {
            packet.ReadInt32E<LfgRoleFlag>("Roles");
            packet.ReadTime("Time");
            packet.ReadUInt32("Instance Id");
            packet.ReadInt32("Type");

            var guid = packet.StartBitStream(0, 1, 6, 7, 3, 5, 2, 4);
            packet.ParseBitStream(guid, 1, 5, 6, 7, 4, 2, 3, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_DF_JOIN)]
        public static void HandleLfgJoin(Packet packet)
        {
            packet.ReadByte("PartyIndex");

            for (int i = 0; i < 3; ++i)
                packet.ReadInt32("Needs", i);

            packet.ReadInt32E<LfgRoleFlag>("Roles");

            var slotsCount = packet.ReadBits(22);
            var commentLen = packet.ReadBits(8);
            packet.ReadBit("QueueAsGroup");

            packet.ReadWoWString("Comment", commentLen);

            for (var i = 0; i < slotsCount; i++)
                packet.ReadLfgEntry("Dungeon Entry", i);
        }

        [Parser(Opcode.SMSG_LFG_UPDATE_STATUS)]
        public static void HandleLfgQueueStatusUpdate(Packet packet)
        {
            var requesterGuid = new byte[8];
            var slotsSize = packet.ReadBits(22);

            requesterGuid[7] = packet.ReadBit();
            packet.ReadBit("Queued");
            packet.ReadBit("IsParty");
            requesterGuid[5] = packet.ReadBit();
            requesterGuid[3] = packet.ReadBit();
            var suspendedPlayersSize = packet.ReadBits(24);
            requesterGuid[6] = packet.ReadBit();
            packet.ReadBit("NotifyUI");
            requesterGuid[2] = packet.ReadBit();
            requesterGuid[1] = packet.ReadBit();

            var suspendedGuids = new byte[suspendedPlayersSize][];

            for (int i = 0; i < suspendedPlayersSize; ++i)
            {
                suspendedGuids[i] = new byte[8];
                packet.StartBitStream(suspendedGuids[i], 1, 3, 6, 7, 4, 5, 2, 0);
            }

            requesterGuid[0] = packet.ReadBit();
            packet.ReadBit("Joined");
            requesterGuid[4] = packet.ReadBit();
            var commentSize = packet.ReadBits(8);
            packet.ReadBit("LfgJoined");

            for (int i = 0; i < suspendedPlayersSize; ++i)
                packet.ParseBitStream(suspendedGuids[i], 6, 5, 2, 0, 1, 7, 4, 3);

            packet.ReadInt32("RequestedRoles");
            packet.ReadInt32("TicketTime");
            packet.ReadXORByte(requesterGuid, 1);

            for (int i = 0; i < 3; i++)
                packet.ReadByte("Needs", i);

            packet.ReadXORByte(requesterGuid, 7);

            for (int i = 0; i < slotsSize; i++)
                packet.ReadInt32("Slots", i);

            packet.ReadXORByte(requesterGuid, 4);
            packet.ReadXORByte(requesterGuid, 5);
            packet.ReadWoWString("Comment", commentSize);
            packet.ReadByte("Reason");
            packet.ReadInt32("TicketId");
            packet.ReadByte("SubType");
            packet.ReadInt32("TicketType");
            packet.ReadXORByte(requesterGuid, 2);
            packet.ReadXORByte(requesterGuid, 3);
            packet.ReadXORByte(requesterGuid, 6);
            packet.ReadXORByte(requesterGuid, 0);

            packet.WriteGuid("RequesterGuid", requesterGuid);
        }

        [Parser(Opcode.SMSG_LFG_BOOT_PLAYER)]
        public static void HandleLfgBootPlayer(Packet packet)
        {
            var target = new byte[8];

            target[3] = packet.ReadBit();
            target[2] = packet.ReadBit();
            packet.ReadBit("MyVote");
            packet.ReadBit("VoteInProgress");
            packet.ReadBit("VotePassed");
            target[7] = packet.ReadBit();
            target[0] = packet.ReadBit();
            target[5] = packet.ReadBit();

            var emptyReason = packet.ReadBit();
            uint reasonSize = 0;

            target[4] = packet.ReadBit();
            target[1] = packet.ReadBit();
            target[6] = packet.ReadBit();
            packet.ReadBit("MyVoteCompleted");

            if (!emptyReason)
                reasonSize = packet.ReadBits(8);

            if (!emptyReason)
                packet.ReadWoWString("Reason", reasonSize);

            packet.ReadXORByte(target, 3);
            packet.ReadXORByte(target, 0);
            packet.ReadXORByte(target, 6);
            packet.ReadXORByte(target, 1);
            packet.ReadUInt32("TimeLeft");
            packet.ReadUInt32("VotesNeeded");
            packet.ReadXORByte(target, 2);
            packet.ReadUInt32("TotalVotes");
            packet.ReadXORByte(target, 7);
            packet.ReadXORByte(target, 5);
            packet.ReadXORByte(target, 4);
            packet.ReadUInt32("BootVotes");

            packet.WriteGuid("Target", target);
        }

        [Parser(Opcode.CMSG_DF_PROPOSAL_RESPONSE)]
        public static void HandleDFProposalResponse(Packet packet)
        {
            var instanceId = new byte[8];
            var requesterGuid = new byte[8];

            packet.ReadUInt32("ProposalID");
            packet.ReadUInt32("TicketTime");
            packet.ReadUInt32("TicketType");
            packet.ReadUInt32("TicketId");

            instanceId[2] = packet.ReadBit();

            requesterGuid[6] = packet.ReadBit();
            instanceId[4] = packet.ReadBit();
            instanceId[3] = packet.ReadBit();
            requesterGuid[2] = packet.ReadBit();
            instanceId[5] = packet.ReadBit();
            instanceId[6] = packet.ReadBit();
            instanceId[1] = packet.ReadBit();
            requesterGuid[5] = packet.ReadBit();
            requesterGuid[0] = packet.ReadBit();
            packet.ReadBit("Accepted");
            requesterGuid[1] = packet.ReadBit();
            requesterGuid[4] = packet.ReadBit();
            requesterGuid[3] = packet.ReadBit();
            requesterGuid[7] = packet.ReadBit();
            instanceId[0] = packet.ReadBit();
            instanceId[7] = packet.ReadBit();

            packet.ReadXORByte(instanceId, 0);
            packet.ReadXORByte(instanceId, 7);
            packet.ReadXORByte(instanceId, 3);
            packet.ReadXORByte(requesterGuid, 1);
            packet.ReadXORByte(requesterGuid, 4);
            packet.ReadXORByte(requesterGuid, 0);
            packet.ReadXORByte(instanceId, 1);
            packet.ReadXORByte(requesterGuid, 5);
            packet.ReadXORByte(requesterGuid, 7);
            packet.ReadXORByte(requesterGuid, 3);
            packet.ReadXORByte(requesterGuid, 6);
            packet.ReadXORByte(instanceId, 4);
            packet.ReadXORByte(instanceId, 2);
            packet.ReadXORByte(instanceId, 5);
            packet.ReadXORByte(instanceId, 6);
            packet.ReadXORByte(requesterGuid, 2);

            packet.WriteGuid("RequesterGuid", requesterGuid);
            packet.WriteGuid("InstanceId", instanceId);
        }
    }
}
