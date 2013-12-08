using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            playerGuid[1] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            playerGuid[3] = packet.ReadBit();
            playerGuid[2] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();

            packet.ParseBitStream(playerGuid, 2, 0, 4, 1, 5, 3, 7, 6);

            var guid = new Guid(BitConverter.ToUInt64(playerGuid, 0));
            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            var count = packet.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                
                guildGuids[c][3] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(6);
                charGuids[c][4] = packet.ReadBit();             
                guildGuids[c][4] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                
            }
            
            packet.ReadBit("Unk bit");
            var count2 = packet.ReadBits("RIDBIT21", 21);

            packet.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                packet.ReadByte("Skin", c); //v4+61
                packet.ReadXORByte(charGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 7);
                packet.ReadInt32("Pet Display ID", c); //v4+108
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c); // v4 + 8
                
                for (int j = 0; j < 23; ++j)
                {
                    packet.ReadInt32("Item DisplayID", c, j);
                    packet.ReadInt32("Item EnchantID", c, j);
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, j);
                }
                
                packet.ReadXORByte(charGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 6);
                
                var level = packet.ReadByte("Level", c); // v4+66
                var y = packet.ReadSingle("Position Y", c); // v4+80
                var x = packet.ReadSingle("Position X", c); //v4+76
                packet.ReadByte("Face", c); // v4+62
                packet.ReadXORByte(guildGuids[c], 0);
                packet.ReadByte("List Order", c); //v4+57
                var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", c);
                packet.ReadXORByte(guildGuids[c], 7);
                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);
                var mapId = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id", c); //v4+72
                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c); //v4+58
                var z = packet.ReadSingle("Position Z", c); //v4+84
                packet.ReadXORByte(guildGuids[c], 1);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c); //v4+60
                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadByte("Hair Color", c); // v4+64
                packet.ReadXORByte(guildGuids[c], 5);
                var clss = packet.ReadEnum<Class>("Class", TypeCode.Byte, c); // v4+59
                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 1);
                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c); //v4+100
                packet.ReadByte("Facial Hair", c); // v4+65
                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(charGuids[c], 0);
                packet.ReadByte("Hair Style", c); // v4+63
                packet.ReadXORByte(charGuids[c], 5);
                packet.ReadInt32("Pet Family", c); // v4+116
                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadInt32("Pet Level", c); // v4+112
                packet.ReadXORByte(guildGuids[c], 4);

                for (var i = 0; i < count2; ++i)
                {
                    packet.ReadByte("unk2");
                    packet.ReadUInt32("unk1");  
                }

                var playerGuid = new Guid(BitConverter.ToUInt64(charGuids[c], 0));

                packet.WriteGuid("Character GUID", charGuids[c], c);
                packet.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    var startPos = new StartPosition();
                    startPos.Map = mapId;
                    startPos.Position = new Vector3(x, y, z);
                    startPos.Zone = zone;

                    Storage.StartPositions.Add(new Tuple<Race, Class>(race, clss), startPos, packet.TimeSpan);
                }

                var playerInfo = new Player { Race = race, Class = clss, Name = name, FirstLogin = firstLogins[c], Level = level };
                if (Storage.Objects.ContainsKey(playerGuid))
                    Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);

                StoreGetters.AddName(playerGuid, name);
            }
        }

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
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, i);

                packet.ReadXORByte(guildGUID[i], 3);
                packet.ReadXORByte(guildGUID[i], 1);

                packet.ReadXORByte(accountId[i], 5);

                packet.ReadXORByte(playerGUID[i], 3);
                packet.ReadXORByte(playerGUID[i], 6);

                packet.ReadXORByte(accountId[i], 6);

                packet.ReadEnum<Race>("Race", TypeCode.Byte, i);
                packet.ReadInt32("RealmId", i);

                packet.ReadXORByte(accountId[i], 1);

                packet.ReadWoWString("Player Name", playerNameLength[i], i);

                packet.ReadXORByte(guildGUID[i], 5);
                packet.ReadXORByte(guildGUID[i], 0);

                packet.ReadXORByte(playerGUID[i], 4);

                packet.ReadEnum<Class>("Class", TypeCode.Byte, i);

                packet.ReadXORByte(guildGUID[i], 6);

                packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", i);

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
                packet.WriteLine("[{0}] Account: {1}", i, BitConverter.ToUInt64(accountId[i], 0));
            }
        }
    }
}