using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class PerksProgramHandler
    {
        [Parser(Opcode.SMSG_PERKS_PROGRAM_ACTIVITY_UPDATE)]
        public static void HandlePerksProgramActivityUpdate(Packet packet)
        {
            var activityCount = packet.ReadUInt32("ActivityCount");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49318))
                packet.ReadTime64("RemainingTime");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_7_51187))
                packet.ReadTime64("StartingTime");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
                packet.ReadInt32("UiThemeID");

            for (var i = 0; i < activityCount; i++)
                packet.ReadInt32("ActivityID", i);
        }

        [Parser(Opcode.SMSG_PERKS_PROGRAM_ACTIVITY_COMPLETE)]
        public static void HandlePerksProgramActivityComplete(Packet packet)
        {
            packet.ReadInt32("ActivityID");
        }
    }
}
