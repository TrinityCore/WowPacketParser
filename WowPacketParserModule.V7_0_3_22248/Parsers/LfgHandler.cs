using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class LfgHandler
    {
        public static void ReadLfgListJoinRequest(Packet packet, params object[] idx)
        {
            packet.ReadInt32("GroupFinderActivityId", idx);
            packet.ReadSingle("RequiredItemLevel", idx);
            packet.ReadUInt32("RequiredHonorLevel", idx);

            packet.ResetBitReader();
            var lenName = packet.ReadBits(8);
            var lenComment = packet.ReadBits(11);
            var lenVoiceChat = packet.ReadBits(8);
            var hasQuest = false;
            packet.ReadBit("AutoAccept", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_5_23360))
            {
                packet.ReadBit("IsPrivate", idx);
                hasQuest = packet.ReadBit("HasQuest", idx);
            }

            packet.ReadWoWString("Name", lenName, idx);
            packet.ReadWoWString("Comment", lenComment, idx);
            packet.ReadWoWString("VoiceChat", lenVoiceChat, idx);

            if (hasQuest)
                packet.ReadUInt32("QuestID", idx);
        }

        public static void ReadLfgListSearchResultMemberInfo(Packet packet, params object[] idx)
        {
            packet.ReadByteE<Class>("Class", idx);
            packet.ReadByteE<LfgRole>("Role", idx);
        }

        public static void ReadLfgListSearchResult(Packet packet, params object[] idx)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, idx, "LFGListRideTicket");

            packet.ReadUInt32("SequenceNum", idx);

            packet.ReadPackedGuid128("Leader", idx);
            packet.ReadPackedGuid128("LastTouchedAny", idx);
            packet.ReadPackedGuid128("LastTouchedName", idx);
            packet.ReadPackedGuid128("LastTouchedComment", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                packet.ReadPackedGuid128("LastTouchedVoiceChat", idx);

            packet.ReadUInt32("VirtualRealmAddress", idx);

            var bnetFriendCount = packet.ReadUInt32();
            var characterFriendCount = packet.ReadUInt32();
            var guildMateCount = packet.ReadUInt32();
            var memberCount = packet.ReadUInt32();

            packet.ReadUInt32("CompletedEncountersMask", idx);
            packet.ReadTime("CreationTime", idx);
            packet.ReadByte("Unk4", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                packet.ReadPackedGuid128("PartyGUID", idx);

            for (int i = 0; i < bnetFriendCount; i++)
                packet.ReadPackedGuid128("BNetFriends", idx, i);
            for (int i = 0; i < characterFriendCount; i++)
                packet.ReadPackedGuid128("CharacterFriends", idx, i);
            for (int i = 0; i < guildMateCount; i++)
                packet.ReadPackedGuid128("GuildMates", idx, i);

            for (int i = 0; i < memberCount; i++)
                ReadLfgListSearchResultMemberInfo(packet, "Members", idx, i);

            ReadLfgListJoinRequest(packet, idx, "LFGListJoinRequest");
        }

        [Parser(Opcode.SMSG_LFG_PROPOSAL_UPDATE)]
        public static void HandleLfgProposalUpdate(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet);

            packet.ReadUInt64("InstanceID");
            packet.ReadUInt32("ProposalID");
            packet.ReadUInt32("Slot");
            packet.ReadSByte("State");
            packet.ReadUInt32("CompletedMask");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                packet.ReadUInt32("EncounterMask");

            var playerCount = packet.ReadUInt32("PlayersCount");
            packet.ReadByte();
            packet.ReadBit("ValidCompletedMask");
            packet.ReadBit("ProposalSilent");
            packet.ReadBit("IsRequeue");

            for (var i = 0u; i < playerCount; i++)
            {
                packet.ReadUInt32("Roles", i);

                packet.ResetBitReader();

                packet.ReadBit("Me", i);
                packet.ReadBit("SameParty", i);
                packet.ReadBit("MyParty", i);
                packet.ReadBit("Responded", i);
                packet.ReadBit("Accepted", i);
            }
        }

        [Parser(Opcode.CMSG_QUICK_JOIN_AUTO_ACCEPT_REQUESTS)]
        public static void HandleLfgQuickJoinAutoAcceptRequests(Packet packet)
        {
            packet.ReadBit("AutoAccept");
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_STATUS)]
        public static void HandleLfgListUpdateStatus(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "RideTicket");
            packet.ReadTime("RemainingTime");
            packet.ReadByte("ResultId");
            ReadLfgListJoinRequest(packet, "LFGListJoinRequest");
            packet.ResetBitReader();
            packet.ReadBit("Listed");
        }

        [Parser(Opcode.CMSG_LFG_LIST_UPDATE_REQUEST)]
        public static void HandleLfgListUpdateRequest(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "RideTicket");
            ReadLfgListJoinRequest(packet, "LFGListJoinRequest");
        }

        [Parser(Opcode.CMSG_LFG_LIST_JOIN)]
        public static void HandleLFGListJoin(Packet packet)
        {
            ReadLfgListJoinRequest(packet, "LFGListJoinRequest");
        }

        [Parser(Opcode.SMSG_LFG_LIST_JOIN_RESULT)]
        public static void HandleLfgListJoinResult(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "RideTicket");
            packet.ReadByte("ResultId");
            packet.ReadByte("Unk"); // unused?
        }

        [Parser(Opcode.CMSG_LFG_LIST_DECLINE_APPLICANT)]
        public static void HandleLfgListDeclineApplicant(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "LFGListRideTicket");
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "ApplicationRideTicket");
        }

        [Parser(Opcode.SMSG_LFG_LIST_APPLY_TO_GROUP_RESULT)]
        public static void HandleLfgApplyForGroupResult(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "ApplicationRideTicket");
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "LFGListRideTicket");

            packet.ReadTime("RemainingTimeoutTime");
            packet.ReadByte("ResultId");
            packet.ReadByte("Unk1"); // always 0
            ReadLfgListSearchResult(packet, "LFGListEntry");
            packet.ResetBitReader();
            packet.ReadBitsE<LfgListApplicationStatus>("Status", 4);
        }

        [Parser(Opcode.CMSG_LFG_LIST_APPLY_TO_GROUP)]
        public static void HandleLfgListApplyToGroup(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "RideTicket");
            packet.ReadInt32("GroupFinderActivityId");
            packet.ReadByteE<LfgRoleFlag>("Roles");
            packet.ResetBitReader();
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Comment", len);
        }

        [Parser(Opcode.CMSG_LFG_LIST_INVITE_APPLICANT)]
        public static void HandleLfgListInviteApplicant(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "LFGListRideTicket");
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "ApplicationRideTicket");
            var memberNum = packet.ReadUInt32("PartyMemberNum");

            for (int i = 0; i < memberNum; i++)
            {
                packet.ReadPackedGuid128("PlayerGUID", i);
                packet.ReadByteE<LfgRoleFlag>("ChosenRoles", i);
            }
        }

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_EXPIRATION)]
        public static void HandleLfgListUpdateExpiration(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "Ticket");
            packet.ReadTime("TimeoutTime");
            packet.ReadByte("Type");
        }

        [Parser(Opcode.SMSG_LFG_LIST_APPLICATION_STATUS_UPDATE)]
        public static void HandleLfgListApplicationUpdate(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "ApplicationRideTicket");
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "LFGListRideTicket");

            packet.ReadInt32("Unk");
            packet.ReadByte("ResultId");
            packet.ReadByteE<LfgRoleFlag>("Role");
            packet.ResetBitReader();
            packet.ReadBitsE<LfgListApplicationStatus>("Status", 4);
        }

        [Parser(Opcode.SMSG_LFG_LIST_SEARCH_RESULTS)]
        public static void HandleLfgListSearchResults(Packet packet)
        {
            packet.ReadUInt16("TotalResults");
            var resultCount = packet.ReadUInt32();

            for (int j = 0; j < resultCount; j++)
                ReadLfgListSearchResult(packet, "Entry", j);
        }

        [Parser(Opcode.SMSG_LFG_LIST_SEARCH_STATUS)]
        public static void HandleLfgListSearchStatus(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "RideTicket");
            packet.ReadByte("Unk1");

            packet.ResetBitReader();
            packet.ReadBit("UnkBit");
        }

        [Parser(Opcode.CMSG_LFG_LIST_SEARCH)]
        public static void HandleLFGListSearch(Packet packet)
        {
            uint searchFilter = 0;
            packet.ResetBitReader();
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_5_24330))
                searchFilter = packet.ReadBits("SearchFilterNum", 5);
            else
                searchFilter = packet.ReadBits("SearchFilterNum", 6);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_5_24330))
            {
                uint[] length = new uint[4];
                for (int i = 0; i < searchFilter; i++)
                {
                    packet.ResetBitReader();
                    for (int z = 0; z < 3; z++)
                        length[z] = packet.ReadBits("SearchFilterLength", 5, i, z);
                    for (int z = 0; z < 3; z++)
                        packet.ReadWoWString("SearchFilter", length[z], i, z);
                }
            }
            packet.ReadInt32("GroupFinderCategoryId");
            packet.ReadInt32("SubActivityGroupID");
            packet.ReadInt32E<LfgListFilter>("LFGListFilter");
            packet.ReadUInt32E<LocaleConstantFlags>("LanguageFilter"); // 0x6b3 is default in enUS client (= enUS, koKR, ptBR, none, zhCN, zhTW, esMX)

            var entryCount = packet.ReadInt32();
            var guidCount = packet.ReadInt32();

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_5_24330))
                packet.ReadWoWString("SearchFilter", searchFilter);

            for (int i = 0; i < entryCount; i++)
            {
                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_5_24330))
                {
                    packet.ReadInt32("GroupFinderActivityId");
                    packet.ReadInt32E<LfgLockStatus>("LockStatus");
                }
                else
                {
                    packet.ReadInt32("Unk");
                }
            }

            for (int i = 0; i < guidCount; i++)
                packet.ReadPackedGuid128("UnkGUID", i); // PartyMember?
        }

        public static void ReadLfgListSearchResultPartialUpdate(Packet packet, params object[] idx)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, idx, "Ticket");
            packet.ReadUInt32("SequenceNum", idx);
            var memberCount = packet.ReadUInt32();
            for (int j = 0; j < memberCount; j++)
                ReadLfgListSearchResultMemberInfo(packet, "Members", idx, j);

            packet.ResetBitReader();
            var hasLeader = packet.ReadBit("ChangeLeader", idx);
            var hasVirtualRealmAddress = packet.ReadBit("ChangeVirtualRealmAddress", idx);
            var hasCompletedEncountersMask = packet.ReadBit("ChangeCompletedEncountersMask", idx);
            packet.ReadBit("Delisted", idx);
            packet.ReadBit("ChangeTitle", idx);
            var hasAny = packet.ReadBit();
            var hasName = packet.ReadBit("ChangeName", idx);
            var hasComment = packet.ReadBit("ChangeComment", idx);
            var hasVoice = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                hasVoice = packet.ReadBit("ChangeVoice", idx);
            var hasItemLevel = packet.ReadBit("ChangeItemLevel", idx);
            packet.ReadBit("ChangeAutoAccept", idx);
            packet.ReadBit("ChangeHonorLevel", idx);
            packet.ReadBit("ChangePrivate", idx);

            ReadLfgListJoinRequest(packet, idx, "LFGListJoinRequest");

            if (hasLeader)
                packet.ReadPackedGuid128("Leader", idx);

            if (hasVirtualRealmAddress)
                packet.ReadUInt32("VirtualRealmAddress", idx);

            if (hasCompletedEncountersMask)
                packet.ReadUInt32("CompletedEncountersMask", idx);

            if (hasAny)
                packet.ReadPackedGuid128("LastTouchedAny");

            if (hasName)
                packet.ReadPackedGuid128("LastTouchedName", idx);

            if (hasComment)
                packet.ReadPackedGuid128("LastTouchedComment", idx);

            if (hasVoice)
                packet.ReadPackedGuid128("LastTouchedVoiceChat", idx);
        }

        [Parser(Opcode.SMSG_LFG_LIST_SEARCH_RESULTS_UPDATE)]
        public static void HandleLfgListSearchResultsUpdate(Packet packet)
        {
            var count = packet.ReadUInt32();

            for (int i = 0; i < count; i++)
                ReadLfgListSearchResultPartialUpdate(packet, "LFGListSearchResultPartialUpdate", i);
        }

        [Parser(Opcode.SMSG_LFG_LIST_APPLICANT_LIST_UPDATE)]
        public static void HandleLfgListApplicantListUpdate(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "Ticket");
            var applicantCount = packet.ReadUInt32();
            packet.ReadUInt32("Result");

            for (int i = 0; i < applicantCount; i++)
            {
                V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, i, "Ticket");
                packet.ReadPackedGuid128("Joiner", i);
                var memberCount = packet.ReadUInt32();
                for (int j = 0; j < memberCount; j++)
                {
                    packet.ReadPackedGuid128("Guid", i, j);
                    packet.ReadUInt32("VirtualRealmAddress", i, j);
                    packet.ReadSingle("ItemLevel", i, j);
                    packet.ReadUInt32("Level", i, j);
                    packet.ReadInt32("HonorLevel", i, j);
                    packet.ReadByteE<LfgRoleFlag>("Queued role", i, j);
                    packet.ReadByteE<LfgRoleFlag>("Assigned role", i, j);

                    var provingGroundRankNum = packet.ReadUInt32();
                    for (int x = 0; x < provingGroundRankNum; x++)
                    {
                        packet.ReadUInt32("CriteriaID", i, j, x);
                        packet.ReadUInt32("Quantity", i, j, x);
                    }
                }

                packet.ResetBitReader();
                packet.ReadBitsE<LfgListApplicationStatus>("Status", 4, i);
                packet.ReadBit("Listed", i);
                var len = packet.ReadBits(8);

                packet.ReadWoWString("Comment", len);
            }
        }
    }
}
