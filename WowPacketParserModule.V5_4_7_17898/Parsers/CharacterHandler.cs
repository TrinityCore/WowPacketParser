using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleCharEnum(Packet packet)
        {
            packet.Translator.ReadBit("Unk bit");

            var unkCounter = packet.Translator.ReadBits("Unk Counter", 21);
            var count = packet.Translator.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                guildGuids[c][4] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][3] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();
                charGuids[c][1] = packet.Translator.ReadBit();
                guildGuids[c][2] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                firstLogins[c] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                guildGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(6);
                packet.Translator.ReadBit("Unkt Bit");
                guildGuids[c][6] = packet.Translator.ReadBit();
                charGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][7] = packet.Translator.ReadBit();
            }

            packet.Translator.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                var pos = new Vector3();

                packet.Translator.ReadByte("Face", c);

                packet.Translator.ReadXORByte(guildGuids[c], 7);

                var race = packet.Translator.ReadByteE<Race>("Race", c);

                packet.Translator.ReadXORByte(charGuids[c], 5);
                packet.Translator.ReadXORByte(guildGuids[c], 2);
                packet.Translator.ReadXORByte(charGuids[c], 6);

                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);

                packet.Translator.ReadXORByte(guildGuids[c], 3);

                packet.Translator.ReadInt32("Pet Level", c); // v4+112
                packet.Translator.ReadInt32("Pet Display ID", c); //v4+108
                packet.Translator.ReadUInt32("Unk 1", c);
                packet.Translator.ReadUInt32("Unk 2", c);

                packet.Translator.ReadXORByte(charGuids[c], 3);
                packet.Translator.ReadXORByte(charGuids[c], 0);

                packet.Translator.ReadByte("Facial Hair", c);
                packet.Translator.ReadByteE<Gender>("Gender", c);

                packet.Translator.ReadXORByte(guildGuids[c], 0);

                packet.Translator.ReadByte("HairStyle", c);
                var level = packet.Translator.ReadByte("Level", c);

                for (int j = 0; j < 23; ++j)
                {
                    packet.Translator.ReadInt32("Item DisplayID", c, j);
                    packet.Translator.ReadInt32("Item EnchantID", c, j);
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, j);
                }

                pos.Z = packet.Translator.ReadSingle();

                packet.Translator.ReadXORByte(guildGuids[c], 1);

                pos.Y = packet.Translator.ReadSingle();
                packet.Translator.ReadByte("Skin", c);
                packet.Translator.ReadByte("List Order", c); //v4+57

                packet.Translator.ReadXORByte(guildGuids[c], 5);
                packet.Translator.ReadXORByte(charGuids[c], 1);

                packet.Translator.ReadUInt32("Unk 3", c);
                pos.X = packet.Translator.ReadSingle();
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c);
                var mapId = packet.Translator.ReadInt32("Map", c);
                packet.Translator.ReadInt32("Pet Family", c); // v4+116
                packet.Translator.ReadByte("Hair Color", c);
                var klass = packet.Translator.ReadByteE<Class>("Class", c);

                packet.Translator.ReadXORByte(guildGuids[c], 4);
                packet.Translator.ReadXORByte(charGuids[c], 2);

                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);

                packet.Translator.ReadXORByte(charGuids[c], 7);
                packet.Translator.ReadXORByte(guildGuids[c], 6);
                packet.Translator.ReadXORByte(charGuids[c], 4);

                var playerGuid = new WowGuid64(BitConverter.ToUInt64(charGuids[c], 0));

                packet.AddValue("Position", pos, c);
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

            for (var i = 0; i < unkCounter; ++i)
            {
                packet.Translator.ReadUInt32("Unk int", i);
                packet.Translator.ReadByte("Unk byte", i);
            }
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            packet.Translator.StartBitStream(playerGuid, 6, 4, 5, 1, 7, 3, 2, 0);
            packet.Translator.ParseBitStream(playerGuid, 1, 2, 3, 4, 0, 7, 6, 5);

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
                hasWeekCap[i] = packet.Translator.ReadBit();
                hasSeasonTotal[i] = packet.Translator.ReadBit();
                hasWeekCount[i] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags {1}", flags[i], i); // 20h
                packet.Translator.ReadUInt32("Currency count", i);
                packet.Translator.ReadUInt32("Currency id", i);

                if (hasWeekCap[i]) // 14h
                    packet.Translator.ReadUInt32("Weekly cap", i);

                if (hasWeekCount[i]) // 1Ch
                    packet.Translator.ReadUInt32("Weekly count", i);

                if (hasSeasonTotal[i]) // 0Ch
                    packet.Translator.ReadUInt32("Season total earned", i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            packet.Translator.ReadBool("Print in chat");
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var count = packet.Translator.ReadBits("Count", 21);
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadByteE<PowerType>("Power type"); // Actually powertype for class
                packet.Translator.ReadInt32("Value");
            }

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];

            var hasBaseXP = !packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var hasGroupRate = !packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Unk Bit");
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);

            if (hasGroupRate)
                packet.Translator.ReadSingle("Group rate");

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);

            if (hasBaseXP)
                packet.Translator.ReadUInt32("Base XP");

            packet.Translator.ReadUInt32("Total XP");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadByte("XP type");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PLAYER_VEHICLE_DATA)]
        public static void HandlePlayerVehicleData(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 1, 6, 3, 7, 4, 2, 5);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadInt32("Vehicle Id");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)]
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 9);

            var guids = new byte[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.Translator.StartBitStream(guids[i], 3, 7, 4, 1, 2, 5, 0, 6);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 7);
                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 3);
                packet.Translator.ReadXORByte(guids[i], 1);
                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.WriteGuid("Character Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED)]
        public static void HandleXPGainAborted(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 0, 2, 7, 4, 5, 1, 3, 6);

            packet.Translator.ReadInt32("Unk Int32 1");
            packet.Translator.ReadInt32("Unk Int32 2");

            packet.Translator.ParseBitStream(guid, 6, 0, 3, 7, 2, 5, 1, 4);

            packet.Translator.ReadInt32("Unk Int32 3");

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
