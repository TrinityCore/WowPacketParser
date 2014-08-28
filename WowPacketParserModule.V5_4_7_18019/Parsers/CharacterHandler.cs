using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.WowGuid;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.CMSG_AUTO_DECLINE_GUILD_INVITES)]
        public static void HandleAutoDeclineGuildInvites434(Packet packet)
        {
            packet.ReadBoolean("Auto decline");
        }

        [Parser(Opcode.CMSG_CHAR_CREATE)]
        public static void HandleClientCharCreate(Packet packet)
        {
            packet.ReadByte("Outfit Id");
            packet.ReadByte("Facial Hair");
            packet.ReadByte("Skin");
            packet.ReadEnum<Race>("Race", TypeCode.Byte);
            packet.ReadByte("Hair Style");
            packet.ReadEnum<Class>("Class", TypeCode.Byte);
            packet.ReadByte("Face");
            packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            packet.ReadByte("Hair Color");

            var NameLenght = packet.ReadBits(6);
            var hasDword = packet.ReadBit();

            packet.ReadWoWString("Name", NameLenght);

            if (hasDword)
                packet.ReadUInt32("dword4C");
        }

        [Parser(Opcode.CMSG_CHAR_DELETE)]
        public static void HandleClientCharDelete(Packet packet)
        {
            var guid = packet.StartBitStream(6, 4, 5, 1, 7, 3, 2, 0);
            packet.ParseBitStream(guid, 1, 2, 3, 4, 0, 7, 6, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_DUEL_PROPOSED)]
        public static void DuelProposed(Packet packet)
        {
            var guid = packet.StartBitStream(2, 0, 4, 3, 7, 1, 5, 6);
            packet.ParseBitStream(guid, 0, 3, 2, 6, 5, 4, 1, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_EMOTE)]
        public static void HandleEmote(Packet packet)
        {
            packet.ReadInt32("Emote");
        }

        [Parser(Opcode.SMSG_CHAR_ENUM)]
        public static void HandleCharEnum(Packet packet)
        {
            packet.ReadBit("Unk bit");

            var unkCounter = packet.ReadBits("Unk Counter", 21);
            var count = packet.ReadBits("Char count", 16);

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
                charGuids[c][3] = packet.ReadBit();
                charGuids[c][6] = packet.ReadBit();
                guildGuids[c][1] = packet.ReadBit();
                charGuids[c][1] = packet.ReadBit();
                guildGuids[c][2] = packet.ReadBit();
                guildGuids[c][3] = packet.ReadBit();
                firstLogins[c] = packet.ReadBit();
                charGuids[c][2] = packet.ReadBit();
                guildGuids[c][0] = packet.ReadBit();
                guildGuids[c][7] = packet.ReadBit();
                nameLenghts[c] = packet.ReadBits(6);
                packet.ReadBit("unk bit 7C");
                guildGuids[c][6] = packet.ReadBit();
                charGuids[c][4] = packet.ReadBit();
                guildGuids[c][5] = packet.ReadBit();
                charGuids[c][5] = packet.ReadBit();
                charGuids[c][7] = packet.ReadBit();
            }

            packet.ResetBitReader();

            for (int c = 0; c < count; ++c)
            {
                packet.ReadByte("Hair Style", c);

                packet.ReadXORByte(guildGuids[c], 7);

                var race = packet.ReadEnum<Race>("Race", TypeCode.Byte, c);

                packet.ReadXORByte(charGuids[c], 5);
                packet.ReadXORByte(guildGuids[c], 2);
                packet.ReadXORByte(charGuids[c], 6);

                packet.ReadEnum<CharacterFlag>("CharacterFlag", TypeCode.Int32, c);
                var zone = packet.ReadEntry<UInt32>(StoreNameType.Zone, "Zone Id", c);

                packet.ReadXORByte(guildGuids[c], 3);

                packet.ReadUInt32("Pet Family", c);
                packet.ReadUInt32("Pet Level", c);
                packet.ReadUInt32("Unk dword1", c);
                packet.ReadUInt32("Unk dword2", c);

                packet.ReadXORByte(charGuids[c], 3);
                packet.ReadXORByte(charGuids[c], 0);

                packet.ReadByte("Facial Hair", c);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte, c);

                packet.ReadXORByte(guildGuids[c], 0);

                packet.ReadByte("Face", c);
                var level = packet.ReadByte("Level", c);

                for (var itm = 0; itm < 23; ++itm)
                {
                    packet.ReadInt32("Item EnchantID", c, itm); //prolly need to replace those 2
                    packet.ReadInt32("Item DisplayID", c, itm);
                    packet.ReadEnum<InventoryType>("Item InventoryType", TypeCode.Byte, c, itm);
                }

                var z = packet.ReadSingle("Position Z", c);

                packet.ReadXORByte(guildGuids[c], 1);

                var y = packet.ReadSingle("Position Y", c);
                packet.ReadByte("Face", c);
                packet.ReadByte("Unk Byte3", c);

                packet.ReadXORByte(guildGuids[c], 5);
                packet.ReadXORByte(charGuids[c], 1);

                packet.ReadUInt32("Unk dword4", c);
                var x = packet.ReadSingle("Position X", c);
                var name = packet.ReadWoWString("Name", (int)nameLenghts[c], c);
                var mapId = packet.ReadInt32("Map", c);
                packet.ReadUInt32("Unk dword5", c);
                packet.ReadByte("Hair Color", c);
                var Class = packet.ReadEnum<Class>("Class", TypeCode.Byte, c);

                packet.ReadXORByte(guildGuids[c], 4);
                packet.ReadXORByte(charGuids[c], 2);

                packet.ReadEnum<CustomizationFlag>("CustomizationFlag", TypeCode.UInt32, c);

                packet.ReadXORByte(charGuids[c], 7);
                packet.ReadXORByte(guildGuids[c], 6);
                packet.ReadXORByte(charGuids[c], 4);

                var playerGuid = new Guid(BitConverter.ToUInt64(charGuids[c], 0));

                packet.WriteGuid("Character GUID", charGuids[c], c);
                packet.WriteGuid("Guild GUID", guildGuids[c], c);

                if (firstLogins[c])
                {
                    var startPos = new StartPosition();
                    startPos.Map = mapId;
                    startPos.Position = new Vector3(x, y, z);
                    startPos.Zone = (int)zone;

                    Storage.StartPositions.Add(new Tuple<Race, Class>(race, Class), startPos, packet.TimeSpan);
                }

                var playerInfo = new Player { Race = race, Class = Class, Name = name, FirstLogin = firstLogins[c], Level = level };
                if (Storage.Objects.ContainsKey(playerGuid))
                    Storage.Objects[playerGuid] = new Tuple<WoWObject, TimeSpan?>(playerInfo, packet.TimeSpan);
                else
                    Storage.Objects.Add(playerGuid, playerInfo, packet.TimeSpan);
                StoreGetters.AddName(playerGuid, name);
            }

            for (var i = 0; i < unkCounter; ++i)
            {
                packet.ReadUInt32("Unk int", i);
                packet.ReadByte("Unk byte", i);              
            }
        }

        [Parser(Opcode.SMSG_INIT_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];
            for (var i = 0; i < count; ++i)
            {
                flags[i] = packet.ReadBits(5);
                hasWeekCap[i] = packet.ReadBit();
                hasWeekCount[i] = packet.ReadBit();
                hasSeasonTotal[i] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                //packet.WriteLine("[{0}] Flags {1}", i, flags[i]);
                packet.ReadUInt32("Currency count", i);
                packet.ReadUInt32("Entry", i);

                if (hasWeekCap[i])
                    packet.ReadUInt32("Weekly cap", i);

                if (hasWeekCount[i])
                    packet.ReadUInt32("Weekly count", i);
            }
        }

        [Parser(Opcode.SMSG_LOG_XPGAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];

            var xpnoBonus = packet.ReadBit("hasnoXPBonus");

            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var grnoBonus = packet.ReadBit("hasnoGroupBonus");

            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var unkb = packet.ReadBit("unk1");
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            //packet.ReadSingle("unk2");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);

            packet.ReadXORByte(guid, 0);

            packet.ReadInt32("XP");
            if (!xpnoBonus) packet.ReadInt32("XP+bonus");
            if (unkb) packet.ReadByte("unk2");
            packet.ReadBoolean("RAF Bonus");

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            var count = packet.ReadBits("Count", 21);

            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);

            for (var i = 0; i < count; i++)
            {
                packet.ReadEnum<PowerType>("Power type", TypeCode.Byte, i); // Actually powertype for class
                packet.ReadInt32("Value", i);
            }

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
        }
    }
}
