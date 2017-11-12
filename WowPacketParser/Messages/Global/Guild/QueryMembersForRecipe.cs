using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct QueryMembersForRecipe
    {
        public ulong GuildGUID;
        public int UniqueBit;
        public int SkillLineID;
        public int SpellID;

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBERS_FOR_RECIPE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQueryMembersForRecipe(Packet packet)
        {
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadUInt32("Skill ID");
            packet.ReadUInt32("Skill Value");

            var guid = packet.StartBitStream(4, 1, 0, 3, 6, 7, 5, 2);
            packet.ParseBitStream(guid, 1, 6, 5, 0, 3, 7, 2, 4);
            packet.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_MEMBERS_FOR_RECIPE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQueryMembersForRecipe602(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadUInt32("SkillLineID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadUInt32("UniqueBit");
        }
    }
}
