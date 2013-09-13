using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_CHAR_ENUM)]
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
                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);
                var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", c);
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
                var clss = packet.ReadEnum<Class>("Class", TypeCode.Byte, c); // v4+59
                packet.ReadInt32("Pet Display ID", c); //v4+108
                packet.ReadByte("List Order", c); //v4+57
                packet.ReadByte("Facial Hair", c); // v4+65
                var z = packet.ReadSingle("Position Z", c); //v4+84
                packet.ReadXORByte(guildGuids[c], 3);
                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c); //v4+58
                packet.ReadXORByte(charGuids[c], 4);

                for (int j = 0; j < 23; ++j)
                {
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, j);
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
                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c); //v4+100
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c); //v4+60
                var mapId = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id", c); //v4+72

                for (var i = 0; i < count2; ++i)
                {
                    packet.ReadUInt32("unk1");
                    packet.ReadByte("unk2");
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
    }
}