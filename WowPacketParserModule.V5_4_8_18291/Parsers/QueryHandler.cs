using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
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
            var entry = packet.ReadEntry("Entry"); // +5

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };
            response.Entry = (uint) entry.Key;

            Bit hasData = packet.ReadBit(); //+16
            response.HasData = hasData;
            if (!hasData)
                return; // nothing to do

            creature.ModelIDs = new uint?[4];
            creature.KillCredits = new uint?[2];

            uint bits24 = packet.ReadBits(11); //+7
            uint qItemCount = packet.ReadBits(22); //+72
            int bits1C = (int)packet.ReadBits(11); //+9

            var stringLens = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            creature.RacialLeader = packet.ReadBit("Racial Leader"); //+68
            uint bits2C = packet.ReadBits(6); //+136

            if (bits1C > 1)
                creature.TitleAlt = packet.ReadCString("TitleAlt");

            creature.KillCredits[0] = packet.ReadUInt32(); //+27
            creature.ModelIDs[3] = packet.ReadUInt32(); //+32
            creature.ModelIDs[2] = packet.ReadUInt32(); //+31
            creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("Expansion"); //+24
            creature.Type = packet.ReadInt32E<CreatureType>("Type"); //+12
            creature.HealthModifier = packet.ReadSingle("Modifier 1"); //+15

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank"); //+14
            creature.MovementID = packet.ReadUInt32("Movement ID"); //+23

            var name = new string[4];
            var femaleName = new string[4];
            for (int i = 0; i < 4; ++i)
            {
                if (stringLens[i][1] > 1)
                    femaleName[i] = packet.ReadCString("Female Name", i);
                if (stringLens[i][0] > 1)
                    name[i] = packet.ReadCString("Name", i);
            }
            creature.Name = name[0];
            creature.FemaleName = femaleName[0];

            if (bits24 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.ModelIDs[0] = packet.ReadUInt32(); //+29
            creature.ModelIDs[1] = packet.ReadUInt32(); //+30

            if (bits2C > 1)
                creature.IconName = packet.ReadCString("Icon Name"); //+100

            //creature.QuestItems = new uint[qItemCount];
            for (int i = 0; i < qItemCount; ++i)
                response.QuestItems.Add((uint)packet.ReadInt32<ItemId>("Quest Item", i)); //+72

            creature.KillCredits[1] = packet.ReadUInt32(); //+28
            creature.ManaModifier = packet.ReadSingle("Modifier 2"); //+16
            creature.Family = packet.ReadInt32E<CreatureFamily>("Family"); //+13

            for (int i = 0; i < 4; ++i)
                response.Models.Add(packet.AddValue("Display ID", creature.ModelIDs[i], i) ?? 0);
            for (int i = 0; i < 2; ++i)
                response.KillCredits.Add(packet.AddValue("Kill Credit", creature.KillCredits[i], i) ?? 0);

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

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
            packet.ReadTime("Hotfix date");
            var type = packet.ReadUInt32E<DB2Hash>("DB2 File");

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

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

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            PacketQueryPlayerNameResponseWrapper responses = packet.Holder.QueryPlayerNameResponse = new();
            PacketQueryPlayerNameResponse response = new();
            responses.Responses.Add(response);
            var guid4 = new byte[8];
            var guid5 = new byte[8];
            var guid1 = new byte[8];

            var bit18 = false;

            guid1[3] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[1] = packet.ReadBit();

            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 2);

            var hasData = packet.ReadByte("HasData");
            if (hasData == 0)
            {
                response.HasData = true;
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("AccountId");
                response.Class = (uint)packet.ReadByteE<Class>("Class");
                response.Race = (uint)packet.ReadByteE<Race>("Race");
                response.Level = packet.ReadByte("Level");
                response.Gender = (uint)packet.ReadByteE<Gender>("Gender");
            }

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 3);

            if (hasData == 0)
            {
                guid4[2] = packet.ReadBit();
                guid4[7] = packet.ReadBit();
                guid5[7] = packet.ReadBit();
                guid5[2] = packet.ReadBit();
                guid5[0] = packet.ReadBit();
                bit18 = packet.ReadBit();

                guid4[4] = packet.ReadBit();
                guid5[5] = packet.ReadBit();
                guid4[1] = packet.ReadBit();
                guid4[3] = packet.ReadBit();
                guid4[0] = packet.ReadBit();

                var count = new int[5];
                for (var i = 0; i < 5; ++i)
                    count[i] = (int)packet.ReadBits(7);

                guid5[6] = packet.ReadBit();
                guid5[3] = packet.ReadBit();
                guid4[5] = packet.ReadBit();
                guid5[1] = packet.ReadBit();
                guid5[4] = packet.ReadBit();

                var nameLen = (int)packet.ReadBits(6);

                guid4[6] = packet.ReadBit();

                packet.ReadXORByte(guid5, 6);
                packet.ReadXORByte(guid5, 0);
                response.PlayerName = packet.ReadWoWString("Name", nameLen);
                packet.ReadXORByte(guid4, 5);
                packet.ReadXORByte(guid4, 2);
                packet.ReadXORByte(guid5, 3);
                packet.ReadXORByte(guid4, 4);
                packet.ReadXORByte(guid4, 3);
                packet.ReadXORByte(guid5, 4);
                packet.ReadXORByte(guid5, 2);
                packet.ReadXORByte(guid4, 7);

                for (var i = 0; i < 5; ++i)
                    packet.ReadWoWString("Name Declined", count[i], i);

                packet.ReadXORByte(guid4, 6);
                packet.ReadXORByte(guid5, 7);
                packet.ReadXORByte(guid5, 1);
                packet.ReadXORByte(guid4, 1);
                packet.ReadXORByte(guid5, 5);
                packet.ReadXORByte(guid4, 0);

                packet.WriteGuid("Guid4", guid4);
                packet.WriteGuid("Guid5", guid5);
            }

            response.PlayerGuid = packet.WriteGuid("Guid1", guid1);
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            var hasRealmId2 = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var hasRealmId1 = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ParseBitStream(guid, 7, 5, 1, 2, 6, 3, 0, 4);

            if (hasRealmId2)
                packet.ReadInt32("Realm Id 2");

            if (hasRealmId1)
                packet.ReadInt32("Realm Id 1");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 2, 1, 3, 7, 6, 4, 0, 5);
            packet.ParseBitStream(guid, 0, 6, 3, 5, 1, 7, 4, 2);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            Bit hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            PageText pageText = new PageText();

            uint textLen = packet.ReadBits(12);

            pageText.NextPageID = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            pageText.Text = packet.ReadWoWString("Page Text", textLen);
            uint entry = packet.ReadUInt32("Entry");
            pageText.ID = entry;

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }
    }
}
