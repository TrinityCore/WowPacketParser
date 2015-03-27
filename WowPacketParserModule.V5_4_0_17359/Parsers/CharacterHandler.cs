using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleCharEnum(Packet packet)
        {

            packet.ReadBit("Unk bit");
            var count2 = packet.ReadBits("RIDBIT21", 21);
            var count = packet.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][3] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(6);
                guildGuids[c][1] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][4] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
            }

            packet.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);
                packet.ReadXORByte(charGuids[c], 0);
                packet.ReadXORByte(guildGuids[c], 5);
                packet.ReadXORByte(charGuids[c], 1);
                packet.ReadXORByte(guildGuids[c], 1);
                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadInt32("Pet Family", c); // v4+116
                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadByte("Hair Style", c); // v4+63
                packet.ReadXORByte(guildGuids[c], 0);
                packet.ReadXORByte(guildGuids[c], 7);
                var y = packet.ReadSingle("Position Y", c); // v4+80
                packet.ReadXORByte(charGuids[c], 6);
                packet.ReadInt32("Pet Level", c); // v4+112
                packet.ReadXORByte(charGuids[c], 7);
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c); // v4 + 8
                var level = packet.ReadByte("Level", c); // v4+66
                var x = packet.ReadSingle("Position X", c); //v4+76
                var clss = packet.ReadByteE<Class>("Class", c); // v4+59
                packet.ReadInt32("Pet Display ID", c); //v4+108
                packet.ReadByte("List Order", c); //v4+57
                packet.ReadByte("Facial Hair", c); // v4+65
                var z = packet.ReadSingle("Position Z", c); //v4+84
                packet.ReadXORByte(guildGuids[c], 3);
                var race = packet.ReadByteE<Race>("Race", c); //v4+58
                packet.ReadXORByte(charGuids[c], 4);

                for (int j = 0; j < 23; ++j)
                {
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, j);
                    packet.ReadInt32("Item DisplayID", c, j);
                    packet.ReadInt32("Item EnchantID", c, j);
                }

                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(charGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 5);
                packet.ReadByte("Skin", c); //v4+61
                packet.ReadByte("Hair Color", c); // v4+64
                packet.ReadByte("Face", c); // v4+62
                packet.ReadXORByte(guildGuids[c], 4);
                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c); //v4+100
                packet.ReadByteE<Gender>("Gender", c); //v4+60
                var mapId = packet.ReadInt32<MapId>("Map Id", c); //v4+72

                for (var i = 0; i < count2; ++i)
                {
                    packet.ReadUInt32("unk1", i);
                    packet.ReadByte("unk2", i);
                }

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
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUp(Packet packet)
        {
            packet.ReadInt32("Health");

            for (var i = 0; i < 5; i++)
                packet.ReadInt32("Stat", (StatType)i);

            packet.ReadInt32("Talent Level"); // 0 - No Talent gain / 1 - Talent Point gain

            packet.ReadInt32("Level");

            for (var i = 0; i < 5; i++)
                packet.ReadInt32("Power", (PowerType) i);
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY_WEEK_LIMIT)]
        public static void HandleUpdateCurrencyWeekLimit(Packet packet)
        {
            packet.ReadUInt32("Currency ID");
            packet.ReadUInt32("Week Cap");
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
                hasWeekCap[i] = packet.ReadBit();       // 0Ch
                hasWeekCount[i] = packet.ReadBit();     // 1Ch
                hasSeasonTotal[i] = packet.ReadBit();   // 14h
                flags[i] = packet.ReadBits(5);          // 20h
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i); // 20h
                packet.ReadUInt32("Currency count", i);

                if (hasSeasonTotal[i]) // 0Ch
                    packet.ReadUInt32("Season total earned", i);

                if (hasWeekCap[i]) // 14h
                    packet.ReadUInt32("Weekly cap", i);

                if (hasWeekCount[i]) // 1Ch
                    packet.ReadUInt32("Weekly count", i);

                packet.ReadUInt32("Currency id", i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadTalentInfo510(Packet packet)
        {
            var specCount = packet.ReadBits("Spec Group count", 19);

            var spentTalents = new uint[specCount];

            for (var i = 0; i < specCount; ++i)
                spentTalents[i] = packet.ReadBits("Spec Talent Count", 23, i);

            for (var i = 0; i < specCount; ++i)
            {
                for (var j = 0; j < 6; ++j)
                    packet.ReadUInt16("Glyph", i, j);

                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.ReadUInt16("Talent Id", i, j);

                packet.ReadUInt32("Spec Id", i);
            }

            packet.ReadByte("Active Spec Group");
        }
    }
}
