using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            //var unkCounter = packet.ReadBits("Unk Counter", 21);
            packet.ReadBit("Unk bit");
            var count = packet.ReadBits("Char count", 16);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                guildGuids[c][4] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(6);
                firstLogins[c] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
            }

            packet.ReadBits("RIDBIT21", 21);
            packet.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                packet.ReadXORByte(charGuids[c], 4);
                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c);
                packet.ReadXORByte(charGuids[c], 6);
                packet.ReadXORByte(guildGuids[c], 1);
                packet.ReadByte("List Order", c);
                packet.ReadByte("Hair Style", c);
                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(charGuids[c], 3);
                var x = packet.ReadSingle("Position X", c);
                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);
                packet.ReadXORByte(guildGuids[c], 0);
                packet.ReadInt32("Pet Level", c);
                var mapId = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id", c);
                packet.ReadXORByte(guildGuids[c], 7);
                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c);
                packet.ReadXORByte(guildGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 5);
                var y = packet.ReadSingle("Position Y", c);
                packet.ReadInt32("Pet Family", c);
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.ReadInt32("Pet Display ID", c);
                packet.ReadXORByte(guildGuids[c], 3);
                packet.ReadXORByte(charGuids[c], 7);
                var level = packet.ReadByte("Level", c);
                packet.ReadXORByte(charGuids[c], 1);
                packet.ReadXORByte(guildGuids[c], 2);

                for (int j = 0; j < 23; ++j)
                {
                    packet.ReadInt32("Item EnchantID", c, j);
                    packet.ReadInt32("Item DisplayID", c, j);
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, j);
                }

                var z = packet.ReadSingle("Position Z", c);
                var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", c);
                packet.ReadByte("Facial Hair", c);
                var clss = packet.ReadEnum<Class>("Class", TypeCode.Byte, c);
                packet.ReadXORByte(guildGuids[c], 5);
                packet.ReadByte("Skin", c);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c);
                packet.ReadByte("Face", c);
                packet.ReadXORByte(charGuids[c], 0);
                packet.ReadByte("Hair Color", c);

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

        [Parser(Opcode.CMSG_CHAR_CREATE)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.ReadByte("Hair Style");
            packet.ReadByte("Face");
            packet.ReadByte("Facial Hair");
            packet.ReadByte("Hair Color");
            packet.ReadEnum<Race>("Race", TypeCode.Byte);
            packet.ReadEnum<Class>("Class", TypeCode.Byte);
            packet.ReadByte("Skin");
            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadByte("Outfit Id");

            var nameLength = packet.ReadBits(6);
            var unk = packet.ReadBit("unk");
            packet.ReadWoWString("Name", (int)nameLength);
            if (unk)
                packet.ReadUInt32("unk20");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var playerGuid = new byte[8];

            playerGuid[2] = packet.ReadBit();
            playerGuid[1] = packet.ReadBit();
            playerGuid[5] = packet.ReadBit();
            playerGuid[7] = packet.ReadBit();
            playerGuid[6] = packet.ReadBit();

            var unknown = packet.ReadBit();

            playerGuid[3] = packet.ReadBit();
            playerGuid[0] = packet.ReadBit();
            playerGuid[4] = packet.ReadBit();

            packet.ReadXORByte(playerGuid, 1);
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(playerGuid, 4);
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(playerGuid, 6);

            var guid = new Guid(BitConverter.ToUInt64(playerGuid, 0));
            packet.WriteGuid("GUID", playerGuid);
        }

        [Parser(Opcode.SMSG_LOG_XPGAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 1, 0, 3, 7);
            var hasBaseXP = !packet.ReadBit();
            packet.StartBitStream(guid, 4, 2, 6, 5);
            var hasGroupRate = !packet.ReadBit();
            packet.ReadBit("RAF Bonus");
            packet.ResetBitReader();

            packet.ReadXORBytes(guid, 5, 2);

            if (hasBaseXP)
                packet.ReadUInt32("Base XP");
            packet.ReadXORByte(guid, 4);
            packet.ReadUInt32("Total XP");
            packet.ReadXORBytes(guid, 6, 0, 3);
            packet.ReadByte("XP type");

            if (hasGroupRate)
                packet.ReadSingle("Group rate");
            packet.ReadXORBytes(guid, 1, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TITLE_EARNED)]
        [Parser(Opcode.SMSG_TITLE_REMOVED)]
        public static void HandleServerTitle(Packet packet)
        {
            packet.ReadUInt32("Title Id");
        }
    }
}
