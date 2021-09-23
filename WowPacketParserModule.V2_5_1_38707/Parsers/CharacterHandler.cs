using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V2_5_1_38835.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadSingle("FarClip");
            packet.ReadBit("UnkBit");
        }

        public static void ReadAzeriteEssenceData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("Index", idx);
            packet.ReadUInt32("AzeriteEssenceID", idx);
            packet.ReadUInt32("Rank", idx);
            packet.ResetBitReader();
            packet.ReadBit("SlotUnlocked", idx);
        }

        public static void ReadInspectItemData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CreatorGUID", idx);
            packet.ReadByte("Index", idx);

            var azeritePowerCount = packet.ReadUInt32("AzeritePowersCount", idx);
            var azeriteEssenceCount = packet.ReadUInt32();

            for (int i = 0; i < azeritePowerCount; i++)
                packet.ReadInt32("AzeritePowerId", idx, i);

            Substructures.ItemHandler.ReadItemInstance(packet, idx, "ItemInstance");

            packet.ResetBitReader();
            packet.ReadBit("Usable", idx);
            var enchantsCount = packet.ReadBits("EnchantsCount", 4, idx);
            var gemsCount = packet.ReadBits("GemsCount", 2, idx);

            for (int i = 0; i < azeriteEssenceCount; i++)
                ReadAzeriteEssenceData(packet, "AzeriteEssence", i);

            for (int i = 0; i < enchantsCount; i++)
            {
                packet.ReadUInt32("Id", idx, "EnchantData", i);
                packet.ReadByte("Index", idx, "EnchantData", i);
            }

            for (int i = 0; i < gemsCount; i++)
            {
                packet.ReadByte("Slot", idx, "GemData", i);
                Substructures.ItemHandler.ReadItemInstance(packet, idx, "GemData", "ItemInstance", i);
            }
        }

        public static void ReadPlayerModelDisplayInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("InspecteeGUID", idx);
            packet.ReadInt32("SpecializationID", idx);
            var itemCount = packet.ReadUInt32();

            packet.ResetBitReader();
            var nameLen = packet.ReadBits(6);

            packet.ReadByteE<Gender>("GenderID", idx);
            packet.ReadByteE<Race>("Race", idx);
            packet.ReadByteE<Class>("ClassID", idx);
            var customizationCount = packet.ReadUInt32();
            packet.ReadWoWString("Name", nameLen, idx);

            for (var i = 0; i < customizationCount; ++i)
                V9_0_1_36216.Parsers.CharacterHandler.ReadChrCustomizationChoice(packet, idx, "Customizations", i);

            for (int i = 0; i < itemCount; i++)
                ReadInspectItemData(packet, idx, "InspectItemData", i);
        }

        [Parser(Opcode.SMSG_INSPECT_RESULT)]
        public static void HandleInspectResult(Packet packet)
        {
            ReadPlayerModelDisplayInfo(packet, "DisplayInfo");
            var glyphCount = packet.ReadUInt32("GlyphsCount");
            var talentCount = packet.ReadUInt32("TalentsCount");
            packet.ReadInt32("ItemLevel");
            packet.ReadByte("LifetimeMaxRank");
            packet.ReadUInt16("TodayHK");
            packet.ReadUInt16("YesterdayHK");
            packet.ReadUInt32("LifetimeHK");
            packet.ReadUInt32("HonorLevel");

            for (int i = 0; i < glyphCount; i++)
                packet.ReadUInt16("Glyphs", i);

            for (int i = 0; i < talentCount; i++)
                packet.ReadByte("Talents", i);

            packet.ResetBitReader();
            var hasGuildData = packet.ReadBit("HasGuildData");
            var hasAzeriteLevel = packet.ReadBit("HasAzeriteLevel");

            for (int i = 0; i < 6; i++)
            {
                packet.ReadByte("Bracket", i, "PvpData");
                packet.ReadInt32("Rating", i, "PvpData");
                packet.ReadInt32("Rank", i, "PvpData");
                packet.ReadInt32("WeeklyPlayed", i, "PvpData");
                packet.ReadInt32("WeeklyWon", i, "PvpData");
                packet.ReadInt32("SeasonPlayed", i, "PvpData");
                packet.ReadInt32("SeasonWon", i, "PvpData");
                packet.ReadInt32("WeeklyBestRating", i, "PvpData");
                packet.ReadInt32("Unk710", i, "PvpData");
                packet.ReadInt32("Unk801_1", i, "PvpData");
                packet.ReadInt32("Unk252_1", i, "PvpData");
                packet.ResetBitReader();
                packet.ReadBit("Unk801_2", i, "PvpData");
            }

            if (hasGuildData)
            {
                packet.ReadPackedGuid128("GuildGUID");
                packet.ReadInt32("NumGuildMembers");
                packet.ReadInt32("GuildAchievementPoints");
            }
            if (hasAzeriteLevel)
                packet.ReadInt32("AzeriteLevel");
        }
    }
}