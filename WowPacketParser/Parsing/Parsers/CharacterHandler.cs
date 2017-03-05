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
            packet.Translator.ReadInt32E<StandState>("StandState");
        }

        [Parser(Opcode.SMSG_STAND_STATE_UPDATE)]
        public static void HandleStandStateUpdate(Packet packet)
        {
            packet.Translator.ReadByteE<StandState>("State");
        }

        [Parser(Opcode.CMSG_CREATE_CHARACTER)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.Translator.ReadCString("Name");
            packet.Translator.ReadByteE<Race>("Race");
            packet.Translator.ReadByteE<Class>("Class");
            packet.Translator.ReadByteE<Gender>("Gender");
            packet.Translator.ReadByte("Skin");
            packet.Translator.ReadByte("Face");
            packet.Translator.ReadByte("Hair Style");
            packet.Translator.ReadByte("Hair Color");
            packet.Translator.ReadByte("Facial Hair");
            packet.Translator.ReadByte("Outfit Id");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_CHARACTER_RENAME_REQUEST)]
        public static void HandleClientCharRename(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadCString("New Name");
        }

        [Parser(Opcode.SMSG_CHARACTER_RENAME_RESULT)]
        public static void HandleServerCharRename(Packet packet)
        {
            if (packet.Translator.ReadByteE<ResponseCode>("Race") != ResponseCode.RESPONSE_SUCCESS)
                return;

            var guid = packet.Translator.ReadGuid("GUID");
            var name = packet.Translator.ReadCString("Name");
            StoreGetters.AddName(guid, name);
        }

        [Parser(Opcode.SMSG_CREATE_CHAR)]
        [Parser(Opcode.SMSG_DELETE_CHAR)]
        public static void HandleCharResponse(Packet packet)
        {
            packet.Translator.ReadByteE<ResponseCode>("Response");
        }

        [Parser(Opcode.CMSG_ALTER_APPEARANCE)]
        public static void HandleAlterAppearance(Packet packet)
        {
            // In some ancient version, this could be ReadByte
            packet.Translator.ReadInt32("Hair Style");
            packet.Translator.ReadInt32("Hair Color");
            packet.Translator.ReadInt32("Facial Hair");
            packet.Translator.ReadInt32("Skin Color");
        }

        [Parser(Opcode.SMSG_BARBER_SHOP_RESULT)]
        public static void HandleBarberShopResult(Packet packet)
        {
            packet.Translator.ReadInt32E<BarberShopResult>("Result");
        }

        [Parser(Opcode.CMSG_CHAR_CUSTOMIZE)]
        public static void HandleClientCharCustomize(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadCString("New Name");
            packet.Translator.ReadByteE<Gender>("Gender");
            packet.Translator.ReadByte("Skin");
            packet.Translator.ReadByte("Face");
            packet.Translator.ReadByte("Hair Style");
            packet.Translator.ReadByte("Hair Color");
            packet.Translator.ReadByte("Facial Hair");
        }

        [Parser(Opcode.SMSG_CHAR_CUSTOMIZE)]
        public static void HandleServerCharCustomize(Packet packet)
        {
            if (packet.Translator.ReadByteE<ResponseCode>("Response") != ResponseCode.RESPONSE_SUCCESS)
                return;

            var guid = packet.Translator.ReadGuid("GUID");
            var name = packet.Translator.ReadCString("Name");

            StoreGetters.AddName(guid, name);

            packet.Translator.ReadByteE<Gender>("Gender");
            packet.Translator.ReadByte("Skin");
            packet.Translator.ReadByte("Face");
            packet.Translator.ReadByte("Hair Style");
            packet.Translator.ReadByte("Hair Color");
            packet.Translator.ReadByte("Facial Hair");
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleCharEnum(Packet packet)
        {
            var count = packet.Translator.ReadByte("Count");

            for (var i = 0; i < count; i++)
            {
                var guid = packet.Translator.ReadGuid("GUID");
                var name = packet.Translator.ReadCString("Name");
                StoreGetters.AddName(guid, name);
                var race = packet.Translator.ReadByteE<Race>("Race");
                var klass = packet.Translator.ReadByteE<Class>("Class");
                packet.Translator.ReadByteE<Gender>("Gender");

                packet.Translator.ReadByte("Skin");
                packet.Translator.ReadByte("Face");
                packet.Translator.ReadByte("Hair Style");
                packet.Translator.ReadByte("Hair Color");
                packet.Translator.ReadByte("Facial Hair");

                var level = packet.Translator.ReadByte("Level");
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id");
                var mapId = packet.Translator.ReadInt32<MapId>("Map Id");

                var pos = packet.Translator.ReadVector3("Position");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_3_13329))
                    packet.Translator.ReadGuid("Guild GUID");
                else
                    packet.Translator.ReadInt32("Guild Id");
                packet.Translator.ReadInt32E<CharacterFlag>("Character Flags");

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                    packet.Translator.ReadInt32E<CustomizationFlag>("Customization Flags");

                var firstLogin = packet.Translator.ReadBool("First Login");
                packet.Translator.ReadInt32("Pet Display Id");
                packet.Translator.ReadInt32("Pet Level");
                packet.Translator.ReadInt32E<CreatureFamily>("Pet Family");

                for (var j = 0; j < 19; j++)
                {
                    packet.Translator.ReadInt32("Equip Display Id");
                    packet.Translator.ReadByteE<InventoryType>("Equip Inventory Type");
                    packet.Translator.ReadInt32("Equip Aura Id");
                }

                int bagCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685) ? 4 : 1;
                for (var j = 0; j < bagCount; j++)
                {
                    packet.Translator.ReadInt32("Bag Display Id");
                    packet.Translator.ReadByteE<InventoryType>("Bag Inventory Type");
                    packet.Translator.ReadInt32("Bag Aura Id");
                }

                if (firstLogin)
                {
                    PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = (uint)mapId, Zone = zone, Position = pos, Orientation = 0 };
                    Storage.StartPositions.Add(startPos, packet.TimeSpan);
                }

                var playerInfo = new Player {Race = race, Class = klass, Name = name, FirstLogin = firstLogin, Level = level};

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
            packet.Translator.ReadByte("Unk Flag");
            int count = packet.Translator.ReadInt32("Char Count");
            packet.Translator.ReadInt32("Unk Count");
            var firstLogin = new bool[count];
            var playerGuid = new byte[count][];
            var guildGuid = new byte[count][];

            for (int c = 0; c < count; c++)
            {
                playerGuid[c] = new byte[8];
                guildGuid[c] = new byte[8];

                guildGuid[c][5] = packet.Translator.ReadBit();//0
                playerGuid[c][4] = packet.Translator.ReadBit();//1
                guildGuid[c][3] = packet.Translator.ReadBit();//2
                guildGuid[c][7] = packet.Translator.ReadBit();//3
                guildGuid[c][1] = packet.Translator.ReadBit();//4
                guildGuid[c][6] = packet.Translator.ReadBit();//5
                playerGuid[c][5] = packet.Translator.ReadBit();//6
                playerGuid[c][6] = packet.Translator.ReadBit();//7
                playerGuid[c][3] = packet.Translator.ReadBit();//8
                playerGuid[c][2] = packet.Translator.ReadBit();//9
                guildGuid[c][4] = packet.Translator.ReadBit();//10
                playerGuid[c][0] = packet.Translator.ReadBit();//11
                playerGuid[c][1] = packet.Translator.ReadBit();//12
                guildGuid[c][2] = packet.Translator.ReadBit();//13
                playerGuid[c][7] = packet.Translator.ReadBit();//14
                guildGuid[c][0] = packet.Translator.ReadBit();//15
                firstLogin[c] = packet.Translator.ReadBit();//16
            }

            for (int c = 0; c < count; c++)
            {
                var name = packet.Translator.ReadCString("Name", c);

                packet.Translator.ReadXORByte(guildGuid[c], 5);

                packet.Translator.ReadByte("Face", c);
                var mapId = packet.Translator.ReadInt32("Map", c);

                packet.Translator.ReadXORByte(playerGuid[c], 1);
                packet.Translator.ReadXORByte(playerGuid[c], 4);
                packet.Translator.ReadXORByte(guildGuid[c], 4);
                packet.Translator.ReadXORByte(guildGuid[c], 0);

                var pos = packet.Translator.ReadVector3("Position", c);

                packet.Translator.ReadXORByte(playerGuid[c], 0);

                var zone = packet.Translator.ReadInt32<ZoneId>("Zone Id", c);
                packet.Translator.ReadInt32("Pet Level", c);

                packet.Translator.ReadXORByte(playerGuid[c], 3);

                packet.Translator.ReadXORByte(playerGuid[c], 7);

                packet.Translator.ReadByte("Facial Hair", c);
                packet.Translator.ReadByte("Skin", c);
                var klass = packet.Translator.ReadByteE<Class>("Class", c);
                packet.Translator.ReadInt32("Pet Family", c);
                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);

                packet.Translator.ReadXORByte(playerGuid[c], 2);

                packet.Translator.ReadInt32("Pet Display ID", c);

                packet.Translator.ReadXORByte(guildGuid[c], 7);

                var level = packet.Translator.ReadByte("Level", c);

                packet.Translator.ReadXORByte(playerGuid[c], 6);

                packet.Translator.ReadByte("Hair Style", c);

                packet.Translator.ReadXORByte(guildGuid[c], 2);

                var race = packet.Translator.ReadByteE<Race>("Race", c);
                packet.Translator.ReadByte("Hair Color", c);

                packet.Translator.ReadXORByte(guildGuid[c], 6);

                packet.Translator.ReadByteE<Gender>("Gender", c);

                packet.Translator.ReadXORByte(playerGuid[c], 5);

                packet.Translator.ReadXORByte(guildGuid[c], 3);

                packet.Translator.ReadByte("List Order", c);

                for (int itm = 0; itm < 19; itm++)
                {
                    packet.Translator.ReadInt32("Item EnchantID", c, itm);
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.Translator.ReadInt32("Item DisplayID", c, itm);
                }

                for (int itm = 0; itm < 4; itm++)
                {
                    packet.Translator.ReadInt32("Bag EnchantID", c, itm);
                    packet.Translator.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                    packet.Translator.ReadInt32("Bag DisplayID", c, itm);
                }

                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);

                packet.Translator.ReadXORByte(guildGuid[c], 1);

                var guidPlayer = new WowGuid64(BitConverter.ToUInt64(playerGuid[c], 0));

                packet.Translator.WriteGuid("Character Guid", playerGuid[c],c);
                packet.Translator.WriteGuid("Guild Guid", guildGuid[c],c);


                if (firstLogin[c])
                {
                    PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = (uint)mapId, Zone = (uint)zone, Position = pos, Orientation = 0 };
                    Storage.StartPositions.Add(startPos, packet.TimeSpan);
                }

                var playerInfo = new Player { Race = race, Class = klass, Name = name, FirstLogin = firstLogin[c], Level = level };
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
            var count = packet.Translator.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (var c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                guildGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(7);
                guildGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][3] = packet.Translator.ReadBit();
                charGuids[c][1] = packet.Translator.ReadBit();
                firstLogins[c] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][7] = packet.Translator.ReadBit();
                guildGuids[c][6] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();
            }

            var unkCounter = packet.Translator.ReadBits("Unk Counter", 23);
            packet.Translator.ReadBit(); // no idea, not used in client

            for (int c = 0; c < count; ++c)
            {
                Vector3 pos = new Vector3();

                for (var itm = 0; itm < 19; ++itm)
                {
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.Translator.ReadInt32("Item DisplayID", c, itm);
                    packet.Translator.ReadInt32("Item EnchantID", c, itm);
                }

                for (var itm = 0; itm < 4; ++itm)
                {
                    packet.Translator.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                    packet.Translator.ReadInt32("Bag DisplayID", c, itm);
                    packet.Translator.ReadInt32("Bag EnchantID", c, itm);
                }

                packet.Translator.ReadXORByte(guildGuids[c], 0);
                packet.Translator.ReadXORByte(guildGuids[c], 1);

                packet.Translator.ReadByte("Face", c);
                packet.Translator.ReadInt32("Pet Display ID", c);
                packet.Translator.ReadXORByte(guildGuids[c], 7);

                packet.Translator.ReadByteE<Gender>("Gender", c);
                var level = packet.Translator.ReadByte("Level", c);
                packet.Translator.ReadInt32("Pet Level", c);
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);
                pos.Y = packet.Translator.ReadSingle("Position Y", c);
                packet.Translator.ReadInt32("Pet Family", c);
                packet.Translator.ReadByte("Hair Style", c);
                packet.Translator.ReadXORByte(charGuids[c], 1);

                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.Translator.ReadXORByte(charGuids[c], 0);

                var race = packet.Translator.ReadByteE<Race>("Race", c);
                packet.Translator.ReadByte("List Order", c);
                packet.Translator.ReadXORByte(charGuids[c], 7);

                pos.Z = packet.Translator.ReadSingle("Position Z", c);
                var mapId = packet.Translator.ReadInt32("Map", c);
                packet.Translator.ReadXORByte(guildGuids[c], 4);

                packet.Translator.ReadByte("Hair Color", c);
                packet.Translator.ReadXORByte(charGuids[c], 3);

                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                packet.Translator.ReadByte("Skin", c);
                packet.Translator.ReadXORByte(charGuids[c], 4);
                packet.Translator.ReadXORByte(charGuids[c], 5);
                packet.Translator.ReadXORByte(guildGuids[c], 5);

                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                pos.X = packet.Translator.ReadSingle("Position X", c);
                packet.Translator.ReadByte("Facial Hair", c);
                packet.Translator.ReadXORByte(charGuids[c], 6);
                packet.Translator.ReadXORByte(guildGuids[c], 3);
                packet.Translator.ReadXORByte(charGuids[c], 2);

                var klass = packet.Translator.ReadByteE<Class>("Class", c);
                packet.Translator.ReadXORByte(guildGuids[c], 6);
                packet.Translator.ReadXORByte(guildGuids[c], 2);

                var playerGuid = new WowGuid64(BitConverter.ToUInt64(charGuids[c], 0));

                packet.Translator.WriteGuid("Character GUID", charGuids[c], c);
                packet.Translator.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    PlayerCreateInfo startPos = new PlayerCreateInfo { Race = race, Class = klass, Map = (uint)mapId, Zone = zone, Position = pos, Orientation = 0 };
                    Storage.StartPositions.Add(startPos, packet.TimeSpan);
                }

                var playerInfo = new Player{Race = race, Class = klass, Name = name, FirstLogin = firstLogins[c], Level = level};
                if (Storage.Objects.ContainsKey(playerGuid))
                    Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
                StoreGetters.AddName(playerGuid, name);
            }

            for (var c = 0; c < unkCounter; c++)
            {
                packet.Translator.ReadUInt32("Unk1", c);
                packet.Translator.ReadByte("Unk2", c);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCharEnum433(Packet packet)
        {
            var unkCounter = packet.Translator.ReadBits("Unk Counter", 23);
            var count = packet.Translator.ReadBits("Char count", 17);

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

                charGuids[c][0] = packet.Translator.ReadBit(); //100%
                guildGuids[c][0] = packet.Translator.ReadBit();//50%
                charGuids[c][2] = packet.Translator.ReadBit(); //100%
                guildGuids[c][2] = packet.Translator.ReadBit();//50%
                firstLogins[c] = packet.Translator.ReadBit();                  //100%
                charGuids[c][3] = packet.Translator.ReadBit(); //100%
                charGuids[c][6] = packet.Translator.ReadBit(); //100%
                guildGuids[c][2] = packet.Translator.ReadBit();//20%

                charGuids[c][4] = packet.Translator.ReadBit(); //20%
                charGuids[c][5] = packet.Translator.ReadBit(); //20%
                nameLenghts[c] = packet.Translator.ReadBits(4);                //100%
                guildGuids[c][3] = packet.Translator.ReadBit();//20%
                guildGuids[c][4] = packet.Translator.ReadBit();//50%

                guildGuids[c][5] = packet.Translator.ReadBit();//20%
                charGuids[c][1] = packet.Translator.ReadBit(); //100%
                packet.Translator.ReadBit();                                   //20%
                guildGuids[c][6] = packet.Translator.ReadBit();//20%
                charGuids[c][7] = packet.Translator.ReadBit(); //100%
                guildGuids[c][7] = packet.Translator.ReadBit();//50%
                packet.Translator.ReadBit();                                   //20%
                packet.Translator.ReadBit();                                   //20%
            }

            // no idea, not used in client
            packet.Translator.ReadByte();

            for (int c = 0; c < count; ++c)
            {
                Vector3 pos = new Vector3();

                for (var itm = 0; itm < 19; ++itm)
                {
                    packet.Translator.ReadInt32("Item EnchantID", c, itm);
                    packet.Translator.ReadInt32("Item DisplayID", c, itm);
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                }

                for (var itm = 0; itm < 4; ++itm)
                {
                    packet.Translator.ReadInt32("Bag EnchantID", c, itm);
                    packet.Translator.ReadInt32("Bag DisplayID", c, itm);
                    packet.Translator.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                }

                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);
                packet.Translator.ReadInt32("Pet Level", c);
                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);

                packet.Translator.ReadByte("Facial Hair", c);

                packet.Translator.ReadXORByte(guildGuids[c], 0);
                    packet.Translator.ReadXORByte(charGuids[c], 0);

                packet.Translator.ReadXORByte(charGuids[c], 2);
                if (guildGuids[c][2] != 0)
                    //  guildGuids[c][2] ^= packet.Translator.ReadByte();

                    if (charGuids[c][7] != 0)
                        charGuids[c][7] ^= packet.Translator.ReadByte();
                if (guildGuids[c][7] != 0)
                    // guildGuids[c][7] ^= packet.Translator.ReadByte();

                    packet.Translator.ReadByte("List Order", c);
                packet.Translator.ReadInt32("Pet Display ID", c);

                // no ideal //////////////////////////////
                if (charGuids[c][4] != 0)
                    charGuids[c][4] ^= packet.Translator.ReadByte();

                if (guildGuids[c][4] != 0)
                    // guildGuids[c][4] ^= packet.Translator.ReadByte();

                if (charGuids[c][5] != 0)
                        // charGuids[c][5] ^= packet.Translator.ReadByte();

                if (guildGuids[c][5] != 0)
                            // guildGuids[c][5] ^= packet.Translator.ReadByte();

                if (guildGuids[c][1] != 0)
                                // guildGuids[c][1] ^= packet.Translator.ReadByte();

                                if (guildGuids[c][3] != 0)
                                    // guildGuids[c][3] ^= packet.Translator.ReadByte();

                                    if (guildGuids[c][6] != 0)
                                        // guildGuids[c][6] ^= packet.Translator.ReadByte();

                                        //////////////////////////////////////////

                                        if (charGuids[c][3] != 0)
                                            charGuids[c][3] ^= packet.Translator.ReadByte();

                var klass = packet.Translator.ReadByteE<Class>("Class", c);

                if (charGuids[c][6] != 0)
                    charGuids[c][6] ^= packet.Translator.ReadByte();

                pos.X = packet.Translator.ReadSingle("Position X", c);

                if (charGuids[c][1] != 0)
                    charGuids[c][1] ^= packet.Translator.ReadByte();

                var race = packet.Translator.ReadByteE<Race>("Race", c);
                packet.Translator.ReadInt32("Pet Family", c);
                pos.Y = packet.Translator.ReadSingle("Position Y", c);
                packet.Translator.ReadByteE<Gender>("Gender", c);
                packet.Translator.ReadByte("Hair Style", c);
                var level = packet.Translator.ReadByte("Level", c);
                pos.Z = packet.Translator.ReadSingle("Position Z", c);
                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                packet.Translator.ReadByte("Skin", c);
                packet.Translator.ReadByte("Hair Color", c);
                packet.Translator.ReadByte("Face", c);
                var mapId = packet.Translator.ReadInt32("Map", c);
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c);

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

            for (var c = 0; c < unkCounter; c++)
            {
                packet.Translator.ReadUInt32("Unk1", c);
                packet.Translator.ReadByte("Unk2", c);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_0_5_16048)]
        public static void HandleCharEnum434(Packet packet)
        {
            var unkCounter = packet.Translator.ReadBits("Unk Counter", 23);
            packet.Translator.ReadBit("Unk bit");
            var count = packet.Translator.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (var c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][3] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();
                guildGuids[c][2] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(7);
                charGuids[c][4] = packet.Translator.ReadBit();
                charGuids[c][7] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                guildGuids[c][6] = packet.Translator.ReadBit();
                charGuids[c][1] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                guildGuids[c][4] = packet.Translator.ReadBit();
                firstLogins[c] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
                guildGuids[c][0] = packet.Translator.ReadBit();
            }

            for (int c = 0; c < count; ++c)
            {
                Vector3 pos = new Vector3();

                var klass = packet.Translator.ReadByteE<Class>("Class", c);

                for (var itm = 0; itm < 19; ++itm)
                {
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.Translator.ReadInt32("Item DisplayID", c, itm);
                    packet.Translator.ReadInt32("Item EnchantID", c, itm);
                }

                for (var itm = 0; itm < 4; ++itm)
                {
                    packet.Translator.ReadByteE<InventoryType>("Bag InventoryType", c, itm);
                    packet.Translator.ReadInt32("Bag DisplayID", c, itm);
                    packet.Translator.ReadInt32("Bag EnchantID", c, itm);
                }

                packet.Translator.ReadInt32("Pet Family", c);

                packet.Translator.ReadXORByte(guildGuids[c], 2);

                packet.Translator.ReadByte("List Order", c);
                packet.Translator.ReadByte("Hair Style", c);
                packet.Translator.ReadXORByte(guildGuids[c], 3);

                packet.Translator.ReadInt32("Pet Display ID", c);
                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                packet.Translator.ReadByte("Hair Color", c);

                packet.Translator.ReadXORByte(charGuids[c], 4);

                var mapId = packet.Translator.ReadInt32("Map", c);
                packet.Translator.ReadXORByte(guildGuids[c], 5);

                pos.Z = packet.Translator.ReadSingle("Position Z", c);
                packet.Translator.ReadXORByte(guildGuids[c], 6);

                packet.Translator.ReadInt32("Pet Level", c);

                packet.Translator.ReadXORByte(charGuids[c], 3);

                pos.Y = packet.Translator.ReadSingle("Position Y", c);

                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                packet.Translator.ReadByte("Facial Hair", c);

                packet.Translator.ReadXORByte(charGuids[c], 7);

                packet.Translator.ReadByteE<Gender>("Gender", c);
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.Translator.ReadByte("Face", c);

                packet.Translator.ReadXORByte(charGuids[c], 0);

                packet.Translator.ReadXORByte(charGuids[c], 2);

                packet.Translator.ReadXORByte(guildGuids[c], 1);

                packet.Translator.ReadXORByte(guildGuids[c], 7);

                pos.X = packet.Translator.ReadSingle("Position X", c);
                packet.Translator.ReadByte("Skin", c);
                var race = packet.Translator.ReadByteE<Race>("Race", c);
                var level = packet.Translator.ReadByte("Level", c);
                packet.Translator.ReadXORByte(charGuids[c], 6);

                packet.Translator.ReadXORByte(guildGuids[c], 4);

                packet.Translator.ReadXORByte(guildGuids[c], 0);

                packet.Translator.ReadXORByte(charGuids[c], 5);

                packet.Translator.ReadXORByte(charGuids[c], 1);

                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);

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

            for (var i = 0; i < unkCounter; ++i)
            {
                packet.Translator.ReadByte("Unk byte", i);
                packet.Translator.ReadUInt32("Unk int", i);
            }
        }

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V5_0_5_16048, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleCharEnum505(Packet packet)
        {
            packet.Translator.ReadBits("Unk Counter", 23);
            packet.Translator.ReadBit("Unk bit");
            var count = packet.Translator.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (var c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][4] = packet.Translator.ReadBit();

                guildGuids[c][7] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                guildGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();

                firstLogins[c] = packet.Translator.ReadBit();

                charGuids[c][6] = packet.Translator.ReadBit();

                guildGuids[c][6] = packet.Translator.ReadBit();

                charGuids[c][1] = packet.Translator.ReadBit();

                nameLenghts[c] = packet.Translator.ReadBits(7);

                guildGuids[c][2] = packet.Translator.ReadBit();

                charGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][3] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();

                guildGuids[c][4] = packet.Translator.ReadBit();

                charGuids[c][7] = packet.Translator.ReadBit();

                guildGuids[c][5] = packet.Translator.ReadBit();
            }

            packet.Translator.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                Vector3 pos = new Vector3();

                for (var itm = 0; itm < 23; ++itm)
                {
                    packet.Translator.ReadInt32("Item EnchantID", c, itm);
                    packet.Translator.ReadInt32("Item DisplayID", c, itm);
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                }

                packet.Translator.ReadByte("Hair Style", c);

                var race = packet.Translator.ReadByteE<Race>("Race", c);

                packet.Translator.ReadXORByte(charGuids[c], 0);
                packet.Translator.ReadXORByte(guildGuids[c], 4);

                packet.Translator.ReadByte("Facial Hair", c);
                packet.Translator.ReadByte("Hair Color", c);

                pos.Z = packet.Translator.ReadSingle("Position Z", c);

                packet.Translator.ReadXORByte(guildGuids[c], 6);
                packet.Translator.ReadXORByte(charGuids[c], 7);
                packet.Translator.ReadXORByte(guildGuids[c], 0);

                packet.Translator.ReadInt32E<CharacterFlag>("Character Flags", c);
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);

                packet.Translator.ReadXORByte(charGuids[c], 5);
                packet.Translator.ReadXORByte(charGuids[c], 6);

                packet.Translator.ReadInt32E<CustomizationFlag>("Customization Flags", c);
                var mapId = packet.Translator.ReadInt32<MapId>("Map Id", c);

                packet.Translator.ReadXORByte(charGuids[c], 1);

                packet.Translator.ReadInt32("Pet Display Id", c);

                packet.Translator.ReadXORByte(guildGuids[c], 1);

                packet.Translator.ReadByte("Face", c);
                packet.Translator.ReadInt32E<CreatureFamily>("Pet Family", c);
                packet.Translator.ReadByte("Skin", c);

                packet.Translator.ReadXORByte(charGuids[c], 4);

                packet.Translator.ReadXORByte(guildGuids[c], 5);

                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c);

                packet.Translator.ReadInt32("Pet Level", c);
                packet.Translator.ReadByte("Gender", c);
                pos.X = packet.Translator.ReadSingle("Position X", c);
                var klass = packet.Translator.ReadByteE<Class>("Class", c);
                packet.Translator.ReadByte("Unk 8", c);
                pos.Y = packet.Translator.ReadSingle("Position Y", c);

                packet.Translator.ReadXORByte(guildGuids[c], 3);
                packet.Translator.ReadXORByte(guildGuids[c], 7);
                packet.Translator.ReadXORByte(guildGuids[c], 2);

                var level = packet.Translator.ReadByte("Level", c);

                packet.Translator.ReadXORByte(charGuids[c], 2);
                packet.Translator.ReadXORByte(charGuids[c], 3);

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

        [Parser(Opcode.SMSG_ENUM_CHARACTERS_RESULT, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleCharEnum510(Packet packet)
        {
            var unkCounter = packet.Translator.ReadBits("Unk Counter", 23);
            var count = packet.Translator.ReadBits("Char count", 17);

            var charGuids = new byte[count][];
            var guildGuids = new byte[count][];
            var firstLogins = new bool[count];
            var nameLenghts = new uint[count];

            for (int c = 0; c < count; ++c)
            {
                charGuids[c] = new byte[8];
                guildGuids[c] = new byte[8];

                charGuids[c][7] = packet.Translator.ReadBit();
                charGuids[c][0] = packet.Translator.ReadBit();
                charGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][5] = packet.Translator.ReadBit();
                charGuids[c][3] = packet.Translator.ReadBit();
                nameLenghts[c] = packet.Translator.ReadBits(7);
                guildGuids[c][0] = packet.Translator.ReadBit();
                guildGuids[c][5] = packet.Translator.ReadBit();
                guildGuids[c][3] = packet.Translator.ReadBit();
                firstLogins[c] = packet.Translator.ReadBit();
                guildGuids[c][6] = packet.Translator.ReadBit();
                guildGuids[c][7] = packet.Translator.ReadBit();
                charGuids[c][1] = packet.Translator.ReadBit();
                guildGuids[c][4] = packet.Translator.ReadBit();
                guildGuids[c][1] = packet.Translator.ReadBit();
                charGuids[c][2] = packet.Translator.ReadBit();
                charGuids[c][6] = packet.Translator.ReadBit();
            }

            packet.Translator.ReadBit("Unk bit");
            packet.Translator.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                Vector3 pos = new Vector3();

                packet.Translator.ReadInt32E<CharacterFlag>("CharacterFlag", c);
                packet.Translator.ReadInt32("Pet Family", c);
                pos.Z = packet.Translator.ReadSingle("Position Z", c);
                packet.Translator.ReadXORByte(charGuids[c], 7);
                packet.Translator.ReadXORByte(guildGuids[c], 6);

                for (var itm = 0; itm < 23; ++itm)
                {
                    packet.Translator.ReadInt32("Item EnchantID", c, itm);
                    packet.Translator.ReadByteE<InventoryType>("Item InventoryType", c, itm);
                    packet.Translator.ReadInt32("Item DisplayID", c, itm);
                }

                pos.X = packet.Translator.ReadSingle("Position X", c);
                var klass = packet.Translator.ReadByteE<Class>("Class", c);
                packet.Translator.ReadXORByte(charGuids[c], 5);
                pos.Y = packet.Translator.ReadSingle("Position Y", c);
                packet.Translator.ReadXORByte(guildGuids[c], 3);
                packet.Translator.ReadXORByte(charGuids[c], 6);
                packet.Translator.ReadInt32("Pet Level", c);
                packet.Translator.ReadInt32("Pet Display ID", c);
                packet.Translator.ReadXORByte(charGuids[c], 2);
                packet.Translator.ReadXORByte(charGuids[c], 1);
                packet.Translator.ReadByte("Hair Color", c);
                packet.Translator.ReadByte("Facial Hair", c);
                packet.Translator.ReadXORByte(guildGuids[c], 2);
                var zone = packet.Translator.ReadUInt32<ZoneId>("Zone Id", c);
                packet.Translator.ReadByte("List Order", c);
                packet.Translator.ReadXORByte(charGuids[c], 0);
                packet.Translator.ReadXORByte(guildGuids[c], 1);
                packet.Translator.ReadByte("Skin", c);
                packet.Translator.ReadXORByte(charGuids[c], 4);
                packet.Translator.ReadXORByte(guildGuids[c], 5);
                var name = packet.Translator.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.Translator.ReadXORByte(guildGuids[c], 0);
                var level = packet.Translator.ReadByte("Level", c);
                packet.Translator.ReadXORByte(charGuids[c], 3);
                packet.Translator.ReadXORByte(guildGuids[c], 7);
                packet.Translator.ReadByte("Hair Style", c);
                packet.Translator.ReadXORByte(guildGuids[c], 4);
                packet.Translator.ReadByteE<Gender>("Gender", c);
                var mapId = packet.Translator.ReadInt32<MapId>("Map Id", c);
                packet.Translator.ReadUInt32E<CustomizationFlag>("CustomizationFlag", c);
                var race = packet.Translator.ReadByteE<Race>("Race", c);
                packet.Translator.ReadByte("Face", c);

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

            for (var i = 0; i < unkCounter; ++i)
            {
                packet.Translator.ReadByte("Unk byte", i);
                packet.Translator.ReadUInt32("Unk int", i);
            }
        }

        [Parser(Opcode.SMSG_COMPRESSED_CHAR_ENUM)]
        public static void HandleCompressedCharEnum(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.Translator.ReadInt32()))
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
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadInt32("Vehicle Id");
        }

        [Parser(Opcode.CMSG_REQUEST_PLAYED_TIME)]
        [Parser(Opcode.SMSG_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PLAYED_TIME, Direction.ServerToClient))
            {
                packet.Translator.ReadInt32("Time Played");
                packet.Translator.ReadInt32("Total");
            }
            packet.Translator.ReadBool("Print in chat");
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Total XP");
            var type = packet.Translator.ReadByte("XP type"); // Need enum

            if (type == 0) // kill
            {
                packet.Translator.ReadUInt32("Base XP");
                packet.Translator.ReadSingle("Group rate (unk)");
            }

            packet.Translator.ReadBool("RAF Bonus");
        }

        [Parser(Opcode.SMSG_TITLE_EARNED)]
        public static void HandleTitleEarned(Packet packet)
        {
            packet.Translator.ReadUInt32("Title Id");
            packet.Translator.ReadUInt32("Earned?"); // vs lost
        }

        [Parser(Opcode.CMSG_SET_TITLE)]
        public static void HandleSetTitle(Packet packet)
        {
            packet.Translator.ReadUInt32("Title Id");
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInitCurrency434(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 23);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];
            for (var i = 0; i < count; ++i)
            {
                hasWeekCount[i] = packet.Translator.ReadBit();
                flags[i] = packet.Translator.ReadBits(4);
                hasWeekCap[i] = packet.Translator.ReadBit();
                hasSeasonTotal[i] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i);
                packet.Translator.ReadUInt32("Currency count", i);
                if (hasWeekCap[i])
                    packet.Translator.ReadUInt32("Weekly cap", i);

                if (hasSeasonTotal[i])
                    packet.Translator.ReadUInt32("Season total earned", i);

                packet.Translator.ReadUInt32("Currency id", i);
                if (hasWeekCount[i])
                    packet.Translator.ReadUInt32("Weekly count", i);
            }
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleInitCurrency422(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            var bits = new bool[count, 3];

            for (var i = 0; i < count; ++i)
                for (var j = 0; j < 3; ++j)
                    bits[i, j] = packet.Translator.ReadBit();

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Currency Id", i);
                if (bits[i, 0])
                    packet.Translator.ReadInt32("Weekly Cap", i);

                packet.Translator.ReadInt32("Total Count", i);
                packet.Translator.ReadByte("Unk Byte1", i);

                if (bits[i, 1])
                    packet.Translator.ReadInt32("Season Total Earned?", i);

                if (bits[i, 2])
                    packet.Translator.ReadUInt32("Week Count", i);
            }
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32("Week Count", i);
                packet.Translator.ReadByte("Unk Byte", i);
                packet.Translator.ReadUInt32("Currency ID", i);
                packet.Translator.ReadTime("Reset Time", i);
                packet.Translator.ReadUInt32("Week Cap", i);
                packet.Translator.ReadInt32("Total Count", i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleUpdateCurrency(Packet packet)
        {
            packet.Translator.ReadUInt32("Currency ID");
            packet.Translator.ReadUInt32("Week Count");
            packet.Translator.ReadUInt32("Total Count");
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleUpdateCurrency430(Packet packet)
        {
            packet.Translator.ReadInt32("Currency ID");
            packet.Translator.ReadInt32("Total Count");

            var hasSeasonCount = packet.Translator.ReadBit();
            var hasWeekCap = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Print in log");

            if (hasWeekCap)
                packet.Translator.ReadInt32("Week Count");

            if (hasSeasonCount)
                packet.Translator.ReadInt32("Season Total Earned");
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleUpdateCurrency434(Packet packet)
        {
            var hasWeekCap = packet.Translator.ReadBit();
            var hasSeasonCount = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Print in log");

            if (hasSeasonCount)
                packet.Translator.ReadInt32("Season Total Earned");

            packet.Translator.ReadInt32("Total Count");
            packet.Translator.ReadInt32("Currency ID");

            if (hasWeekCap)
                packet.Translator.ReadInt32("Week Count");
        }

        [Parser(Opcode.SMSG_UPDATE_CURRENCY_WEEK_LIMIT)]
        public static void HandleUpdateCurrencyWeekLimit(Packet packet)
        {
            packet.Translator.ReadUInt32("Week Cap");
            packet.Translator.ReadUInt32("Currency ID");
        }

        [Parser(Opcode.CMSG_SET_CURRENCY_FLAGS)]
        public static void HandleSetCurrencyFlags(Packet packet)
        {
            packet.Translator.ReadUInt32("Flags");
            packet.Translator.ReadUInt32("Currency ID");
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleXPGainAborted430(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Int32 1");
            packet.Translator.ReadInt32("Unk Int32 2");
            packet.Translator.ReadInt32("Unk Int32 3");

            var guid = packet.Translator.StartBitStream(5, 7, 4, 2, 3, 1, 6, 0);
            packet.Translator.ParseBitStream(guid, 0, 6, 1, 2, 4, 7, 3, 5);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_XP_GAIN_ABORTED, ClientVersionBuild.V4_3_4_15595)] // 4.3.4, related to EVENT_TRIAL_CAP_REACHED_LEVEL
        public static void HandleXPGainAborted434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 0, 1, 2, 6, 7, 5, 3);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.ReadInt32("Unk Int32 1");

            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.ReadInt32("Unk Int32 2");

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.ReadInt32("Unk Int32 3");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_FAILED_PLAYER_CONDITION)]
        public static void HandleFailedPlayerCondition(Packet packet)
        {
            packet.Translator.ReadInt32("Id");
        }

        [Parser(Opcode.CMSG_SHOWING_CLOAK)]
        [Parser(Opcode.CMSG_SHOWING_HELM)]
        public static void HandleShowingCloakAndHelm434(Packet packet)
        {
            packet.Translator.ReadBool("Showing");
        }

        [Parser(Opcode.CMSG_AUTO_DECLINE_GUILD_INVITES)]
        public static void HandleAutoDeclineGuildInvites434(Packet packet)
        {
            packet.Translator.ReadBool("Auto decline");
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)] // 4.3.4
        public static void HandleReorderCharacters(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 10);

            var guids = new byte[count][];

            for (int i = 0; i < count; ++i)
                guids[i] = packet.Translator.StartBitStream(1, 4, 5, 3, 0, 7, 6, 2);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 5);
                packet.Translator.ReadXORByte(guids[i], 1);
                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 0);
                packet.Translator.ReadXORByte(guids[i], 3);
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.ReadXORByte(guids[i], 7);

                packet.Translator.WriteGuid("Character Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_LEVEL_UP_INFO)]
        public static void HandleLevelUp(Packet packet)
        {
            packet.Translator.ReadInt32("Level");
            packet.Translator.ReadInt32("Health");

            var powerCount = 5;
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                powerCount = 7;
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                powerCount = 5;

            // TODO: Exclude happiness on Cata
            for (var i = 0; i < powerCount; i++)
                packet.Translator.ReadInt32("Power", (PowerType) i);

            for (var i = 0; i < 5; i++)
                packet.Translator.ReadInt32("Stat", (StatType)i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.Translator.ReadInt32("Talent Level"); // 0 - No Talent gain / 1 - Talent Point gain
        }

        [Parser(Opcode.SMSG_HEALTH_UPDATE)]
        public static void HandleHealthUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadUInt32("Value");
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");

            var count = 1;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                count = packet.Translator.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadByteE<PowerType>("Power type"); // Actually powertype for class
                packet.Translator.ReadInt32("Value");
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
