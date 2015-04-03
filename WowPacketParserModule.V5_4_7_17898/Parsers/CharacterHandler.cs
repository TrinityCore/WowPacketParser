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
            packet.ReadBit("Unk bit");

            var unkCounter = packet.ReadBits("Unk Counter", 21);
            var count = packet.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                guildGuids[c][4] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(6);
                packet.ReadBit("Unkt Bit");
                guildGuids[c][6] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
            }

            packet.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                var pos = new Vector3();

                packet.ReadByte("Face", c);

                packet.ReadXORByte(guildGuids[c], 7);

                var race = packet.ReadByteE<Race>("Race", c);

                packet.ReadXORByte(charGuids[c], 5);
                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 6);

                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);

                packet.ReadXORByte(guildGuids[c], 3);

                packet.ReadInt32("Pet Level", c); // v4+112
                packet.ReadInt32("Pet Display ID", c); //v4+108
                packet.ReadUInt32("Unk 1", c);
                packet.ReadUInt32("Unk 2", c);

                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadXORByte(charGuids[c], 0);

                packet.ReadByte("Facial Hair", c);
                packet.ReadByteE<Gender>("Gender", c);

                packet.ReadXORByte(guildGuids[c], 0);

                packet.ReadByte("HairStyle", c);
                var level = packet.ReadByte("Level", c);

                for (int j = 0; j < 23; ++j)
                {
                    packet.ReadInt32("Item DisplayID", c, j);
                    packet.ReadInt32("Item EnchantID", c, j);
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, j);
                }

                pos.Z = packet.ReadSingle();

                packet.ReadXORByte(guildGuids[c], 1);

                pos.Y = packet.ReadSingle();
                packet.ReadByte("Skin", c);
                packet.ReadByte("List Order", c); //v4+57

                packet.ReadXORByte(guildGuids[c], 5);
                packet.ReadXORByte(charGuids[c], 1);

                packet.ReadUInt32("Unk 3", c);
                pos.X = packet.ReadSingle();
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                var mapId = packet.ReadInt32("Map", c);
                packet.ReadInt32("Pet Family", c); // v4+116
                packet.ReadByte("Hair Color", c);
                var Class = packet.ReadByteE<Class>("Class", c);

                packet.ReadXORByte(guildGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 2);

                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);

                packet.ReadXORByte(charGuids[c], 7);
                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(charGuids[c], 4);

                var playerGuid = new WowGuid64(BitConverter.ToUInt64(charGuids[c], 0));

                packet.AddValue("Position", pos, c);
                packet.WriteGuid("Character GUID", charGuids[c], c);
                packet.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    var startPos = new StartPosition {Map = (uint) mapId, Position = pos, Zone = zone};

                    Storage.StartPositions.Add(new Tuple<Race, Class>(race, Class), startPos, packet.TimeSpan);
                }

                var playerInfo = new Player { Race = race, Class = Class, Name = name, FirstLogin = firstLogins[c], Level = level };
                if (Storage.Objects.ContainsKey(playerGuid))
                    Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
                StoreGetters.AddName(playerGuid, name);
            }

            for (var i = 0; i < unkCounter; ++i)
            {
                packet.ReadUInt32("Unk int", i);
                packet.ReadByte("Unk byte", i);
            }
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            packet.StartBitStream(playerGuid, 6, 4, 5, 1, 7, 3, 2, 0);
            packet.ParseBitStream(playerGuid, 1, 2, 3, 4, 0, 7, 6, 5);

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
                hasWeekCap[i] = packet.ReadBit();
                hasSeasonTotal[i] = packet.ReadBit();
                hasWeekCount[i] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags {1}", flags[i], i); // 20h
                packet.ReadUInt32("Currency count", i);
                packet.ReadUInt32("Currency id", i);

                if (hasWeekCap[i]) // 14h
                    packet.ReadUInt32("Weekly cap", i);

                if (hasWeekCount[i]) // 1Ch
                    packet.ReadUInt32("Weekly count", i);

                if (hasSeasonTotal[i]) // 0Ch
                    packet.ReadUInt32("Season total earned", i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            packet.ReadBool("Print in chat");
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var count = packet.ReadBits("Count", 21);
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);

            for (var i = 0; i < count; i++)
            {
                packet.ReadByteE<PowerType>("Power type"); // Actually powertype for class
                packet.ReadInt32("Value");
            }

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];

            var hasBaseXP = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var hasGroupRate = !packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Unk Bit");
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            if (hasGroupRate)
                packet.ReadSingle("Group rate");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);

            if (hasBaseXP)
                packet.ReadUInt32("Base XP");

            packet.ReadUInt32("Total XP");
            packet.ReadXORByte(guid, 0);
            packet.ReadByte("XP type");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PLAYER_VEHICLE_DATA)]
        public static void HandlePlayerVehicleData(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 1, 6, 3, 7, 4, 2, 5);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadInt32("Vehicle Id");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)]
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.ReadBits("Count", 9);

            var guids = new byte[count][];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 3, 7, 4, 1, 2, 5, 0, 6);
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 3);
                packet.ReadXORByte(guids[i], 1);
                packet.ReadXORByte(guids[i], 5);

                packet.WriteGuid("Character Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED)]
        public static void HandleXPGainAborted(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 0, 2, 7, 4, 5, 1, 3, 6);

            packet.ReadInt32("Unk Int32 1");
            packet.ReadInt32("Unk Int32 2");

            packet.ParseBitStream(guid, 6, 0, 3, 7, 2, 5, 1, 4);

            packet.ReadInt32("Unk Int32 3");

            packet.WriteGuid("Guid", guid);
        }
    }
}
