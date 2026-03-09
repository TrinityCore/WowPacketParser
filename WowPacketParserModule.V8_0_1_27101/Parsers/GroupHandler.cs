using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.SMSG_PARTY_UPDATE)]
        public static void HandlePartyUpdate(Packet packet)
        {
            packet.ReadUInt16("PartyFlags");
            packet.ReadByte("PartyIndex");
            packet.ReadByte("PartyType");

            packet.ReadInt32("MyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadInt32("SequenceNum");
            packet.ReadPackedGuid128("LeaderGUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                packet.ReadByte("LeaderFactionGroup");

            var playerCount = packet.ReadUInt32("PlayerListCount");
            var hasChallengeMode = ClientVersion.AddedInVersion(ClientVersionBuild.V11_2_0_62213) && packet.ReadBit("HasChallengeMode");
            var hasLFG = packet.ReadBit("HasLfgInfo");
            var hasLootSettings = packet.ReadBit("HasLootSettings");
            var hasDifficultySettings = packet.ReadBit("HasDifficultySettings");

            for (var i = 0; i < playerCount; i++)
            {
                packet.ResetBitReader();
                var playerNameLength = packet.ReadBits(6);
                var voiceStateLength = packet.ReadBits(6);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_7_45114))
                    packet.ReadBit("Connected", i);
                packet.ReadBit("VoiceChatSilenced", i);
                packet.ReadBit("FromSocialQueue", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_2_0_62213))
                {
                    packet.ResetBitReader();
                    packet.ReadPackedGuid128("BnetAccountGUID", "LeaverInfo");
                    packet.ReadSingle("LeaveScore", "LeaverInfo");
                    packet.ReadUInt32("SeasonID", "LeaverInfo");
                    packet.ReadUInt32("TotalLeaves", "LeaverInfo");
                    packet.ReadUInt32("TotalSuccesses", "LeaverInfo");
                    packet.ReadInt32("ConsecutiveSuccesses", "LeaverInfo");
                    packet.ReadTime64("LastPenaltyTime", "LeaverInfo");
                    packet.ReadTime64("LeaverExpirationTime", "LeaverInfo");
                    packet.ReadInt32("Unknown_1120", "LeaverInfo");
                    packet.ReadBit("LeaverStatus", "LeaverInfo");
                }

                packet.ReadPackedGuid128("Guid", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_2_7_45114))
                    packet.ReadByte("Status", i);
                packet.ReadByte("Subgroup", i);
                packet.ReadByte("Flags", i);
                packet.ReadByte("RolesAssigned", i);
                packet.ReadByteE<Class>("Class", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_5_43903))
                    packet.ReadByte("FactionGroup", i);

                packet.ReadWoWString("Name", playerNameLength, i);
                packet.ReadDynamicString("VoiceStateID", voiceStateLength, i);
            }

            packet.ResetBitReader();

            if (hasLootSettings)
            {
                packet.ReadByte("Method", "PartyLootSettings");
                packet.ReadPackedGuid128("LootMaster", "PartyLootSettings");
                packet.ReadByte("Threshold", "PartyLootSettings");
            }

            if (hasDifficultySettings)
            {
                if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V12_0_0_65390))
                {
                    packet.ReadInt16<DifficultyId>("DungeonDifficultyID");
                    packet.ReadInt16<DifficultyId>("RaidDifficultyID");
                    packet.ReadInt16<DifficultyId>("LegacyRaidDifficultyID");
                }
                else
                {
                    packet.ReadUInt32<DifficultyId>("DungeonDifficultyID");
                    packet.ReadUInt32<DifficultyId>("RaidDifficultyID");
                    packet.ReadUInt32<DifficultyId>("LegacyRaidDifficultyID");
                }
            }

            if (hasChallengeMode)
            {
                packet.ResetBitReader();
                packet.ReadInt32("MapID", "ChallengeMode");
                packet.ReadInt32("InitialPlayerCount", "ChallengeMode");
                packet.ReadUInt64("InstanceID", "ChallengeMode");
                packet.ReadTime64("StartTime", "ChallengeMode");
                packet.ReadPackedGuid128("KeystoneOwnerGUID", "ChallengeMode");
                packet.ReadPackedGuid128("LeaverGUID", "ChallengeMode");
                packet.ReadBit("IsActive", "ChallengeMode");
                packet.ReadBit("HasRestrictions", "ChallengeMode");
                packet.ReadBit("CanVoteAbandon", "ChallengeMode");
            }

            if (hasLFG)
            {
                packet.ResetBitReader();

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V11_1_7_61491))
                    packet.ReadByte("MyFlags");

                packet.ReadUInt32("Slot");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_1_7_61491))
                    packet.ReadByte("MyFlags");

                packet.ReadUInt32("MyRandomSlot");
                packet.ReadByte("MyPartialClear");
                packet.ReadSingle("MyGearDiff");
                packet.ReadByte("MyStrangerCount");
                packet.ReadByte("MyKickVoteCount");
                packet.ReadByte("BootCount");
                packet.ReadBit("Aborted");
                packet.ReadBit("MyFirstReward");
            }
        }
    }
}
