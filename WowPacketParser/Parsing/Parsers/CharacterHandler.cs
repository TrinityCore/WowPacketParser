using System;
using System.Collections;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid=WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CharacterHandler
    {
        public static readonly Dictionary<Guid, Player> Characters =
            new Dictionary<Guid, Player>();

        public static readonly List<StartInfo> StartInfos = new List<StartInfo>();

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
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
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
            Characters.Clear();

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
                var zone = packet.ReadInt32("Zone Id");
                var mapId = packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

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

                bool all = true;
                foreach (StartInfo info in StartInfos)
                {
                    if (info.Race == race || info.Class == clss)
                    {
                        all = false;
                        break;
                    }
                }
                if (firstLogin && all)
                {
                    var startInfo = new StartInfo
                                        {
                                            StartPos = new StartPos
                                                           {
                                                               Map = mapId,
                                                               Position = pos,
                                                               Zone = zone
                                                           }
                                        };

                    Stuffing.StartInformation.TryAdd(new Tuple<Race, Class>(race, clss), startInfo);
                    SQLStore.WriteData(SQLStore.StartPositions.GetCommand(race, clss, mapId, zone, pos));
                }

                var chInfo = new Player
                                 {
                                     Race = race,
                                     Class = clss,
                                     Name = name,
                                     FirstLogin = firstLogin,
                                     Level = level
                                 };

                Characters.Add(guid, chInfo); // TODO Remove when its usage is converted to Stuffing.Objects
                Stuffing.Objects.TryAdd(guid, chInfo);

            }
        }

        public static BitArray ReverseBitArray(BitArray bits)
        {
            BitArray rev = new BitArray(bits.Length, false);
            for (int i = 0; i < bits.Length; i++)
            {
                rev[i] = bits[bits.Length - i - 1];
            }
            return rev;
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
                packet.ReadCString("Name", c);

                if (bits[c, 0])
                    guild[5] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("Face", c);
                packet.ReadInt32("Map", c);

                if (bits[c, 12])
                    low[1] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 1])
                    low[4] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 10])
                    guild[4] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 15])
                    guild[0] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadSingle("Position X", c);
                packet.ReadSingle("Position Y", c);
                packet.ReadSingle("Position Z", c);

                if (bits[c, 11])
                    low[0] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadInt32("ZoneID", c);
                packet.ReadInt32("Pet Level", c);

                if (bits[c, 8])
                    low[3] = (byte)(packet.ReadByte() ^ 1);

                if (bits[c, 14])
                    low[7] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("Facial Hair", c);
                packet.ReadByte("Skin", c);
                packet.ReadEnum<Class>("Class", TypeCode.Byte, c);
                packet.ReadInt32("Pet Family", c);
                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);

                if (bits[c, 9])
                    low[2] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadInt32("Pet Display ID", c);

                if (bits[c, 3])
                    guild[7] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("Level", c);

                if (bits[c, 7])
                    low[6] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadByte("Hair Style", c);

                if (bits[c, 13])
                    guild[2] = (byte)(packet.ReadByte() ^ 1);

                packet.ReadEnum<Race>("Race", TypeCode.Byte, c);
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

                packet.Writer.WriteLine("[{0}] Character GUID: {1}", c, new Guid(BitConverter.ToUInt64(low, 0)));
                packet.Writer.WriteLine("[{0}] Guild GUID: {1}", c, new Guid(BitConverter.ToUInt64(guild, 0)));
            }
        }

        [Parser(Opcode.SMSG_CHAR_ENUM, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleCharEnum430(Packet packet)
        {
            uint count = 0;

            BitArray bits = packet.ReadBitArray(17);
            for (int i = 0; i < bits.Length; i++)
                if(bits[i])
                    count |= (uint)((1 << (i)));

            packet.Writer.WriteLine("Count: " + count);

            var PlrGuidBits = new bool[count, 8];
            var GuildGuidBits = new bool[count, 8];
            var FirstLoginBits = new bool[count];
            var NameLenghts = new uint[count];

            for (uint c = 0; c < count; c++)
            {
                bits = ReverseBitArray(packet.ReadBitArray(24));

                GuildGuidBits[c, 2] = bits[0];
                PlrGuidBits[c, 2] = bits[1];
                PlrGuidBits[c, 6] = bits[2];
                PlrGuidBits[c, 5] = bits[3];
                PlrGuidBits[c, 4] = bits[4];
                GuildGuidBits[c, 4] = bits[5];
                GuildGuidBits[c, 3] = bits[6];
                GuildGuidBits[c, 7] = bits[7];

                for (int i = 0; i < 7; i++) 
                    if (bits[14-i])
                        NameLenghts[c] |= (uint)((1 << (i)));

                //packet.Writer.WriteLine("[{0}] Name Lenght: {1}", c, NameLenghts[c]);

                GuildGuidBits[c, 0] = bits[15];
                PlrGuidBits[c, 0] = bits[16];
                PlrGuidBits[c, 3] = bits[17];
                PlrGuidBits[c, 1] = bits[18];
                FirstLoginBits[c] = bits[19];
                GuildGuidBits[c, 5] = bits[20];
                PlrGuidBits[c, 7] = bits[21];
                GuildGuidBits[c, 6] = bits[22];
                GuildGuidBits[c, 1] = bits[23];
            }

            uint UnkCounter = packet.ReadBits("Unk Counter", 23);

            packet.ReadBit();//no idea, not used in client
            
            for (int c = 0; c < count; c++)
            {
                var low = new byte[8];
                var guild = new byte[8];

                for (int itm = 0; itm < 19; itm++)
                {
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, itm);
                    packet.ReadInt32("Item DisplayID", c, itm);
                    packet.ReadInt32("Item EnchantID", c, itm);
                }

                for (int itm = 0; itm < 4; itm++)
                {
                    packet.ReadEnum<InventoryType>("Bag InventoryType", TypeCode.Byte, c, itm);
                    packet.ReadInt32("Bag DisplayID", c, itm);
                    packet.ReadInt32("Bag EnchantID", c, itm);
                }

                if (GuildGuidBits[c, 0]) guild[0] = (byte)(packet.ReadByte() ^ 1);
                if (GuildGuidBits[c, 1]) guild[1] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadByte("Face", c);
                packet.ReadInt32("Pet Display ID", c);
                if (GuildGuidBits[c, 7]) guild[7] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c);
                packet.ReadByte("Level", c);
                packet.ReadInt32("Pet Level", c);
                packet.ReadInt32("ZoneID", c);
                packet.ReadSingle("Position Y", c);
                packet.ReadInt32("Pet Family", c);
                packet.ReadByte("Hair Style", c);
                if (PlrGuidBits[c, 1]) low[1] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadWoWString("Name", (int)(NameLenghts[(int)c]), c);
                if (PlrGuidBits[c, 0]) low[0] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadEnum<Race>("Race", TypeCode.Byte, c);
                packet.ReadByte("List Order", c);
                if (PlrGuidBits[c, 7]) low[7] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadSingle("Position Z", c);
                packet.ReadInt32("Map", c);
                if (GuildGuidBits[c, 4]) guild[4] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadByte("Hair Color", c);
                if (PlrGuidBits[c, 3]) low[3] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);
                packet.ReadByte("Skin", c);
                if (PlrGuidBits[c, 4]) low[4] = (byte)(packet.ReadByte() ^ 1);
                if (PlrGuidBits[c, 5]) low[5] = (byte)(packet.ReadByte() ^ 1);
                if (GuildGuidBits[c, 5]) guild[5] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c);
                packet.ReadSingle("Position X", c);
                packet.ReadByte("Facial Hair", c);
                if (PlrGuidBits[c, 6]) low[6] = (byte)(packet.ReadByte() ^ 1);
                if (GuildGuidBits[c, 3]) guild[3] = (byte)(packet.ReadByte() ^ 1);
                if (PlrGuidBits[c, 2]) low[2] = (byte)(packet.ReadByte() ^ 1);
                packet.ReadEnum<Class>("Class", TypeCode.Byte, c);
                if (GuildGuidBits[c, 6]) guild[6] = (byte)(packet.ReadByte() ^ 1);
                if (GuildGuidBits[c, 2]) guild[2] = (byte)(packet.ReadByte() ^ 1);

                packet.Writer.WriteLine("[{0}] Character GUID: {1}", c, new Guid(BitConverter.ToUInt64(low, 0)));
                packet.Writer.WriteLine("[{0}] Guild GUID: {1}", c, new Guid(BitConverter.ToUInt64(guild, 0)));
            }
            for (int c = 0; c < UnkCounter; c++)
                packet.Writer.WriteLine("Unk Loop: {0}, {1}", packet.ReadUInt32(), packet.ReadByte());
        }

        [Parser(Opcode.SMSG_COMPRESSED_CHAR_ENUM)]
        public static void HandleCompressedCharEnum(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                HandleCharEnum430(packet.Inflate(packet.ReadInt32()));
            else
                HandleCharEnum422(packet.Inflate(packet.ReadInt32()));
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
    }
}
