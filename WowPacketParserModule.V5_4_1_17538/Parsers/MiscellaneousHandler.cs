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

            var counter = packet.Translator.ReadBits(6);

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

                playerGUID[i][5] = packet.Translator.ReadBit();
                accountId[i][4] = packet.Translator.ReadBit();
                guildGUID[i][1] = packet.Translator.ReadBit();

                guildNameLength[i] = packet.Translator.ReadBits(7);
                playerNameLength[i] = packet.Translator.ReadBits(6);

                accountId[i][2] = packet.Translator.ReadBit();
                guildGUID[i][2] = packet.Translator.ReadBit();
                guildGUID[i][5] = packet.Translator.ReadBit();
                playerGUID[i][3] = packet.Translator.ReadBit();
                playerGUID[i][1] = packet.Translator.ReadBit();
                playerGUID[i][0] = packet.Translator.ReadBit();
                guildGUID[i][4] = packet.Translator.ReadBit();

                bitEB = packet.Translator.ReadBit();

                accountId[i][6] = packet.Translator.ReadBit();
                guildGUID[i][0] = packet.Translator.ReadBit();
                guildGUID[i][3] = packet.Translator.ReadBit();
                playerGUID[i][4] = packet.Translator.ReadBit();
                guildGUID[i][6] = packet.Translator.ReadBit();

                bits14[i] = new uint[5];
                for (var j = 0; j < 5; ++j)
                    bits14[i][j] = packet.Translator.ReadBits(7);

                guildGUID[i][7] = packet.Translator.ReadBit();
                playerGUID[i][6] = packet.Translator.ReadBit();
                accountId[i][3] = packet.Translator.ReadBit();
                playerGUID[i][2] = packet.Translator.ReadBit();
                playerGUID[i][7] = packet.Translator.ReadBit();
                accountId[i][7] = packet.Translator.ReadBit();
                accountId[i][1] = packet.Translator.ReadBit();
                accountId[i][5] = packet.Translator.ReadBit();

                bit214 = packet.Translator.ReadBit();

                accountId[i][0] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.Translator.ReadByteE<Gender>("Gender", i);

                packet.Translator.ReadXORByte(guildGUID[i], 3);
                packet.Translator.ReadXORByte(guildGUID[i], 1);

                packet.Translator.ReadXORByte(accountId[i], 5);

                packet.Translator.ReadXORByte(playerGUID[i], 3);
                packet.Translator.ReadXORByte(playerGUID[i], 6);

                packet.Translator.ReadXORByte(accountId[i], 6);

                packet.Translator.ReadByteE<Race>("Race", i);
                packet.Translator.ReadInt32("RealmId", i);

                packet.Translator.ReadXORByte(accountId[i], 1);

                packet.Translator.ReadWoWString("Player Name", playerNameLength[i], i);

                packet.Translator.ReadXORByte(guildGUID[i], 5);
                packet.Translator.ReadXORByte(guildGUID[i], 0);

                packet.Translator.ReadXORByte(playerGUID[i], 4);

                packet.Translator.ReadByteE<Class>("Class", i);

                packet.Translator.ReadXORByte(guildGUID[i], 6);

                packet.Translator.ReadUInt32<ZoneId>("Zone Id", i);

                packet.Translator.ReadXORByte(accountId[i], 0);

                packet.Translator.ReadInt32("RealmID", i);

                packet.Translator.ReadXORByte(playerGUID[i], 1);

                packet.Translator.ReadXORByte(accountId[i], 4);

                packet.Translator.ReadByte("Level", i);

                packet.Translator.ReadXORByte(guildGUID[i], 4);
                packet.Translator.ReadXORByte(playerGUID[i], 2);

                packet.Translator.ReadWoWString("Guild Name", guildNameLength[i], i);

                packet.Translator.ReadXORByte(playerGUID[i], 7);
                packet.Translator.ReadXORByte(playerGUID[i], 0);

                packet.Translator.ReadXORByte(accountId[i], 2);
                packet.Translator.ReadXORByte(accountId[i], 7);

                packet.Translator.ReadInt32("Unk1", i);

                packet.Translator.ReadXORByte(playerGUID[i], 5);

                packet.Translator.ReadXORByte(guildGUID[i], 7);

                packet.Translator.ReadXORByte(accountId[i], 3);

                for (var j = 0; j < 5; ++j)
                    packet.Translator.ReadWoWString("String14", bits14[i][j], i, j);

                packet.Translator.ReadXORByte(guildGUID[i], 2);

                packet.Translator.WriteGuid("PlayerGUID", playerGUID[i], i);
                packet.Translator.WriteGuid("GuildGUID", guildGUID[i], i);
                packet.AddValue("Account", BitConverter.ToUInt64(accountId[i], 0), i);
            }
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Area Trigger Id");
            packet.Translator.ReadBit("Unk bit1");
            packet.Translator.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.Translator.ReadUInt32E<UnknownFlags>("Mask");
            packet.Translator.ReadByteE<ItemClass>("Class");
        }

        [Parser(Opcode.CMSG_DESTROY_ITEM)]
        public static void HandleDestroyItem(Packet packet)
        {
            packet.Translator.ReadUInt32("Count");
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Slot");
        }
    }
}
