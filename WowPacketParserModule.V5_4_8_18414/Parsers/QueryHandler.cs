using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_8_18414.Enums;
using Guid = WowPacketParser.Misc.WowGuid;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.ReadUInt32("Entry");
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadEnum<DB2Hash>("DB2 File", TypeCode.Int32);
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 6, 3, 0, 1, 4, 5, 7, 2);
            }

            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 1);

                packet.ReadInt32("Entry", i);

                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 5);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadXORByte(guids[i], 3);

                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];
            guid[4] = packet.ReadBit();
            var hasRealmID = packet.ReadBit("hasRealmID"); // 20
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var byte28 = packet.ReadBit("has byte28");
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ParseBitStream(guid, 7, 5, 1, 2, 6, 3, 0, 4);
            packet.WriteGuid("Guid", guid);

            if (hasRealmID)
                packet.ReadInt32("RealmID"); // 20

            if (byte28)
                packet.ReadInt32("int24");
        }

        [Parser(Opcode.CMSG_PAGE_TEXT_QUERY)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 1, 5, 2, 3, 6, 4, 0, 7);
            packet.ParseBitStream(guid, 6, 4, 0, 3, 7, 5, 2, 1);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_CORPSE_MAP_POSITION_QUERY_RESPONSE)]
        public static void HandleCorpseMapPositionQueryResponce(Packet packet)
        {
            packet.ReadSingle("Y");
            packet.ReadSingle("X");
            packet.ReadSingle("O");
            packet.ReadSingle("Z");
        }

        [Parser(Opcode.SMSG_CORPSE_QUERY_RESPONSE)]
        public static void HandleCorpseQuery(Packet packet)
        {
            var pos = new Vector3();
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadBit("Corpse Found");
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            packet.ReadXORByte(guid, 5);
            pos.Z = packet.ReadSingle();
            packet.ReadXORByte(guid, 1);
            packet.ReadEntry<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            pos.X = packet.ReadSingle();
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadEntry<Int32>(StoreNameType.Map, "Corpse Map ID");
            pos.Y = packet.ReadSingle();

            packet.WriteLine("Position: {0}", pos);
            packet.WriteGuid("Corpse Low GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");

            var hasData = packet.ReadBit("hasData");

            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            uint lengthSubname = packet.ReadBits("Subname length", 11);
            uint qItemCount = packet.ReadBits("itemCount", 22);

            packet.ReadBits(11); // Unk String length. Needs reading somewhere?
			
            var lengthName = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                lengthName[i] = new int[2];
                lengthName[i][0] = (int)packet.ReadBits("Name length female", 11);
                lengthName[i][1] = (int)packet.ReadBits("Name length male", 11);
            }

            creature.RacialLeader = packet.ReadBit("Racial Leader");

            uint lengthIconName = packet.ReadBits(6) ^ 1;

            creature.KillCredits = new uint[2];
            creature.KillCredits[0] = packet.ReadUInt32("Kill Credit 1");
			
            creature.DisplayIds = new uint[4];
            creature.DisplayIds[3] = packet.ReadUInt32("Display Id 4");
            creature.DisplayIds[1] = packet.ReadUInt32("Display Id 2");
			
            creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);
			
            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);
			
            creature.Modifier2 = packet.ReadSingle("Modifier Health");

            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);
			
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2");

            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);
            creature.MovementId = packet.ReadUInt32("Movement Id");

            var name = new string[8];
            for (var i = 0; i < 4; i++)
            {
                if (lengthName[i][1] > 1)
                    packet.ReadCString("Male Name", i);

                if (lengthName[i][0] > 1)
                    name[i] = packet.ReadCString("Female name", i);
            }
            creature.Name = name[0];
			
            if (lengthSubname > 1)
                creature.SubName = packet.ReadCString("Sub Name");
				
			
            creature.DisplayIds[0] = packet.ReadUInt32("Display Id 1");	
				
            creature.DisplayIds[2] = packet.ReadUInt32("Display Id 3");
			
            if (lengthIconName > 1)
                creature.IconName = packet.ReadCString("Icon Name");
			
            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; i++)
                creature.QuestItems[i] = (uint)packet.ReadEntry<UInt32>(StoreNameType.Item, "Quest Item", i);			
			
            creature.KillCredits[1] = packet.ReadUInt32("Kill Credit 2");

            creature.Modifier1 = packet.ReadSingle("Modifier Mana");

            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid = packet.StartBitStream(3, 6, 7, 2, 5, 4, 0, 1);
            packet.ParseBitStream(guid, 5, 4, 7, 6, 1, 2);

            var nameData = !packet.ReadBoolean("!nameData");
            if (nameData)
            {
                packet.ReadInt32("RealmID"); // 108
                packet.ReadInt32("unk36");
                packet.ReadEnum<Class>("Class", TypeCode.Byte);
                packet.ReadEnum<Race>("Race", TypeCode.Byte);
                packet.ReadByte("Level");
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
            }
            packet.ParseBitStream(guid, 0, 3);

            packet.WriteGuid("Guid", guid);

            if (!nameData)
                return;

            var guid2 = new byte[8];
            var guid3 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid3[7] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            var unk32 = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            var len = new uint[5];
            for (var i = 5; i > 0; i--)
                len[i - 1] = packet.ReadBits(7);

            guid3[6] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            guid3[4] = packet.ReadBit();

            var len56 = packet.ReadBits(6);

            guid2[6] = packet.ReadBit();

            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 0);

            var name = packet.ReadWoWString("Name", len56);
            var playerGuid = new Guid(BitConverter.ToUInt64(guid, 0));
            StoreGetters.AddName(playerGuid, name);

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid3, 2);
            packet.ReadXORByte(guid2, 7);

            for (var i = 5; i > 0; i--)
                packet.ReadWoWString("str", len[i - 1], i);

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid3, 5);
            packet.ReadXORByte(guid2, 0);

            packet.WriteLine("Account: {0}", BitConverter.ToUInt64(guid2, 0));
            packet.WriteGuid("Guid3", guid3);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Player,
                Name = name,
            };
            Storage.ObjectNames.Add((uint)playerGuid.GetLow(), objectName, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            packet.ReadInt32("TextID");

            var txtSize = packet.ReadInt32("Size");
            var Probability = new float[8];
            var TextID = new Int32[8];

            for (var i = 0; i < 8; i++)
                Probability[i] = packet.ReadSingle("Probability", i);

            for (var i = 0; i < 8; i++)
                TextID[i] = packet.ReadInt32("Broadcast Text Id", i);

            packet.ReadBit("hasData");

            //packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            //Storage.NpcTexts.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var textLen = packet.ReadBits(12);

            pageText.NextPageId = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            var entry = packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleQueryTimeResponse(Packet packet)
        {
            packet.ReadTime("Current Time");
            packet.ReadInt32("Daily Quest Reset");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleQueryRealmNameResponse(Packet packet)
        {
            var hasData = !packet.ReadBoolean("!HasData");
            packet.ReadInt32("RealmID");
            if (hasData)
            {
                var len278 = packet.ReadBits(8);
                packet.ReadBit("unk21");
                var len88 = packet.ReadBits(8);
                packet.ReadWoWString("RealmName", len88);
                packet.ReadWoWString("RealmName2", len278);
            }
        }
    }
}
