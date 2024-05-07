using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V3_4_0_45166.Parsers.V3_4_3_54261
{
    public static class CharacterHandler
    {
        public static void ReadChrCustomizationChoice(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("ChrCustomizationOptionID", indexes);
            packet.ReadUInt32("ChrCustomizationChoiceID", indexes);
        }

        public static void ReadVisualItemInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("DisplayID", idx);
            packet.ReadUInt32("DisplayEnchantID", idx);
            packet.ReadInt32("SecondaryItemModifiedAppearanceID", idx);
            packet.ReadByteE<InventoryType>("InvType", idx);
            packet.ReadByte("Subclass", idx);
        }

        public static void ReadRaceLimitDisableInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32E<Race>("RaceID", idx);
            packet.ReadInt32("BlockReason", idx);
        }

        public static void ReadCharactersListEntry(Packet packet, params object[] idx)
        {
            var playerGuid = packet.ReadPackedGuid128("Guid", idx);
            packet.ReadUInt64("GuildClubMemberID", idx);
            packet.ReadByte("ListPosition", idx);
            var race = packet.ReadByteE<Race>("RaceID", idx);
            var @class = packet.ReadByteE<Class>("ClassID", idx);
            packet.ReadByteE<Gender>("SexID", idx);
            var customizationCount = packet.ReadUInt32();
            var level = packet.ReadByte("ExperienceLevel", idx);
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

            for (uint j = 0; j < 34; ++j)
                ReadVisualItemInfo(packet, idx, j);

            packet.ReadTime64("LastPlayedTime", idx);
            packet.ReadInt16("SpecID", idx);
            packet.ReadInt32("Unknown703", idx);
            packet.ReadInt32("LastLoginVersion", idx);
            packet.ReadUInt32("Flags4", idx);
            var mailSendersLen = new uint[packet.ReadUInt32()];
            var mailSenderTypesLen = new uint[packet.ReadUInt32()];
            packet.ReadUInt32("OverrideSelectScreenFileDataID", idx);

            for (var j = 0u; j < customizationCount; ++j)
                ReadChrCustomizationChoice(packet, idx, "Customizations", j);

            for (var j = 0; j < mailSenderTypesLen.Length; ++j)
                packet.ReadUInt32("MailSenderType", idx, j);            

            packet.ResetBitReader();

            var nameLength = packet.ReadBits("Character Name Length", 6, idx);
            var firstLogin = packet.ReadBit("FirstLogin", idx);
            packet.ReadBit("BoostInProgress", idx);
            packet.ReadBits("UnkWod61x", 5, idx);
            packet.ReadBits("Unk", 2, idx);
            packet.ReadBit("RpeResetAvailable", idx);
            packet.ReadBit("RpeResetQuestClearAvailable", idx);

            for (var j = 0; j < mailSendersLen.Length; ++j)
                mailSendersLen[j] = packet.ReadBits(6);

            for (var j = 0; j < mailSendersLen.Length; ++j)
                if (mailSendersLen[j] > 1)
                    packet.ReadDynamicString("MailSender", mailSendersLen[j], idx);                    

            var name = packet.ReadWoWString("Character Name", nameLength, idx);   

            if (firstLogin)
            {
                PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = @class, Map = (uint)mapId, Zone = (uint)zone, Position = pos, Orientation = 0 };
                Storage.StartPositions.Add(startPos, packet.TimeSpan);
            }

            var playerInfo = new Player { Race = race, Class = @class, Name = name, FirstLogin = firstLogin, Level = level, Type = ObjectType.Player };
            if (Storage.Objects.ContainsKey(playerGuid))
                Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
            else
                Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
        }

        public static void ReadRaceUnlockData(Packet packet, params object[] idx)
        {
            packet.ReadInt32E<Race>("RaceID", idx);
            packet.ResetBitReader();
            packet.ReadBit("HasExpansion", idx);
            packet.ReadBit("HasAchievement", idx);
            packet.ReadBit("HasHeritageArmor", idx);
            packet.ReadBit("IsLocked", idx);
        }
                
        [BuildMatch(ClientVersionBuild.V3_4_3_54261)]
        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, true)]
        public static void HandleEnumCharactersResult54261(Packet packet)
        {
            packet.ReadBit("Success");
            packet.ReadBit("IsDeletedCharacters");
            packet.ReadBit("IsNewPlayerRestrictionSkipped");
            packet.ReadBit("IsNewPlayerRestricted");
            packet.ReadBit("IsNewPlayer");
            packet.ReadBit("IsTrialAccountRestricted");
            var hasDisabledClassesMask = packet.ReadBit("HasDisabledClassesMask");

            var charsCount = packet.ReadUInt32("CharactersCount");
            packet.ReadInt32("MaxCharacterLevel");
            var raceUnlockCount = packet.ReadUInt32("RaceUnlockCount");
            var unlockedConditionalAppearanceCount = packet.ReadUInt32("UnlockedConditionalAppearanceCount");
            var raceLimitDisablesCount = packet.ReadUInt32("RaceLimitDisablesCount");

            if (hasDisabledClassesMask)
                packet.ReadUInt32("DisabledClassesMask");

            for (var i = 0u; i < unlockedConditionalAppearanceCount; ++i)
                V8_0_1_27101.Parsers.CharacterHandler.ReadUnlockedConditionalAppearance(packet, "UnlockedConditionalAppearances", i);

            for (var i = 0u; i < raceLimitDisablesCount; i++)
                ReadRaceLimitDisableInfo(packet, "RaceLimitDisableInfo", i);

            for (var i = 0u; i < charsCount; ++i)
                ReadCharactersListEntry(packet, i, "Characters");

            for (var i = 0u; i < raceUnlockCount; ++i)
                ReadRaceUnlockData(packet, i, "RaceUnlockData");
        }
    }
}
