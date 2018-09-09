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
            packet.ReadUInt32("DifficultyID");

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

        public static void ReadUnkEncouter(Packet packet, params object[] idx)
        {
            packet.ReadInt32("UnkInt32_13", idx);
            packet.ReadInt32("UnkInt32_14", idx);

            var count1 = packet.ReadUInt32("UnkCount1", idx);
            var count2 = packet.ReadUInt32("UnkCount2", idx);
            var count3 = packet.ReadUInt32("UnkCount3", idx);

            for (var j = 0; j < count1; ++j)
            {
                packet.ReadInt32("UnkInt32_15", idx, j);
            }

            for (var j = 0; j < count2; ++j)
            {
                packet.ReadInt32("UnkInt32_16", idx, j);
            }

            for (var j = 0; j < count3; ++j)
            {
                ReadUnkEncouter(packet, idx, j);
            }
        }

        [Parser(Opcode.SMSG_ENCOUNTER_START)]
        public static void HandleEncounterStart(Packet packet)
        {
            packet.ReadInt32("EncounterID");
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadInt32("GroupSize");

            // Debug only???
            var count = packet.ReadUInt32("UnkCount");

            for (var i = 0; i < count; ++i)
            {
                packet.ReadPackedGuid128("Guid", i);

                var count1 = packet.ReadUInt32("UnkCount1", i);
                var count2 = packet.ReadUInt32("UnkCount2", i);
                var count3 = packet.ReadUInt32("UnkCount3", i);

                packet.ReadInt32("UnkInt32", i);

                var count4 = packet.ReadUInt32("UnkCount4", i);
                var count5 = packet.ReadUInt32("UnkCount5", i);

                for (var j = 0; j < count4; ++j)
                {
                    packet.ReadInt32("UnkInt32_5", i, j);
                }

                for (var j = 0; j < count5; ++j)
                {
                    packet.ReadInt32("UnkInt32_6", i, j);
                }

                var count6 = packet.ReadUInt32("UnkInt32_7", i);
                var count7 = packet.ReadUInt32("UnkInt32_8", i);

                for (var j = 0; j < count1; ++j)
                {
                    packet.ReadInt32("UnkInt32_9", i, j);
                }

                for (var j = 0; j < count2; ++j)
                {
                    packet.ReadInt32("UnkInt32_10", i, j);
                }

                for (var j = 0; j < count3; ++j)
                {
                    packet.ReadPackedGuid128("Guid", i, j);
                    packet.ReadInt32("UnkInt32_11", i, j);
                }

                for (var j = 0; j < count6; ++j)
                {
                    packet.ReadInt32("UnkInt32_12", i, j);
                    packet.ReadInt16("UnkInt16_1", i, j);
                }

                for (var j = 0; j < count7; ++j)
                {
                    ReadUnkEncouter(packet, i, j);
                }
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

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_SET_SUPPRESSING_RELEASE)]
        public static void HandleInstanceEncounterSetSuppressingRelease(Packet packet)
        {
            packet.ReadBit("ReleaseSuppressed");
        }

        [Parser(Opcode.SMSG_INSTANCE_ENCOUNTER_SET_ALLOWING_RELEASE)]
        public static void HandleInstanceEncounterSetAllowingRelease(Packet packet)
        {
            packet.ReadBit("ReleaseAllowed");
        }        
    }
}
