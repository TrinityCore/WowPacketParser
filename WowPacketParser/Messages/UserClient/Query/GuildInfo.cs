using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Query
{
    public unsafe struct GuildInfo
    {
        public ulong PlayerGuid;
        public ulong GuildGuid;

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleGuildQuery(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Not sure when it was changed
            {
                packet.ReadGuid("Guild GUID");
                packet.ReadGuid("Player GUID");
            }
            else
                packet.ReadUInt32("Guild Id");
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildQuery542(Packet packet)
        {
            var playerGUID = new byte[8];
            var guildGUID = new byte[8];

            guildGUID[5] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            guildGUID[2] = packet.ReadBit();
            guildGUID[0] = packet.ReadBit();
            guildGUID[4] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guildGUID[1] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            guildGUID[3] = packet.ReadBit();
            guildGUID[6] = packet.ReadBit();
            guildGUID[7] = packet.ReadBit();

            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(guildGUID, 1);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(guildGUID, 0);
            packet.ReadXORByte(guildGUID, 5);
            packet.ReadXORByte(guildGUID, 2);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(guildGUID, 3);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(guildGUID, 7);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(guildGUID, 6);
            packet.ReadXORByte(guildGUID, 4);

            packet.WriteGuid("PlayerGUID", playerGUID);
            packet.WriteGuid("GuildGUID", guildGUID);
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleGuildQuery547(Packet packet)
        {
            var playerGUID = new byte[8];
            var guildGUID = new byte[8];

            guildGUID[0] = packet.ReadBit();
            guildGUID[4] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            guildGUID[1] = packet.ReadBit();
            guildGUID[7] = packet.ReadBit();
            guildGUID[6] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            guildGUID[5] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guildGUID[2] = packet.ReadBit();
            guildGUID[3] = packet.ReadBit();

            packet.ReadXORByte(guildGUID, 7);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(guildGUID, 2);
            packet.ReadXORByte(guildGUID, 5);
            packet.ReadXORByte(guildGUID, 0);
            packet.ReadXORByte(guildGUID, 3);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(guildGUID, 1);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(guildGUID, 6);
            packet.ReadXORByte(guildGUID, 4);

            packet.WriteGuid("PlayerGUID", playerGUID);
            packet.WriteGuid("GuildGUID", guildGUID);
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQuery548(Packet packet)
        {
            var playerGUID = new byte[8];
            var guildGUID = new byte[8];

            playerGUID[7] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guildGUID[3] = packet.ReadBit();
            guildGUID[4] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            guildGUID[2] = packet.ReadBit();
            guildGUID[5] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            guildGUID[7] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            guildGUID[1] = packet.ReadBit();
            guildGUID[6] = packet.ReadBit();
            guildGUID[0] = packet.ReadBit();

            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(guildGUID, 2);
            packet.ReadXORByte(guildGUID, 4);
            packet.ReadXORByte(guildGUID, 7);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(guildGUID, 6);
            packet.ReadXORByte(guildGUID, 0);
            packet.ReadXORByte(guildGUID, 3);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(guildGUID, 5);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(guildGUID, 1);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(playerGUID, 5);

            packet.WriteGuid("Player GUID", playerGUID);
            packet.WriteGuid("Guild GUID", guildGUID);
        }

        [Parser(Opcode.CMSG_QUERY_GUILD_INFO, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQuery602(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");
            packet.ReadPackedGuid128("Player Guid");
        }
    }
}
