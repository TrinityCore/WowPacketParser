using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATE)]
        public static void HandlePartyMemberState(Packet packet)
        {
            packet.Translator.ReadBit("ForEnemy");

            for (var i = 0; i < 2; i++)
                packet.Translator.ReadByte("PartyType", i);

            packet.Translator.ReadInt16E<GroupMemberStatusFlag>("Flags");

            packet.Translator.ReadByte("PowerType");
            packet.Translator.ReadInt16("OverrideDisplayPower");
            packet.Translator.ReadInt32("CurrentHealth");
            packet.Translator.ReadInt32("MaxHealth");
            packet.Translator.ReadInt16("MaxPower");
            packet.Translator.ReadInt16("MaxPower");
            packet.Translator.ReadInt16("Level");
            packet.Translator.ReadInt16("Spec");
            packet.Translator.ReadInt16("AreaID");

            packet.Translator.ReadInt16("WmoGroupID");
            packet.Translator.ReadInt32("WmoDoodadPlacementID");

            packet.Translator.ReadInt16("PositionX");
            packet.Translator.ReadInt16("PositionY");
            packet.Translator.ReadInt16("PositionZ");

            packet.Translator.ReadInt32("VehicleSeatRecID");
            var auraCount = packet.Translator.ReadInt32("AuraCount");

            packet.Translator.ReadInt32("PhaseShiftFlags");
            var int4 = packet.Translator.ReadInt32("PhaseCount");
            packet.Translator.ReadPackedGuid128("PersonalGUID");
            for (int i = 0; i < int4; i++)
            {
                packet.Translator.ReadInt16("PhaseFlags", i);
                packet.Translator.ReadInt16("Id", i);
            }

            for (int i = 0; i < auraCount; i++)
            {
                packet.Translator.ReadInt32<SpellId>("Aura", i);
                packet.Translator.ReadByte("Flags", i);
                packet.Translator.ReadInt32("ActiveFlags", i);
                var byte3 = packet.Translator.ReadInt32("PointsCount", i);

                for (int j = 0; j < byte3; j++)
                    packet.Translator.ReadSingle("Points", i, j);
            }

            packet.Translator.ResetBitReader();

            var hasPet = packet.Translator.ReadBit("HasPet");
            if (hasPet) // Pet
            {
                packet.Translator.ReadPackedGuid128("PetGuid");
                packet.Translator.ReadInt16("PetDisplayID");
                packet.Translator.ReadInt32("PetMaxHealth");
                packet.Translator.ReadInt32("PetHealth");

                var petAuraCount = packet.Translator.ReadInt32("PetAuraCount");
                for (int i = 0; i < petAuraCount; i++)
                {
                    packet.Translator.ReadInt32<SpellId>("PetAura", i);
                    packet.Translator.ReadByte("PetFlags", i);
                    packet.Translator.ReadInt32("PetActiveFlags", i);
                    var byte3 = packet.Translator.ReadInt32("PetPointsCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.Translator.ReadSingle("PetPoints", i, j);
                }

                packet.Translator.ResetBitReader();

                var len = packet.Translator.ReadBits(8);
                packet.Translator.ReadWoWString("PetName", len);
            }

            packet.Translator.ReadPackedGuid128("MemberGuid");
        }

        [Parser(Opcode.CMSG_PARTY_INVITE, ClientVersionBuild.V7_1_0_22900)]
        public static void HandleClientPartyInvite(Packet packet)
        {
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadInt32("ProposedRoles");
            packet.Translator.ReadPackedGuid128("TargetGuid");

            packet.Translator.ResetBitReader();

            var lenTargetName = packet.Translator.ReadBits(9);
            var lenTargetRealm = packet.Translator.ReadBits(9);

            packet.Translator.ReadWoWString("TargetName", lenTargetName);
            packet.Translator.ReadWoWString("TargetRealm", lenTargetRealm);
        }

        [Parser(Opcode.SMSG_PARTY_INVITE)]
        public static void HandlePartyInvite(Packet packet)
        {
            packet.Translator.ReadBit("CanAccept");
            packet.Translator.ReadBit("MightCRZYou");
            packet.Translator.ReadBit("IsXRealm");
            packet.Translator.ReadBit("MustBeBNetFriend");
            packet.Translator.ReadBit("AllowMultipleRoles");
            var len = packet.Translator.ReadBits(6);

            packet.Translator.ResetBitReader();
            packet.Translator.ReadInt32("InviterVirtualRealmAddress");
            packet.Translator.ReadBit("IsLocal");
            packet.Translator.ReadBit("Unk2");
            var bits2 = packet.Translator.ReadBits(8);
            var bits258 = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("InviterRealmNameActual", bits2);
            packet.Translator.ReadWoWString("InviterRealmNameNormalized", bits258);

            packet.Translator.ReadPackedGuid128("InviterGuid");
            packet.Translator.ReadPackedGuid128("InviterBNetAccountID");
            packet.Translator.ReadInt16("Unk1");
            packet.Translator.ReadInt32("ProposedRoles");
            var lfgSlots = packet.Translator.ReadInt32();
            packet.Translator.ReadInt32("LfgCompletedMask");
            packet.Translator.ReadWoWString("InviterName", len);
            for (int i = 0; i < lfgSlots; i++)
                packet.Translator.ReadInt32("LfgSlots", i);
        }

        [Parser(Opcode.SMSG_PARTY_UPDATE, ClientVersionBuild.V7_1_0_22900)]
        public static void HandlePartyUpdate(Packet packet)
        {
            packet.Translator.ReadUInt16("PartyFlags");
            packet.Translator.ReadByte("PartyIndex");
            packet.Translator.ReadByte("PartyType");

            packet.Translator.ReadInt32("MyIndex");
            packet.Translator.ReadPackedGuid128("PartyGUID");
            packet.Translator.ReadInt32("SequenceNum");
            packet.Translator.ReadPackedGuid128("LeaderGUID");

            var playerCount = packet.Translator.ReadInt32("PlayerListCount");
            var hasLFG = packet.Translator.ReadBit("HasLfgInfo");
            var hasLootSettings = packet.Translator.ReadBit("HasLootSettings");
            var hasDifficultySettings = packet.Translator.ReadBit("HasDifficultySettings");

            for (int i = 0; i < playerCount; i++)
            {
                packet.Translator.ResetBitReader();
                var playerNameLength = packet.Translator.ReadBits(6);
                packet.Translator.ReadBit("FromSocialQueue", i);

                packet.Translator.ReadPackedGuid128("Guid", i);

                packet.Translator.ReadByte("Status", i);
                packet.Translator.ReadByte("Subgroup", i);
                packet.Translator.ReadByte("Flags", i);
                packet.Translator.ReadByte("RolesAssigned", i);
                packet.Translator.ReadByteE<Class>("Class", i);

                packet.Translator.ReadWoWString("Name", playerNameLength, i);
            }

            packet.Translator.ResetBitReader();

            if (hasLootSettings)
            {
                packet.Translator.ReadByte("Method", "PartyLootSettings");
                packet.Translator.ReadPackedGuid128("LootMaster", "PartyLootSettings");
                packet.Translator.ReadByte("Threshold", "PartyLootSettings");
            }

            if (hasDifficultySettings)
            {
                packet.Translator.ReadInt32("DungeonDifficultyID");
                packet.Translator.ReadInt32("RaidDifficultyID");
                packet.Translator.ReadInt32("LegacyRaidDifficultyID");
            }

            if (hasLFG)
            {
                packet.Translator.ResetBitReader();

                packet.Translator.ReadByte("MyFlags");
                packet.Translator.ReadInt32("Slot");
                packet.Translator.ReadInt32("MyRandomSlot");
                packet.Translator.ReadByte("MyPartialClear");
                packet.Translator.ReadSingle("MyGearDiff");
                packet.Translator.ReadByte("MyStrangerCount");
                packet.Translator.ReadByte("MyKickVoteCount");
                packet.Translator.ReadByte("BootCount");
                packet.Translator.ReadBit("Aborted");
                packet.Translator.ReadBit("MyFirstReward");
            }
        }
    }
}
