using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            var hasPartyIndex = packet.ReadBit("HasPartyIndex");
            var targetCount = packet.ReadUInt32();

            if (hasPartyIndex)
                packet.ReadByte("PartyIndex");

            for (var i = 0u; i < targetCount; ++i)
                packet.ReadPackedGuid128("Target", i);
        }

        [Parser(Opcode.SMSG_PARTY_INVITE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandlePartyInvite(Packet packet)
        {
            packet.ReadBit("CanAccept");
            packet.ReadBit("MightCRZYou");
            packet.ReadBit("IsXRealm");
            packet.ReadBit("ShouldSquelch");
            packet.ReadBit("AllowMultipleRoles");
            packet.ReadBit("QuestSessionActive");
            var len = packet.ReadBits(6);
            packet.ReadBit("IsCrossFaction");

            packet.ReadPackedGuid128("InviterGuid");
            packet.ReadPackedGuid128("InviterBNetAccountID");
            packet.ReadInt16("InviterCfgRealmID");
            V6_0_2_19033.Parsers.SessionHandler.ReadVirtualRealmInfo(packet, "InviterRealm");
            packet.ReadByteE<LfgRoleFlag>("ProposedRoles");
            var lfgSlots = packet.ReadInt32();
            packet.ReadUInt32("LfgCompletedMask");
            packet.ReadWoWString("InviterName", len);
            for (var i = 0; i < lfgSlots; i++)
                packet.ReadInt32("LfgSlots", i);
        }

        [Parser(Opcode.SMSG_PARTY_UPDATE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandlePartyUpdate(Packet packet)
        {
            packet.ReadUInt16("PartyFlags");
            packet.ReadByte("PartyIndex");
            packet.ReadByte("PartyType");

            packet.ReadInt32("MyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadInt32("SequenceNum");
            packet.ReadPackedGuid128("LeaderGUID");
            packet.ReadByte("LeaderFactionGroup");
            packet.ReadInt32("PingRestriction");
            var playerCount = packet.ReadUInt32("PlayerListCount");

            for (var i = 0; i < playerCount; i++)
            {
                packet.ResetBitReader();
                var playerNameLength = packet.ReadBits(6);
                var voiceStateLength = packet.ReadBits(6);
                packet.ReadBit("Connected", i);
                packet.ReadBit("FromSocialQueue", i);
                packet.ReadBit("VoiceChatSilenced", i);

                packet.ReadPackedGuid128("Guid", i);
                packet.ReadByte("Subgroup", i);
                packet.ReadByte("Flags", i);
                packet.ReadByte("RolesAssigned", i);
                packet.ReadByte("RolesUnk_1210", i);
                packet.ReadByteE<Class>("Class", i);
                packet.ReadByte("FactionGroup", i);

                {
                    packet.ResetBitReader();
                    packet.ReadPackedGuid128("BnetAccountGUID", i, "LeaverInfo");
                    packet.ReadSingle("LeaveScore", i, "LeaverInfo");
                    packet.ReadUInt32("SeasonID", i, "LeaverInfo");
                    packet.ReadUInt32("TotalLeaves", i, "LeaverInfo");
                    packet.ReadUInt32("TotalSuccesses", i, "LeaverInfo");
                    packet.ReadInt32("ConsecutiveSuccesses", i, "LeaverInfo");
                    packet.ReadTime64("LastPenaltyTime", i, "LeaverInfo");
                    packet.ReadTime64("LeaverExpirationTime", i, "LeaverInfo");
                    packet.ReadInt32("Flags", i, "LeaverInfo");
                    packet.ReadBit("LeaverStatus", i, "LeaverInfo");
                }

                packet.ReadWoWString("Name", playerNameLength, i);
                packet.ReadDynamicString("VoiceStateID", voiceStateLength, i);
            }

            packet.ResetBitReader();

            var hasChallengeMode = packet.ReadBit("HasChallengeMode");
            var hasLFG = packet.ReadBit("HasLfgInfo");
            var hasLootSettings = packet.ReadBit("HasLootSettings");
            var hasDifficultySettings = packet.ReadBit("HasDifficultySettings");

            if (hasChallengeMode)
            {
                packet.ReadInt32("MapID", "ChallengeMode");
                packet.ReadInt32("InitialPlayerCount", "ChallengeMode");
                packet.ReadUInt64("InstanceID", "ChallengeMode");
                packet.ReadTime64("StartTime", "ChallengeMode");
                packet.ReadPackedGuid128("KeystoneOwnerGUID", "ChallengeMode");
                packet.ReadPackedGuid128("LeaverGUID", "ChallengeMode");

                packet.ResetBitReader();
                packet.ReadBit("IsActive", "ChallengeMode");
                packet.ReadBit("HasRestrictions", "ChallengeMode");
                packet.ReadBit("CanVoteAbandon", "ChallengeMode");
            }

            if (hasLFG)
            {
                packet.ReadUInt32("Slot");
                packet.ReadByte("MyFlags");
                packet.ReadUInt32("MyRandomSlot");
                packet.ReadByte("MyPartialClear");
                packet.ReadSingle("MyGearDiff");
                packet.ReadByte("MyStrangerCount");
                packet.ReadByte("MyKickVoteCount");
                packet.ReadByte("BootCount");

                packet.ResetBitReader();
                packet.ReadBit("Aborted");
                packet.ReadBit("MyFirstReward");
            }

            if (hasLootSettings)
            {
                packet.ReadByte("Method", "PartyLootSettings");
                packet.ReadPackedGuid128("LootMaster", "PartyLootSettings");
                packet.ReadByte("Threshold", "PartyLootSettings");
            }

            if (hasDifficultySettings)
            {
                packet.ReadInt16<DifficultyId>("DungeonDifficultyID");
                packet.ReadInt16<DifficultyId>("RaidDifficultyID");
                packet.ReadInt16<DifficultyId>("LegacyRaidDifficultyID");
            }
        }

    }
}
