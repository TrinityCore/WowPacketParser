using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct SetAchievementTracking
    {
        public List<int> AchievementIDs;

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildSetAchievementTracking(Packet packet)
        {
            var count = packet.ReadBits("Count", 24);
            for (var i = 0; i < count; ++i)
                packet.ReadInt32<AchievementId>("Achievement Id", i);
        }

        [Parser(Opcode.CMSG_GUILD_SET_ACHIEVEMENT_TRACKING, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildSetAchievementTracking602(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadInt32<AchievementId>("AchievementIDs", i);
        }
    }
}
