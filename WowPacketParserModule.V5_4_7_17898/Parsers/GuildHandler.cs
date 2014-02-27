using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_QUERY)]
        public static void HandleGuildQuery(Packet packet)
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

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("Text", (int)packet.ReadBits(10));
        }
    }
}
