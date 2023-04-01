using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V6_0_2_19033.Enums;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadFactionChangeRestrictionsData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("Mask", idx);
            packet.ReadByte("RaceID", idx);
        }

        public static void ReadCharactersData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);

            packet.ReadByte("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            var klass = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByte("SexID", idx);
            packet.ReadByte("SkinID", idx);
            packet.ReadByte("FaceID", idx);
            packet.ReadByte("HairStyle", idx);
            packet.ReadByte("HairColor", idx);
            packet.ReadByte("FacialHairStyle", idx);
            packet.ReadByte("ExperienceLevel", idx);
            var zone = packet.ReadUInt32<ZoneId>("ZoneID", idx);
            var mapId = packet.ReadUInt32<MapId>("MapID", idx);

            var pos = packet.ReadVector3("PreloadPos", idx);

            packet.ReadPackedGuid128("GuildGUID", idx);

            packet.ReadUInt32("Flags", idx);
            packet.ReadUInt32("Flags2", idx);
            packet.ReadUInt32("Flags3", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.ReadUInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < 23; ++j)
            {
                packet.ReadUInt32("InventoryItem DisplayID", idx, j);
                packet.ReadUInt32("InventoryItem DisplayEnchantID", idx, j);
                packet.ReadByteE<InventoryType>("InventoryItem InvType", idx, j);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.ReadTime("LastPlayedTime", idx);

            packet.ResetBitReader();
            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                packet.ReadBits("Unk Bits141", 5, idx);

            packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = mapId, Zone = zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_SHOW_NEUTRAL_PLAYER_FACTION_SELECT_UI)]
        [Parser(Opcode.CMSG_ENUM_CHARACTERS_DELETED_BY_CLIENT)]
        public static void HandleCharacterZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");

            var charsCount = packet.ReadUInt32("Characters Count");
            var restrictionsCount = packet.ReadUInt32("FactionChangeRestrictionsCount");

            for (uint i = 0; i < charsCount; ++i)
                ReadCharactersData(packet, i, "CharactersData");

            for (var i = 0; i < restrictionsCount; ++i)
                ReadFactionChangeRestrictionsData(packet, i, "FactionChangeRestrictionsData");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var bits29 = packet.ReadBits(6);
            var bit24 = packet.ReadBit();

            packet.ReadByteE<Race>("RaceID");
            packet.ReadByteE<Class>("ClassID");
            packet.ReadByteE<Gender>("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("FaceID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("OutfitID");

            packet.ReadWoWString("Name", bits29);

            if (bit24)
                packet.ReadInt32("TemplateSetID");
        }

        [Parser(Opcode.CMSG_UNDELETE_CHARACTER)]
        public static void HandleUndeleteCharacter(Packet packet)
        {
            packet.ReadInt32("ClientToken");
            packet.ReadPackedGuid128("CharacterGuid");
        }

        [Parser(Opcode.SMSG_UNDELETE_CHARACTER_RESPONSE)]
        public static void HandleUndeleteCharacterResponse(Packet packet)
        {
            packet.ReadInt32("ClientToken");
            packet.ReadInt32E<CharacterUndeleteResult>("Result");
            packet.ReadPackedGuid128("CharacterGuid");
        }

        [Parser(Opcode.CMSG_GET_UNDELETE_CHARACTER_COOLDOWN_STATUS)]
        public static void HandleGetUndeleteCooldownStatus(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE)]
        public static void HandleUndeleteCooldownStatusResponse(Packet packet)
        {
            packet.ReadBit("OnCooldown");
            packet.ReadUInt32("MaxCooldown"); // In Sec
            packet.ReadUInt32("CurrentCooldown"); // In Sec
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            var int32 = packet.ReadInt32("Count");
            for (var i = 0; i < int32; i++)
            {
                packet.ReadInt32("Power", i);
                packet.ReadByteE<PowerType>("PowerType", i);
            }
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)]
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.ReadBits("CharactersCount", 9);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("PlayerGUID");
                packet.ReadByte("NewPosition", i);
            }
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.CMSG_GENERATE_RANDOM_CHARACTER_NAME)]
        public static void HandleGenerateRandomCharacterNameQuery(Packet packet)
        {
            packet.ReadByteE<Race>("Race");
            packet.ReadByteE<Gender>("Sex");
        }

        [Parser(Opcode.SMSG_GENERATE_RANDOM_CHARACTER_NAME_RESULT)]
        public static void HandleGenerateRandomCharacterNameResponse(Packet packet)
        {
            packet.ReadBit("Success");
            var bits17 = packet.ReadBits(6);

            packet.ReadWoWString("Name", bits17);
        }

        [Parser(Opcode.CMSG_CHARACTER_RENAME_REQUEST)]
        public static void HandleClientCharRename(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            packet.ResetBitReader();

            var bits16 = packet.ReadBits(6);
            packet.ReadWoWString("NewName", bits16);
        }

        [Parser(Opcode.SMSG_CHARACTER_RENAME_RESULT)]
        public static void HandleServerCharRename(Packet packet)
        {
            packet.ReadByte("Result");

            packet.ResetBitReader();
            var bit32 = packet.ReadBit("HasGuid");
            var bits41 = packet.ReadBits(6);

            if (bit32)
                packet.ReadPackedGuid128("Guid");

            packet.ReadWoWString("Name", bits41);
        }

        [Parser(Opcode.CMSG_CHAR_RACE_OR_FACTION_CHANGE)]
        public static void HandleCharRaceOrFactionChange(Packet packet)
        {
            packet.ReadBit("FactionChange");

            var bits20 = packet.ReadBits(6);

            var bit93 = packet.ReadBit("HasSkinID");
            var bit96 = packet.ReadBit("HasHairColor");
            var bit89 = packet.ReadBit("HasHairStyleID");
            var bit17 = packet.ReadBit("HasFacialHairStyleID");
            var bit19 = packet.ReadBit("HasFaceID");

            packet.ReadPackedGuid128("Guid");
            packet.ReadByte("SexID");
            packet.ReadByte("RaceID");

            packet.ReadWoWString("Name", bits20);

            if (bit93)
                packet.ReadByte("SkinID");

            if (bit96)
                packet.ReadByte("HairColorID");

            if (bit89)
                packet.ReadByte("HairStyleID");

            if (bit17)
                packet.ReadByte("FacialHairStyleID");

            if (bit19)
                packet.ReadByte("FaceID");
        }

        [Parser(Opcode.SMSG_CHAR_FACTION_CHANGE_RESULT)]
        public static void HandleCharFactionChangeResult(Packet packet)
        {
            packet.ReadByte("Result");
            packet.ReadPackedGuid128("Guid");

            packet.ResetBitReader();

            var bit72 = packet.ReadBit("HasDisplayInfo");
            if (bit72)
            {
                packet.ResetBitReader();
                var bits55 = packet.ReadBits(6);

                packet.ReadByte("SexID");
                packet.ReadByte("SkinID");
                packet.ReadByte("HairColorID");
                packet.ReadByte("HairStyleID");
                packet.ReadByte("FacialHairStyleID");
                packet.ReadByte("FaceID");
                packet.ReadByte("RaceID");

                packet.ReadWoWString("Name", bits55);
            }
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            packet.ReadPackedGuid128("CharGUID");

            packet.ReadByte("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");

            packet.ResetBitReader();

            var bits19 = packet.ReadBits(6);
            packet.ReadWoWString("CharName", bits19);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE_SUCCESS, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleServerCharCustomize60x(Packet packet)
        {
            packet.ReadPackedGuid128("CharGUID");

            packet.ResetBitReader();
            var bits55 = packet.ReadBits(6);

            packet.ReadByte("SexID");

            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");

            packet.ReadWoWString("Name", bits55);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE_SUCCESS, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleServerCharCustomize61x(Packet packet)
        {
            packet.ReadPackedGuid128("CharGUID");
            packet.ReadByte("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("FaceID");

            packet.ResetBitReader();
            var bits55 = packet.ReadBits(6);
            packet.ReadWoWString("Name", bits55);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE_FAILURE)]
        public static void HandleServerCharCustomizeResult(Packet packet)
        {
            packet.ReadByte("Result");
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SET_LOOT_SPECIALIZATION)]
        public static void HandleSetLootSpecialization(Packet packet)
        {
            packet.ReadInt32("SpecID");
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
            packet.ReadInt32("Original");

            packet.ReadByte("Reason");
            packet.ReadInt32("Amount");
            packet.ReadSingle("GroupBonus");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_2_46479))
                packet.ReadBit("ReferAFriend");
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED)]
        public static void HandleXPGainAborted(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");

            packet.ReadInt32("XpToAdd");
            packet.ReadInt32("XpGainReason");
            packet.ReadInt32("XpAbortReason");
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME)]
        public static void HandleNameQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");

            if (!ClientVersion.RemovedInVersion(ClientVersionBuild.V6_1_0_19678))
                return;

            var bit4 = packet.ReadBit();
            var bit12 = packet.ReadBit();

            if (bit4)
                packet.ReadInt32("VirtualRealmAddress");

            if (bit12)
                packet.ReadInt32("NativeRealmAddress");
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            PacketQueryPlayerNameResponseWrapper responses = packet.Holder.QueryPlayerNameResponse = new();
            PacketQueryPlayerNameResponse response = new();
            responses.Responses.Add(response);
            var hasData = packet.ReadByte("HasData");

            response.PlayerGuid = packet.ReadPackedGuid128("Player Guid");

            if (hasData == 0)
            {
                packet.ReadBit("IsDeleted");
                var bits15 = (int)packet.ReadBits(6);

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadPackedGuid128("AccountID");
                packet.ReadPackedGuid128("BnetAccountID");
                packet.ReadPackedGuid128("Player Guid");

                packet.ReadUInt32("VirtualRealmAddress");

                response.Race = (uint)packet.ReadByteE<Race>("Race");
                response.Gender = (uint)packet.ReadByteE<Gender>("Gender");
                response.Class = (uint)packet.ReadByteE<Class>("Class");
                response.Level = packet.ReadByte("Level");

                response.PlayerName = packet.ReadWoWString("Name", bits15);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        public static void HandleClientPlayedTime(Packet packet)
        {
            packet.ReadBit("TriggerScriptEvent");
        }

        [Parser(Opcode.SMSG_PLAYED_TIME)]
        public static void HandleServerPlayedTime(Packet packet)
        {
            packet.ReadInt32("TotalTime");
            packet.ReadInt32("LevelTime");

            packet.ReadBit("TriggerEvent");
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUpInfo(Packet packet)
        {
            packet.ReadInt32("Level");
            packet.ReadInt32("HealthDelta");

            for (var i = 0; i < 6; i++)
                packet.ReadInt32("PowerDelta", (PowerType)i);

            for (var i = 0; i < 5; i++)
                packet.ReadInt32("StatDelta", (StatType)i);

            packet.ReadInt32("Cp");
        }

        [Parser(Opcode.CMSG_SET_PLAYER_DECLINED_NAMES)]
        public static void HandleSetPlayerDeclinedNames(Packet packet)
        {
            packet.ReadPackedGuid128("Player");

            var count = new int[5];
            for (var i = 0; i < 5; ++i)
                count[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < 5; ++i)
                packet.ReadWoWString("DeclinedName", count[i], i);
        }

        [Parser(Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT)]
        public static void HandleSetPlayerDeclinedNamesResult(Packet packet)
        {
            packet.ReadInt32("ResultCode");
            packet.ReadPackedGuid128("Player");
        }

        [Parser(Opcode.SMSG_HEALTH_UPDATE)]
        public static void HandleHealthUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Health");
        }

        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleInspect(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT)]
        public static void HandleInspectResult(Packet packet)
        {
            packet.ReadPackedGuid128("InspecteeGUID");

            var int112 = packet.ReadInt32("ItemsCount");
            var int36 = packet.ReadInt32("GlyphsCount");
            var int20 = packet.ReadInt32("TalentsCount");
            packet.ReadInt32("ClassID");
            packet.ReadInt32("SpecializationID");
            packet.ReadInt32("Gender");

            for (int i = 0; i < int112; i++)
            {
                // sub_614FDF
                packet.ReadPackedGuid128("CreatorGUID", i);

                Substructures.ItemHandler.ReadItemInstance(packet, i);

                packet.ReadByte("Index", i);

                var int80 = packet.ReadInt32("EnchantsCount", i);
                for (int j = 0; j < int80; j++)
                {
                    packet.ReadInt32("Id", i, j);
                    packet.ReadByte("Index", i, j);
                }

                packet.ResetBitReader();

                packet.ReadBit("Usable", i);
            }

            for (int i = 0; i < int36; i++)
                packet.ReadInt16("Glyphs", i);

            for (int i = 0; i < int20; i++)
                packet.ReadInt16("Talents", i);

            packet.ResetBitReader();

            var bit80 = packet.ReadBit("HasGuildData");
            if (bit80)
            {
                // sub_5F9390
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        public static void HandleRequestHonorStats(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStats(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");

            packet.ReadByte("LifetimeMaxRank");

            packet.ReadInt16("YesterdayHK");    // unconfirmed order
            packet.ReadInt16("TodayHK");        // unconfirmed order

            packet.ReadInt32("LifetimeHK");
        }

        public static void ReadPVPBracketData(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Rating", idx);
            packet.ReadInt32("Rank", idx);
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("WeeklyBestRating", idx);
            packet.ReadByte("Bracket", idx);
        }

        [Parser(Opcode.CMSG_INSPECT_PVP)]
        public static void HandleRequestInspectPVP(Packet packet)
        {
            packet.ReadPackedGuid128("InspectTarget");
            packet.ReadInt32("InspectRealmAddress");
        }

        [Parser(Opcode.SMSG_INSPECT_PVP)]
        public static void HandleInspectPVP(Packet packet)
        {
            packet.ReadPackedGuid128("ClientGUID");

            var bracketCount = packet.ReadBits(3);
            for (var i = 0; i < bracketCount; i++)
                ReadPVPBracketData(packet, i, "PVPBracketData");
        }

        [Parser(Opcode.CMSG_MOUNT_SET_FAVORITE)]
        public static void HandleMountSetFavorite(Packet packet)
        {
            packet.ReadInt32("MountSpellID");
            packet.ReadBit("IsFavorite");
        }

        [Parser(Opcode.SMSG_TITLE_EARNED)]
        [Parser(Opcode.SMSG_TITLE_LOST)]
        public static void HandleTitleEarned(Packet packet)
        {
            packet.ReadUInt32("Index");
        }

        [Parser(Opcode.SMSG_NEUTRAL_PLAYER_FACTION_SELECT_RESULT)]
        public static void HandleNeutralPlayerFactionSelectResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadUInt32("NewRaceID");
        }

        [Parser(Opcode.CMSG_NEUTRAL_PLAYER_SELECT_FACTION)]
        public static void HandleNeutralPlayerSelectFaction(Packet packet)
        {
            packet.ReadUInt32("Faction");
        }

        [Parser(Opcode.CMSG_ALTER_APPEARANCE)]
        public static void HandleAlterAppearance(Packet packet)
        {
            packet.ReadUInt32("NewHairStyle");
            packet.ReadUInt32("NewHairColor");
            packet.ReadUInt32("NewFacialHair");
            packet.ReadUInt32("NewSkinColor");
            packet.ReadUInt32("NewFace");
        }

        [Parser(Opcode.SMSG_STAND_STATE_UPDATE)]
        public static void HandleStandStateUpdate(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                packet.ReadInt32("AnimKitID");

            packet.ReadByteE<StandState>("State");
        }

        [Parser(Opcode.CMSG_SET_PVP)]
        public static void HandleSetPVP(Packet packet)
        {
            packet.ReadBit("EnablePVP");
        }

        [Parser(Opcode.CMSG_KEYBOUND_OVERRIDE)]
        public static void HandleKeyboundOverride(Packet packet)
        {
            packet.ReadUInt16("OverrideID");
        }
    }
}
