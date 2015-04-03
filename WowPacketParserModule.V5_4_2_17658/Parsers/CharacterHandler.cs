using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleCharEnum(Packet packet)
        {
            var guidB = new byte[8];

            var count2 = (int)packet.ReadBits(21);
            var count1 = (int)packet.ReadBits(16);

            var charGuids = new byte[count1][];
            var guildGuids = new byte[count1][];
            var firstLogins = new bool[count1];
            var nameLenghts = new uint[count1];

            for (int c = 0; c < count1; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][0] = packet.ReadBit();
                guildGuids[c][4] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(6);
            }

            packet.ReadBit("Unk bit");

            for (int c = 0; c < count1; ++c)
            {
                packet.ReadInt32("Pet Level", c); // v4+112
                var level = packet.ReadByte("Level", c);

                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadXORByte(guildGuids[c], 3);

                for (int j = 0; j < 23; ++j)
                {
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, j);
                    packet.ReadInt32("Item EnchantID", c, j);
                    packet.ReadInt32("Item DisplayID", c, j);
                }

                packet.ReadXORByte(guildGuids[c], 6);

                packet.ReadByte("List Order", c); //v4+57
                packet.ReadByte("Hair Style", c); // v4+63
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var clss = packet.ReadByteE<Class>("Class", c); // v4+59

                packet.ReadXORByte(guildGuids[c], 7);

                packet.ReadXORByte(charGuids[c], 0);

                packet.ReadInt32("Pet Family", c); // v4+116

                packet.ReadXORByte(guildGuids[c], 1);
                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadXORByte(charGuids[c], 7);
                packet.ReadXORByte(charGuids[c], 2);
                packet.ReadXORByte(guildGuids[c], 5);

                packet.ReadByteE<Gender>("Gender", c); //v4+60
                packet.ReadInt32("Pet Display ID", c); //v4+108
                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);
                packet.ReadXORByte(charGuids[c], 6);
                packet.ReadByte("Hair Color", c); // v4+64
                packet.ReadByte("Facial Hair", c); // v4+65
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c); // v4 + 8
                var race = packet.ReadByteE<Race>("Race", c); //v4+58

                packet.ReadXORByte(guildGuids[c], 4);

                packet.ReadByte("Skin", c); // v4+63
                var mapId = packet.ReadInt32<MapId>("Map Id", c); //v4+72
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var y = packet.ReadSingle("Position Y", c); // v4+80

                packet.ReadXORByte(guildGuids[c], 0);
                packet.ReadXORByte(charGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 1);

                var z = packet.ReadSingle("Position Z", c); //v4+84

                packet.ReadXORByte(charGuids[c], 5);

                packet.ReadByte("Face", c); // v4+62
                var x = packet.ReadSingle("Position X", c); //v4+76

                var playerGuid = new WowGuid64(BitConverter.ToUInt64(charGuids[c], 0));

                packet.WriteGuid("Character GUID", charGuids[c], c);
                packet.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    var startPos = new StartPosition {Map = (uint) mapId, Position = new Vector3(x, y, z), Zone = zone};

                    Storage.StartPositions.Add(new Tuple<Race, Class>(race, clss), startPos, packet.TimeSpan);
                }

                var playerInfo = new Player { Race = race, Class = clss, Name = name, FirstLogin = firstLogins[c], Level = level };
                if (Storage.Objects.ContainsKey(playerGuid))
                    Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);

                StoreGetters.AddName(playerGuid, name);
            }

            for (var i = 0; i < count2; ++i)
            {
                packet.ReadUInt32("unk1", i);
                packet.ReadByte("unk2", i);
            }
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.ReadByte("Hair Style");
            packet.ReadByteE<Gender>("Gender");
            packet.ReadByte("Skin");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
            packet.ReadByteE<Class>("Class");
            packet.ReadByteE<Race>("Race");
            packet.ReadByte("Face");
            packet.ReadByte("Outfit Id");

            var unk = packet.ReadBit("unk");
            var nameLength = packet.ReadBits(6);
            packet.ReadWoWString("Name", (int)nameLength);
            if (unk)
                packet.ReadUInt32("unk20");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            packet.StartBitStream(playerGuid, 7, 0, 1, 3, 5, 2, 4, 6);
            packet.ParseBitStream(playerGuid, 6, 7, 5, 0, 4, 2, 3, 1);

            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                flags[i] = packet.ReadBits(5);          // 20h
                hasWeekCount[i] = packet.ReadBit();
                hasWeekCap[i] = packet.ReadBit();
                hasSeasonTotal[i] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i); // 20h
                packet.ReadUInt32("Currency count", i);

                if (hasWeekCap[i]) // 14h
                    packet.ReadUInt32("Weekly cap", i);

                packet.ReadUInt32("Currency id", i);

                if (hasSeasonTotal[i]) // 0Ch
                    packet.ReadUInt32("Season total earned", i);

                if (hasWeekCount[i]) // 1Ch
                    packet.ReadUInt32("Weekly count", i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            packet.ReadBool("Print in chat");
        }
    }
}
