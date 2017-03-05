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
            var count2 = (int)packet.Translator.ReadBits(21);
            var count1 = (int)packet.Translator.ReadBits(16);

            var charGuids = new byte[count1][];
            var guildGuids = new byte[count1][];
            var firstLogins = new bool[count1];
            var nameLenghts = new uint[count1];

            for (int c = 0; c < count1; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][4] = packet.Translator.ReadBit();
                charGuids[c][7] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][4] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();
                charGuids[c][3] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                guildGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][1] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                firstLogins[c] = packet.Translator.ReadBit();
                guildGuids[c][6] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(6);
            }

            packet.Translator.ReadBit("Unk bit");

            for (int c = 0; c < count1; ++c)
            {
                Vector3 pos = new Vector3();

                packet.Translator.ReadInt32("Pet Level", c); // v4+112
                var level = packet.Translator.ReadByte("Level", c);

                packet.Translator.ReadXORByte(guildGuids[c], 2);
                packet.Translator.ReadXORByte(guildGuids[c], 3);

                for (int j = 0; j < 23; ++j)
                {
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, j);
                    packet.Translator.ReadInt32("Item EnchantID", c, j);
                    packet.Translator.ReadInt32("Item DisplayID", c, j);
                }

                packet.Translator.ReadXORByte(guildGuids[c], 6);

                packet.Translator.ReadByte("List Order", c); //v4+57
                packet.Translator.ReadByte("Hair Style", c); // v4+63
                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var klass = packet.Translator.ReadByteE<Class>("Class", c); // v4+59

                packet.Translator.ReadXORByte(guildGuids[c], 7);

                packet.Translator.ReadXORByte(charGuids[c], 0);

                packet.Translator.ReadInt32("Pet Family", c); // v4+116

                packet.Translator.ReadXORByte(guildGuids[c], 1);
                packet.Translator.ReadXORByte(charGuids[c], 3);
                packet.Translator.ReadXORByte(charGuids[c], 7);
                packet.Translator.ReadXORByte(charGuids[c], 2);
                packet.Translator.ReadXORByte(guildGuids[c], 5);

                packet.Translator.ReadByteE<Gender>("Gender", c); //v4+60
                packet.Translator.ReadInt32("Pet Display ID", c); //v4+108
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);
                packet.Translator.ReadXORByte(charGuids[c], 6);
                packet.Translator.ReadByte("Hair Color", c); // v4+64
                packet.Translator.ReadByte("Facial Hair", c); // v4+65
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c); // v4 + 8
                var race = packet.Translator.ReadByteE<Race>("Race", c); //v4+58

                packet.Translator.ReadXORByte(guildGuids[c], 4);

                packet.Translator.ReadByte("Skin", c); // v4+63
                var mapId = packet.Translator.ReadInt32<MapId>("Map Id", c); //v4+72
                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                pos.Y = packet.Translator.ReadSingle("Position Y", c); // v4+80

                packet.Translator.ReadXORByte(guildGuids[c], 0);
                packet.Translator.ReadXORByte(charGuids[c], 4);
                packet.Translator.ReadXORByte(charGuids[c], 1);

                pos.Z = packet.Translator.ReadSingle("Position Z", c); //v4+84

                packet.Translator.ReadXORByte(charGuids[c], 5);

                packet.Translator.ReadByte("Face", c); // v4+62
                pos.X = packet.Translator.ReadSingle("Position X", c); //v4+76

                var playerGuid = new WowGuid64(BitConverter.ToUInt64(charGuids[c], 0));

                packet.Translator.WriteGuid("Character GUID", charGuids[c], c);
                packet.Translator.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = (uint)mapId, Zone = zone, Position = pos, Orientation = 0 };
                    Storage.StartPositions.Add(startPos, packet.TimeSpan);
                }

                var playerInfo = new Player { Race = race, Class = klass, Name = name, FirstLogin = firstLogins[c], Level = level };
                if (Storage.Objects.ContainsKey(playerGuid))
                    Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);

                StoreGetters.AddName(playerGuid, name);
            }

            for (var i = 0; i < count2; ++i)
            {
                packet.Translator.ReadUInt32("unk1", i);
                packet.Translator.ReadByte("unk2", i);
            }
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.Translator.ReadByte("Hair Style");
            packet.Translator.ReadByteE<Gender>("Gender");
            packet.Translator.ReadByte("Skin");
            packet.Translator.ReadByte("Hair Color");
            packet.Translator.ReadByte("Facial Hair");
            packet.Translator.ReadByteE<Class>("Class");
            packet.Translator.ReadByteE<Race>("Race");
            packet.Translator.ReadByte("Face");
            packet.Translator.ReadByte("Outfit Id");

            var unk = packet.Translator.ReadBit("unk");
            var nameLength = packet.Translator.ReadBits(6);
            packet.Translator.ReadWoWString("Name", (int)nameLength);
            if (unk)
                packet.Translator.ReadUInt32("unk20");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            packet.Translator.StartBitStream(playerGuid, 7, 0, 1, 3, 5, 2, 4, 6);
            packet.Translator.ParseBitStream(playerGuid, 6, 7, 5, 0, 4, 2, 3, 1);

            packet.Translator.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                flags[i] = packet.Translator.ReadBits(5);          // 20h
                hasWeekCount[i] = packet.Translator.ReadBit();
                hasWeekCap[i] = packet.Translator.ReadBit();
                hasSeasonTotal[i] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i); // 20h
                packet.Translator.ReadUInt32("Currency count", i);

                if (hasWeekCap[i]) // 14h
                    packet.Translator.ReadUInt32("Weekly cap", i);

                packet.Translator.ReadUInt32("Currency id", i);

                if (hasSeasonTotal[i]) // 0Ch
                    packet.Translator.ReadUInt32("Season total earned", i);

                if (hasWeekCount[i]) // 1Ch
                    packet.Translator.ReadUInt32("Weekly count", i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            packet.Translator.ReadBool("Print in chat");
        }
    }
}
