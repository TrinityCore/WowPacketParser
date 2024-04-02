using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class InstanceHandler
    {
        public static void ReadAuraInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("CasterGUID", idx);
            packet.ReadUInt32<SpellId>("SpellID", idx);
        }

        public static void ReadItemInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32<ItemId>("ItemID", idx);
            packet.ReadInt32("ItemLevel", idx);
            var enchantmentCount = packet.ReadUInt32();
            var bonusListCount = packet.ReadUInt32();
            var gemCount = packet.ReadUInt32();

            for (var i = 0; i < enchantmentCount; i++)
            {
                packet.ReadInt32("EnchantmentID", idx, i);
            }

            for (var i = 0; i < bonusListCount; i++)
            {
                packet.ReadInt32("BonusListID", idx, i);
            }

            for (var i = 0; i < gemCount; i++)
            {
                ReadItemInfo(packet, idx, "GemInfo", i);
            }
        }

        public static void ReadBFACharacterInfo(Packet packet, params object[] idx)
        {
            var azeritePowerInfoCount = packet.ReadUInt32();
            var azeriteEssenceInfoCount = packet.ReadUInt32();

            for (var i = 0; i < azeritePowerInfoCount; i++)
            {
                packet.ReadInt32("AzeritePowerID", idx, i);
                packet.ReadUInt16("Unk1", idx, i);
                packet.ReadByte("Unk2", idx, i);
            }

            for (var i = 0; i < azeriteEssenceInfoCount; i++)
            {
                packet.ReadInt32("AzeriteEssenceID", idx, i);
                packet.ReadByte("Rank", idx, i);
                packet.ReadByte("Slot", idx, i);
            }
        }

        public static void ReadShadowlandsCharacterInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("SoulbindID", idx);
            packet.ReadInt32("CovenantID", idx);
            var garrTalentCount = packet.ReadUInt32();
            var conduitInfoCount = packet.ReadUInt32();
            var mawPowerCount = packet.ReadUInt32();

            for (var i = 0; i < garrTalentCount; i++)
            {
                packet.ReadInt32("GarrTalentID", idx, i);
            }

            for (var i = 0; i < conduitInfoCount; i++)
            {
                packet.ReadInt32("SoulbindConduitID", idx, i);
                packet.ReadInt32("Rank", idx, i);
            }

            for (var i = 0; i < mawPowerCount; i++)
            {
                packet.ReadInt32<SpellId>("SpellID", idx, i);
                packet.ReadInt32("MawPowerID", idx, i);
                packet.ReadInt32("Stacks", idx, i);
            }
        }

        public static void ReadEncounterStartPlayerInfo(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadPackedGuid128("PlayerGUID", idx);
            packet.ReadByte("Faction", idx);
            var statCount = packet.ReadUInt32();
            var combatRatingCount = packet.ReadUInt32();
            var pvpTalentCount = packet.ReadUInt32();
            var auraInfoCount = packet.ReadUInt32();
            packet.ReadInt32("HonorLevel", idx);
            packet.ReadInt32("Season", idx);
            packet.ReadInt32("Rating", idx);
            packet.ReadInt32("Tier", idx);
            var itemInfoCount = packet.ReadUInt32();

            for (var i = 0; i < statCount; i++)
            {
                packet.ReadInt32(((StatType)i).ToString(), idx, i);
            }

            for (var i = 0; i < combatRatingCount; i++)
            {
                packet.ReadInt32("CombatRating", idx, i);
            }

            for (var i = 0; i < pvpTalentCount; i++)
            {
                packet.ReadInt32<SpellId>("PvpTalentSpellID", idx, i);
            }

            for (var i = 0; i < auraInfoCount; i++)
            {
                ReadAuraInfo(packet, idx, "AuraInfo", i);
            }

            for (var i = 0; i < itemInfoCount; i++)
            {
                ReadItemInfo(packet, idx, "ItemInfo", i);
            }

            TraitHandler.ReadTraitConfig(packet, idx, "TraitConfig");

            packet.ResetBitReader();
            var hasBFACharacterInfo = packet.ReadBit("HasBFACharacterInfo", idx);
            var hasShadowlandsCharacterInfo = packet.ReadBit("HasShadowlandsCharacterInfo", idx);

            if (hasBFACharacterInfo)
                ReadBFACharacterInfo(packet, idx, "BFACharacterInfo");

            if (hasShadowlandsCharacterInfo)
                ReadShadowlandsCharacterInfo(packet, idx, "ShadowlandsCharacterInfo");
        }

        [Parser(Opcode.SMSG_MULTI_FLOOR_NEW_FLOOR)]
        public static void HandleMultiFloorNewFloor(Packet packet)
        {
            packet.ReadInt32("GeneratedFloorID");
            packet.ReadInt32("FloorID");
            packet.ReadInt32("FloorIndex");
            var count = packet.ReadUInt32();

            for (var i = 0; i < count; i++)
                ReadEncounterStartPlayerInfo(packet, "EncounterStartPlayerInfo", i);
        }

        [Parser(Opcode.SMSG_ENCOUNTER_START)]
        public static void HandleEncounterstart(Packet packet)
        {
            packet.ReadInt32("DungeonEncounterID");
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadInt32("GroupSize");
            var count = packet.ReadUInt32();

            for (var i = 0; i < count; i++)
                ReadEncounterStartPlayerInfo(packet, "EncounterStartPlayerInfo", i);
        }
    }
}
