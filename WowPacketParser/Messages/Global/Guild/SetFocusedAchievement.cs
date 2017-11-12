using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct SetFocusedAchievement
    {
        public int AchievementID;

        [Parser(Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildSetFocusedAchievement(Packet packet)
        {
            packet.ReadInt32<AchievementId>("Achievement Id");
        }

        [Parser(Opcode.CMSG_GUILD_SET_FOCUSED_ACHIEVEMENT, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildSetFocusedAchievement602(Packet packet)
        {
            packet.ReadUInt32("AchievementID");
        }
    }
}
