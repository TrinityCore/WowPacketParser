using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;
using WowPacketParser.Store;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_STANDSTATECHANGE)]
        public static void HandleStandStateChange(Packet packet)
        {
            packet.ReadInt32("Standstate");
        }

        [Parser(Opcode.SMSG_STANDSTATE_UPDATE)]
        public static void HandleStandStateUpdate(Packet packet)
        {
            packet.ReadByte("Standstate");
        }

        [Parser(Opcode.CMSG_CHAR_CREATE)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadEnum<Race>("Race", TypeCode.Byte);
            packet.ReadEnum<Class>("Class", TypeCode.Byte);
            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
            packet.ReadByte("Outfit Id");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_CHAR_RENAME)]
        public static void HandleClientCharRename(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("New Name");
        }

        [Parser(Opcode.SMSG_CHAR_RENAME)]
        public static void HandleServerCharRename(Packet packet)
        {
            if (packet.ReadEnum<ResponseCode>("Race", TypeCode.Byte) != ResponseCode.RESPONSE_SUCCESS)
                return;

            packet.ReadGuid("GUID");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_CHAR_CREATE)]
        [Parser(Opcode.SMSG_CHAR_DELETE)]
        public static void HandleCharResponse(Packet packet)
        {
            packet.ReadEnum<ResponseCode>("Response", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_ALTER_APPEARANCE)]
        public static void HandleAlterAppearance(Packet packet)
        {
            // In some ancient version, this could be ReadByte
            packet.ReadInt32("Hair Style");
            packet.ReadInt32("Hair Color");
            packet.ReadInt32("Facial Hair");
            packet.ReadInt32("Skin Color");
        }

        [Parser(Opcode.SMSG_BARBER_SHOP_RESULT)]
        public static void HandleBarberShopResult(Packet packet)
        {
            packet.ReadEnum<BarberShopResult>("Result", TypeCode.Int32);
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("New Name");
            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE)]
        public static void HandleServerCharCustomize(Packet packet)
        {
            if (packet.ReadEnum<ResponseCode>("Response", TypeCode.Byte) != ResponseCode.RESPONSE_SUCCESS)
                return;

            packet.ReadGuid("GUID");
            packet.ReadCString("Name");
            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
        }

        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            var count = packet.ReadByte("Count");

            for (var i = 0; i < count; i++)
            {
                var guid = packet.ReadGuid("GUID");
                var name = packet.ReadCString("Name");
                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte);
                var clss = packet.ReadEnum<Class>("Class", TypeCode.Byte);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte);

                packet.ReadByte("Skin");
                packet.ReadByte("Face");
                packet.ReadByte("Hair Style");
                packet.ReadByte("Hair Color");
                packet.ReadByte("Facial Hair");

                var level = packet.ReadByte("Level");
                var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id");
                var mapId = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");

                var pos = packet.ReadVector3("Position");
                packet.ReadInt32("Guild Id");
                packet.ReadEnum<CharacterFlag>("Character Flags", TypeCode.Int32);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    packet.ReadEnum<CustomizationFlag>("Customization Flags", TypeCode.Int32);

                var firstLogin = packet.ReadBoolean("First Login");
                packet.ReadInt32("Pet Display Id");
                packet.ReadInt32("Pet Level");
                packet.ReadEnum<CreatureFamily>("Pet Family", TypeCode.Int32);

                for (var j = 0; j < 19; j++)
                {
                    packet.ReadInt32("Equip Display Id");
                    packet.ReadEnum<InventoryType>("Equip Inventory Type", TypeCode.Byte);
                    packet.ReadInt32("Equip Aura Id");
                }

                int bagCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685) ? 4 : 1;
                for (var j = 0; j < bagCount; j++)
                {
                    packet.ReadInt32("Bag Display Id");
                    packet.ReadEnum<InventoryType>("Bag Inventory Type", TypeCode.Byte);
                    packet.ReadInt32("Bag Aura Id");
                }

                if (firstLogin)
                {
                    var startPos = new StartPosition {Map = mapId, Position = pos, Zone = zone};
                    packet.SniffFileInfo.Storage.StartPositions.TryAdd(new Tuple<Race, Class>(race, clss), startPos);
                }

                var playerInfo = new Player {Race = race, Class = clss, Name = name, FirstLogin = firstLogin, Level = level};
                packet.SniffFileInfo.Storage.Objects.AddOrUpdate(guid, playerInfo);
            }
        }

        [Parser(Opcode.SMSG_CHAR_ENUM, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleCharEnum422(Packet packet)
        {
            packet.ReadByte("Unk Flag");
            int count = packet.ReadInt32("Char Count");
            packet.ReadInt32("Unk Count");

            var bits = new bool[count, 17];

            for (int c = 0; c < count; c++)
                for (int j = 0; j < 17; j++)
                    bits[c, j] = packet.ReadBit();

            for (int c = 0; c < count; c++)
            {
                var low = new byte[8];
                var guild = new byte[8];
                var name = packet.ReadCString("Name", c);

                if (bits[c, 0])
                    guild[5] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("Face", c);
                var mapId = packet.ReadInt32("Map", c);

                if (bits[c, 12])
                    low[1] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 1])
                    low[4] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 10])
                    guild[4] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 15])
                    guild[0] = (byte)(packet.ReadByte() ^ 1);

                var pos = packet.ReadVector3("Position", c);

                if (bits[c, 11])
                    low[0] = (byte)(packet.ReadByte() ^ 1);

                var zone = packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id", c);
                packet.ReadInt32("Pet Level", c);

                if (bits[c, 8])
                    low[3] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 14])
                    low[7] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("Facial Hair", c);
                packet.ReadByte("Skin", c);
                var clss = packet.ReadEnum<Class>("Class", TypeCode.Byte, c);
                packet.ReadInt32("Pet Family", c);
                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);

                if (bits[c, 9])
                    low[2] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadInt32("Pet Display ID", c);

                if (bits[c, 3])
                    guild[7] = (byte)(packet.ReadByte() ^ 1);

                var level = packet.ReadByte("Level", c);

                if (bits[c, 7])
                    low[6] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("Hair Style", c);

                if (bits[c, 13])
                    guild[2] = (byte)(packet.ReadByte() ^ 1);

                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c);
                packet.ReadByte("Hair Color", c);

                if (bits[c, 5])
                    guild[6] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c);

                if (bits[c, 6])
                    low[5] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 2])
                    guild[3] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("List Order", c);

                for (int itm = 0; itm < 19; itm++)
                {
                    packet.ReadInt32("Item EnchantID", c, itm);
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                }

                for (int itm = 0; itm < 4; itm++)
                {
                    packet.ReadInt32("Bag EnchantID", c, itm);
                    packet.ReadEnum<InventoryType>("Bag InventoryType", TypeCode.Byte, c, itm);
                    packet.ReadInt32("Bag DisplayID", c, itm);
                }

                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c);

                if (bits[c, 4])
                    guild[1] = (byte)(packet.ReadByte() ^ 1);

                var playerGuid = new Guid(BitConverter.ToUInt64(low, 0));

                packet.WriteLine("[{0}] Character GUID: {1}", c, playerGuid);
                packet.WriteLine("[{0}] Guild GUID: {1}", c, new Guid(BitConverter.ToUInt64(guild, 0)));

                var firstLogin = bits[c, 16];
                if (firstLogin)
                {
                    var startPos = new StartPosition {Map = mapId, Position = pos, Zone = zone};

                    packet.SniffFileInfo.Storage.StartPositions.TryAdd(new Tuple<Race, Class>(race, clss), startPos);
                }

                var playerInfo = new Player { Race = race, Class = clss, Name = name, FirstLogin = firstLogin, Level = level };
                packet.SniffFileInfo.Storage.Objects.AddOrUpdate(playerGuid, playerInfo);
            }
        }

        [Parser(Opcode.SMSG_CHAR_ENUM, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleCharEnum430(Packet packet)
        {
            var count = packet.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (var c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                guildGuids[c][2] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][2] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][6] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][5] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][4] = (byte)(packet.ReadBit() ? 1 : 0);
                guildGuids[c][4] = (byte)(packet.ReadBit() ? 1 : 0);
                guildGuids[c][3] = (byte)(packet.ReadBit() ? 1 : 0);
                guildGuids[c][7] = (byte)(packet.ReadBit() ? 1 : 0);
                nameLenghts[c] = packet.ReadBits(7);
                guildGuids[c][0] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][0] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][3] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][1] = (byte)(packet.ReadBit() ? 1 : 0);
                firstLogins[c] = packet.ReadBit();
                guildGuids[c][5] = (byte)(packet.ReadBit() ? 1 : 0);
                charGuids[c][7] = (byte)(packet.ReadBit() ? 1 : 0);
                guildGuids[c][6] = (byte)(packet.ReadBit() ? 1 : 0);
                guildGuids[c][1] = (byte)(packet.ReadBit() ? 1 : 0);
            }

            var unkCounter = packet.ReadBits("Unk Counter", 23);
            packet.ReadBit(); // no idea, not used in client

            for (int c = 0; c < count; ++c)
            {
                for (var itm = 0; itm < 19; ++itm)
                {
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                    packet.ReadInt32("Item EnchantID", c, itm);
                }

                for (var itm = 0; itm < 4; ++itm)
                {
                    packet.ReadEnum<InventoryType>("Bag InventoryType", TypeCode.Byte, c, itm);
                    packet.ReadInt32("Bag DisplayID", c, itm);
                    packet.ReadInt32("Bag EnchantID", c, itm);
                }

                if (guildGuids[c][0] != 0)
                    guildGuids[c][0] ^= packet.ReadByte();
                if (guildGuids[c][1] != 0)
                    guildGuids[c][1] ^= packet.ReadByte();

                packet.ReadByte("Face", c);
                packet.ReadInt32("Pet Display ID", c);
                if (guildGuids[c][7] != 0)
                    guildGuids[c][7] ^= packet.ReadByte();

                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c);
                var level = packet.ReadByte("Level", c);
                packet.ReadInt32("Pet Level", c);
                var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", c);
                var y = packet.ReadSingle("Position Y", c);
                packet.ReadInt32("Pet Family", c);
                packet.ReadByte("Hair Style", c);
                if (charGuids[c][1] != 0)
                    charGuids[c][1] ^= packet.ReadByte();

                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                if (charGuids[c][0] != 0)
                    charGuids[c][0] ^= packet.ReadByte();

                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c);
                packet.ReadByte("List Order", c);
                if (charGuids[c][7] != 0)
                    charGuids[c][7] ^= packet.ReadByte();

                var z = packet.ReadSingle("Position Z", c);
                var mapId = packet.ReadInt32("Map", c);
                if (guildGuids[c][4] != 0)
                    guildGuids[c][4] ^= packet.ReadByte();

                packet.ReadByte("Hair Color", c);
                if (charGuids[c][3] != 0)
                    charGuids[c][3] ^= packet.ReadByte();

                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);
                packet.ReadByte("Skin", c);
                if (charGuids[c][4] != 0)
                    charGuids[c][4] ^= packet.ReadByte();
                if (charGuids[c][5] != 0)
                    charGuids[c][5] ^= packet.ReadByte();
                if (guildGuids[c][5] != 0)
                    guildGuids[c][5] ^= packet.ReadByte();

                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c);
                var x = packet.ReadSingle("Position X", c);
                packet.ReadByte("Facial Hair", c);
                if (charGuids[c][6] != 0)
                    charGuids[c][6] ^= packet.ReadByte();
                if (guildGuids[c][3] != 0)
                    guildGuids[c][3] ^= packet.ReadByte();
                if (charGuids[c][2] != 0)
                    charGuids[c][2] ^= packet.ReadByte();

                var clss = packet.ReadEnum<Class>("Class", TypeCode.Byte, c);
                if (guildGuids[c][6] != 0)
                    guildGuids[c][6] ^= packet.ReadByte();
                if (guildGuids[c][2] != 0)
                    guildGuids[c][2] ^= packet.ReadByte();

                var playerGuid = new Guid(BitConverter.ToUInt64(charGuids[c], 0));

                packet.WriteLine("[{0}] Character GUID: {1}", c, playerGuid);
                packet.WriteLine("[{0}] Guild GUID: {1}", c, new Guid(BitConverter.ToUInt64(guildGuids[c], 0)));

                if (firstLogins[c])
                {
                    var startPos = new StartPosition();
                    startPos.Map = mapId;
                    startPos.Position = new Vector3(x, y, z);
                    startPos.Zone = zone;

                    packet.SniffFileInfo.Storage.StartPositions.TryAdd(new Tuple<Race, Class>(race, clss), startPos);
                }

                var playerInfo = new Player{Race = race, Class = clss, Name = name, FirstLogin = firstLogins[c], Level = level};
                packet.SniffFileInfo.Storage.Objects.AddOrUpdate(playerGuid, playerInfo);
            }

            for (var c = 0; c < unkCounter; c++)
                packet.WriteLine("Unk Loop: {0}, {1}", packet.ReadUInt32(), packet.ReadByte());
        }

        [Parser(Opcode.SMSG_COMPRESSED_CHAR_ENUM)]
        public static void HandleCompressedCharEnum(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                    HandleCharEnum430(packet2);
                else
                    HandleCharEnum422(packet2);
            }
        }

        [Parser(Opcode.SMSG_PLAYER_VEHICLE_DATA)]
        public static void HandlePlayerVehicleData(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt32("Vehicle Id");
        }

        [Parser(Opcode.CMSG_PLAYED_TIME)]
        [Parser(Opcode.SMSG_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PLAYED_TIME))
            {
                packet.ReadInt32("Time Played");
                packet.ReadInt32("Total");
            }
            packet.ReadBoolean("Print in chat");
        }

        [Parser(Opcode.SMSG_LOG_XPGAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Total XP");
            var type = packet.ReadByte("XP type"); // Need enum

            if (type == 0) // kill
            {
                packet.ReadUInt32("Base XP");
                packet.ReadSingle("Group rate (unk)");
            }

            packet.ReadBoolean("RAF Bonus");
        }

        [Parser(Opcode.SMSG_TITLE_EARNED)]
        public static void HandleTitleEarned(Packet packet)
        {
            packet.ReadUInt32("Title Id");
            packet.ReadUInt32("Earned?"); // vs lost
        }

        [Parser(Opcode.CMSG_SET_TITLE)]
        public static void HandleSetTitle(Packet packet)
        {
            packet.ReadUInt32("Title Id");
        }

        [Parser(Opcode.SMSG_INIT_CURRENCY, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleInitCurrency422(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            var bits = new bool[count, 3];

            for (var i = 0; i < count; ++i)
                for (var j = 0; j < 3; ++j)
                    bits[i, j] = packet.ReadBit();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Currency Id", i);
                if (bits[i, 0])
                    packet.ReadInt32("Weekly Cap", i);

                packet.ReadInt32("Total Count", i);
                packet.ReadByte("Unk Byte1", i);

                if (bits[i, 1])
                    packet.ReadInt32("Season Total Earned?", i);

                if (bits[i, 2])
                    packet.ReadUInt32("Week Count", i);
            }
        }

        [Parser(Opcode.SMSG_INIT_CURRENCY, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Week Count", i);
                packet.ReadByte("Unk Byte", i);
                packet.ReadUInt32("Currency ID", i);
                packet.ReadTime("Reset Time", i);
                packet.ReadUInt32("Week Cap", i);
                packet.ReadInt32("Total Count", i);
            }
        }
    }
}
