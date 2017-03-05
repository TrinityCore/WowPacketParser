using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class InstanceHandler
    {

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_END)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_IN_COMBAT_RESURRECTION)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_PHASE_SHIFT_CHANGED)]
        [Parser(Opcode.SMSG_RESET_FAILED_NOTIFY)]
        public static void HandleInstanceZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BOSS_KILL_CREDIT)]
        public static void HandleBossKillCredit(Packet packet)
        {
            packet.Translator.ReadUInt32("EncounterID");
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)]
        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)]
        public static void HandleCUFProfiles(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");

            for (int i = 0; i < count; ++i)
            {
                var strlen = packet.Translator.ReadBits(7);

                packet.Translator.ReadBit("KeepGroupsTogether", i);
                packet.Translator.ReadBit("DisplayPets", i);
                packet.Translator.ReadBit("DisplayMainTankAndAssist", i);
                packet.Translator.ReadBit("DisplayHealPrediction", i);
                packet.Translator.ReadBit("DisplayAggroHighlight", i);
                packet.Translator.ReadBit("DisplayOnlyDispellableDebuffs", i);
                packet.Translator.ReadBit("DisplayPowerBar", i);
                packet.Translator.ReadBit("DisplayBorder", i);
                packet.Translator.ReadBit("UseClassColors", i);
                packet.Translator.ReadBit("HorizontalGroups", i);
                packet.Translator.ReadBit("DisplayNonBossDebuffs", i);
                packet.Translator.ReadBit("DynamicPosition", i);
                packet.Translator.ReadBit("Locked", i);
                packet.Translator.ReadBit("Shown", i);
                packet.Translator.ReadBit("AutoActivate2Players", i);
                packet.Translator.ReadBit("AutoActivate3Players", i);
                packet.Translator.ReadBit("AutoActivate5Players", i);
                packet.Translator.ReadBit("AutoActivate10Players", i);
                packet.Translator.ReadBit("AutoActivate15Players", i);
                packet.Translator.ReadBit("AutoActivate25Players", i);
                packet.Translator.ReadBit("AutoActivate40Players", i);
                packet.Translator.ReadBit("AutoActivateSpec1", i);
                packet.Translator.ReadBit("AutoActivateSpec2", i);
                packet.Translator.ReadBit("AutoActivatePvP", i);
                packet.Translator.ReadBit("AutoActivatePvE", i);

                packet.Translator.ReadInt16("FrameHeight", i);
                packet.Translator.ReadInt16("FrameWidth", i);

                packet.Translator.ReadByte("SortBy", i);
                packet.Translator.ReadByte("HealthText", i);
                packet.Translator.ReadByte("TopPoint", i);
                packet.Translator.ReadByte("BottomPoint", i);
                packet.Translator.ReadByte("LeftPoint", i);

                packet.Translator.ReadInt16("TopOffset", i);
                packet.Translator.ReadInt16("BottomOffset", i);
                packet.Translator.ReadInt16("LeftOffset", i);

                packet.Translator.ReadWoWString("Name", strlen, i);
            }
        }

        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.SMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.Translator.ReadInt32("DifficultyID");
        }

        [Parser(Opcode.CMSG_SET_RAID_DIFFICULTY)]
        [Parser(Opcode.SMSG_RAID_DIFFICULTY_SET)]
        public static void HandleSetRaidDifficulty(Packet packet)
        {
            packet.Translator.ReadInt32("DifficultyID");
            packet.Translator.ReadByte("Force");
        }

        [Parser(Opcode.SMSG_INSTANCE_INFO)]
        public static void HandleInstanceInfo(Packet packet)
        {
            var int16 = packet.Translator.ReadInt32("LocksCount");
            for (int i = 0; i < int16; i++)
            {
                packet.Translator.ReadInt32("MapID", i);
                packet.Translator.ReadInt32("DifficultyID", i);
                packet.Translator.ReadInt64("InstanceID", i);
                packet.Translator.ReadInt32("TimeRemaining", i);
                packet.Translator.ReadInt32("Completed_mask", i);

                packet.Translator.ResetBitReader();
                packet.Translator.ReadBit("Locked", i);
                packet.Translator.ReadBit("Extended", i);
            }
        }

        [Parser(Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND)]
        public static void HandleSetSavedInstanceExtend(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("MapID");
            packet.Translator.ReadInt32("DifficultyID");
            packet.Translator.ReadBit("Extended");
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            packet.Translator.ReadByte("Type");

            packet.Translator.ReadUInt32<MapId>("MapID");
            packet.Translator.ReadUInt32("DifficultyID");
            packet.Translator.ReadInt32("TimeLeft");

            packet.Translator.ReadBit("Locked");
            packet.Translator.ReadBit("Extended");
        }

        [Parser(Opcode.SMSG_INSTANCE_SAVE_CREATED)]
        public static void HandleInstanceSaveCreated(Packet packet)
        {
            packet.Translator.ReadBit("Gm");
        }

        [Parser(Opcode.SMSG_CHANGE_PLAYER_DIFFICULTY_RESULT)]
        public static void HandlePlayerChangeDifficulty(Packet packet)
        {
            var type = packet.Translator.ReadBits("Result", 4);
            switch (type)
            {
                case 5:
                case 8:
                    packet.Translator.ReadBit("Cooldown");
                    packet.Translator.ReadUInt32("CooldownReason");
                    break;
                case 11:
                    packet.Translator.ReadUInt32("InstanceDifficultyID");
                    packet.Translator.ReadUInt32("DifficultyRecID");
                    break;
                case 2:
                    packet.Translator.ReadUInt32("MapID");
                    break;
                case 4:
                    packet.Translator.ReadPackedGuid128("Guid");
                    break;
            }
        }

        [Parser(Opcode.SMSG_INSTANCE_GROUP_SIZE_CHANGED)]
        public static void HandleInstanceGroupSizeChanged(Packet packet)
        {
            packet.Translator.ReadUInt32("GroupSize");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_START)]
        public static void HandleInstanceEncounterStart(Packet packet)
        {
            packet.Translator.ReadInt32("InCombatResCount");
            packet.Translator.ReadInt32("MaxInCombatResCount");
            packet.Translator.ReadInt32("CombatResChargeRecovery");
            packet.Translator.ReadInt32("NextCombatResChargeTime");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_ENGAGE_UNIT)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_CHANGE_PRIORITY)]
        public static void HandleInstanceEncounterEngageUnit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
            packet.Translator.ReadByte("TargetFramePriority");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_DISENGAGE_UNIT)]
        public static void HandleInstanceEncounterDisengageUnit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_GAIN_COMBAT_RESURRECTION_CHARGE)]
        public static void HandleInstanceEncounterGainCombatResurrectionCharge(Packet packet)
        {
            packet.Translator.ReadInt32("InCombatResCount");
            packet.Translator.ReadInt32("CombatResChargeRecovery");
        }

        [Parser(Opcode.SMSG_ENCOUNTER_START)]
        public static void HandleEncounterStart(Packet packet)
        {
            packet.Translator.ReadInt32("EncounterID");
            packet.Translator.ReadInt32("DifficultyID");
            packet.Translator.ReadInt32("GroupSize");
        }

        [Parser(Opcode.SMSG_ENCOUNTER_END)]
        public static void HandleEncounterStop(Packet packet)
        {
            packet.Translator.ReadInt32("EncounterID");
            packet.Translator.ReadInt32("DifficultyID");
            packet.Translator.ReadInt32("GroupSize");
            packet.Translator.ReadBit("Success");
        }

        [Parser(Opcode.SMSG_INSTANCE_RESET_FAILED)]
        public static void HandleInstanceResetFailed(Packet packet)
        {
            packet.Translator.ReadInt32("MapID");
            packet.Translator.ReadBits("ResetFailedReason", 2);
        }

        [Parser(Opcode.SMSG_PENDING_RAID_LOCK)]
        public static void HandlePendingRaidLock(Packet packet)
        {
            packet.Translator.ReadInt32("TimeUntilLock");
            packet.Translator.ReadUInt32("CompletedMask");
            packet.Translator.ReadBit("Extending");
            packet.Translator.ReadBit("WarningOnly");
        }
    }
}
