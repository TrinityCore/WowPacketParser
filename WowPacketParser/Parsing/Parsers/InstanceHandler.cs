using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_UPDATE_INSTANCE_ENCOUNTER_UNIT)]
        public static void HandleUpdateInstanceEncounterUnit(Packet packet)
        {
            // Note: Enum values changed after 3.3.5a
            var type = packet.ReadEnum<EncounterFrame>("Type", TypeCode.UInt32);
            switch (type)
            {
                case EncounterFrame.Engage:
                case EncounterFrame.Disengage:
                case EncounterFrame.UpdatePriority:
                    packet.ReadPackedGuid("GUID");
                    packet.ReadByte("Param 1");
                    break;
                case EncounterFrame.AddTimer:
                case EncounterFrame.EnableObjective:
                case EncounterFrame.DisableObjective:
                    packet.ReadByte("Param 1");
                    break;
                case EncounterFrame.UpdateObjective:
                    packet.ReadByte("Param 1");
                    packet.ReadByte("Param 2");
                    break;
            }
        }

        [Parser(Opcode.MSG_SET_DUNGEON_DIFFICULTY)]
        [Parser(Opcode.MSG_SET_RAID_DIFFICULTY)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
            if (packet.Direction != Direction.ServerToClient)
                return;

            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("In Group");
        }

        [Parser(Opcode.SMSG_INSTANCE_DIFFICULTY)]
        public static void HandleInstanceDifficulty(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing)) // don't know when this was added, doesn't exist in 2.4.1
                packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);

            packet.ReadInt32("Player Difficulty");
        }

        [Parser(Opcode.SMSG_CHANGEPLAYER_DIFFICULTY_RESULT)]
        public static void HandlePlayerChangeDifficulty(Packet packet)
        {
            var type = packet.ReadEnum<DifficultyChangeType>("Change Type", TypeCode.Int32);
            switch (type)
            {
                case DifficultyChangeType.PlayerDifficulty1:
                    packet.ReadByte("Player Difficulty");
                    break;
                case DifficultyChangeType.SpellDuration:
                    packet.ReadInt32("Spell Duration");
                    break;
                case DifficultyChangeType.Time:
                    packet.ReadInt32("Time");
                    break;
                case DifficultyChangeType.MapDifficulty:
                    packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
                    break;
            }
        }

        [Parser(Opcode.SMSG_UPDATE_LAST_INSTANCE)]
        [Parser(Opcode.SMSG_RESET_FAILED_NOTIFY)]
        public static void HandleResetFailedNotify(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
        }

        [Parser(Opcode.MSG_RAID_TARGET_UPDATE)]
        public static void HandleRaidTargetUpdate(Packet packet)
        {
            var type = packet.ReadSByte("Type");
            if (type != -1 && packet.Direction == Direction.ClientToServer)
            {
                packet.ReadGuid("Target GUID");
                return;
            }

            if (type == 0)
            {
                packet.ReadGuid("Who GUID");
                packet.ReadByte("Icon Id");
                packet.ReadGuid("Target GUID");
            }
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_MESSAGE)]
        public static void HandleRaidInstanceMessage(Packet packet)
        {
            var type = packet.ReadEnum<RaidInstanceResetWarning>("Warning Type", TypeCode.Int32);
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
            packet.ReadEnum<MapDifficulty>("Difficulty", TypeCode.Int32);
            packet.ReadInt32("Reset time");
            if (type == RaidInstanceResetWarning.Welcome)
            {
                packet.ReadBoolean("Unk bool");
                packet.ReadBoolean("Is Extended");
            }
        }

        [Parser(Opcode.SMSG_UPDATE_INSTANCE_OWNERSHIP)]
        [Parser(Opcode.SMSG_INSTANCE_SAVE_CREATED)]
        public static void HandleUpdateInstanceOwnership(Packet packet)
        {
            packet.ReadInt32("Unk");
        }

        [Parser(Opcode.SMSG_INSTANCE_RESET)]
        public static void HandleUpdateInstanceReset(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
        }

        [Parser(Opcode.CMSG_INSTANCE_LOCK_RESPONSE)]
        public static void HandleInstanceLockResponse(Packet packet)
        {
            packet.ReadBoolean("Accept");
        }

        [Parser(Opcode.SMSG_INSTANCE_LOCK_WARNING_QUERY)]
        public static void HandleInstanceLockWarningQuery(Packet packet)
        {
            packet.ReadInt32("Time");
            packet.ReadInt32("Encounter Mask");
            packet.ReadByte("Unk");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // guessing
                packet.ReadByte("Unk2"); // events it throws: 1 : INSTANCE_LOCK_WARNING  0 : INSTANCE_LOCK_STOP / INSTANCE_LOCK_START
        }

        [Parser(Opcode.SMSG_RAID_INSTANCE_INFO)]
        public static void HandleRaidInstanceInfo(Packet packet)
        {
            var counter = packet.ReadInt32("Counter");
            for (var i = 0; i < counter; ++i)
            {
                packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID", i);
                packet.ReadEnum<MapDifficulty>("Map Difficulty", TypeCode.UInt32, i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadUInt32("Unk1", i);
                packet.ReadGuid("Instance GUID", i);
                packet.ReadBoolean("Expired", i);
                packet.ReadBoolean("Extended", i);
                packet.ReadUInt32("Reset Time", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadUInt32("Unk2", i);
            }
        }
    }
}
