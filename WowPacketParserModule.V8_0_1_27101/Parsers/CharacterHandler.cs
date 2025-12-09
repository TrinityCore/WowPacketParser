using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class CharacterHandler
    {
        public static void ReadAzeriteEssenceData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("Index", idx);
            packet.ReadUInt32("AzeriteEssenceID", idx);
            packet.ReadUInt32("Rank", idx);
            packet.ReadBit("SlotUnlocked", idx);
            packet.ResetBitReader();
        }

        public static void ReadInspectItemData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CreatorGUID", idx);
            packet.ReadByte("Index", idx);

            var azeritePowerCount = packet.ReadUInt32("AzeritePowersCount", idx);
            uint azeriteEssenceCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                azeriteEssenceCount = packet.ReadUInt32();

            for (int j = 0; j < azeritePowerCount; j++)
                packet.ReadInt32("AzeritePowerId", idx, j);

            Substructures.ItemHandler.ReadItemInstance(packet, idx);

            packet.ReadBit("Usable", idx);
            var enchantsCount = packet.ReadBits("EnchantsCount", 4, idx);
            var gemsCount = packet.ReadBits("GemsCount", 2, idx);
            packet.ResetBitReader();

            for (int i = 0; i < azeriteEssenceCount; i++)
                ReadAzeriteEssenceData(packet, "AzeriteEssence", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
            {
                for (int i = 0; i < enchantsCount; i++)
                {
                    packet.ReadUInt32("Id", idx, i);
                    packet.ReadByte("Index", idx, i);
                }

                for (int i = 0; i < gemsCount; i++)
                {
                    packet.ReadByte("Slot", idx, i);
                    Substructures.ItemHandler.ReadItemInstance(packet, idx, i);
                }
            }
            else
            {
                for (int i = 0; i < gemsCount; i++)
                {
                    packet.ReadByte("Slot", idx, i);
                    Substructures.ItemHandler.ReadItemInstance(packet, idx, i);
                }

                for (int i = 0; i < enchantsCount; i++)
                {
                    packet.ReadUInt32("Id", idx, i);
                    packet.ReadByte("Index", idx, i);
                }
            }
        }

        public static void ReadPlayerModelDisplayInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("InspecteeGUID", idx);
            packet.ReadInt32("SpecializationID", idx);
            var itemCount = packet.ReadUInt32();
            var nameLen = packet.ReadBits(6);
            packet.ResetBitReader();
            packet.ReadByteE<Gender>("GenderID", idx);
            packet.ReadByte("Skin", idx);
            packet.ReadByte("HairColor", idx);
            packet.ReadByte("HairStyle", idx);
            packet.ReadByte("FacialHairStyle", idx);
            packet.ReadByte("Face", idx);
            packet.ReadByteE<Race>("Race", idx);
            packet.ReadByteE<Class>("ClassID", idx);
            for (int i = 0; i < 3; i++)
                packet.ReadByte("CustomDisplay", idx);
            packet.ReadWoWString("Name", nameLen, idx);

            for (int i = 0; i < itemCount; i++)
                ReadInspectItemData(packet, idx, i);
        }

        public static PlayerGuidLookupData ReadPlayerGuidLookupData(Packet packet, params object[] idx)
        {
            PlayerGuidLookupData data = new PlayerGuidLookupData();

            packet.ResetBitReader();
            packet.ReadBit("IsDeleted", idx);
            var bits15 = (int)packet.ReadBits(6);

            var count = new int[5];
            for (var i = 0; i < 5; ++i)
                count[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < 5; ++i)
                packet.ReadWoWString("Name Declined", count[i], i, idx);

            packet.ReadPackedGuid128("AccountID", idx);
            packet.ReadPackedGuid128("BnetAccountID", idx);
            packet.ReadPackedGuid128("Player Guid", idx);

            packet.ReadUInt64("GuildClubMemberID", idx);
            packet.ReadUInt32("VirtualRealmAddress", idx);

            data.Race = packet.ReadByteE<Race>("Race", idx);
            data.Gender = packet.ReadByteE<Gender>("Gender", idx);
            data.Class = packet.ReadByteE<Class>("Class", idx);
            data.Level = packet.ReadByte("Level", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadByte("PvpFaction", idx);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_7_54577))
                packet.ReadInt32("TimerunningSeasonID", idx);

            data.Name = packet.ReadWoWString("Name", bits15, idx);

            return data;
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
                var data = ReadPlayerGuidLookupData(packet);
                response.Race = (uint)data.Race;
                response.Gender = (uint)data.Gender;
                response.Class = (uint)data.Class;
                response.Level = data.Level;
                response.HasData = true;
            }
        }

        public static void ReadCharactersData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);

            packet.ReadUInt64("GuildClubMemberID", idx);

            packet.ReadByte("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            var klass = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            packet.ReadByte("SkinID", idx);
            packet.ReadByte("FaceID", idx);
            packet.ReadByte("HairStyle", idx);
            packet.ReadByte("HairColor", idx);
            packet.ReadByte("FacialHairStyle", idx);

            for (uint j = 0; j < 3; ++j)
                packet.ReadByte("CustomDisplay", idx, j);

            packet.ReadByte("ExperienceLevel", idx);
            var zone = packet.ReadInt32<ZoneId>("ZoneID", idx);
            var mapId = packet.ReadInt32<MapId>("MapID", idx);

            var pos = packet.ReadVector3("PreloadPos", idx);

            packet.ReadPackedGuid128("GuildGUID", idx);

            packet.ReadUInt32("Flags", idx);
            packet.ReadUInt32("Flags2", idx);
            packet.ReadUInt32("Flags3", idx);
            packet.ReadUInt32("PetCreatureDisplayID", idx);
            packet.ReadUInt32("PetExperienceLevel", idx);
            packet.ReadUInt32("PetCreatureFamilyID", idx);

            for (uint j = 0; j < 2; ++j)
                packet.ReadInt32("ProfessionIDs", idx, j);

            for (uint j = 0; j < 23; ++j)
            {
                packet.ReadUInt32("DisplayID", idx, "VisualItems", j);
                packet.ReadUInt32("DisplayEnchantID", idx, "VisualItems", j);
                packet.ReadByteE<InventoryType>("InvType", idx, "VisualItems", j);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                    packet.ReadByte("Subclass", idx, "VisualItems", j);
            }

            packet.ReadTime("LastPlayedTime", idx);

            packet.ReadInt16("SpecID", idx);
            packet.ReadInt32("SaveVersion", idx);
            packet.ReadInt32("InterfaceVersion", idx);
            packet.ReadUInt32("RestrictionFlags", idx);
            var mailSenderLengths = new uint[packet.ReadUInt32()];

            packet.ResetBitReader();

            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("CantLoginReason", 5, idx);

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                mailSenderLengths[j] = packet.ReadBits(6);

            for (var j = 0; j < mailSenderLengths.Length; ++j)
                if (mailSenderLengths[j] > 1)
                    packet.ReadDynamicString("MailSender", mailSenderLengths[j], idx);

            packet.ReadWoWString("Character Name", nameLength, idx);

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = (uint)mapId, Zone = (uint)zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }
        }

        public static void ReadUnlockedConditionalAppearance(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("AchievementId", indexes);
            packet.ReadUInt32("Unused", indexes);
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleEnumCharactersResult(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsTestDemonHunterCreationAllowed");
            packet.ReadBit("HasDemonHunterOnRealm");
            packet.ReadBit("IsDemonHunterCreationAllowed");

            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");
            packet.ReadBit("IsAlliedRacesCreationAllowed");

            var charsCount = packet.ReadUInt32("CharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");

            uint unlockedConditionalAppearanceCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                unlockedConditionalAppearanceCount = packet.ReadUInt32("UnlockedConditionalAppearanceCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
            {
                for (uint i = 0; i < unlockedConditionalAppearanceCount; ++i)
                    ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearance", i);
            }

            for (uint i = 0; i < charsCount; ++i)
                ReadCharactersData(packet, i, "CharactersData");

            for (var i = 0; i < raceUnlockCount; ++i)
                V7_0_3_22248.Parsers.CharacterHandler.ReadRaceUnlockData(packet, i, "RaceUnlockData");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT, ClientVersionBuild.V8_0_1_27101, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleInspectResult(Packet packet)
        {
            packet.ReadPackedGuid128("InspecteeGUID");

            var itemCount = packet.ReadUInt32("ItemsCount");
            var glyphCount = packet.ReadUInt32("GlyphsCount");
            var talentCount = packet.ReadUInt32("TalentsCount");
            var pvpTalentCount = packet.ReadUInt32("PvpTalentsCount");
            packet.ReadInt32E<Class>("ClassID");
            packet.ReadInt32("SpecializationID");
            packet.ReadInt32E<Gender>("Gender");

            for (int i = 0; i < glyphCount; i++)
                packet.ReadUInt16("Glyphs", i);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talents", i);

            for (int i = 0; i < pvpTalentCount; i++)
                packet.ReadUInt16("PvpTalents", i);

            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");
            packet.ResetBitReader();

            for (int i = 0; i < itemCount; i++)
                ReadInspectItemData(packet, "Item", i);

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleInspectResult815(Packet packet)
        {
            uint itemCount = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
            {
                ReadPlayerModelDisplayInfo(packet, "DisplayInfo");
            }
            else
            {
                packet.ReadPackedGuid128("InspecteeGUID");
                itemCount = packet.ReadUInt32("ItemsCount");
            }
            var glyphCount = packet.ReadUInt32("GlyphsCount");
            var talentCount = packet.ReadUInt32("TalentsCount");
            var pvpTalentCount = packet.ReadUInt32("PvpTalentsCount");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
            {
                packet.ReadInt32("ItemLevel");
            }
            else
            {
                packet.ReadInt32E<Class>("ClassID");
                packet.ReadInt32("SpecializationID");
                packet.ReadInt32E<Gender>("Gender");
            }
            packet.ReadByte("LifetimeMaxRank");
            packet.ReadUInt16("TodayHK");
            packet.ReadUInt16("YesterdayHK");
            packet.ReadUInt32("LifetimeHK");
            packet.ReadUInt32("HonorLevel");

            for (int i = 0; i < glyphCount; i++)
                packet.ReadUInt16("Glyphs", i);

            for (int i = 0; i < talentCount; i++)
                packet.ReadUInt16("Talents", i);

            for (int i = 0; i < pvpTalentCount; i++)
                packet.ReadUInt16("PvpTalents", i);

            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");
            packet.ResetBitReader();

            for (int i = 0; i < 6; i++)
            {
                packet.ReadByte("Bracket", i);
                packet.ReadInt32("Rating", i);
                packet.ReadInt32("Rank", i);
                packet.ReadInt32("WeeklyPlayed", i);
                packet.ReadInt32("WeeklyWon", i);
                packet.ReadInt32("SeasonPlayed", i);
                packet.ReadInt32("SeasonWon", i);
                packet.ReadInt32("WeeklyBestRating", i);
                packet.ReadInt32("SeasonBestRating", i);
                packet.ReadInt32("PvpTierID", i);
                packet.ResetBitReader();
                packet.ReadBit("Disqualified", i);
            }

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");

            for (int i = 0; i < itemCount; i++)
                ReadInspectItemData(packet, i);
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            var nameLen = packet.ReadBits(6);
            var hasTemplateSet = packet.ReadBit();

            packet.ReadBit("IsTrialBoost");
            packet.ReadByteE<Race>("RaceID");
            packet.ReadByteE<Class>("ClassID");
            packet.ReadByteE<Gender>("SexID");
            packet.ReadByte("SkinID");
            packet.ReadByte("FaceID");
            packet.ReadByte("HairStyleID");
            packet.ReadByte("HairColorID");
            packet.ReadByte("FacialHairStyleID");
            packet.ReadByte("OutfitID");

            for (var i = 0; i < 3; i++)
                packet.ReadByte("CustomDisplay", i);

            packet.ReadWoWString("Name", nameLen);

            if (hasTemplateSet)
                packet.ReadInt32("TemplateSetID");
        }

        [Parser(Opcode.SMSG_CREATE_CHAR)]
        public static void HandleCharResponse(Packet packet)
        {
            packet.ReadByteE<ResponseCode>("Response");
            packet.ReadPackedGuid128("GUID");
        }

        public static void ReadPVPBracketData(Packet packet, params object[] idx)
        {
            packet.ReadByte("Bracket", idx);
            packet.ReadInt32("Rating", idx);
            packet.ReadInt32("Rank", idx);
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("WeeklyBestRating", idx);
            packet.ReadInt32("SeasonBestRating", idx);
            packet.ReadInt32("PvpTierID", idx);
            packet.ReadBit("Disqualified", idx);
        }

        [Parser(Opcode.SMSG_INSPECT_PVP)]
        public static void HandleInspectPVP(Packet packet)
        {
            packet.ReadPackedGuid128("ClientGUID");

            var bracketCount = packet.ReadBits(3);
            for (var i = 0; i < bracketCount; i++)
                ReadPVPBracketData(packet, i, "PVPBracketData");
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUpInfo(Packet packet)
        {
            packet.ReadInt32("Level");
            packet.ReadInt32("HealthDelta");

            for (var i = 0; i < 6; i++)
                packet.ReadInt32("PowerDelta", (PowerType)i);

            for (var i = 0; i < 4; i++)
                packet.ReadInt32("StatDelta", (StatType)i);

            packet.ReadInt32("NumNewTalents");
            packet.ReadInt32("NumNewPvpTalentSlots");
        }

        [Parser(Opcode.SMSG_PLAYER_AZERITE_ITEM_GAINS)]
        public static void HandlePlayerAzeriteItemGains(Packet packet)
        {
            packet.ReadPackedGuid128("Item");
            packet.ReadUInt64("AzeriteXPGained");
        }

        [Parser(Opcode.SMSG_PLAYER_AZERITE_ITEM_EQUIPPED_STATUS_CHANGED)]
        public static void HandlePlayerAzeriteItemEquippedStatusChanged(Packet packet)
        {
            packet.ReadBit("IsHeartEquipped");
        }
    }
}
