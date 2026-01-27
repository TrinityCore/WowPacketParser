using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            packet.ReadByte("Type");

            packet.ReadUInt32<MapId>("MapID");
            packet.ReadUInt32<DifficultyId>("DifficultyID");

            packet.ResetBitReader();
            packet.ReadBit("Locked");
            packet.ReadBit("Extended");
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleCUFProfiles(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                packet.ResetBitReader();

                var strlen = packet.ReadBits(7);

                packet.ReadBit("KeepGroupsTogether", i);
                packet.ReadBit("DisplayPets", i);
                packet.ReadBit("DisplayMainTankAndAssist", i);
                packet.ReadBit("DisplayHealPrediction", i);
                packet.ReadBit("DisplayAggroHighlight", i);
                packet.ReadBit("DisplayOnlyDispellableDebuffs", i);
                packet.ReadBit("DisplayPowerBar", i);
                packet.ReadBit("DisplayBorder", i);
                packet.ReadBit("UseClassColors", i);
                packet.ReadBit("HorizontalGroups", i);
                packet.ReadBit("DisplayNonBossDebuffs", i);
                packet.ReadBit("DynamicPosition", i);
                packet.ReadBit("Locked", i);
                packet.ReadBit("Shown", i);
                packet.ReadBit("AutoActivate2Players", i);
                packet.ReadBit("AutoActivate3Players", i);
                packet.ReadBit("AutoActivate5Players", i);
                packet.ReadBit("AutoActivate10Players", i);
                packet.ReadBit("AutoActivate15Players", i);
                packet.ReadBit("AutoActivate25Players", i);
                packet.ReadBit("AutoActivate40Players", i);
                packet.ReadBit("AutoActivateSpec1", i);
                packet.ReadBit("AutoActivateSpec2", i);
                packet.ReadBit("AutoActivateSpec3", i);
                packet.ReadBit("AutoActivateSpec4", i);
                packet.ReadBit("AutoActivatePvP", i);
                packet.ReadBit("AutoActivatePvE", i);

                packet.ReadInt16("FrameHeight", i);
                packet.ReadInt16("FrameWidth", i);

                packet.ReadByte("SortBy", i);
                packet.ReadByte("HealthText", i);
                packet.ReadByte("TopPoint", i);
                packet.ReadByte("BottomPoint", i);
                packet.ReadByte("LeftPoint", i);

                packet.ReadInt16("TopOffset", i);
                packet.ReadInt16("BottomOffset", i);
                packet.ReadInt16("LeftOffset", i);

                packet.ReadWoWString("Name", strlen, i);
            }
        }

        public static void ReadEncounterItemInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ItemID", idx);
            packet.ReadInt32("ItemLevel", idx);

            var enchantmentCount = packet.ReadUInt32("PermanentEnchantmentID", idx);
            var bonusListCount = packet.ReadUInt32("TemporaryEnchantmentID", idx);
            var gemCount = packet.ReadUInt32("GemCount", idx);

            for (var j = 0; j < enchantmentCount; ++j)
                packet.ReadInt32("EnchantmentID", idx, j);

            for (var j = 0; j < bonusListCount; ++j)
                packet.ReadInt32("ItemBonusListID", idx, j);

            for (var j = 0; j < gemCount; ++j)
                ReadEncounterItemInfo(packet, idx, "Gem", j);
        }

        static readonly string[] FilteredRatingList =
        {
            "Dodge",
            "Parry",
            "Block",
            "CritMelee",
            "CritRanged",
            "CritSpell",
            "Speed",
            "Lifesteal",
            "HasteMelee",
            "HasteRanged",
            "HasteSpell",
            "Avoidance",
            "Mastery",
            "VersatilityDamageDone",
            "VersatilityHealingDone",
            "VersatilityDamageTaken",
            "Armor"
        };

        [Parser(Opcode.SMSG_ENCOUNTER_START)]
        public static void HandleEncounterStart(Packet packet)
        {
            packet.ReadInt32("EncounterID");
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadInt32("GroupSize");

            var playerCount = packet.ReadUInt32("Players");
            for (var i = 0; i < playerCount; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);

                var statCount = packet.ReadUInt32("StatCount", i);
                var combatRatingCount = packet.ReadUInt32("CombatRatingCount", i);
                var auraCount = packet.ReadUInt32("AuraCount", i);

                packet.ReadInt32("SpecID", i);

                var talentCount = packet.ReadUInt32("TalentCount", i);
                var pvpTalentCount = packet.ReadUInt32("PvPTalentCount", i);

                for (var j = 0; j < talentCount; ++j)
                    packet.ReadInt32("TalentSpellID", i, j);

                for (var j = 0; j < pvpTalentCount; ++j)
                    packet.ReadInt32("PvPTalentSpellID", i, j);

                var artifactPowerCount = packet.ReadUInt32("ArtifactPowerCount", i);
                var itemCount = packet.ReadUInt32("ItemCount", i);

                for (var j = 0; j < statCount; ++j)
                    packet.ReadInt32(((StatType)j).ToString(), i);

                for (var j = 0; j < combatRatingCount; ++j)
                    packet.ReadInt32(j < FilteredRatingList.Length ? FilteredRatingList[j] : $"[{j}] CombatRatingValue", i);

                for (var j = 0; j < auraCount; ++j)
                {
                    packet.ReadPackedGuid128("CasterGUID", i, j);
                    packet.ReadInt32("SpellID", i, j);
                }

                for (var j = 0; j < artifactPowerCount; ++j)
                {
                    packet.ReadInt32("ArtifactPowerID", i, j);
                    packet.ReadInt16("Rank", i, j);
                }

                for (var j = 0; j < itemCount; ++j)
                    ReadEncounterItemInfo(packet, i, j);
            }
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_START)]
        public static void HandleInstanceEncounterStart(Packet packet)
        {
            packet.ReadInt32("InCombatResCount");
            packet.ReadInt32("MaxInCombatResCount");
            packet.ReadInt32("CombatResChargeRecovery");
            packet.ReadInt32("NextCombatResChargeTime");

            packet.ResetBitReader();
            packet.ReadBit("InProgress");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_UPDATE_SUPPRESS_RELEASE)]
        public static void HandleInstanceEncounterUpdateSuppressRelease(Packet packet)
        {
            packet.ReadBit("ReleaseSuppressed");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_UPDATE_ALLOW_RELEASE_IN_PROGRESS)]
        public static void HandleInstanceEncounterUpdateAllowReleaseInProgress(Packet packet)
        {
            packet.ReadBit("ReleaseAllowed");
        }

        [Parser(Opcode.CMSG_START_CHALLENGE_MODE)]
        public static void HandleStartChallengeMode(Packet packet)
        {
            packet.ReadByte("Bag");
            packet.ReadInt32("Slot");
            packet.ReadPackedGuid128("GobGUID");
        }
    }
}
