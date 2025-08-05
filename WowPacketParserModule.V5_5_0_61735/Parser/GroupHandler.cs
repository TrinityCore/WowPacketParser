
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class GroupHandler
    {
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

        [Parser(Opcode.SMSG_ROLE_CHANGED_INFORM)]
        public static void HandleRoleChangedInform(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("From");
            packet.ReadPackedGuid128("ChangedUnit");
            packet.ReadByteE<LfgRoleFlag>("OldRole");
            packet.ReadByteE<LfgRoleFlag>("NewRole");
        }

        [Parser(Opcode.SMSG_ROLE_POLL_INFORM)]
        public static void HandleRolePollInform(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("From");
        }

        [Parser(Opcode.SMSG_RAID_MARKERS_CHANGED)]
        public static void HandleRaidMarkersChanged(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadInt32("ActiveMarkers");

            var count = packet.ReadBits(4);
            for (int i = 0; i < count; i++)
            {
                packet.ReadPackedGuid128("TransportGUID");
                packet.ReadInt32("MapID");
                packet.ReadVector3("Position");
            }
        }

        [Parser(Opcode.SMSG_PARTY_INVITE)]
        public static void HandlePartyInvite(Packet packet)
        {
            packet.ReadBit("CanAccept");
            packet.ReadBit("MightCRZYou");
            packet.ReadBit("IsXRealm");
            packet.ReadBit("MustBeBNetFriend");
            packet.ReadBit("AllowMultipleRoles");
            packet.ReadBit("QuestSessionActive");
            var len = packet.ReadBits(6);
            packet.ReadBit("IsCrossFaction");

            SessionHandler.ReadVirtualRealmInfo(packet, "VirtualRealm");

            packet.ReadPackedGuid128("InviterGuid");
            packet.ReadPackedGuid128("InviterBNetAccountID");
            packet.ReadInt16("InviterCfgRealmID");
            packet.ReadByte("ProposedRoles");
            var lfgSlots = packet.ReadInt32();
            packet.ReadInt32("LfgCompletedMask");
            packet.ReadWoWString("InviterName", len);
            for (int i = 0; i < lfgSlots; i++)
                packet.ReadInt32("LfgSlots", i);
        }

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
            packet.ReadByte("LeaderFactionGroup");
            packet.ReadInt32("PingRestriction");

            var playerCount = packet.ReadUInt32("PlayerListCount");
            var hasLFG = packet.ReadBit("HasLfgInfo");
            var hasLootSettings = packet.ReadBit("HasLootSettings");
            var hasDifficultySettings = packet.ReadBit("HasDifficultySettings");

            for (var i = 0; i < playerCount; i++)
            {
                packet.ResetBitReader();
                var playerNameLength = packet.ReadBits(6);
                var voiceStateLength = packet.ReadBits(6);
                packet.ReadBit("Connected", i);
                packet.ReadBit("VoiceChatSilenced", i);
                packet.ReadBit("FromSocialQueue", i);
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadByte("Subgroup", i);
                packet.ReadByte("Flags", i);
                packet.ReadByte("RolesAssigned", i);
                packet.ReadByteE<Class>("Class", i);
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
                packet.ReadUInt32("DungeonDifficultyID");
                packet.ReadUInt32("RaidDifficultyID");
                packet.ReadUInt32("LegacyRaidDifficultyID");
            }

            if (hasLFG)
            {
                packet.ResetBitReader();

                packet.ReadUInt32("Slot");
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

        [Parser(Opcode.SMSG_READY_CHECK_STARTED)]
        public static void HandleReadyCheckStarted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadPackedGuid128("InitiatorGUID");
            packet.ReadInt64("Duration");
        }

        [Parser(Opcode.SMSG_READY_CHECK_RESPONSE)]
        public static void HandleReadyCheckResponse(Packet packet)
        {
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadPackedGuid128("Player");
            packet.ReadBit("IsReady");
        }

        [Parser(Opcode.SMSG_READY_CHECK_COMPLETED)]
        public static void HandleReadyCheckCompleted(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
        }

        [Parser(Opcode.SMSG_GROUP_NEW_LEADER)]
        public static void HandleGroupNewLeader(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.SMSG_SEND_RAID_TARGET_UPDATE_ALL)]
        public static void HandleSendRaidTargetUpdateAll(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            var raidTargetSymbolCount = packet.ReadInt32("RaidTargetSymbolCount");
            for (int i = 0; i < raidTargetSymbolCount; i++)
            {
                packet.ReadPackedGuid128("Target", i);
                packet.ReadByte("Symbol", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_RAID_TARGET_UPDATE_SINGLE)]
        public static void HandleSendRaidTargetUpdateSingle(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadByte("Symbol");
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("ChangedBy");
        }
    }
}
