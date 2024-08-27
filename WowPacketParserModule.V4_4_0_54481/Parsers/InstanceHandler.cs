using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_BOSS_KILL)]
        public static void HandleBossKill(Packet packet)
        {
            packet.ReadUInt32("DungeonEncounterID");
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
                packet.ReadBit("DisplayOnlyDispellableDebuffs", i);
                packet.ReadBit("DisplayPowerBar", i);
                packet.ReadBit("DisplayBorder", i);
                packet.ReadBit("UseClassColors", i);
                packet.ReadBit("DisplayNonBossDebuffs", i);
                packet.ReadBit("HorizontalGroups", i);

                packet.ReadBit("DynamicPosition", i);
                packet.ReadBit("Locked", i);
                packet.ReadBit("Shown", i);
                packet.ReadBit("AutoActivate2Players", i);
                packet.ReadBit("AutoActivate3Players", i);
                packet.ReadBit("AutoActivate5Players", i);
                packet.ReadBit("AutoActivate10Players", i);
                packet.ReadBit("AutoActivate15Players", i);

                packet.ReadBit("AutoActivate20Players", i);
                packet.ReadBit("AutoActivate40Players", i);
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

        [Parser(Opcode.SMSG_INSTANCE_INFO)]
        public static void HandleInstanceInfo(Packet packet)
        {
            var int16 = packet.ReadInt32("LocksCount");
            for (int i = 0; i < int16; i++)
            {
                packet.ReadInt32<MapId>("MapID", i);
                packet.ReadInt32<DifficultyId>("DifficultyID", i);
                packet.ReadInt64("InstanceID", i);
                packet.ReadInt32("TimeRemaining", i);
                packet.ReadInt32("Completed_mask", i);

                packet.ResetBitReader();
                packet.ReadBit("Locked", i);
                packet.ReadBit("Extended", i);
            }
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_CHANGE_PRIORITY)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_ENGAGE_UNIT)]
        public static void HandleInstanceEncounterChangePriority(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadByte("TargetFramePriority");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_DISENGAGE_UNIT)]
        public static void HandleInstanceEncounterDisengageUnit(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_GAIN_COMBAT_RESURRECTION_CHARGE)]
        public static void HandleInstanceEncounterGainCombatResurrectionCharge(Packet packet)
        {
            packet.ReadInt32("InCombatResCount");
            packet.ReadUInt32("CombatResChargeRecovery");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_COMPLETE)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_START)]
        public static void HandleInstanceEncounterObjectiveComplete(Packet packet)
        {
            packet.ReadInt32("ObjectiveID");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_OBJECTIVE_UPDATE)]
        public static void HandleInstanceEncounterObjectiveUpdate(Packet packet)
        {
            packet.ReadInt32("ObjectiveID");
            packet.ReadInt32("ProgressAmount");
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

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_TIMER_START)]
        public static void HandleInstanceEncounterTimerStart(Packet packet)
        {
            packet.ReadInt32("TimeRemaining");
        }

        [Parser(Opcode.SMSG_INSTANCE_RESET)]
        public static void HandleInstanceReset(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
        }

        [Parser(Opcode.SMSG_INSTANCE_RESET_FAILED)]
        public static void HandleInstanceResetFailed(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
            packet.ReadBits("ResetFailedReason", 2);
        }

        [Parser(Opcode.SMSG_INSTANCE_SAVE_CREATED)]
        public static void HandleInstanceSaveCreated(Packet packet)
        {
            packet.ReadBit("Gm");
        }

        [Parser(Opcode.SMSG_PENDING_RAID_LOCK)]
        public static void HandlePendingRaidLock(Packet packet)
        {
            packet.ReadInt32("TimeUntilLock");
            packet.ReadUInt32("CompletedMask");
            packet.ReadBit("Extending");
            packet.ReadBit("WarningOnly");
        }

        [Parser(Opcode.SMSG_RAID_DIFFICULTY_SET)]
        public static void HandleSetRaidDifficulty(Packet packet)
        {
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadByte("Legacy");
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            packet.ReadByte("Type");

            packet.ReadUInt32<MapId>("MapID");
            packet.ReadUInt32("DifficultyID");

            packet.ResetBitReader();
            packet.ReadBit("Locked");
            packet.ReadBit("Extended");
        }

        [Parser(Opcode.SMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.ReadInt32<DifficultyId>("DifficultyID");
        }

        [Parser(Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP)]
        public static void HandleUpdateInstanceOwnership(Packet packet)
        {
            packet.ReadInt32("IOwnInstance");
        }

        [Parser(Opcode.SMSG_UPDATE_LAST_INSTANCE)]
        public static void HandleUpdateLastInstance(Packet packet)
        {
            packet.ReadInt32<MapId>("MapID");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_END)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_IN_COMBAT_RESURRECTION)]
        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_PHASE_SHIFT_CHANGED)]
        [Parser(Opcode.SMSG_RESET_FAILED_NOTIFY)]
        public static void HandleInstanceZero(Packet packet)
        {
        }
    }
}
