using System;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QueryHandler
    {
        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            PacketQueryCreatureResponse response = packet.Holder.QueryCreatureResponse = new PacketQueryCreatureResponse();
            CreatureTemplate creature = new CreatureTemplate();
            Bit hasData = packet.ReadBit();
            response.HasData = hasData;
            if (!hasData)
                return; // nothing to do

            creature.RacialLeader = packet.ReadBit("Racial Leader");

            uint bits2C = packet.ReadBits(6);
            uint bits24 = packet.ReadBits(11);
            uint qItemCount = packet.ReadBits(22);

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][1] = (int)packet.ReadBits(11);
                stringLens[i][0] = (int)packet.ReadBits(11);
            }

            int bits1C = (int)packet.ReadBits(11);

            creature.ModelIDs = new uint?[4];
            creature.KillCredits = new uint?[2];

            creature.ModelIDs[1] = packet.ReadUInt32("Display ID 1");
            creature.KillCredits[1] = packet.ReadUInt32("Kill Credit 2");
            creature.Type = packet.ReadInt32E<CreatureType>("Type");

            var name = new string[4];
            var femaleName = new string[4];
            for (int i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    name[i] = packet.ReadCString("Name", i);
                if (stringLens[i][1] > 1)
                    femaleName[i] = packet.ReadCString("Female Name", i);
            }
            creature.Name = name[0];
            creature.FemaleName = femaleName[0];

            creature.ManaModifier = packet.ReadSingle("Modifier 2");

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");
            creature.KillCredits[0] = packet.ReadUInt32("Kill Credit 1");
            creature.ModelIDs[3] = packet.ReadUInt32("Display ID 3");

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/response.QuestItems.Add((uint)packet.ReadInt32<ItemId>("Quest Item", i));

            creature.HealthModifier = packet.ReadSingle("Modifier 1");

            if (bits24 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.MovementID = packet.ReadUInt32("Movement ID");
            creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("Expansion");

            if (bits2C > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.ModelIDs[2] = packet.ReadUInt32("Display ID 2");
            creature.ModelIDs[0] = packet.ReadUInt32("Display ID 0");
            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");

            if (bits1C > 1)
                creature.TitleAlt = packet.ReadCString("TitleAlt");

            var entry = packet.ReadEntry("Entry");
            creature.Entry = response.Entry = (uint)entry.Key;

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            if (ClientLocale.PacketLocale != LocaleConstant.enUS)
            {
                CreatureTemplateLocale localesCreature = new CreatureTemplateLocale
                {
                    ID = (uint)entry.Key,
                    Name = creature.Name,
                    NameAlt = creature.FemaleName,
                    Title = creature.SubName,
                    TitleAlt = creature.TitleAlt
                };

                Storage.LocalesCreatures.Add(localesCreature, packet.TimeSpan);
            }

            Storage.CreatureTemplates.Add(creature.Entry.Value, creature, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = StoreNameType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);

            for (int i = 0; i < 4; ++i)
                response.Models.Add(creature.ModelIDs[i] ?? 0);
            for (int i = 0; i < 2; ++i)
                response.KillCredits.Add(creature.KillCredits[i] ?? 0);
            response.Name = creature.Name;
            response.NameAlt = creature.FemaleName;
            response.Title = creature.SubName;
            response.TitleAlt = creature.TitleAlt;
            response.IconName = creature.IconName;
            response.TypeFlags = (uint?)creature.TypeFlags ?? 0;
            response.TypeFlags2 = creature.TypeFlags2 ?? 0;
            response.Type = (int?)creature.Type ?? 0;
            response.Family = (int?)creature.Family ?? 0;
            response.Rank = (int?)creature.Rank ?? 0;
            response.HpMod = creature.HealthModifier ?? 1.0f;
            response.ManaMod = creature.ManaModifier ?? 1.0f;
            response.Leader = creature.RacialLeader ?? false;
            response.Expansion = (uint?) creature.RequiredExpansion ?? 0;
            response.MovementId = creature.MovementID ?? 0;
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            uint count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 3, 7, 5, 6, 2, 0, 4, 1);
            }

            packet.ResetBitReader();
            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 5, 1, 4, 6, 7, 2, 0, 3);
                packet.ReadInt32("Entry", i);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
            packet.ReadTime("Hotfix date");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            var type = packet.ReadUInt32E<DB2Hash>("DB2 File");
            if (entry < 0)
            {
                packet.WriteLine("Row {0} has been removed.", -entry);
                HotfixStoreMgr.RemoveRecord(type, entry);
            }
            else
            {
                packet.AddSniffData(StoreNameType.None, entry, type.ToString());
                HotfixStoreMgr.AddRecord(type, entry, db2File);
                db2File.ClosePacket(false);
            }
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            var hasRealmId2 = packet.ReadBit();
            packet.StartBitStream(guid, 0, 6, 2, 1, 7);

            var hasRealmId1 = packet.ReadBit();
            packet.StartBitStream(guid, 3, 5, 4);
            packet.ParseBitStream(guid, 0, 5, 2, 4, 7, 6, 1, 3);

            if (hasRealmId1)
                packet.ReadInt32("Realm Id 1");

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 0, 7, 3, 5, 6, 2, 4, 1);
            packet.ParseBitStream(guid, 0, 5, 1, 3, 2, 4, 7, 6);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            Bit hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            PageText pageText = new PageText();

            uint textLen = packet.ReadBits(12);

            packet.ResetBitReader();
            pageText.Text = packet.ReadWoWString("Page Text", textLen);

            pageText.NextPageID = packet.ReadUInt32("Next Page");
            uint entry = packet.ReadUInt32("Entry");
            pageText.ID = entry;
            packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            PacketQueryPlayerNameResponseWrapper responses = packet.Holder.QueryPlayerNameResponse = new();
            PacketQueryPlayerNameResponse response = new();
            responses.Responses.Add(response);
            var guid1 = new byte[8];
            var accountId = new byte[8];
            var guid2 = new byte[8];

            guid1[4] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 1);

            var hasData = packet.ReadByte("HasData");
            if (hasData == 0)
            {
                response.HasData = true;
                response.Class = (uint)packet.ReadByteE<Class>("Class");
                response.Gender = (uint)packet.ReadByteE<Gender>("Gender");
                packet.ReadInt32("Realm Id");
                response.Race = (uint)packet.ReadByteE<Race>("Race");
                packet.ReadInt32("Int24");
                response.Level = packet.ReadByte("Level");
            }

            packet.ReadXORByte(guid1, 5);

            if (hasData == 0)
            {
                guid2[3] = packet.ReadBit();
                accountId[7] = packet.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                accountId[3] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                accountId[5] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                accountId[0] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
                guid2[7] = packet.ReadBit();
                accountId[6] = packet.ReadBit();
                accountId[1] = packet.ReadBit();
                var bit20 = packet.ReadBit();
                guid2[1] = packet.ReadBit();
                var bits38 = (int)packet.ReadBits(6);
                guid2[2] = packet.ReadBit();
                accountId[4] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                accountId[2] = packet.ReadBit();

                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid2, 5);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                response.PlayerName = packet.ReadWoWString("Name", bits38);

                packet.ReadXORByte(accountId, 2);
                packet.ReadXORByte(accountId, 5);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(accountId, 0);
                packet.ReadXORByte(accountId, 6);
                packet.ReadXORByte(accountId, 1);
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(accountId, 4);
                packet.ReadXORByte(accountId, 3);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(accountId, 7);
                packet.ReadXORByte(guid2, 2);

                packet.AddValue("Account", BitConverter.ToUInt64(accountId, 0));
                packet.WriteGuid("Guid2", guid2);

            }

            packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_QUERY_REALM_NAME)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.ReadInt32("Realm Id");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadByte("Unk byte");
            packet.ReadInt32("Realm Id");

            packet.ReadBit();

            var bits22 = packet.ReadBits(8);
            var bits278 = packet.ReadBits(8);

            packet.ReadWoWString("Realmname", bits22);
            packet.ReadWoWString("Realmname (without white char)", bits278);
        }
    }
}
