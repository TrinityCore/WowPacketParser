using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

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

            var guid = new WowGuid64(BitConverter.ToUInt64(playerGuid, 0));
            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
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
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, j);
                }

                packet.ReadXORByte(charGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 6);

                var level = packet.ReadByte("Level", c); // v4+66
                var y = packet.ReadSingle("Position Y", c); // v4+80
                var x = packet.ReadSingle("Position X", c); //v4+76
                packet.ReadByte("Face", c); // v4+62
                packet.ReadXORByte(guildGuids[c], 0);
                packet.ReadByte("List Order", c); //v4+57
                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);
                packet.ReadXORByte(guildGuids[c], 7);
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var mapId = packet.ReadInt32<MapId>("Map Id", c); //v4+72
                var race = packet.ReadByteE<Race>("Race", c); //v4+58
                var z = packet.ReadSingle("Position Z", c); //v4+84
                packet.ReadXORByte(guildGuids[c], 1);
                packet.ReadByteE<Gender>("Gender", c); //v4+60
                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadByte("Hair Color", c); // v4+64
                packet.ReadXORByte(guildGuids[c], 5);
                var clss = packet.ReadByteE<Class>("Class", c); // v4+59
                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 1);
                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c); //v4+100
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
                    packet.ReadByte("unk2", i);
                    packet.ReadUInt32("unk1", i);
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
    }
}