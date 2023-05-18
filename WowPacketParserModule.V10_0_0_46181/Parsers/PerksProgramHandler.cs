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
