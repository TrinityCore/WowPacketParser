using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_UPDATE_INSTANCE_ENCOUNTER_UNIT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleUpdateInstanceEncounterUnit(Packet packet)
        {
            // Note: Enum values changed after 3.3.5a
            var type = packet.Translator.ReadUInt32E<EncounterFrame>("Type");
            switch (type)
            {
                case EncounterFrame.Engage:
                case EncounterFrame.Disengage:
                case EncounterFrame.UpdatePriority:
                    packet.Translator.ReadPackedGuid("GUID");
                    packet.Translator.ReadByte("Param 1");
                    break;
                case EncounterFrame.AddTimer:
                case EncounterFrame.EnableObjective:
                case EncounterFrame.DisableObjective:
                    packet.Translator.ReadByte("Param 1");
                    break;
                case EncounterFrame.UpdateObjective:
                    packet.Translator.ReadByte("Param 1");
                    packet.Translator.ReadByte("Param 2");
                    break;
            }
        }

        [Parser(Opcode.SMSG_UPDATE_INSTANCE_ENCOUNTER_UNIT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleUpdateInstanceEncounterUnit434(Packet packet)
        {
            var type = packet.Translator.ReadUInt32E<EncounterFrame434>("Type");
            switch (type)
            {
                case EncounterFrame434.Engage:
                case EncounterFrame434.Disengage:
                case EncounterFrame434.UpdatePriority:
                    packet.Translator.ReadPackedGuid("GUID");
                    packet.Translator.ReadByte("Param 1");
                    break;
                case EncounterFrame434.SetCombatResLimit:
                case EncounterFrame434.AddTimer:
                case EncounterFrame434.EnableObjective:
                case EncounterFrame434.DisableObjective:
                    packet.Translator.ReadByte("Param 1");
                    break;
                case EncounterFrame434.UpdateObjective:
                    packet.Translator.ReadByte("Param 1");
                    packet.Translator.ReadByte("Param 2");
                    break;
            }
        }

        [Parser(Opcode.MSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.MSG_SET_RAID_DIFFICULTY)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
            if (packet.Direction != Direction.ServerToClient)
                return;

            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadInt32("In Group");
        }

        [Parser(Opcode.SMSG_INSTANCE_DIFFICULTY)]
        public static void HandleInstanceDifficulty(Packet packet)
        {
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing)
                && ClientVersion.RemovedInVersion(ClientType.Cataclysm))
                packet.Translator.ReadInt32("Player Difficulty");
        }

        [Parser(Opcode.SMSG_CHANGE_PLAYER_DIFFICULTY_RESULT)]
        public static void HandlePlayerChangeDifficulty(Packet packet)
        {
            var type = packet.Translator.ReadInt32E<DifficultyChangeType>("Change Type");
            switch (type)
            {
                case DifficultyChangeType.PlayerDifficulty1:
                    packet.Translator.ReadByte("Player Difficulty");
                    break;
                case DifficultyChangeType.SpellDuration:
                    packet.Translator.ReadInt32("Spell Duration");
                    break;
                case DifficultyChangeType.Time:
                    packet.Translator.ReadInt32("Time");
                    break;
                case DifficultyChangeType.MapDifficulty:
                    packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
                    break;
            }
        }

        [Parser(Opcode.CMSG_CHANGEPLAYER_DIFFICULTY)]
        public static void HandleChangePlayerDifficulty434(Packet packet)
        {
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
        }

        [Parser(Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE)]
        public static void HandlePlayerDifficultyChange434(Packet packet)
        {
            var type = packet.Translator.ReadInt32E<DifficultyChangeType434>("Change Type");
            switch (type)
            {
                case DifficultyChangeType434.Cooldown:
                    packet.Translator.ReadInt32("Cooldown");
                    break;
                case DifficultyChangeType434.Time:
                    packet.Translator.ReadInt32("Time");
                    break;
                case DifficultyChangeType434.MapDifficultyRequirement:
                    packet.Translator.ReadInt32("Map Difficulty Id");
                    break;
                case DifficultyChangeType434.PlayerAlreadyLocked:
                    packet.Translator.ReadPackedGuid("Guid");
                    break;
                case DifficultyChangeType434.DifficultyChanged:
                    packet.Translator.ReadInt32("Map");
                    packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
                    break;
            }
        }

        [Parser(Opcode.SMSG_UPDATE_LAST_INSTANCE)]
        [Parser(Opcode.SMSG_RESET_FAILED_NOTIFY)]
        public static void HandleResetFailedNotify(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("MapID");
        }

        [Parser(Opcode.MSG_RAID_TARGET_UPDATE)]
        public static void HandleRaidTargetUpdate(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var icon = packet.Translator.ReadSByteE<TargetIcon>("Icon Id");
                if (icon != TargetIcon.None)
                    packet.Translator.ReadGuid("Target GUID");

                return;
            }

            var test = packet.Translator.ReadBool("List target"); // false == Set Target
            if (!test)
                packet.Translator.ReadGuid("Owner GUID");

            for (int i = 0; packet.CanRead(); ++i)
            {
                packet.Translator.ReadByteE<TargetIcon>("Icon Id", i);
                packet.Translator.ReadGuid("Target Guid", i);
            }
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            var type = packet.Translator.ReadInt32E<RaidInstanceResetWarning>("Warning Type");
            packet.Translator.ReadInt32<MapId>("Map Id");
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
            packet.Translator.ReadInt32("Reset time");
            if (type == RaidInstanceResetWarning.Welcome)
            {
                packet.Translator.ReadBool("Locked");
                packet.Translator.ReadBool("Extended");
            }
        }

        [Parser(Opcode.CMSG_SET_SAVED_INSTANCE_EXTEND)]
        public static void HandleSetSavedInstanceExtend(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map Id");
            packet.Translator.ReadInt32E<MapDifficulty>("Difficulty");
            packet.Translator.ReadBool("Extended");
        }

        [Parser(Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP)]
        [Parser(Opcode.SMSG_INSTANCE_SAVE_CREATED)]
        public static void HandleUpdateInstanceOwnership(Packet packet)
        {
            packet.Translator.ReadInt32("Unk");
        }

        [Parser(Opcode.SMSG_INSTANCE_RESET)]
        public static void HandleUpdateInstanceReset(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map Id");
        }

        [Parser(Opcode.CMSG_INSTANCE_LOCK_WARNING_RESPONSE)]
        [Parser(Opcode.CMSG_INSTANCE_LOCK_RESPONSE)]
        public static void HandleInstanceLockResponse(Packet packet)
        {
            packet.Translator.ReadBool("Accept");
        }

        [Parser(Opcode.SMSG_INSTANCE_LOCK_WARNING_QUERY)]
        public static void HandleInstanceLockWarningQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Time");
            packet.Translator.ReadInt32("Encounters Completed Mask");
            packet.Translator.ReadBool("Extending");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623)) // guessing
                packet.Translator.ReadBool("Locked warning"); // Displays a window asking if the player choose to join an instance which is saved.
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_INFO)]
        public static void HandleRaidInstanceInfo(Packet packet)
        {
            var counter = packet.Translator.ReadInt32("Counter");
            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadInt32<MapId>("Map ID", i);
                packet.Translator.ReadUInt32E<MapDifficulty>("Map Difficulty", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.Translator.ReadUInt32("Heroic", i);
                packet.Translator.ReadGuid("Instance GUID", i);
                packet.Translator.ReadBool("Expired", i);
                packet.Translator.ReadBool("Extended", i);
                packet.Translator.ReadUInt32("Reset Time", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.Translator.ReadUInt32("Completed Encounters Mask", i);
            }
        }

        [Parser(Opcode.CMSG_SAVE_CUF_PROFILES)] // 4.3.4
        public static void HandleSaveCufProfiles(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 20);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Talent spec 2", i);
                packet.Translator.ReadBit("10 player group", i);
                packet.Translator.ReadBit("Unk 157", i);
                packet.Translator.ReadBit("Incoming heals", i);
                packet.Translator.ReadBit("Talent spec 1", i);
                packet.Translator.ReadBit("PvP", i);
                packet.Translator.ReadBit("Power bars", i);
                packet.Translator.ReadBit("15 player group", i);
                packet.Translator.ReadBit("40 player group", i);
                packet.Translator.ReadBit("Pets", i);
                packet.Translator.ReadBit("5 player group", i);
                packet.Translator.ReadBit("Dispellable debuffs", i);
                packet.Translator.ReadBit("2 player group", i);
                packet.Translator.ReadBit("Unk 156", i);
                packet.Translator.ReadBit("Debuffs", i);
                packet.Translator.ReadBit("Main tank and assist", i);
                packet.Translator.ReadBit("Aggro highlight", i);
                packet.Translator.ReadBit("3 player group", i);
                packet.Translator.ReadBit("Border", i);
                packet.Translator.ReadBit("Class colors", i);
                packet.Translator.ReadBit("Unk 145", i);
                strlen[i] = packet.Translator.ReadBits("String length", 8, i);
                packet.Translator.ReadBit("PvE", i);
                packet.Translator.ReadBit("Horizontal Groups", i);
                packet.Translator.ReadBit("25 player group", i);
                packet.Translator.ReadBit("Keep groups together", i);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("Unk 146", i);
                packet.Translator.ReadWoWString("Name", (int)strlen[i], i);
                packet.Translator.ReadInt16("Unk 152", i);
                packet.Translator.ReadInt16("Frame height", i);
                packet.Translator.ReadInt16("Frame width", i);
                packet.Translator.ReadInt16("Unk 150", i);
                packet.Translator.ReadByte("Health text", i);
                packet.Translator.ReadByte("Unk 147", i);
                packet.Translator.ReadByte("Sort by", i);
                packet.Translator.ReadInt16("Unk 154", i);
                packet.Translator.ReadByte("Unk 148", i);
            }
        }

        [Parser(Opcode.SMSG_LOAD_CUF_PROFILES)] // 4.3.4
        public static void HandleLoadCUFProfiles(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 20);

            var strlen = new uint[count];

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadBit("Unk 157", i);
                packet.Translator.ReadBit("10 player group", i);
                packet.Translator.ReadBit("5 player group", i);
                packet.Translator.ReadBit("25 player group", i);
                packet.Translator.ReadBit("Incoming heals", i);
                packet.Translator.ReadBit("PvE", i);
                packet.Translator.ReadBit("Horizontal groups", i);
                packet.Translator.ReadBit("40 player group", i);
                packet.Translator.ReadBit("3 player group", i);
                packet.Translator.ReadBit("Aggro highlight", i);
                packet.Translator.ReadBit("Border", i);
                packet.Translator.ReadBit("2 player group", i);
                packet.Translator.ReadBit("Debuffs", i);
                packet.Translator.ReadBit("Main tank and assist", i);
                packet.Translator.ReadBit("Unk 156", i);
                packet.Translator.ReadBit("Talent spec 2", i);
                packet.Translator.ReadBit("Class colors", i);
                packet.Translator.ReadBit("Display power bars", i);
                packet.Translator.ReadBit("Talent spec 1", i);
                strlen[i] = packet.Translator.ReadBits("String length", 8, i);
                packet.Translator.ReadBit("Dispellable debuffs", i);
                packet.Translator.ReadBit("Keep groups together", i);
                packet.Translator.ReadBit("Unk 145", i);
                packet.Translator.ReadBit("15 player group", i);
                packet.Translator.ReadBit("Pets", i);
                packet.Translator.ReadBit("PvP", i);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt16("Unk 154", i);
                packet.Translator.ReadInt16("Frame height", i);
                packet.Translator.ReadInt16("Unk 152", i);
                packet.Translator.ReadByte("Unk 147", i);
                packet.Translator.ReadInt16("Unk 150", i);
                packet.Translator.ReadByte("Unk 146", i);
                packet.Translator.ReadByte("Health text", i); // 0 - none, 1 - remaining, 2 - lost, 3 - percentage
                packet.Translator.ReadByte("Sort by", i); // 0 - role, 1 - group, 2 - alphabetical
                packet.Translator.ReadInt16("Frame width", i);
                packet.Translator.ReadByte("Unk 148", i);
                packet.Translator.ReadWoWString("Name", (int)strlen[i], i);
            }
        }

        [Parser(Opcode.CMSG_RESET_INSTANCES)]
        [Parser(Opcode.SMSG_UPDATE_DUNGEON_ENCOUNTER_FOR_LOOT)]
        public static void HandleInstanceNull(Packet packet)
        {
        }

    }
}
