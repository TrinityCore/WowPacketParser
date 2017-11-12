using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct QueryMemberRecipes
    {
        public ulong GuildMember;
        public ulong GuildGUID;
        public int SkillLineID;

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQueryMemberRecipes(Packet packet)
        {
            var guildGuid = new byte[8];
            var guid = new byte[8];

            packet.ReadInt32("Skill ID");

            guid[2] = packet.ReadBit();
            guildGuid[1] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guildGuid[0] = packet.ReadBit();
            guildGuid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guildGuid[4] = packet.ReadBit();
            guildGuid[3] = packet.ReadBit();
            guildGuid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guildGuid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guildGuid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guildGuid, 4);
            packet.ReadXORByte(guildGuid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guildGuid, 7);
            packet.ReadXORByte(guildGuid, 3);
            packet.ReadXORByte(guildGuid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guildGuid, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guildGuid, 5);
            packet.ReadXORByte(guildGuid, 6);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guild GUID", guildGuid);
            packet.WriteGuid("Player GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBER_RECIPES, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQueryMemberRecipes602(Packet packet)
        {
            packet.ReadPackedGuid128("GuildMember");
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32("SkillLineID");
        }
    }
}
