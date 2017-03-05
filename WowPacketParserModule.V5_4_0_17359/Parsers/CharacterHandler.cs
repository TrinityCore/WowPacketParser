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

            packet.Translator.ReadBit("Unk bit");
            var count2 = packet.Translator.ReadBits("RIDBIT21", 21);
            var count = packet.Translator.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][3] = packet.Translator.ReadBit();
                guildGuids[c][6] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                firstLogins[c] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(6);
                guildGuids[c][1] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
                charGuids[c][1] = packet.Translator.ReadBit();
                guildGuids[c][4] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                guildGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][7] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][2] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][4] = packet.Translator.ReadBit();
            }

            packet.Translator.ResetBitReader();

            for (int c = 0; c < count; ++c)
            { 
                Vector3 pos = new Vector3();

                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);
                packet.Translator.ReadXORByte(charGuids[c], 0);
                packet.Translator.ReadXORByte(guildGuids[c], 5);
                packet.Translator.ReadXORByte(charGuids[c], 1);
                packet.Translator.ReadXORByte(guildGuids[c], 1);
                packet.Translator.ReadXORByte(charGuids[c], 3);
                packet.Translator.ReadInt32("Pet Family", c); // v4+116
                packet.Translator.ReadXORByte(guildGuids[c], 2);
                packet.Translator.ReadByte("Hair Style", c); // v4+63
                packet.Translator.ReadXORByte(guildGuids[c], 0);
                packet.Translator.ReadXORByte(guildGuids[c], 7);
                pos.Y = packet.Translator.ReadSingle("Position Y", c); // v4+80
                packet.Translator.ReadXORByte(charGuids[c], 6);
                packet.Translator.ReadInt32("Pet Level", c); // v4+112
                packet.Translator.ReadXORByte(charGuids[c], 7);
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c); // v4 + 8
                var level = packet.Translator.ReadByte("Level", c); // v4+66
                pos.X = packet.Translator.ReadSingle("Position X", c); //v4+76
                var klass = packet.Translator.ReadByteE<Class>("Class", c); // v4+59
                packet.Translator.ReadInt32("Pet Display ID", c); //v4+108
                packet.Translator.ReadByte("List Order", c); //v4+57
                packet.Translator.ReadByte("Facial Hair", c); // v4+65
                pos.Z = packet.Translator.ReadSingle("Position Z", c); //v4+84
                packet.Translator.ReadXORByte(guildGuids[c], 3);
                var race = packet.Translator.ReadByteE<Race>("Race", c); //v4+58
                packet.Translator.ReadXORByte(charGuids[c], 4);

                for (int j = 0; j < 23; ++j)
                {
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, j);
                    packet.Translator.ReadInt32("Item DisplayID", c, j);
                    packet.Translator.ReadInt32("Item EnchantID", c, j);
                }

                packet.Translator.ReadXORByte(guildGuids[c], 6);
                packet.Translator.ReadXORByte(charGuids[c], 2);
                packet.Translator.ReadXORByte(charGuids[c], 5);
                packet.Translator.ReadByte("Skin", c); //v4+61
                packet.Translator.ReadByte("Hair Color", c); // v4+64
                packet.Translator.ReadByte("Face", c); // v4+62
                packet.Translator.ReadXORByte(guildGuids[c], 4);
                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c); //v4+100
                packet.Translator.ReadByteE<Gender>("Gender", c); //v4+60
                var mapId = packet.Translator.ReadInt32<MapId>("Map Id", c); //v4+72

                for (var i = 0; i < count2; ++i)
                {
                    packet.Translator.ReadUInt32("unk1", i);
                    packet.Translator.ReadByte("unk2", i);
                }

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
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUp(Packet packet)
        {
            packet.Translator.ReadInt32("Health");

            for (var i = 0; i < 5; i++)
                packet.Translator.ReadInt32("Stat", (StatType)i);

            packet.Translator.ReadInt32("Talent Level"); // 0 - No Talent gain / 1 - Talent Point gain

            packet.Translator.ReadInt32("Level");

            for (var i = 0; i < 5; i++)
                packet.Translator.ReadInt32("Power", (PowerType) i);
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY_WEEK_LIMIT)]
        public static void HandleUpdateCurrencyWeekLimit(Packet packet)
        {
            packet.Translator.ReadUInt32("Currency ID");
            packet.Translator.ReadUInt32("Week Cap");
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
                hasWeekCap[i] = packet.Translator.ReadBit();       // 0Ch
                hasWeekCount[i] = packet.Translator.ReadBit();     // 1Ch
                hasSeasonTotal[i] = packet.Translator.ReadBit();   // 14h
                flags[i] = packet.Translator.ReadBits(5);          // 20h
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i); // 20h
                packet.Translator.ReadUInt32("Currency count", i);

                if (hasSeasonTotal[i]) // 0Ch
                    packet.Translator.ReadUInt32("Season total earned", i);

                if (hasWeekCap[i]) // 14h
                    packet.Translator.ReadUInt32("Weekly cap", i);

                if (hasWeekCount[i]) // 1Ch
                    packet.Translator.ReadUInt32("Weekly count", i);

                packet.Translator.ReadUInt32("Currency id", i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_TALENT_DATA)]
        public static void ReadTalentInfo510(Packet packet)
        {
            var specCount = packet.Translator.ReadBits("Spec Group count", 19);

            var spentTalents = new uint[specCount];

            for (var i = 0; i < specCount; ++i)
                spentTalents[i] = packet.Translator.ReadBits("Spec Talent Count", 23, i);

            for (var i = 0; i < specCount; ++i)
            {
                for (var j = 0; j < 6; ++j)
                    packet.Translator.ReadUInt16("Glyph", i, j);

                for (var j = 0; j < spentTalents[i]; ++j)
                    packet.Translator.ReadUInt16("Talent Id", i, j);

                packet.Translator.ReadUInt32("Spec Id", i);
            }

            packet.Translator.ReadByte("Active Spec Group");
        }
    }
}
