using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class GroupHandler
    {
        public static void ReadAuraInfos(Packet packet, params object[] index)
        {
            packet.ReadInt32<SpellId>("Aura", index);
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands) ||
                ClientVersion.IsBurningCrusadeClassicClientVersionBuild(ClientVersion.Build) ||
                ClientVersion.IsWotLKClientVersionBuild(ClientVersion.Build))
                packet.ReadUInt16("Flags", index);
            else
                packet.ReadByte("Flags", index);
            packet.ReadUInt32("ActiveFlags", index);
            var byte3 = packet.ReadUInt32("PointsCount", index);

            for (int j = 0; j < byte3; j++)
                packet.ReadSingle("Points", index, j);
        }

        public static void ReadPetInfos(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("PetGuid", index);
            packet.ReadInt32("PetDisplayID", index);
            packet.ReadInt32("PetMaxHealth", index);
            packet.ReadInt32("PetHealth", index);

            var petAuraCount = packet.ReadUInt32("PetAuraCount", index);
            for (int i = 0; i < petAuraCount; i++)
                ReadAuraInfos(packet, index, i);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("PetName", len, index);
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_FULL_STATE)]
        public static void HandlePartyMemberFullState(Packet packet)
        {
            packet.ReadBit("ForEnemy");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("PartyType", i);

            packet.ReadUInt16E<GroupMemberStatusFlag>("Flags");

            packet.ReadByte("PowerType");
            packet.ReadUInt16("OverrideDisplayPower");
            packet.ReadInt32("CurrentHealth");
            packet.ReadInt32("MaxHealth");
            packet.ReadUInt16("MaxPower");
            packet.ReadUInt16("MaxPower");
            packet.ReadUInt16("Level");
            packet.ReadUInt16("Spec");
            packet.ReadUInt16("AreaID");

            packet.ReadUInt16("WmoGroupID");
            packet.ReadUInt32("WmoDoodadPlacementID");

            packet.ReadInt16("PositionX");
            packet.ReadInt16("PositionY");
            packet.ReadInt16("PositionZ");

            packet.ReadInt32("VehicleSeatRecID");
            var auraCount = packet.ReadInt32("AuraCount");

            V6_0_2_19033.Parsers.GroupHandler.ReadPhaseInfos(packet, "Phase");

            if (ClientVersion.AddedInVersion(ClientType.Shadowlands) ||
                ClientVersion.IsBurningCrusadeClassicClientVersionBuild(ClientVersion.Build) ||
                ClientVersion.IsWotLKClientVersionBuild(ClientVersion.Build))
            {
                packet.ReadUInt32("ContentTuningConditionMask", "CTROptions");
                packet.ReadInt32("Unused901", "CTROptions");
                packet.ReadUInt32("ExpansionLevelMask", "CTROptions");
            }

            for (int i = 0; i < auraCount; i++)
                ReadAuraInfos(packet, "Aura", i);

            packet.ResetBitReader();
            var hasPet = packet.ReadBit("HasPet");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                Substructures.MythicPlusHandler.ReadDungeonScoreSummary(packet, "DungeonScoreSummary");

            if (hasPet) // Pet
                ReadPetInfos(packet, "Pet");

            packet.ReadPackedGuid128("MemberGuid");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_1_5_40772))
            {
                packet.ReadInt32("ProposedRoles");
                packet.ReadPackedGuid128("TargetGuid");
            }
            packet.ResetBitReader();

            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
            {
                packet.ReadInt32("ProposedRoles");
                packet.ReadPackedGuid128("TargetGuid");
            }

            packet.ReadWoWString("TargetName", lenTargetName);
            packet.ReadWoWString("TargetRealm", lenTargetRealm);
        }

        [Parser(Opcode.SMSG_PARTY_INVITE)]
        public static void HandlePartyInvite(Packet packet)
        {
            packet.ReadBit("CanAccept");
            packet.ReadBit("MightCRZYou");
            packet.ReadBit("IsXRealm");
            packet.ReadBit("MustBeBNetFriend");
            packet.ReadBit("AllowMultipleRoles");
            var len = packet.ReadBits(6);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V11_0_0_55666))
                packet.ReadBit("IsCrossFaction");

            V6_0_2_19033.Parsers.SessionHandler.ReadVirtualRealmInfo(packet, "InviterRealmInfo");

            packet.ReadPackedGuid128("InviterGuid");
            packet.ReadPackedGuid128("InviterBNetAccountID");
            packet.ReadInt16("InviterCfgRealmID");
            packet.ReadInt32("ProposedRoles");
            var lfgSlots = packet.ReadInt32();
            packet.ReadInt32("LfgCompletedMask");
            packet.ReadWoWString("InviterName", len);
            for (int i = 0; i < lfgSlots; i++)
                packet.ReadInt32("LfgSlots", i);
        }

        [Parser(Opcode.SMSG_PARTY_UPDATE, ClientVersionBuild.V7_1_0_22900)]
        public static void HandlePartyUpdate(Packet packet)
        {
            packet.ReadUInt16("PartyFlags");
            packet.ReadByte("PartyIndex");
            packet.ReadByte("PartyType");

            packet.ReadInt32("MyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadInt32("SequenceNum");
            packet.ReadPackedGuid128("LeaderGUID");

            var playerCount = packet.ReadInt32("PlayerListCount");
            packet.ResetBitReader();
            var hasLFG = packet.ReadBit("HasLfgInfo");
            var hasLootSettings = packet.ReadBit("HasLootSettings");
            var hasDifficultySettings = packet.ReadBit("HasDifficultySettings");

            for (int i = 0; i < playerCount; i++)
            {
                packet.ResetBitReader();
                var playerNameLength = packet.ReadBits(6);
                packet.ReadBit("FromSocialQueue", i);

                packet.ReadPackedGuid128("Guid", i);

                packet.ReadByte("Status", i);
                packet.ReadByte("Subgroup", i);
                packet.ReadByte("Flags", i);
                packet.ReadByte("RolesAssigned", i);
                packet.ReadByteE<Class>("Class", i);

                packet.ReadWoWString("Name", playerNameLength, i);
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
                packet.ReadInt32("DungeonDifficultyID");
                packet.ReadInt32("RaidDifficultyID");
                packet.ReadInt32("LegacyRaidDifficultyID");
            }

            if (hasLFG)
            {
                packet.ResetBitReader();

                packet.ReadByte("MyFlags");
                packet.ReadInt32("Slot");
                packet.ReadInt32("MyRandomSlot");
                packet.ReadByte("MyPartialClear");
                packet.ReadSingle("MyGearDiff");
                packet.ReadByte("MyStrangerCount");
                packet.ReadByte("MyKickVoteCount");
                packet.ReadByte("BootCount");
                packet.ReadBit("Aborted");
                packet.ReadBit("MyFirstReward");
            }
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_PARTIAL_STATE)]
        public static void HandlePartyMemberPartialState(Packet packet)
        {
            packet.ReadBit("ForEnemyChanged");
            packet.ReadBit("SetPvPInactive"); // adds GroupMemberStatusFlag 0x0020 if true, removes 0x0020 if false

            var partyTypeChanged = packet.ReadBit("PartyTypeChanged");
            var flagsChanged = packet.ReadBit("FlagsChanged");
            var powerTypeChanged = packet.ReadBit("PowerTypeChanged");
            var overrideDisplayPowerChanged = packet.ReadBit("OverrideDisplayPowerChanged");
            var currentHealthChanged = packet.ReadBit("CurrentHealthChanged");
            var maxHealthChanged = packet.ReadBit("MaxHealthChanged");
            var powerChanged = packet.ReadBit("PowerChanged");
            var maxPowerChanged = packet.ReadBit("MaxPowerChanged");
            var levelChanged = packet.ReadBit("LevelChanged");
            var specChanged = packet.ReadBit("SpecChanged");
            var areaIdChanged = packet.ReadBit("AreaIdChanged");
            var wmoGroupIdChanged = packet.ReadBit("WmoGroupIdChanged");
            var wmoDoodadPlacementIdChanged = packet.ReadBit("WmoDoodadPlacementIdChanged");
            var positionChanged = packet.ReadBit("PositionChanged");
            var vehicleSeatRecIdChanged = packet.ReadBit("VehicleSeatRecIdChanged");
            var aurasChanged = packet.ReadBit("AurasChanged");
            var petChanged = packet.ReadBit("PetChanged");
            var phaseChanged = packet.ReadBit("PhaseChanged");

            if (petChanged)
            {
                packet.ResetBitReader();
                var petGuidChanged = packet.ReadBit("GuidChanged", "Pet");
                var petNameChanged = packet.ReadBit("NameChanged", "Pet");
                var petDisplayIdChanged = packet.ReadBit("DisplayIdChanged", "Pet");
                var petMaxHealthChanged = packet.ReadBit("MaxHealthChanged", "Pet");
                var petHealthChanged = packet.ReadBit("HealthChanged", "Pet");
                var petAurasChanged = packet.ReadBit("AurasChanged", "Pet");
                if (petNameChanged)
                {
                    packet.ResetBitReader();
                    var len = packet.ReadBits(8);
                    packet.ReadWoWString("NewPetName", len, "Pet");
                }
                if (petGuidChanged)
                    packet.ReadPackedGuid128("NewPetGuid", "Pet");
                if (petDisplayIdChanged)
                    packet.ReadUInt32("PetDisplayID", "Pet");
                if (petMaxHealthChanged)
                    packet.ReadUInt32("PetMaxHealth", "Pet");
                if (petHealthChanged)
                    packet.ReadUInt32("PetHealth", "Pet");
                if (petAurasChanged)
                {
                    var cnt = packet.ReadInt32("AuraCount", "Pet", "Aura");
                    for (int i = 0; i < cnt; i++)
                        ReadAuraInfos(packet, "Pet", "Aura", i);
                }
            }

            packet.ReadPackedGuid128("AffectedGUID");
            if (partyTypeChanged)
            {
                for (int i = 0; i < 2; i++)
                    packet.ReadByte("PartyType", i);
            }

            if (flagsChanged)
                packet.ReadUInt16E<GroupMemberStatusFlag>("Flags");
            if (powerTypeChanged)
                packet.ReadByte("PowerType");
            if (overrideDisplayPowerChanged)
                packet.ReadUInt16("OverrideDisplayPower");
            if (currentHealthChanged)
                packet.ReadUInt32("CurrentHealth");
            if (maxHealthChanged)
                packet.ReadUInt32("MaxHealth");
            if (powerChanged)
                packet.ReadUInt16("Power");
            if (maxPowerChanged)
                packet.ReadUInt16("MaxPower");
            if (levelChanged)
                packet.ReadUInt16("Level");
            if (specChanged)
                packet.ReadUInt16("Spec");
            if (areaIdChanged)
                packet.ReadUInt16("AreaID");
            if (wmoGroupIdChanged)
                packet.ReadUInt16("WmoGroupID");
            if (wmoDoodadPlacementIdChanged)
                packet.ReadUInt32("WmoDoodadPlacementID");
            if (positionChanged)
            {
                packet.ReadUInt16("PositionX");
                packet.ReadUInt16("PositionY");
                packet.ReadUInt16("PositionZ");
            }
            if (vehicleSeatRecIdChanged)
                packet.ReadUInt32("VehicleSeatRecID");
            if (aurasChanged)
            {
                var cnt = packet.ReadInt32("AuraCount", "Aura");
                for (int i = 0; i < cnt; i++)
                    ReadAuraInfos(packet, "Aura", i);
            }
            if (phaseChanged)
                V6_0_2_19033.Parsers.GroupHandler.ReadPhaseInfos(packet, "Phase");
        }

        [Parser(Opcode.CMSG_SET_EVERYONE_IS_ASSISTANT)]
        public static void HandleEveryoneIsAssistant(Packet packet)
        {
            // might be valid for 602+ too
            packet.ReadByte("PartyIndex");
            packet.ReadBit("Active");
        }

        [Parser(Opcode.CMSG_READY_CHECK_RESPONSE)]
        public static void HandleClientReadyCheckResponse(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadBit("IsReady");
        }
    }
}
