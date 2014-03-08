using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_CHAR_ENUM)]
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

                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c);

                packet.ReadXORByte(charGuids[c], 5);
                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 6);

                packet.ReadUInt32("Unk 1", c);
                var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", c);

                packet.ReadXORByte(guildGuids[c], 3);

                packet.ReadInt32("Pet Level", c); // v4+112
                packet.ReadInt32("Pet Display ID", c); //v4+108
                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c);
                packet.ReadUInt32("Unk 2", c);

                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadXORByte(charGuids[c], 0);

                packet.ReadByte("Facial Hair", c);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c);

                packet.ReadXORByte(guildGuids[c], 0);

                packet.ReadByte("HairStyle", c);
                var level = packet.ReadByte("Level", c);

                for (int j = 0; j < 23; ++j)
                {
                    packet.ReadInt32("Item DisplayID", c, j);
                    packet.ReadInt32("Item EnchantID", c, j);
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, j);
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
                var Class = packet.ReadEnum<Class>("Class", TypeCode.Byte, c);

                packet.ReadXORByte(guildGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 2);

                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);

                packet.ReadXORByte(charGuids[c], 7);
                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(charGuids[c], 4);

                var playerGuid = new Guid(BitConverter.ToUInt64(charGuids[c], 0));

                packet.WriteLine("[{0}] Position: {1}", c, pos);
                packet.WriteGuid("Character GUID", charGuids[c], c);
                packet.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    var startPos = new StartPosition();
                    startPos.Map = mapId;
                    startPos.Position = pos;
                    startPos.Zone = zone;

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

        [Parser(Opcode.SMSG_INIT_CURRENCY)]
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
                packet.WriteLine("[{0}] Flags {1}", i, flags[i]); // 20h
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

        [Parser(Opcode.CMSG_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            packet.ReadBoolean("Print in chat");
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
                packet.ReadEnum<PowerType>("Power type", TypeCode.Byte); // Actually powertype for class
                packet.ReadInt32("Value");
            }

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_LOG_XPGAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];

            var hasBaseXP = !packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit("Unk Bit");
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            var hasGroupRate = packet.ReadBit();
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
    }
}
