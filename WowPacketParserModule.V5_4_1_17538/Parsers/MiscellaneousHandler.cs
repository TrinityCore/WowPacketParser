using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            byte[][] accountId;
            byte[][] playerGUID;
            byte[][] guildGUID;
            uint[][] bits14;
            uint[] guildNameLength;
            uint[] playerNameLength;

            var counter = packet.ReadBits(6);

            var bitEB = 0;
            var bit214 = 0;

            accountId = new byte[counter][];
            playerGUID = new byte[counter][];
            guildGUID = new byte[counter][];
            bits14 = new uint[counter][];
            guildNameLength = new uint[counter];
            playerNameLength = new uint[counter];

            for (var i = 0; i < counter; ++i)
            {
                accountId[i] = new byte[8];
                playerGUID[i] = new byte[8];
                guildGUID[i] = new byte[8];

                playerGUID[i][5] = packet.ReadBit();
                accountId[i][4] = packet.ReadBit();
                guildGUID[i][1] = packet.ReadBit();

                guildNameLength[i] = packet.ReadBits(7);
                playerNameLength[i] = packet.ReadBits(6);

                accountId[i][2] = packet.ReadBit();
                guildGUID[i][2] = packet.ReadBit();
                guildGUID[i][5] = packet.ReadBit();
                playerGUID[i][3] = packet.ReadBit();
                playerGUID[i][1] = packet.ReadBit();
                playerGUID[i][0] = packet.ReadBit();
                guildGUID[i][4] = packet.ReadBit();

                bitEB = packet.ReadBit();

                accountId[i][6] = packet.ReadBit();
                guildGUID[i][0] = packet.ReadBit();
                guildGUID[i][3] = packet.ReadBit();
                playerGUID[i][4] = packet.ReadBit();
                guildGUID[i][6] = packet.ReadBit();

                bits14[i] = new uint[5];
                for (var j = 0; j < 5; ++j)
                    bits14[i][j] = packet.ReadBits(7);

                guildGUID[i][7] = packet.ReadBit();
                playerGUID[i][6] = packet.ReadBit();
                accountId[i][3] = packet.ReadBit();
                playerGUID[i][2] = packet.ReadBit();
                playerGUID[i][7] = packet.ReadBit();
                accountId[i][7] = packet.ReadBit();
                accountId[i][1] = packet.ReadBit();
                accountId[i][5] = packet.ReadBit();

                bit214 = packet.ReadBit();

                accountId[i][0] = packet.ReadBit();
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadByteE<Gender>("Gender", i);

                packet.ReadXORByte(guildGUID[i], 3);
                packet.ReadXORByte(guildGUID[i], 1);

                packet.ReadXORByte(accountId[i], 5);

                packet.ReadXORByte(playerGUID[i], 3);
                packet.ReadXORByte(playerGUID[i], 6);

                packet.ReadXORByte(accountId[i], 6);

                packet.ReadByteE<Race>("Race", i);
                packet.ReadInt32("RealmId", i);

                packet.ReadXORByte(accountId[i], 1);

                packet.ReadWoWString("Player Name", playerNameLength[i], i);

                packet.ReadXORByte(guildGUID[i], 5);
                packet.ReadXORByte(guildGUID[i], 0);

                packet.ReadXORByte(playerGUID[i], 4);

                packet.ReadByteE<Class>("Class", i);

                packet.ReadXORByte(guildGUID[i], 6);

                packet.ReadUInt32<ZoneId>("Zone Id", i);

                packet.ReadXORByte(accountId[i], 0);

                packet.ReadInt32("RealmID", i);

                packet.ReadXORByte(playerGUID[i], 1);

                packet.ReadXORByte(accountId[i], 4);

                packet.ReadByte("Level", i);

                packet.ReadXORByte(guildGUID[i], 4);
                packet.ReadXORByte(playerGUID[i], 2);

                packet.ReadWoWString("Guild Name", guildNameLength[i], i);

                packet.ReadXORByte(playerGUID[i], 7);
                packet.ReadXORByte(playerGUID[i], 0);

                packet.ReadXORByte(accountId[i], 2);
                packet.ReadXORByte(accountId[i], 7);

                packet.ReadInt32("Unk1", i);

                packet.ReadXORByte(playerGUID[i], 5);

                packet.ReadXORByte(guildGUID[i], 7);

                packet.ReadXORByte(accountId[i], 3);

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("String14", bits14[i][j], i, j);

                packet.ReadXORByte(guildGUID[i], 2);

                packet.WriteGuid("PlayerGUID", playerGUID[i], i);
                packet.WriteGuid("GuildGUID", guildGUID[i], i);
                packet.AddValue("Account", BitConverter.ToUInt64(accountId[i], 0), i);
            }
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadUInt32E<UnknownFlags>("Mask");
            packet.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.ReadUInt32("Count");
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
        }
    }
}
