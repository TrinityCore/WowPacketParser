using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_CHAR_CREATE)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var guid = packet.StartBitStream(1, 3, 2, 7, 4, 6, 0, 5);
            packet.ParseBitStream(guid, 7, 1, 6, 0, 3, 4, 2, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PLAYED_TIME)]
        public static void HandleCPlayedTime(Packet packet)
        {
            packet.ReadBoolean("Print in chat");
        }

        [Parser(Opcode.CMSG_REORDER_CHARACTERS)]
        public static void HandleReorderCharacters(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SHOWING_CLOAK)]
        [Parser(Opcode.CMSG_SHOWING_HELM)]
        public static void HandleShowingCloakAndHelm(Packet packet)
        {
            packet.ReadBoolean("Showing");
        }

        [Parser(Opcode.CMSG_STANDSTATECHANGE)]
        public static void HandleStandStateChange(Packet packet)
        {
            packet.ReadInt32("Standstate");
        }

        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            // імена не перевірено, лише послідовність типів данних
            var unkCounter = packet.ReadBits("Unk Counter", 21);//[DW5]
            var count = packet.ReadBits("Char count", 16);//[DW9]

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
                guildGuids[c][3] = packet.ReadBit();
                charGuids[c][3] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
                packet.ReadBit("unk bit 124", c); //124
                firstLogins[c] = packet.ReadBit(); //108
                charGuids[c][6] = packet.ReadBit();
                guildGuids[c][6] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(6);
                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
            }//+=416

            //packet.ResetBitReader();
            packet.ReadBit("UnkB16");

            for (int c = 0; c < count; ++c)
            {
                packet.ReadInt32("DW132", c);
                packet.ReadXORByte(charGuids[c], 1);//1
                packet.ReadByte("Slot", c); //57
                packet.ReadByte("Hair Style", c); //63
                packet.ReadXORByte(guildGuids[c], 2);//90
                packet.ReadXORByte(guildGuids[c], 0);//88
                packet.ReadXORByte(guildGuids[c], 6);//94
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                packet.ReadXORByte(guildGuids[c], 3);//91
                var x = packet.ReadSingle("Position X", c); //4Ch
                packet.ReadInt32("DW104", c);
                packet.ReadByte("Face", c); //62
                var Class = packet.ReadEnum<Class>("Class", TypeCode.Byte, c); //59
                packet.ReadXORByte(guildGuids[c], 5); //93
             
                for (var itm = 0; itm < 23; itm++)
                {
                    packet.ReadInt32("Item EnchantID", c, itm); //140 prolly need to replace those 2
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, itm); //144
                    packet.ReadInt32("Item DisplayID", c, itm); //136
                }

                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c); //100
                packet.ReadXORByte(charGuids[c], 3); //3
                packet.ReadXORByte(charGuids[c], 5); //5
                packet.ReadInt32("PetFamily", c); //120
                packet.ReadXORByte(guildGuids[c], 4); //92
                var mapId = packet.ReadInt32("Map", c); //72
                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c); //58
                packet.ReadByte("Skin", c); //61
                packet.ReadXORByte(guildGuids[c], 1); //89
                var level = packet.ReadByte("Level", c); //66
                packet.ReadXORByte(charGuids[c], 0); //0
                packet.ReadXORByte(charGuids[c], 2); //2
                packet.ReadByte("Hair Color", c); //64
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c); //60
                packet.ReadByte("Facial Hair", c); //65
                packet.ReadInt32("Pet Level", c); //116
                packet.ReadXORByte(charGuids[c], 4); //4
                packet.ReadXORByte(charGuids[c], 7); //7
                var y = packet.ReadSingle("Position Y", c); //50h
                packet.ReadInt32("Pet DisplayID", c); //112
                packet.ReadInt32("DW128", c);
                packet.ReadXORByte(charGuids[c], 6); //6
                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c); //96
                var zone = packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id", c); //68
                packet.ReadXORByte(guildGuids[c], 7); //95
                var z = packet.ReadSingle("Position Z", c); //54h

                var playerGuid = new Guid(BitConverter.ToUInt64(charGuids[c], 0));

                packet.WriteGuid("Character GUID", charGuids[c], c);
                packet.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    var startPos = new StartPosition();
                    startPos.Map = mapId;
                    startPos.Position = new Vector3(x, y, z);
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

            for (var i = 0; i < unkCounter; i++)
            {
                packet.ReadByte("Unk byte", i); // char_table+28+i*8
                packet.ReadUInt32("Unk int", i); // char_table+24+i*8
            }
        }

        [Parser(Opcode.SMSG_CHAR_CREATE)]
        [Parser(Opcode.SMSG_CHAR_DELETE)]
        [Parser(Opcode.SMSG_INIT_CURRENCY)]
        [Parser(Opcode.SMSG_STANDSTATE_UPDATE)]
        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandleInitCurrency(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
