using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleCharEnum(Packet packet)
        {
            //var unkCounter = packet.Translator.ReadBits("Unk Counter", 21);
            packet.Translator.ReadBit("Unk bit");
            var count = packet.Translator.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][1] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();
                guildGuids[c][6] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                guildGuids[c][4] = packet.Translator.ReadBit();
                charGuids[c][7] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(6);
                firstLogins[c] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();
                charGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][2] = packet.Translator.ReadBit();
                guildGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
                charGuids[c][3] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
            }

            packet.Translator.ReadBits("RIDBIT21", 21);
            packet.Translator.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                Vector3 pos = new Vector3();

                packet.Translator.ReadXORByte(charGuids[c], 4);
                var race = packet.Translator.ReadByteE<Race>("Race", c);
                packet.Translator.ReadXORByte(charGuids[c], 6);
                packet.Translator.ReadXORByte(guildGuids[c], 1);
                packet.Translator.ReadByte("List Order", c);
                packet.Translator.ReadByte("Hair Style", c);
                packet.Translator.ReadXORByte(guildGuids[c], 6);
                packet.Translator.ReadXORByte(charGuids[c], 3);
                pos.X = packet.Translator.ReadSingle("Position X", c);
                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                packet.Translator.ReadXORByte(guildGuids[c], 0);
                packet.Translator.ReadInt32("Pet Level", c);
                var mapId = packet.Translator.ReadInt32<MapId>("Map Id", c);
                packet.Translator.ReadXORByte(guildGuids[c], 7);
                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                packet.Translator.ReadXORByte(guildGuids[c], 4);
                packet.Translator.ReadXORByte(charGuids[c], 2);
                packet.Translator.ReadXORByte(charGuids[c], 5);
                pos.Y = packet.Translator.ReadSingle("Position Y", c);
                packet.Translator.ReadInt32("Pet Family", c);
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.Translator.ReadInt32("Pet Display ID", c);
                packet.Translator.ReadXORByte(guildGuids[c], 3);
                packet.Translator.ReadXORByte(charGuids[c], 7);
                var level = packet.Translator.ReadByte("Level", c);
                packet.Translator.ReadXORByte(charGuids[c], 1);
                packet.Translator.ReadXORByte(guildGuids[c], 2);

                for (int j = 0; j < 23; ++j)
                {
                    packet.Translator.ReadInt32("Item EnchantID", c, j);
                    packet.Translator.ReadInt32("Item DisplayID", c, j);
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, j);
                }

                pos.Z = packet.Translator.ReadSingle("Position Z", c);
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);
                packet.Translator.ReadByte("Facial Hair", c);
                var klass = packet.Translator.ReadByteE<Class>("Class", c);
                packet.Translator.ReadXORByte(guildGuids[c], 5);
                packet.Translator.ReadByte("Skin", c);
                packet.Translator.ReadByteE<Gender>("Gender", c);
                packet.Translator.ReadByte("Face", c);
                packet.Translator.ReadXORByte(charGuids[c], 0);
                packet.Translator.ReadByte("Hair Color", c);

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

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.Translator.ReadByte("Hair Style");
            packet.Translator.ReadByte("Face");
            packet.Translator.ReadByte("Facial Hair");
            packet.Translator.ReadByte("Hair Color");
            packet.Translator.ReadByteE<Race>("Race");
            packet.Translator.ReadByteE<Class>("Class");
            packet.Translator.ReadByte("Skin");
            packet.Translator.ReadByteE<Gender>("Gender");
            packet.Translator.ReadByte("Outfit Id");

            var nameLength = packet.Translator.ReadBits(6);
            var unk = packet.Translator.ReadBit("unk");
            packet.Translator.ReadWoWString("Name", (int)nameLength);
            if (unk)
                packet.Translator.ReadUInt32("unk20");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            playerGuid[2] = packet.Translator.ReadBit();
            playerGuid[1] = packet.Translator.ReadBit();
            playerGuid[5] = packet.Translator.ReadBit();
            playerGuid[7] = packet.Translator.ReadBit();
            playerGuid[6] = packet.Translator.ReadBit();

            packet.Translator.ReadBit();

            playerGuid[3] = packet.Translator.ReadBit();
            playerGuid[0] = packet.Translator.ReadBit();
            playerGuid[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(playerGuid, 1);
            packet.Translator.ReadXORByte(playerGuid, 3);
            packet.Translator.ReadXORByte(playerGuid, 4);
            packet.Translator.ReadXORByte(playerGuid, 0);
            packet.Translator.ReadXORByte(playerGuid, 7);
            packet.Translator.ReadXORByte(playerGuid, 2);
            packet.Translator.ReadXORByte(playerGuid, 5);
            packet.Translator.ReadXORByte(playerGuid, 6);

            packet.Translator.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.StartBitStream(guid, 1, 0, 3, 7);
            var hasBaseXP = !packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 4, 2, 6, 5);
            var hasGroupRate = !packet.Translator.ReadBit();
            packet.Translator.ReadBit("RAF Bonus");
            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORBytes(guid, 5, 2);

            if (hasBaseXP)
                packet.Translator.ReadUInt32("Base XP");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadUInt32("Total XP");
            packet.Translator.ReadXORBytes(guid, 6, 0, 3);
            packet.Translator.ReadByte("XP type");

            if (hasGroupRate)
                packet.Translator.ReadSingle("Group rate");
            packet.Translator.ReadXORBytes(guid, 1, 7);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TITLE_EARNED)]
        [Parser(Opcode.SMSG_TITLE_LOST)]
        public static void HandleServerTitle(Packet packet)
        {
            packet.Translator.ReadInt32("Index");
        }
    }
}
