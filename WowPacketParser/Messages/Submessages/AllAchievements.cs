using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct AllAchievements
    {
        public List<EarnedAchievement> Earned;
        public List<CriteriaProgress> Progress;

        public static void Read3(Packet packet)
        {
            while (true)
            {
                var id = packet.ReadInt32<AchievementId>("Achievement Id");

                if (id == -1)
                    break;

                packet.ReadPackedTime("Achievement Time");
            }

            while (true)
            {
                var id = packet.ReadInt32("Criteria ID");

                if (id == -1)
                    break;

                packet.ReadPackedUInt64("Criteria Counter");
                packet.ReadPackedGuid("Player GUID");
                packet.ReadInt32("Unk Int32"); // Unk flag, same as in SMSG_CRITERIA_UPDATE
                packet.ReadPackedTime("Criteria Time");

                for (var i = 0; i < 2; i++)
                    packet.ReadInt32("Timer " + i);
            }
        }

        public static void Read6(Packet packet, params object[] idx)
        {
            var earnedCount = packet.ReadUInt32("EarnedCount", idx);
            var progressCount = packet.ReadUInt32("ProgressCount", idx);

            for (var i = 0; i < earnedCount; ++i)
                EarnedAchievement.Read6(packet, "Earned", i);

            for (var i = 0; i < progressCount; ++i)
                CriteriaProgress.Read6(packet, "Progress", i);
        }
    }
}
