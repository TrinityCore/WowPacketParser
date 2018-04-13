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
    }
}
