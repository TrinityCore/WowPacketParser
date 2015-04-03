using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_STAND_STATE_CHANGE)]
        public static void HandleStandStateChange(Packet packet)
        {
            packet.ReadInt32E<StandState>("StandState");
        }

        [Parser(Opcode.SMSG_STAND_STATE_UPDATE)]
        public static void HandleStandStateUpdate(Packet packet)
        {
            packet.ReadByteE<StandState>("State");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadByteE<Race>("Race");
            packet.ReadByteE<Class>("Class");
            packet.ReadByteE<Gender>("Gender");
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

        [Parser(Opcode.CMSG_CHARACTER_RENAME_REQUEST)]
        public static void HandleClientCharRename(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("New Name");
        }

        [Parser(Opcode.SMSG_CHARACTER_RENAME_RESULT)]
        public static void HandleServerCharRename(Packet packet)
        {
            if (packet.ReadByteE<ResponseCode>("Race") != ResponseCode.RESPONSE_SUCCESS)
                return;

            var guid = packet.ReadGuid("GUID");
            var name = packet.ReadCString("Name");
            StoreGetters.AddName(guid, name);
        }

        [Parser(Opcode.SMSG_CREATE_CHAR)]
        [Parser(Opcode.SMSG_DELETE_CHAR)]
        public static void HandleCharResponse(Packet packet)
        {
            packet.ReadByteE<ResponseCode>("Response");
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
            packet.ReadInt32E<BarberShopResult>("Result");
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("New Name");
            packet.ReadByteE<Gender>("Gender");
            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE)]
        public static void HandleServerCharCustomize(Packet packet)
        {
            if (packet.ReadByteE<ResponseCode>("Response") != ResponseCode.RESPONSE_SUCCESS)
                return;

            var guid = packet.ReadGuid("GUID");
            var name = packet.ReadCString("Name");

            StoreGetters.AddName(guid, name);

            packet.ReadByteE<Gender>("Gender");
            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleCharEnum(Packet packet)
        {
            var count = packet.ReadByte("Count");

            for (var i = 0; i < count; i++)
            {
                var guid = packet.ReadGuid("GUID");
                var name = packet.ReadCString("Name");
                StoreGetters.AddName(guid, name);
                var race = packet.ReadByteE<Race>("Race");
                var clss = packet.ReadByteE<Class>("Class");
                packet.ReadByteE<Gender>("Gender");

                packet.ReadByte("Skin");
                packet.ReadByte("Face");
                packet.ReadByte("Hair Style");
                packet.ReadByte("Hair Color");
                packet.ReadByte("Facial Hair");

                var level = packet.ReadByte("Level");
                var zone = packet.ReadUInt32<ZoneId>("Zone Id");
                var mapId = packet.ReadInt32<MapId>("Map Id");

                var pos = packet.ReadVector3("Position");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    packet.ReadGuid("Guild GUID");
                else
                    packet.ReadInt32("Guild Id");
                packet.ReadInt32E<CharacterFlag>("Character Flags");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    packet.ReadInt32E<CustomizationFlag>("Customization Flags");

                var firstLogin = packet.ReadBool("First Login");
                packet.ReadInt32("Pet Display Id");
                packet.ReadInt32("Pet Level");
                packet.ReadInt32E<CreatureFamily>("Pet Family");

                for (var j = 0; j < 19; j++)
                {
                    packet.ReadInt32("Equip Display Id");
                    packet.ReadByteE<InventoryType>("Equip Inventory Type");
                    packet.ReadInt32("Equip Aura Id");
                }

                int bagCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685) ? 4 : 1;
                for (var j = 0; j < bagCount; j++)
                {
                    packet.ReadInt32("Bag Display Id");
                    packet.ReadByteE<InventoryType>("Bag Inventory Type");
                    packet.ReadInt32("Bag Aura Id");
                }

                if (firstLogin)
                {
                    var startPos = new StartPosition {Map = (uint) mapId, Position = pos, Zone = zone};
                    Storage.StartPositions.Add(new Tuple<Race, Class>(race, clss), startPos, packet.TimeSpan);
                }

                var playerInfo = new Player {Race = race, Class = clss, Name = name, FirstLogin = firstLogin, Level = level};

                if (Storage.Objects.ContainsKey(guid))
                    Storage.Objects[guid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(guid, playerInfo, packet.TimeSpan);
                StoreGetters.AddName(guid, name);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleCharEnum422(Packet packet)
        {
            packet.ReadByte("Unk Flag");
            int count = packet.ReadInt32("Char Count");
            packet.ReadInt32("Unk Count");
            var firstLogin = new bool[count];
            var playerGuid = new byte[count][];
            var guildGuid = new byte[count][];

            for (int c = 0; c < count; c++)
            {
                playerGuid[c] = new byte[8];
                guildGuid[c] = new byte[8];

                guildGuid[c][5] = packet.ReadBit();//0
                playerGuid[c][4] = packet.ReadBit();//1
                guildGuid[c][3] = packet.ReadBit();//2
                guildGuid[c][7] = packet.ReadBit();//3
                guildGuid[c][1] = packet.ReadBit();//4
                guildGuid[c][6] = packet.ReadBit();//5
                playerGuid[c][5] = packet.ReadBit();//6
                playerGuid[c][6] = packet.ReadBit();//7
                playerGuid[c][3] = packet.ReadBit();//8
                playerGuid[c][2] = packet.ReadBit();//9
                guildGuid[c][4] = packet.ReadBit();//10
                playerGuid[c][0] = packet.ReadBit();//11
                playerGuid[c][1] = packet.ReadBit();//12
                guildGuid[c][2] = packet.ReadBit();//13
                playerGuid[c][7] = packet.ReadBit();//14
                guildGuid[c][0] = packet.ReadBit();//15
                firstLogin[c] = packet.ReadBit();//16
            }

            for (int c = 0; c < count; c++)
            {
                var name = packet.ReadCString("Name", c);

                packet.ReadXORByte(guildGuid[c], 5);

                packet.ReadByte("Face", c);
                var mapId = packet.ReadInt32("Map", c);

                packet.ReadXORByte(playerGuid[c], 1);
                packet.ReadXORByte(playerGuid[c], 4);
                packet.ReadXORByte(guildGuid[c], 4);
                packet.ReadXORByte(guildGuid[c], 0);

                var pos = packet.ReadVector3("Position", c);

                packet.ReadXORByte(playerGuid[c], 0);

                var zone = packet.ReadInt32<ZoneId>("Zone Id", c);
                packet.ReadInt32("Pet Level", c);

                packet.ReadXORByte(playerGuid[c], 3);

                packet.ReadXORByte(playerGuid[c], 7);

                packet.ReadByte("Facial Hair", c);
                packet.ReadByte("Skin", c);
                var clss = packet.ReadByteE<Class>("Class", c);
                packet.ReadInt32("Pet Family", c);
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);

                packet.ReadXORByte(playerGuid[c], 2);

                packet.ReadInt32("Pet Display ID", c);

                packet.ReadXORByte(guildGuid[c], 7);

                var level = packet.ReadByte("Level", c);

                packet.ReadXORByte(playerGuid[c], 6);

                packet.ReadByte("Hair Style", c);

                packet.ReadXORByte(guildGuid[c], 2);

                var race = packet.ReadByteE<Race>("Race", c);
                packet.ReadByte("Hair Color", c);

                packet.ReadXORByte(guildGuid[c], 6);

                packet.ReadByteE<Gender>("Gender", c);

                packet.ReadXORByte(playerGuid[c], 5);

                packet.ReadXORByte(guildGuid[c], 3);

                packet.ReadByte("List Order", c);

                for (int itm = 0; itm < 19; itm++)
                {
                    packet.ReadInt32("Item EnchantID", c, itm);
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                }

                for (int itm = 0; itm < 4; itm++)
                {
                    packet.ReadInt32("Bag EnchantID", c, itm);
                    packet.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                    packet.ReadInt32("Bag DisplayID", c, itm);
                }

                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);

                packet.ReadXORByte(guildGuid[c], 1);

                var guidPlayer = new WowGuid64(BitConverter.ToUInt64(playerGuid[c], 0));

                packet.WriteGuid("Character Guid", playerGuid[c],c);
                packet.WriteGuid("Guild Guid", guildGuid[c],c);


                if (firstLogin[c])
                {
                    var startPos = new StartPosition {Map = (uint) mapId, Position = pos, Zone = (uint) zone};

                    Storage.StartPositions.Add(new Tuple<Race, Class>(race, clss), startPos, packet.TimeSpan);
                }

                var playerInfo = new Player { Race = race, Class = clss, Name = name, FirstLogin = firstLogin[c], Level = level };
                if (Storage.Objects.ContainsKey(guidPlayer))
                    Storage.Objects[guidPlayer] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(guidPlayer, playerInfo, packet.TimeSpan);
                StoreGetters.AddName(guidPlayer, name);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
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

                guildGuids[c][2] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
                guildGuids[c][4] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(7);
                guildGuids[c][0] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
            }

            var unkCounter = packet.ReadBits("Unk Counter", 23);
            packet.ReadBit(); // no idea, not used in client

            for (int c = 0; c < count; ++c)
            {
                for (var itm = 0; itm < 19; ++itm)
                {
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                    packet.ReadInt32("Item EnchantID", c, itm);
                }

                for (var itm = 0; itm < 4; ++itm)
                {
                    packet.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                    packet.ReadInt32("Bag DisplayID", c, itm);
                    packet.ReadInt32("Bag EnchantID", c, itm);
                }

                packet.ReadXORByte(guildGuids[c], 0);
                packet.ReadXORByte(guildGuids[c], 1);

                packet.ReadByte("Face", c);
                packet.ReadInt32("Pet Display ID", c);
                packet.ReadXORByte(guildGuids[c], 7);

                packet.ReadByteE<Gender>("Gender", c);
                var level = packet.ReadByte("Level", c);
                packet.ReadInt32("Pet Level", c);
                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);
                var y = packet.ReadSingle("Position Y", c);
                packet.ReadInt32("Pet Family", c);
                packet.ReadByte("Hair Style", c);
                packet.ReadXORByte(charGuids[c], 1);

                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.ReadXORByte(charGuids[c], 0);

                var race = packet.ReadByteE<Race>("Race", c);
                packet.ReadByte("List Order", c);
                packet.ReadXORByte(charGuids[c], 7);

                var z = packet.ReadSingle("Position Z", c);
                var mapId = packet.ReadInt32("Map", c);
                packet.ReadXORByte(guildGuids[c], 4);

                packet.ReadByte("Hair Color", c);
                packet.ReadXORByte(charGuids[c], 3);

                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                packet.ReadByte("Skin", c);
                packet.ReadXORByte(charGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 5);
                packet.ReadXORByte(guildGuids[c], 5);

                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                var x = packet.ReadSingle("Position X", c);
                packet.ReadByte("Facial Hair", c);
                packet.ReadXORByte(charGuids[c], 6);
                packet.ReadXORByte(guildGuids[c], 3);
                packet.ReadXORByte(charGuids[c], 2);

                var clss = packet.ReadByteE<Class>("Class", c);
                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(guildGuids[c], 2);

                var playerGuid = new WowGuid64(BitConverter.ToUInt64(charGuids[c], 0));

                packet.WriteGuid("Character GUID", charGuids[c], c);
                packet.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    var startPos = new StartPosition { Map = (uint) mapId, Position = new Vector3(x, y, z), Zone = zone };

                    Storage.StartPositions.Add(new Tuple<Race, Class>(race, clss), startPos, packet.TimeSpan);
                }

                var playerInfo = new Player{Race = race, Class = clss, Name = name, FirstLogin = firstLogins[c], Level = level};
                if (Storage.Objects.ContainsKey(playerGuid))
                    Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
                StoreGetters.AddName(playerGuid, name);
            }

            for (var c = 0; c < unkCounter; c++)
            {
                packet.ReadUInt32("Unk1", c);
                packet.ReadByte("Unk2", c);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCharEnum433(Packet packet)
        {
            var unkCounter = packet.ReadBits("Unk Counter", 23);
            var count = packet.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (var c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];
                //100%  pozition, and flag
                //%50   flag
                //20    nothing

                charGuids[c][0] = packet.ReadBit(); //100%
                guildGuids[c][0] = packet.ReadBit();//50%
                charGuids[c][2] = packet.ReadBit(); //100%
                guildGuids[c][2] = packet.ReadBit();//50%
                firstLogins[c] = packet.ReadBit();                  //100%
                charGuids[c][3] = packet.ReadBit(); //100%
                charGuids[c][6] = packet.ReadBit(); //100%
                guildGuids[c][2] = packet.ReadBit();//20%

                charGuids[c][4] = packet.ReadBit(); //20%
                charGuids[c][5] = packet.ReadBit(); //20%
                nameLenghts[c] = packet.ReadBits(4);                //100%
                guildGuids[c][3] = packet.ReadBit();//20%
                guildGuids[c][4] = packet.ReadBit();//50%

                guildGuids[c][5] = packet.ReadBit();//20%
                charGuids[c][1] = packet.ReadBit(); //100%
                packet.ReadBit();                                   //20%
                guildGuids[c][6] = packet.ReadBit();//20%
                charGuids[c][7] = packet.ReadBit(); //100%
                guildGuids[c][7] = packet.ReadBit();//50%
                packet.ReadBit();                                   //20%
                packet.ReadBit();                                   //20%
            }

            // no idea, not used in client
            packet.ReadByte();

            for (int c = 0; c < count; ++c)
            {
                for (var itm = 0; itm < 19; ++itm)
                {
                    packet.ReadInt32("Item EnchantID", c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                }

                for (var itm = 0; itm < 4; ++itm)
                {
                    packet.ReadInt32("Bag EnchantID", c, itm);
                    packet.ReadInt32("Bag DisplayID", c, itm);
                    packet.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                }

                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);
                packet.ReadInt32("Pet Level", c);
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);

                packet.ReadByte("Facial Hair", c);

                packet.ReadXORByte(guildGuids[c], 0);
                    packet.ReadXORByte(charGuids[c], 0);

                packet.ReadXORByte(charGuids[c], 2);
                if (guildGuids[c][2] != 0)
                    //  guildGuids[c][2] ^= packet.ReadByte();

                    if (charGuids[c][7] != 0)
                        charGuids[c][7] ^= packet.ReadByte();
                if (guildGuids[c][7] != 0)
                    // guildGuids[c][7] ^= packet.ReadByte();

                    packet.ReadByte("List Order", c);
                packet.ReadInt32("Pet Display ID", c);

                // no ideal //////////////////////////////
                if (charGuids[c][4] != 0)
                    charGuids[c][4] ^= packet.ReadByte();

                if (guildGuids[c][4] != 0)
                    // guildGuids[c][4] ^= packet.ReadByte();

                if (charGuids[c][5] != 0)
                        // charGuids[c][5] ^= packet.ReadByte();

                if (guildGuids[c][5] != 0)
                            // guildGuids[c][5] ^= packet.ReadByte();

                if (guildGuids[c][1] != 0)
                                // guildGuids[c][1] ^= packet.ReadByte();

                                if (guildGuids[c][3] != 0)
                                    // guildGuids[c][3] ^= packet.ReadByte();

                                    if (guildGuids[c][6] != 0)
                                        // guildGuids[c][6] ^= packet.ReadByte();

                                        //////////////////////////////////////////

                                        if (charGuids[c][3] != 0)
                                            charGuids[c][3] ^= packet.ReadByte();

                var clss = packet.ReadByteE<Class>("Class", c);

                if (charGuids[c][6] != 0)
                    charGuids[c][6] ^= packet.ReadByte();

                var x = packet.ReadSingle("Position X", c);

                if (charGuids[c][1] != 0)
                    charGuids[c][1] ^= packet.ReadByte();

                var race = packet.ReadByteE<Race>("Race", c);
                packet.ReadInt32("Pet Family", c);
                var y = packet.ReadSingle("Position Y", c);
                packet.ReadByteE<Gender>("Gender", c);
                packet.ReadByte("Hair Style", c);
                var level = packet.ReadByte("Level", c);
                var z = packet.ReadSingle("Position Z", c);
                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                packet.ReadByte("Skin", c);
                packet.ReadByte("Hair Color", c);
                packet.ReadByte("Face", c);
                var mapId = packet.ReadInt32("Map", c);
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);

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

            for (var c = 0; c < unkCounter; c++)
            {
                packet.ReadUInt32("Unk1", c);
                packet.ReadByte("Unk2", c);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleCharEnum434(Packet packet)
        {
            var unkCounter = packet.ReadBits("Unk Counter", 23);
            packet.ReadBit("Unk bit");
            var count = packet.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (var c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][3] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(7);
                charGuids[c][4] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                guildGuids[c][4] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
            }

            for (int c = 0; c < count; ++c)
            {
                var clss = packet.ReadByteE<Class>("Class", c);

                for (var itm = 0; itm < 19; ++itm)
                {
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                    packet.ReadInt32("Item EnchantID", c, itm);
                }

                for (var itm = 0; itm < 4; ++itm)
                {
                    packet.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                    packet.ReadInt32("Bag DisplayID", c, itm);
                    packet.ReadInt32("Bag EnchantID", c, itm);
                }

                packet.ReadInt32("Pet Family", c);

                packet.ReadXORByte(guildGuids[c], 2);

                packet.ReadByte("List Order", c);
                packet.ReadByte("Hair Style", c);
                packet.ReadXORByte(guildGuids[c], 3);

                packet.ReadInt32("Pet Display ID", c);
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                packet.ReadByte("Hair Color", c);

                packet.ReadXORByte(charGuids[c], 4);

                var mapId = packet.ReadInt32("Map", c);
                packet.ReadXORByte(guildGuids[c], 5);

                var z = packet.ReadSingle("Position Z", c);
                packet.ReadXORByte(guildGuids[c], 6);

                packet.ReadInt32("Pet Level", c);

                packet.ReadXORByte(charGuids[c], 3);

                var y = packet.ReadSingle("Position Y", c);

                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                packet.ReadByte("Facial Hair", c);

                packet.ReadXORByte(charGuids[c], 7);

                packet.ReadByteE<Gender>("Gender", c);
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.ReadByte("Face", c);

                packet.ReadXORByte(charGuids[c], 0);

                packet.ReadXORByte(charGuids[c], 2);

                packet.ReadXORByte(guildGuids[c], 1);

                packet.ReadXORByte(guildGuids[c], 7);

                var x = packet.ReadSingle("Position X", c);
                packet.ReadByte("Skin", c);
                var race = packet.ReadByteE<Race>("Race", c);
                var level = packet.ReadByte("Level", c);
                packet.ReadXORByte(charGuids[c], 6);

                packet.ReadXORByte(guildGuids[c], 4);

                packet.ReadXORByte(guildGuids[c], 0);

                packet.ReadXORByte(charGuids[c], 5);

                packet.ReadXORByte(charGuids[c], 1);

                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);

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

            for (var i = 0; i < unkCounter; ++i)
            {
                packet.ReadByte("Unk byte", i);
                packet.ReadUInt32("Unk int", i);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V5_0_5_16048, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleCharEnum505(Packet packet)
        {
            packet.ReadBits("Unk Counter", 23);
            packet.ReadBit("Unk bit");
            var count = packet.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (var c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][4] = packet.ReadBit();

                guildGuids[c][7] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();

                firstLogins[c] = packet.ReadBit();

                charGuids[c][6] = packet.ReadBit();

                guildGuids[c][6] = packet.ReadBit();

                charGuids[c][1] = packet.ReadBit();

                nameLenghts[c] = packet.ReadBits(7);

                guildGuids[c][2] = packet.ReadBit();

                charGuids[c][2] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();

                guildGuids[c][4] = packet.ReadBit();

                charGuids[c][7] = packet.ReadBit();

                guildGuids[c][5] = packet.ReadBit();
            }

            packet.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                for (var itm = 0; itm < 23; ++itm)
                {
                    packet.ReadInt32("Item EnchantID", c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                }

                packet.ReadByte("Hair Style", c);

                var race = packet.ReadByteE<Race>("Race", c);

                packet.ReadXORByte(charGuids[c], 0);
                packet.ReadXORByte(guildGuids[c], 4);

                packet.ReadByte("Facial Hair", c);
                packet.ReadByte("Hair Color", c);

                var z = packet.ReadSingle("Position Z", c);

                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(charGuids[c], 7);
                packet.ReadXORByte(guildGuids[c], 0);

                packet.ReadInt32E<CharacterFlag>("Character Flags", c);
                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);

                packet.ReadXORByte(charGuids[c], 5);
                packet.ReadXORByte(charGuids[c], 6);

                packet.ReadInt32E<CustomizationFlag>("Customization Flags", c);
                var mapId = packet.ReadInt32<MapId>("Map Id", c);

                packet.ReadXORByte(charGuids[c], 1);

                packet.ReadInt32("Pet Display Id", c);

                packet.ReadXORByte(guildGuids[c], 1);

                packet.ReadByte("Face", c);
                packet.ReadInt32E<CreatureFamily>("Pet Family", c);
                packet.ReadByte("Skin", c);

                packet.ReadXORByte(charGuids[c], 4);

                packet.ReadXORByte(guildGuids[c], 5);

                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);

                packet.ReadInt32("Pet Level", c);
                packet.ReadByte("Gender", c);
                var x = packet.ReadSingle("Position X", c);
                var clss = packet.ReadByteE<Class>("Class", c);
                packet.ReadByte("Unk 8", c);
                var y = packet.ReadSingle("Position Y", c);

                packet.ReadXORByte(guildGuids[c], 3);
                packet.ReadXORByte(guildGuids[c], 7);
                packet.ReadXORByte(guildGuids[c], 2);

                var level = packet.ReadByte("Level", c);

                packet.ReadXORByte(charGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 3);

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

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleCharEnum510(Packet packet)
        {
            var unkCounter = packet.ReadBits("Unk Counter", 23);
            var count = packet.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][7] = packet.ReadBit();
                charGuids[c][0] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(7);
                guildGuids[c][0] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][4] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
            }

            packet.ReadBit("Unk bit");
            packet.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                packet.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                packet.ReadInt32("Pet Family", c);
                var z = packet.ReadSingle("Position Z", c);
                packet.ReadXORByte(charGuids[c], 7);
                packet.ReadXORByte(guildGuids[c], 6);

                for (var itm = 0; itm < 23; ++itm)
                {
                    packet.ReadInt32("Item EnchantID", c, itm);
                    packet.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                }

                var x = packet.ReadSingle("Position X", c);
                var clss = packet.ReadByteE<Class>("Class", c);
                packet.ReadXORByte(charGuids[c], 5);
                var y = packet.ReadSingle("Position Y", c);
                packet.ReadXORByte(guildGuids[c], 3);
                packet.ReadXORByte(charGuids[c], 6);
                packet.ReadInt32("Pet Level", c);
                packet.ReadInt32("Pet Display ID", c);
                packet.ReadXORByte(charGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 1);
                packet.ReadByte("Hair Color", c);
                packet.ReadByte("Facial Hair", c);
                packet.ReadXORByte(guildGuids[c], 2);
                var zone = packet.ReadUInt32<ZoneId>("Zone Id", c);
                packet.ReadByte("List Order", c);
                packet.ReadXORByte(charGuids[c], 0);
                packet.ReadXORByte(guildGuids[c], 1);
                packet.ReadByte("Skin", c);
                packet.ReadXORByte(charGuids[c], 4);
                packet.ReadXORByte(guildGuids[c], 5);
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.ReadXORByte(guildGuids[c], 0);
                var level = packet.ReadByte("Level", c);
                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadXORByte(guildGuids[c], 7);
                packet.ReadByte("Hair Style", c);
                packet.ReadXORByte(guildGuids[c], 4);
                packet.ReadByteE<Gender>("Gender", c);
                var mapId = packet.ReadInt32<MapId>("Map Id", c);
                packet.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                var race = packet.ReadByteE<Race>("Race", c);
                packet.ReadByte("Face", c);

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

            for (var i = 0; i < unkCounter; ++i)
            {
                packet.ReadByte("Unk byte", i);
                packet.ReadUInt32("Unk int", i);
            }
        }

        [Parser(Opcode.SMSG_COMPRESSED_CHAR_ENUM)]
        public static void HandleCompressedCharEnum(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
            {
                switch (ClientVersion.Build)
                {
                    case ClientVersionBuild.V4_3_4_15595:
                        HandleCharEnum434(packet2);
                        break;
                    case ClientVersionBuild.V4_3_3_15354:
                        HandleCharEnum433(packet2);
                        break;
                    case ClientVersionBuild.V4_3_0_15005:
                        HandleCharEnum430(packet2);
                        break;
                    case ClientVersionBuild.V4_2_2_14545:
                        HandleCharEnum422(packet2);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        [Parser(Opcode.SMSG_PLAYER_VEHICLE_DATA)]
        public static void HandlePlayerVehicleData(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt32("Vehicle Id");
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        [Parser(Opcode.SMSG_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PLAYED_TIME, Direction.ServerToClient))
            {
                packet.ReadInt32("Time Played");
                packet.ReadInt32("Total");
            }
            packet.ReadBool("Print in chat");
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
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

            packet.ReadBool("RAF Bonus");
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

        [Parser(Opcode.SMSG_SETUP_CURRENCY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInitCurrency434(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];
            for (var i = 0; i < count; ++i)
            {
                hasWeekCount[i] = packet.ReadBit();
                flags[i] = packet.ReadBits(4);
                hasWeekCap[i] = packet.ReadBit();
                hasSeasonTotal[i] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i);
                packet.ReadUInt32("Currency count", i);
                if (hasWeekCap[i])
                    packet.ReadUInt32("Weekly cap", i);

                if (hasSeasonTotal[i])
                    packet.ReadUInt32("Season total earned", i);

                packet.ReadUInt32("Currency id", i);
                if (hasWeekCount[i])
                    packet.ReadUInt32("Weekly count", i);
            }
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
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

        [Parser(Opcode.SMSG_SETUP_CURRENCY, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
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

        [Parser(Opcode.SMSG_UPDATE_CURRENCY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleUpdateCurrency(Packet packet)
        {
            packet.ReadUInt32("Currency ID");
            packet.ReadUInt32("Week Count");
            packet.ReadUInt32("Total Count");
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleUpdateCurrency430(Packet packet)
        {
            packet.ReadInt32("Currency ID");
            packet.ReadInt32("Total Count");

            var hasSeasonCount = packet.ReadBit();
            var hasWeekCap = packet.ReadBit();
            packet.ReadBit("Print in log");

            if (hasWeekCap)
                packet.ReadInt32("Week Count");

            if (hasSeasonCount)
                packet.ReadInt32("Season Total Earned");
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleUpdateCurrency434(Packet packet)
        {
            var hasWeekCap = packet.ReadBit();
            var hasSeasonCount = packet.ReadBit();
            packet.ReadBit("Print in log");

            if (hasSeasonCount)
                packet.ReadInt32("Season Total Earned");

            packet.ReadInt32("Total Count");
            packet.ReadInt32("Currency ID");

            if (hasWeekCap)
                packet.ReadInt32("Week Count");
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY_WEEK_LIMIT)]
        public static void HandleUpdateCurrencyWeekLimit(Packet packet)
        {
            packet.ReadUInt32("Week Cap");
            packet.ReadUInt32("Currency ID");
        }

        [Parser(Opcode.CMSG_SET_CURRENCY_FLAGS)]
        public static void HandleSetCurrencyFlags(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadUInt32("Currency ID");
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleXPGainAborted430(Packet packet)
        {
            packet.ReadInt32("Unk Int32 1");
            packet.ReadInt32("Unk Int32 2");
            packet.ReadInt32("Unk Int32 3");

            var guid = packet.StartBitStream(5, 7, 4, 2, 3, 1, 6, 0);
            packet.ParseBitStream(guid, 0, 6, 1, 2, 4, 7, 3, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED, ClientVersionBuild.V4_3_4_15595)] // 4.3.4, related to EVENT_TRIAL_CAP_REACHED_LEVEL
        public static void HandleXPGainAborted434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 0, 1, 2, 6, 7, 5, 3);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);

            packet.ReadInt32("Unk Int32 1");

            packet.ReadXORByte(guid, 6);

            packet.ReadInt32("Unk Int32 2");

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);

            packet.ReadInt32("Unk Int32 3");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_FAILED_PLAYER_CONDITION)]
        public static void HandleFailedPlayerCondition(Packet packet)
        {
            packet.ReadInt32("Id");
        }

        [Parser(Opcode.CMSG_SHOWING_CLOAK)]
        [Parser(Opcode.CMSG_SHOWING_HELM)]
        public static void HandleShowingCloakAndHelm434(Packet packet)
        {
            packet.ReadBool("Showing");
        }

        [Parser(Opcode.CMSG_AUTO_DECLINE_GUILD_INVITES)]
        public static void HandleAutoDeclineGuildInvites434(Packet packet)
        {
            packet.ReadBool("Auto decline");
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)] // 4.3.4
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.ReadBits("Count", 10);

            var guids = new byte[count][];

            for (int i = 0; i < count; ++i)
                guids[i] = packet.StartBitStream(1, 4, 5, 3, 0, 7, 6, 2);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 5);
                packet.ReadXORByte(guids[i], 1);
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 3);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 7);

                packet.WriteGuid("Character Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUp(Packet packet)
        {
            packet.ReadInt32("Level");
            packet.ReadInt32("Health");

            var powerCount = 5;
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                powerCount = 7;
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                powerCount = 5;

            // TODO: Exclude happiness on Cata
            for (var i = 0; i < powerCount; i++)
                packet.ReadInt32("Power", (PowerType) i);

            for (var i = 0; i < 5; i++)
                packet.ReadInt32("Stat", (StatType)i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.ReadInt32("Talent Level"); // 0 - No Talent gain / 1 - Talent Point gain
        }

        [Parser(Opcode.SMSG_HEALTH_UPDATE)]
        public static void HandleHealthUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadUInt32("Value");
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            var count = 1;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadByteE<PowerType>("Power type"); // Actually powertype for class
                packet.ReadInt32("Value");
            }
        }

        [Parser(Opcode.CMSG_ENUM_CHARACTERS)]
        [Parser(Opcode.CMSG_HEARTH_AND_RESURRECT)]
        [Parser(Opcode.CMSG_SELF_RES)]
        public static void HandleCharacterNull(Packet packet)
        {
        }
    }
}
