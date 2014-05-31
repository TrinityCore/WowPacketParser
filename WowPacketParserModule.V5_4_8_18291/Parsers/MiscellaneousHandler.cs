using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class MiscellaneousHandler
    {
        [HasSniffData]
        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map");
            packet.ReadBit("Loading");
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            packet.ReadUInt32("Counter");
            packet.ReadUInt32("Ticks");
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var counter = (int)packet.ReadBits("List count", 6);

            var accountId = new byte[counter][];
            var playerGUID = new byte[counter][];
            var guildGUID = new byte[counter][];

            var guildNameLength = new uint[counter];
            var playerNameLength = new uint[counter];
            var bits14 = new uint[counter][];
            var bitED = new bool[counter];
            var bit214 = new bool[counter];

            for (var i = 0; i < counter; ++i)
            {
                accountId[i] = new byte[8];
                playerGUID[i] = new byte[8];
                guildGUID[i] = new byte[8];

                accountId[i][2] = packet.ReadBit();
                playerGUID[i][2] = packet.ReadBit();
                accountId[i][7] = packet.ReadBit();
                guildGUID[i][5] = packet.ReadBit();
                guildNameLength[i] = packet.ReadBits(7);
                accountId[i][1] = packet.ReadBit();
                accountId[i][5] = packet.ReadBit();
                guildGUID[i][7] = packet.ReadBit();
                playerGUID[i][5] = packet.ReadBit();
                bitED[i] = packet.ReadBit();
                guildGUID[i][1] = packet.ReadBit();
                playerGUID[i][6] = packet.ReadBit();
                guildGUID[i][2] = packet.ReadBit();
                playerGUID[i][4] = packet.ReadBit();
                guildGUID[i][0] = packet.ReadBit();
                guildGUID[i][3] = packet.ReadBit();
                accountId[i][6] = packet.ReadBit();
                bit214[i] = packet.ReadBit();
                playerGUID[i][1] = packet.ReadBit();
                guildGUID[i][4] = packet.ReadBit();
                accountId[i][0] = packet.ReadBit();

                bits14[i] = new uint[5];
                for (var j = 0; j < 5; ++j)
                    bits14[i][j] = packet.ReadBits(7);

                playerGUID[i][3] = packet.ReadBit();
                guildGUID[i][6] = packet.ReadBit();
                playerGUID[i][0] = packet.ReadBit();
                accountId[i][4] = packet.ReadBit();
                accountId[i][3] = packet.ReadBit();
                playerGUID[i][7] = packet.ReadBit();
                playerNameLength[i] = packet.ReadBits(6);
            }

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadXORByte(playerGUID[i], 1);
                packet.ReadInt32("RealmID", i);
                packet.ReadXORByte(playerGUID[i], 7);
                packet.ReadInt32("RealmID", i);
                packet.ReadXORByte(playerGUID[i], 4);
                packet.ReadWoWString("Player Name", playerNameLength[i], i);
                packet.ReadXORByte(guildGUID[i], 1);
                packet.ReadXORByte(playerGUID[i], 0);
                packet.ReadXORByte(guildGUID[i], 2);
                packet.ReadXORByte(guildGUID[i], 0);
                packet.ReadXORByte(guildGUID[i], 4);
                packet.ReadXORByte(playerGUID[i], 3);
                packet.ReadXORByte(guildGUID[i], 6);
                packet.ReadInt32("Unk1", i);
                packet.ReadWoWString("Guild Name", guildNameLength[i], i);
                packet.ReadXORByte(guildGUID[i], 3);
                packet.ReadXORByte(accountId[i], 4);
                packet.ReadEnum<Class>("Class", TypeCode.Byte, i);
                packet.ReadXORByte(accountId[i], 7);
                packet.ReadXORByte(playerGUID[i], 6);
                packet.ReadXORByte(playerGUID[i], 2);

                for (var j = 0; j < 5; ++j)
                    packet.ReadWoWString("String14", bits14[i][j]);

                packet.ReadXORByte(accountId[i], 2);
                packet.ReadXORByte(accountId[i], 3);
                packet.ReadEnum<Race>("Race", TypeCode.Byte, i);
                packet.ReadXORByte(guildGUID[i], 7);
                packet.ReadXORByte(accountId[i], 1);
                packet.ReadXORByte(accountId[i], 5);
                packet.ReadXORByte(accountId[i], 6);
                packet.ReadXORByte(playerGUID[i], 5);
                packet.ReadXORByte(accountId[i], 0);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, i);
                packet.ReadXORByte(guildGUID[i], 5);
                packet.ReadByte("Level", i);
                packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id", i);

                packet.WriteGuid("PlayerGUID", playerGUID[i], i);
                packet.WriteGuid("GuildGUID", guildGUID[i], i);
                packet.WriteLine("[{0}] Account: {1}", i, BitConverter.ToUInt64(accountId[i], 0));
            }
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 5, 4, 3, 2, 1, 0);
            packet.ParseBitStream(guid, 0, 7, 3, 5, 1, 4, 6, 2);
            packet.WriteGuid("Guid", guid);
        }
    }
}
