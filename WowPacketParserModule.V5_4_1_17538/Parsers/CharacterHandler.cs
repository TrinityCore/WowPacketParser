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

            playerGuid[1] = packet.Translator.ReadBit();
            playerGuid[4] = packet.Translator.ReadBit();
            playerGuid[7] = packet.Translator.ReadBit();
            playerGuid[5] = packet.Translator.ReadBit();
            playerGuid[3] = packet.Translator.ReadBit();
            playerGuid[2] = packet.Translator.ReadBit();
            playerGuid[0] = packet.Translator.ReadBit();
            playerGuid[6] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(playerGuid, 2, 0, 4, 1, 5, 3, 7, 6);

            var guid = new WowGuid64(BitConverter.ToUInt64(playerGuid, 0));
            packet.Translator.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleCharEnum(Packet packet)
        {
            var count = packet.Translator.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];


                guildGuids[c][3] = packet.Translator.ReadBit();
                firstLogins[c] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();
                charGuids[c][1] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                guildGuids[c][6] = packet.Translator.ReadBit();
                charGuids[c][7] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(6);
                charGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][3] = packet.Translator.ReadBit();
                guildGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();

            }

            packet.Translator.ReadBit("Unk bit");
            var count2 = packet.Translator.ReadBits("RIDBIT21", 21);

            packet.Translator.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                Vector3 pos = new Vector3();

                packet.Translator.ReadByte("Skin", c); //v4+61
                packet.Translator.ReadXORByte(charGuids[c], 2);
                packet.Translator.ReadXORByte(charGuids[c], 7);
                packet.Translator.ReadInt32("Pet Display ID", c); //v4+108
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c); // v4 + 8

                for (int j = 0; j < 23; ++j)
                {
                    packet.Translator.ReadInt32("Item DisplayID", c, j);
                    packet.Translator.ReadInt32("Item EnchantID", c, j);
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, j);
                }

                packet.Translator.ReadXORByte(charGuids[c], 4);
                packet.Translator.ReadXORByte(charGuids[c], 6);

                var level = packet.Translator.ReadByte("Level", c); // v4+66
                pos.Y = packet.Translator.ReadSingle("Position Y", c); // v4+80
                pos.X = packet.Translator.ReadSingle("Position X", c); //v4+76
                packet.Translator.ReadByte("Face", c); // v4+62
                packet.Translator.ReadXORByte(guildGuids[c], 0);
                packet.Translator.ReadByte("List Order", c); //v4+57
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);
                packet.Translator.ReadXORByte(guildGuids[c], 7);
                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                var mapId = packet.Translator.ReadInt32<MapId>("Map Id", c); //v4+72
                var race = packet.Translator.ReadByteE<Race>("Race", c); //v4+58
                pos.Z = packet.Translator.ReadSingle("Position Z", c); //v4+84
                packet.Translator.ReadXORByte(guildGuids[c], 1);
                packet.Translator.ReadByteE<Gender>("Gender", c); //v4+60
                packet.Translator.ReadXORByte(charGuids[c], 3);
                packet.Translator.ReadByte("Hair Color", c); // v4+64
                packet.Translator.ReadXORByte(guildGuids[c], 5);
                var klass = packet.Translator.ReadByteE<Class>("Class", c); // v4+59
                packet.Translator.ReadXORByte(guildGuids[c], 2);
                packet.Translator.ReadXORByte(charGuids[c], 1);
                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c); //v4+100
                packet.Translator.ReadByte("Facial Hair", c); // v4+65
                packet.Translator.ReadXORByte(guildGuids[c], 6);
                packet.Translator.ReadXORByte(charGuids[c], 0);
                packet.Translator.ReadByte("Hair Style", c); // v4+63
                packet.Translator.ReadXORByte(charGuids[c], 5);
                packet.Translator.ReadInt32("Pet Family", c); // v4+116
                packet.Translator.ReadXORByte(guildGuids[c], 2);
                packet.Translator.ReadInt32("Pet Level", c); // v4+112
                packet.Translator.ReadXORByte(guildGuids[c], 4);

                for (var i = 0; i < count2; ++i)
                {
                    packet.Translator.ReadByte("unk2", i);
                    packet.Translator.ReadUInt32("unk1", i);
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
    }
}