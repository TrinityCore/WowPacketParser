using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class GroupHandler
    {
        public static void ReadAuraInfos(Packet packet, params object[] index)
        {
            packet.ReadUInt32<SpellId>("Aura", index);
            packet.ReadByte("Flags", index);
            packet.ReadInt32("ActiveFlags", index);
            var byte3 = packet.ReadInt32("PointsCount", index);

            for (int j = 0; j < byte3; j++)
                packet.ReadSingle("Points", index, j);
        }

        public static void ReadPetInfos(Packet packet, params object[] index)
        {
            packet.ReadPackedGuid128("PetGuid", index);
            packet.ReadUInt32("PetDisplayID", index);
            packet.ReadUInt32("PetMaxHealth", index);
            packet.ReadUInt32("PetHealth", index);

            var petAuraCount = packet.ReadInt32("PetAuraCount", index);
            for (int i = 0; i < petAuraCount; i++)
                ReadAuraInfos(packet, index, i);

            packet.ResetBitReader();

            var len = packet.ReadBits(8);
            packet.ReadWoWString("PetName", len, index);
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATE)]
        public static void HandlePartyMemberState(Packet packet)
        {
            packet.ReadBit("ForEnemy");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("PartyType", i);

            packet.ReadInt16E<GroupMemberStatusFlag>("Flags");

            packet.ReadByte("PowerType");
            packet.ReadInt16("OverrideDisplayPower");
            packet.ReadInt32("CurrentHealth");
            packet.ReadInt32("MaxHealth");
            packet.ReadInt16("MaxPower");
            packet.ReadInt16("MaxPower");
            packet.ReadInt16("Level");
            packet.ReadInt16("Spec");
            packet.ReadInt16("AreaID");

            packet.ReadInt16("WmoGroupID");
            packet.ReadInt32("WmoDoodadPlacementID");

            packet.ReadInt16("PositionX");
            packet.ReadInt16("PositionY");
            packet.ReadInt16("PositionZ");

            packet.ReadInt32("VehicleSeatRecID");
            var auraCount = packet.ReadInt32("AuraCount");

            V6_0_2_19033.Parsers.GroupHandler.ReadPhaseInfos(packet, "Phase");

            for (int i = 0; i < auraCount; i++)
                ReadAuraInfos(packet, "Aura", i);

            packet.ResetBitReader();

            var hasPet = packet.ReadBit("HasPet");
            if (hasPet) // Pet
                ReadPetInfos(packet, "Pet");

            packet.ReadPackedGuid128("MemberGuid");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadInt32("ProposedRoles");
            packet.ReadPackedGuid128("TargetGuid");

            packet.ResetBitReader();

            var lenTargetName = packet.ReadBits(9);
            var lenTargetRealm = packet.ReadBits(9);

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

            packet.ResetBitReader();
            packet.ReadInt32("InviterVirtualRealmAddress");
            packet.ReadBit("IsLocal");
            packet.ReadBit("Unk2");
            var bits2 = packet.ReadBits(8);
            var bits258 = packet.ReadBits(8);
            packet.ReadWoWString("InviterRealmNameActual", bits2);
            packet.ReadWoWString("InviterRealmNameNormalized", bits258);

            packet.ReadPackedGuid128("InviterGuid");
            packet.ReadPackedGuid128("InviterBNetAccountID");
            packet.ReadInt16("Unk1");
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

        [Parser(Opcode.SMSG_PARTY_MEMBER_STATE_UPDATE)]
        public static void HandlePartyMemberStateUpdate(Packet packet)
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
