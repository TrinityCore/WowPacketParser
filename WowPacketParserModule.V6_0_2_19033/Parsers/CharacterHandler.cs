using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V6_0_2_19033.Enums;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadFactionChangeRestrictionsData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("Mask", idx);
            packet.Translator.ReadByte("RaceID", idx);
        }

        public static void ReadCharactersData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("Guid", idx);

            packet.Translator.ReadByte("ListPosition", idx);
            var race = packet.Translator.ReadByteE<Race>("RaceID", idx);
            var klass = packet.Translator.ReadByteE<Class>("ClassID", idx);
            packet.Translator.ReadByte("SexID", idx);
            packet.Translator.ReadByte("SkinID", idx);
            packet.Translator.ReadByte("FaceID", idx);
            packet.Translator.ReadByte("HairStyle", idx);
            packet.Translator.ReadByte("HairColor", idx);
            packet.Translator.ReadByte("FacialHairStyle", idx);
            packet.Translator.ReadByte("ExperienceLevel", idx);
            var zone = packet.Translator.ReadUInt32("ZoneID", idx);
            var mapId = packet.Translator.ReadUInt32("MapID", idx);

            var pos = packet.Translator.ReadVector3("PreloadPos", idx);

            packet.Translator.ReadPackedGuid128("GuildGUID", idx);

            packet.Translator.ReadUInt32("Flags", idx);
            packet.Translator.ReadUInt32("Flags2", idx);
            packet.Translator.ReadUInt32("Flags3", idx);
            packet.Translator.ReadUInt32("PetCreatureDisplayID", idx);
            packet.Translator.ReadUInt32("PetExperienceLevel", idx);
            packet.Translator.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.Translator.ReadUInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < 23; ++j)
            {
                packet.Translator.ReadUInt32("InventoryItem DisplayID", idx, j);
                packet.Translator.ReadUInt32("InventoryItem DisplayEnchantID", idx, j);
                packet.Translator.ReadByteE<InventoryType>("InventoryItem InvType", idx, j);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_4_21315))
                packet.Translator.ReadTime("LastPlayedTime", idx);

            packet.Translator.ResetBitReader();
            var nameLength = packet.Translator.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.Translator.ReadBit("FirstLogin", idx);
            packet.Translator.ReadBit("BoostInProgress", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                packet.Translator.ReadBits("Unk Bits141", 5, idx);

            packet.Translator.ReadWoWString("Character Name", nameLength, idx);

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
            packet.Translator.ReadBit("Success");
            packet.Translator.ReadBit("IsDeletedCharacters");

            var charsCount = packet.Translator.ReadUInt32("Characters Count");
            var restrictionsCount = packet.Translator.ReadUInt32("FactionChangeRestrictionsCount");

            for (uint i = 0; i < charsCount; ++i)
                ReadCharactersData(packet, i, "CharactersData");

            for (var i = 0; i < restrictionsCount; ++i)
                ReadFactionChangeRestrictionsData(packet, i, "FactionChangeRestrictionsData");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var bits29 = packet.Translator.ReadBits(6);
            var bit24 = packet.Translator.ReadBit();

            packet.Translator.ReadByteE<Race>("RaceID");
            packet.Translator.ReadByteE<Class>("ClassID");
            packet.Translator.ReadByteE<Gender>("SexID");
            packet.Translator.ReadByte("SkinID");
            packet.Translator.ReadByte("FaceID");
            packet.Translator.ReadByte("HairStyleID");
            packet.Translator.ReadByte("HairColorID");
            packet.Translator.ReadByte("FacialHairStyleID");
            packet.Translator.ReadByte("OutfitID");

            packet.Translator.ReadWoWString("Name", bits29);

            if (bit24)
                packet.Translator.ReadInt32("TemplateSetID");
        }

        [Parser(Opcode.CMSG_UNDELETE_CHARACTER)]
        public static void HandleUndeleteCharacter(Packet packet)
        {
            packet.Translator.ReadInt32("ClientToken");
            packet.Translator.ReadPackedGuid128("CharacterGuid");
        }

        [Parser(Opcode.SMSG_UNDELETE_CHARACTER_RESPONSE)]
        public static void HandleUndeleteCharacterResponse(Packet packet)
        {
            packet.Translator.ReadInt32("ClientToken");
            packet.Translator.ReadInt32E<CharacterUndeleteResult>("Result");
            packet.Translator.ReadPackedGuid128("CharacterGuid");
        }

        [Parser(Opcode.CMSG_GET_UNDELETE_CHARACTER_COOLDOWN_STATUS)]
        public static void HandleGetUndeleteCooldownStatus(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_UNDELETE_COOLDOWN_STATUS_RESPONSE)]
        public static void HandleUndeleteCooldownStatusResponse(Packet packet)
        {
            packet.Translator.ReadBit("OnCooldown");
            packet.Translator.ReadInt32("MaxCooldown"); // In Sec
            packet.Translator.ReadInt32("CurrentCooldown"); // In Sec
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");

            var int32 = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < int32; i++)
            {
                packet.Translator.ReadInt32("Power", i);
                packet.Translator.ReadByteE<PowerType>("PowerType", i);
            }
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)]
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.Translator.ReadBits("CharactersCount", 9);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadPackedGuid128("PlayerGUID");
                packet.Translator.ReadByte("NewPosition", i);
            }
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");
        }

        [Parser(Opcode.CMSG_GENERATE_RANDOM_CHARACTER_NAME)]
        public static void HandleGenerateRandomCharacterNameQuery(Packet packet)
        {
            packet.Translator.ReadByteE<Race>("Race");
            packet.Translator.ReadByteE<Gender>("Sex");
        }

        [Parser(Opcode.SMSG_GENERATE_RANDOM_CHARACTER_NAME_RESULT)]
        public static void HandleGenerateRandomCharacterNameResponse(Packet packet)
        {
            packet.Translator.ReadBit("Success");
            var bits17 = packet.Translator.ReadBits(6);

            packet.Translator.ReadWoWString("Name", bits17);
        }

        [Parser(Opcode.CMSG_CHARACTER_RENAME_REQUEST)]
        public static void HandleClientCharRename(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");

            packet.Translator.ResetBitReader();

            var bits16 = packet.Translator.ReadBits(6);
            packet.Translator.ReadWoWString("NewName", bits16);
        }

        [Parser(Opcode.SMSG_CHARACTER_RENAME_RESULT)]
        public static void HandleServerCharRename(Packet packet)
        {
            packet.Translator.ReadByte("Result");

            packet.Translator.ResetBitReader();
            var bit32 = packet.Translator.ReadBit("HasGuid");
            var bits41 = packet.Translator.ReadBits(6);

            if (bit32)
                packet.Translator.ReadPackedGuid128("Guid");

            packet.Translator.ReadWoWString("Name", bits41);
        }

        [Parser(Opcode.CMSG_CHAR_RACE_OR_FACTION_CHANGE)]
        public static void HandleCharRaceOrFactionChange(Packet packet)
        {
            packet.Translator.ReadBit("FactionChange");

            var bits20 = packet.Translator.ReadBits(6);

            var bit93 = packet.Translator.ReadBit("HasSkinID");
            var bit96 = packet.Translator.ReadBit("HasHairColor");
            var bit89 = packet.Translator.ReadBit("HasHairStyleID");
            var bit17 = packet.Translator.ReadBit("HasFacialHairStyleID");
            var bit19 = packet.Translator.ReadBit("HasFaceID");

            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadByte("SexID");
            packet.Translator.ReadByte("RaceID");

            packet.Translator.ReadWoWString("Name", bits20);

            if (bit93)
                packet.Translator.ReadByte("SkinID");

            if (bit96)
                packet.Translator.ReadByte("HairColorID");

            if (bit89)
                packet.Translator.ReadByte("HairStyleID");

            if (bit17)
                packet.Translator.ReadByte("FacialHairStyleID");

            if (bit19)
                packet.Translator.ReadByte("FaceID");
        }

        [Parser(Opcode.SMSG_CHAR_FACTION_CHANGE_RESULT)]
        public static void HandleCharFactionChangeResult(Packet packet)
        {
            packet.Translator.ReadByte("Result");
            packet.Translator.ReadPackedGuid128("Guid");

            packet.Translator.ResetBitReader();

            var bit72 = packet.Translator.ReadBit("HasDisplayInfo");
            if (bit72)
            {
                packet.Translator.ResetBitReader();
                var bits55 = packet.Translator.ReadBits(6);

                packet.Translator.ReadByte("SexID");
                packet.Translator.ReadByte("SkinID");
                packet.Translator.ReadByte("HairColorID");
                packet.Translator.ReadByte("HairStyleID");
                packet.Translator.ReadByte("FacialHairStyleID");
                packet.Translator.ReadByte("FaceID");
                packet.Translator.ReadByte("RaceID");

                packet.Translator.ReadWoWString("Name", bits55);
            }
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CharGUID");

            packet.Translator.ReadByte("SexID");
            packet.Translator.ReadByte("SkinID");
            packet.Translator.ReadByte("HairColorID");
            packet.Translator.ReadByte("HairStyleID");
            packet.Translator.ReadByte("FacialHairStyleID");
            packet.Translator.ReadByte("FaceID");

            packet.Translator.ResetBitReader();

            var bits19 = packet.Translator.ReadBits(6);
            packet.Translator.ReadWoWString("CharName", bits19);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleServerCharCustomize60x(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CharGUID");

            packet.Translator.ResetBitReader();
            var bits55 = packet.Translator.ReadBits(6);

            packet.Translator.ReadByte("SexID");

            packet.Translator.ReadByte("SkinID");
            packet.Translator.ReadByte("HairColorID");
            packet.Translator.ReadByte("HairStyleID");
            packet.Translator.ReadByte("FacialHairStyleID");
            packet.Translator.ReadByte("FaceID");

            packet.Translator.ReadWoWString("Name", bits55);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleServerCharCustomize61x(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CharGUID");
            packet.Translator.ReadByte("SexID");
            packet.Translator.ReadByte("SkinID");
            packet.Translator.ReadByte("HairColorID");
            packet.Translator.ReadByte("HairStyleID");
            packet.Translator.ReadByte("FacialHairStyleID");
            packet.Translator.ReadByte("FaceID");

            packet.Translator.ResetBitReader();
            var bits55 = packet.Translator.ReadBits(6);
            packet.Translator.ReadWoWString("Name", bits55);
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE_RESULT)]
        public static void HandleServerCharCustomizeResult(Packet packet)
        {
            packet.Translator.ReadByte("Result");
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SET_LOOT_SPECIALIZATION)]
        public static void HandleSetLootSpecialization(Packet packet)
        {
            packet.Translator.ReadInt32("SpecID");
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Victim");
            packet.Translator.ReadInt32("Original");

            packet.Translator.ReadByte("Reason");
            packet.Translator.ReadInt32("Amount");
            packet.Translator.ReadSingle("GroupBonus");

            packet.Translator.ReadBit("ReferAFriend");
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED)]
        public static void HandleXPGainAborted(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Victim");

            packet.Translator.ReadInt32("XpToAdd");
            packet.Translator.ReadInt32("XpGainReason");
            packet.Translator.ReadInt32("XpAbortReason");
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME)]
        public static void HandleNameQuery(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");

            if (!ClientVersion.RemovedInVersion(ClientVersionBuild.V6_1_0_19678))
                return;

            var bit4 = packet.Translator.ReadBit();
            var bit12 = packet.Translator.ReadBit();

            if (bit4)
                packet.Translator.ReadInt32("VirtualRealmAddress");

            if (bit12)
                packet.Translator.ReadInt32("NativeRealmAddress");
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var hasData = packet.Translator.ReadByte("HasData");

            packet.Translator.ReadPackedGuid128("Player Guid");

            if (hasData == 0)
            {
                packet.Translator.ReadBit("IsDeleted");
                var bits15 = (int)packet.Translator.ReadBits(6);

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.Translator.ReadBits(7);

                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadWoWString("Name Declined", count[i], i);

                packet.Translator.ReadPackedGuid128("AccountID");
                packet.Translator.ReadPackedGuid128("BnetAccountID");
                packet.Translator.ReadPackedGuid128("Player Guid");

                packet.Translator.ReadUInt32("VirtualRealmAddress");

                packet.Translator.ReadByteE<Race>("Race");
                packet.Translator.ReadByteE<Gender>("Gender");
                packet.Translator.ReadByteE<Class>("Class");
                packet.Translator.ReadByte("Level");

                packet.Translator.ReadWoWString("Name", bits15);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        public static void HandleClientPlayedTime(Packet packet)
        {
            packet.Translator.ReadBit("TriggerScriptEvent");
        }

        [Parser(Opcode.SMSG_PLAYED_TIME)]
        public static void HandleServerPlayedTime(Packet packet)
        {
            packet.Translator.ReadInt32("TotalTime");
            packet.Translator.ReadInt32("LevelTime");

            packet.Translator.ReadBit("TriggerEvent");
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUpInfo(Packet packet)
        {
            packet.Translator.ReadInt32("Level");
            packet.Translator.ReadInt32("HealthDelta");

            for (var i = 0; i < 6; i++)
                packet.Translator.ReadInt32("PowerDelta", (PowerType)i);

            for (var i = 0; i < 5; i++)
                packet.Translator.ReadInt32("StatDelta", (StatType)i);

            packet.Translator.ReadInt32("Cp");
        }

        [Parser(Opcode.CMSG_SET_PLAYER_DECLINED_NAMES)]
        public static void HandleSetPlayerDeclinedNames(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Player");

            var count = new int[5];
            for (var i = 0; i < 5; ++i)
                count[i] = (int)packet.Translator.ReadBits(7);

            for (var i = 0; i < 5; ++i)
                packet.Translator.ReadWoWString("DeclinedName", count[i], i);
        }

        [Parser(Opcode.SMSG_SET_PLAYER_DECLINED_NAMES_RESULT)]
        public static void HandleSetPlayerDeclinedNamesResult(Packet packet)
        {
            packet.Translator.ReadInt32("ResultCode");
            packet.Translator.ReadPackedGuid128("Player");
        }

        [Parser(Opcode.SMSG_HEALTH_UPDATE)]
        public static void HandleHealthUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
            packet.Translator.ReadInt32("Health");
        }

        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleInspect(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Target");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT)]
        public static void HandleInspectResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InspecteeGUID");

            var int112 = packet.Translator.ReadInt32("ItemsCount");
            var int36 = packet.Translator.ReadInt32("GlyphsCount");
            var int20 = packet.Translator.ReadInt32("TalentsCount");
            packet.Translator.ReadInt32("ClassID");
            packet.Translator.ReadInt32("SpecializationID");
            packet.Translator.ReadInt32("Gender");

            for (int i = 0; i < int112; i++)
            {
                // sub_614FDF
                packet.Translator.ReadPackedGuid128("CreatorGUID", i);

                ItemHandler.ReadItemInstance(packet, i);

                packet.Translator.ReadByte("Index", i);

                var int80 = packet.Translator.ReadInt32("EnchantsCount", i);
                for (int j = 0; j < int80; j++)
                {
                    packet.Translator.ReadInt32("Id", i, j);
                    packet.Translator.ReadByte("Index", i, j);
                }

                packet.Translator.ResetBitReader();

                packet.Translator.ReadBit("Usable", i);
            }

            for (int i = 0; i < int36; i++)
                packet.Translator.ReadInt16("Glyphs", i);

            for (int i = 0; i < int20; i++)
                packet.Translator.ReadInt16("Talents", i);

            packet.Translator.ResetBitReader();

            var bit80 = packet.Translator.ReadBit("HasGuildData");
            if (bit80)
            {
                // sub_5F9390
                packet.Translator.ReadPackedGuid128("GuildGUID");
                packet.Translator.ReadInt32("NumGuildMembers");
                packet.Translator.ReadInt32("GuildAchievementPoints");
            }
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS)]
        public static void HandleRequestHonorStats(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStats(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");

            packet.Translator.ReadByte("LifetimeMaxRank");

            packet.Translator.ReadInt16("YesterdayHK");    // unconfirmed order
            packet.Translator.ReadInt16("TodayHK");        // unconfirmed order

            packet.Translator.ReadInt32("LifetimeHK");
        }

        public static void ReadPVPBracketData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("Rating", idx);
            packet.Translator.ReadInt32("Rank", idx);
            packet.Translator.ReadInt32("WeeklyPlayed", idx);
            packet.Translator.ReadInt32("WeeklyWon", idx);
            packet.Translator.ReadInt32("SeasonPlayed", idx);
            packet.Translator.ReadInt32("SeasonWon", idx);
            packet.Translator.ReadInt32("WeeklyBestRating", idx);
            packet.Translator.ReadByte("Bracket", idx);
        }

        [Parser(Opcode.CMSG_INSPECT_PVP)]
        public static void HandleRequestInspectPVP(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("InspectTarget");
            packet.Translator.ReadInt32("InspectRealmAddress");
        }

        [Parser(Opcode.SMSG_INSPECT_PVP)]
        public static void HandleInspectPVP(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ClientGUID");

            var bracketCount = packet.Translator.ReadBits(3);
            for (var i = 0; i < bracketCount; i++)
                ReadPVPBracketData(packet, i, "PVPBracketData");
        }

        [Parser(Opcode.CMSG_MOUNT_SET_FAVORITE)]
        public static void HandleMountSetFavorite(Packet packet)
        {
            packet.Translator.ReadInt32("MountSpellID");
            packet.Translator.ReadBit("IsFavorite");
        }

        [Parser(Opcode.SMSG_TITLE_EARNED)]
        [Parser(Opcode.SMSG_TITLE_LOST)]
        public static void HandleTitleEarned(Packet packet)
        {
            packet.Translator.ReadUInt32("Index");
        }

        [Parser(Opcode.SMSG_NEUTRAL_PLAYER_FACTION_SELECT_RESULT)]
        public static void HandleNeutralPlayerFactionSelectResult(Packet packet)
        {
            packet.Translator.ReadBit("Success");
            packet.Translator.ReadUInt32("NewRaceID");
        }

        [Parser(Opcode.CMSG_NEUTRAL_PLAYER_SELECT_FACTION)]
        public static void HandleNeutralPlayerSelectFaction(Packet packet)
        {
            packet.Translator.ReadUInt32("Faction");
        }

        [Parser(Opcode.CMSG_ALTER_APPEARANCE)]
        public static void HandleAlterAppearance(Packet packet)
        {
            packet.Translator.ReadUInt32("NewHairStyle");
            packet.Translator.ReadUInt32("NewHairColor");
            packet.Translator.ReadUInt32("NewFacialHair");
            packet.Translator.ReadUInt32("NewSkinColor");
            packet.Translator.ReadUInt32("NewFace");
        }

        [Parser(Opcode.SMSG_STAND_STATE_UPDATE)]
        public static void HandleStandStateUpdate(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                packet.Translator.ReadInt32("AnimKitID");

            packet.Translator.ReadByteE<StandState>("State");
        }

        [Parser(Opcode.CMSG_SET_PVP)]
        public static void HandleSetPVP(Packet packet)
        {
            packet.Translator.ReadBit("EnablePVP");
        }
    }
}
