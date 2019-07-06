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
                packet.ReadUInt32("QuestId", idx);
        }

        public static void ReadLfgListEntry(Packet packet, params object[] idx)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, idx, "LFGListRideTicket");

            packet.ReadUInt32("Unk1", idx);

            packet.ReadPackedGuid128("LeaderGUID1", idx); // somehow these guids are always equivalent to leader?
            packet.ReadPackedGuid128("LeaderGUID2", idx);
            packet.ReadPackedGuid128("LeaderGUID3", idx);
            packet.ReadPackedGuid128("LeaderGUID4", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                packet.ReadPackedGuid128("UnkGuid_730", idx);

            packet.ReadUInt32("VirtualRealmAddress", idx);

            var unkCount1 = packet.ReadUInt32();
            var unkCount2 = packet.ReadUInt32();
            var unkCount3 = packet.ReadUInt32();
            var memberCount = packet.ReadUInt32();

            packet.ReadUInt32("Unk2", idx);
            packet.ReadTime("CreatedTime", idx);
            packet.ReadByte("Unk4", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_5_25848))
                packet.ReadPackedGuid128("UnkGuid_735", idx);

            for (int i = 0; i < unkCount1; i++)
                packet.ReadPackedGuid128("UnkGuid1", idx, i);
            for (int i = 0; i < unkCount2; i++)
                packet.ReadPackedGuid128("UnkGuid2", idx, i);
            for (int i = 0; i < unkCount3; i++)
                packet.ReadPackedGuid128("UnkGuid3", idx, i);

            for (int i = 0; i < memberCount; i++)
            {
                packet.ReadByteE<Class>("Class", idx, i);
                packet.ReadByteE<LfgRole>("Role", idx, i);
            }
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
            ReadLfgListEntry(packet, "LFGListEntry");
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

        [Parser(Opcode.SMSG_LFG_LIST_APPLICATION_UPDATE)]
        public static void HandleLfgListApplicationUpdate(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "ApplicationRideTicket");
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "LFGListRideTicket");

            packet.ReadInt32("Unk");
            packet.ReadByte("ResultId");
            packet.ReadByteE<LfgRoleFlag>("Role");
            packet.ReadBitsE<LfgListApplicationStatus>("Status", 4);

        }

        [Parser(Opcode.SMSG_LFG_LIST_SEARCH_RESULTS)]
        public static void HandleLfgListSearchResults(Packet packet)
        {
            packet.ReadUInt16("TotalResults"); // this field and resultCount always have the same value
            var resultCount = packet.ReadUInt32();

            for (int j = 0; j < resultCount; j++)
                ReadLfgListEntry(packet, "Entry", j);
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
                searchFilter = packet.ReadBits("SearchFilterLength", 6);

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

        [Parser(Opcode.SMSG_LFG_LIST_UPDATE_ENTRY)]
        public static void HandleLfgListUpdateEntry(Packet packet)
        {
            var count = packet.ReadUInt32();

            for (int i = 0; i < count; i++)
            {
                V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, i, "Ticket");
                packet.ReadUInt32("Unk2", i); // this is an increasing number, always +1 per update on this entry => UpdateNum?
                var memberCount = packet.ReadUInt32();
                for (int j = 0; j < memberCount; j++)
                {
                    packet.ReadByteE<Class>("Class", i, "Member", j);
                    packet.ReadByteE<LfgRole>("Role", i, "Member", j);
                }

                packet.ResetBitReader();
                var changeLeader = packet.ReadBit("ChangeLeader", i);
                var changeVirtualRealmAddress = packet.ReadBit("ChangeVirtualRealmAddress", i);
                var changeCompletedEncountersMask = packet.ReadBit("ChangeCompletedEncountersMask", i);
                packet.ReadBit("Delisted", i);
                packet.ReadBit("ChangeTitle", i);
                var changeComment = packet.ReadBit("ChangeComment", i);
                var changeVoice = packet.ReadBit("ChangeVoice", i);
                var changeItemLevel = packet.ReadBit("ChangeItemLevel", i);
                var changeUnk = false;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                    changeUnk = packet.ReadBit("Unk5_730", i);
                packet.ReadBit("ChangeAutoAccept", i);
                packet.ReadBit("Unk3", i);
                packet.ReadBit("Unk4", i);
                packet.ReadBit("Unk5", i);

                ReadLfgListJoinRequest(packet, i, "LFGListJoinRequest");
                if (changeLeader)
                    packet.ReadPackedGuid128("NewLeaderGUID", i);
                if (changeVirtualRealmAddress)
                    packet.ReadUInt32("NewVirtualRealmAddress", i);
                if (changeCompletedEncountersMask)
                    packet.ReadUInt32("CompletedEncountersMask", i);
                if (changeComment)
                    packet.ReadPackedGuid128("LeaderGUID", i);
                if (changeVoice)
                    packet.ReadPackedGuid128("LeaderGUID", i);
                if (changeItemLevel)
                    packet.ReadPackedGuid128("LeaderGUID", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_3_0_24920))
                {
                    if (changeUnk)
                        packet.ReadPackedGuid128("UnkGuid_730");
                }
            }
        }

        [Parser(Opcode.SMSG_LFG_LIST_APPLICATION_LIST_UPDATE)]
        public static void HandleLfgListApplicationListUpdate(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, "LFGListRideTicket");
            var applicationCount = packet.ReadUInt32();
            packet.ReadUInt32("ResultId");

            for (int i = 0; i < applicationCount; i++)
            {
                V6_0_2_19033.Parsers.LfgHandler.ReadCliRideTicket(packet, i, "ApplicationRideTicket");
                packet.ReadPackedGuid128("UnkGuid", i);
                var memberNum = packet.ReadUInt32();
                for (int j = 0; j < memberNum; j++)
                {
                    packet.ReadPackedGuid128("UnkGuid", i, j);
                    packet.ReadUInt32("VirtualRealmAddress", i, j);
                    packet.ReadSingle("ItemLevel", i, j);
                    packet.ReadUInt32("Level", i, j);
                    packet.ReadInt32("Unk1", i, j);
                    packet.ReadByteE<LfgRoleFlag>("Queued role", i, j);
                    packet.ReadByteE<LfgRoleFlag>("Assigned role", i, j);

                    var provingGroundRankNum = packet.ReadUInt32();
                    for (int x = 0; x < provingGroundRankNum; x++)
                    {
                        packet.ReadUInt32("Criteria", i, j, x);
                        packet.ReadUInt32("Achieved", i, j, x);
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
