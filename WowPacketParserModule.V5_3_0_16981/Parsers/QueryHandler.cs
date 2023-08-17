using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
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
            var entry = packet.ReadEntry("Entry");
            response.Entry = (uint)entry.Key;
            var hasData = packet.ReadBit();
            response.HasData = hasData;
            if (!hasData)
                return; // nothing to do

            var creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            var lenS3 = packet.ReadBits(11);
            creature.RacialLeader = packet.ReadBit("Racial Leader");

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            var lenS4 = packet.ReadBits(6);
            var lenS5 = packet.ReadBits(11);
            var qItemCount = packet.ReadBits(22);

            packet.ResetBitReader();

            var name = new string[8];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    packet.ReadCString("Female Name", i);
                if (stringLens[i][1] > 1)
                    name[i] = packet.ReadCString("Name", i);
            }
            creature.Name = name[0];

            creature.HealthModifier = packet.ReadSingle("HealthModifier");
            if (lenS3 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/response.QuestItems.Add((uint)packet.ReadInt32<ItemId>("Quest Item", i));

            creature.Type = packet.ReadInt32E<CreatureType>("Type");
            creature.KillCredits = new uint?[2];
            for (var i = 0; i < 2; ++i)
            {
                creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);
                response.KillCredits.Add(creature.KillCredits[i] ?? 0);
            }
            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");
            if (lenS4 > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.ModelIDs = new uint?[4];
            creature.ModelIDs[1] = packet.ReadUInt32("CreatureDisplayID", 1);
            creature.ModelIDs[0] = packet.ReadUInt32("CreatureDisplayID", 0);
            creature.MovementID = packet.ReadUInt32("MovementID");
            creature.ModelIDs[3] = packet.ReadUInt32("CreatureDisplayID", 3);

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            if (lenS5 > 1)
                packet.ReadCString("string5");
            creature.ModelIDs[2] = packet.ReadUInt32("CreatureDisplayID", 2);
            creature.ManaModifier = packet.ReadSingle("ManaModifier");
            creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("Expansion");

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

            var objectName = new ObjectName
            {
                ObjectType = StoreNameType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);

            for (int i = 0; i < 4; ++i)
                response.Models.Add(creature.ModelIDs[i] ?? 0);
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

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");

            var guid = packet.StartBitStream(7, 1, 4, 3, 0, 2, 6, 5);
            packet.ParseBitStream(guid, 4, 5, 6, 7, 1, 0, 2, 3);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME)]
        public static void HandlePlayerQueryName(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bit16 = packet.ReadBit("bit16");
            guid[6] = packet.ReadBit();
            var bit24 = packet.ReadBit("bit24");

            packet.ParseBitStream(guid, 6, 0, 2, 3, 4, 5, 7, 1);

            if (bit24)
                packet.ReadUInt32("unk28");

            if (bit16)
                packet.ReadUInt32("unk20");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REALM_QUERY)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.ReadUInt32("Realm Id");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadUInt32("Realm Id");
            packet.ReadByte("byte20");

            var bits22 = packet.ReadBits(8);
            var bits278 = packet.ReadBits(8);
            packet.ReadBit();

            packet.ReadWoWString("Realmname (without white char)", bits278);
            packet.ReadWoWString("Realmname", bits22);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            PacketQueryPlayerNameResponseWrapper responses = packet.Holder.QueryPlayerNameResponse = new();
            PacketQueryPlayerNameResponse response = new();
            responses.Responses.Add(response);
            var guid = new byte[8];

            var bit16 = packet.ReadBit();
            packet.StartBitStream(guid, 1, 3, 2);

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.ReadBits("bits", 7);

            var bits32 = packet.ReadBits(6);
            packet.StartBitStream(guid, 6, 4, 0);
            packet.ReadBit();
            packet.StartBitStream(guid, 5, 7);

            packet.ReadXORByte(guid, 1);
            response.PlayerName = packet.ReadWoWString("Name: ", bits32);
            packet.ReadXORBytes(guid, 0, 7);

            response.Race = packet.ReadByte("Race");
            packet.ReadByte("unk81");
            response.Gender = packet.ReadByte("Gender");

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.ReadCString("Declined Name");

            response.Class = packet.ReadByte("Class");
            packet.ReadXORBytes(guid, 4, 6, 5);
            packet.ReadUInt32("Realm Id");
            packet.ReadXORBytes(guid, 3, 2);

            response.PlayerGuid = packet.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.ReadUInt32E<DB2Hash>("DB2 File");
            packet.ReadTime("Hotfix date");
            var size = packet.ReadInt32("Size");

            var data = packet.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            var entry = packet.ReadInt32("Entry");
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

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var entry = packet.ReadUInt32("Entry");

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var pageText = new PageText();
            pageText.ID = entry;

            var textLen = (int)packet.ReadBits(12);

            packet.ResetBitReader();

            pageText.Text = packet.ReadWoWString("Page Text", textLen);
            pageText.NextPageID = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse510(Packet packet)
        {
            var hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            var questTurnTextWindow = (int)packet.ReadBits(10);
            var details = (int)packet.ReadBits(12);
            var questGiverTextWindow = (int)packet.ReadBits(10);
            var len1658 = (int)packet.ReadBits(9);
            var completedText = (int)packet.ReadBits(11);
            var len158 = (int)packet.ReadBits(12);
            var questGiverTargetName = (int)packet.ReadBits(8);
            var title = (int)packet.ReadBits(9);
            var questTurnTargetName = (int)packet.ReadBits(8);
            var count = (int)packet.ReadBits("Requirement Count", 19);

            var len294920 = new int[count];
            var counter = new int[count];
            for (var i = 0; i < count; ++i)
            {
                len294920[i] = (int)packet.ReadBits(8);
                counter[i] = (int)packet.ReadBits(22);
            }
            packet.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.ReadWoWString("string2949+20", len294920[i], i);
                packet.ReadInt32("int2949+16", i);
                packet.ReadByte("byte2949+5", i);
                packet.ReadByte("byte2949+4", i);
                packet.ReadInt32("int2949+12", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.ReadInt32("Unk UInt32", i, j);

                packet.ReadInt32("Unk UInt32", i);
                packet.ReadInt32("int2949+8", i);
            }

            packet.ReadWoWString("string158", len158);
            packet.ReadInt32<QuestId>("Next Chain Quest");
            packet.ReadInt32("int2971");
            packet.ReadInt32<SpellId>("Reward Spell Cast");
            packet.ReadInt32("int2955");
            packet.ReadSingle("Reward Honor Multiplier");
            packet.ReadInt32("int2970");
            packet.ReadInt32("int2984");
            packet.ReadInt32("int2979");
            packet.ReadInt32("Min Level");
            packet.ReadUInt32("RewSkillPoints");
            packet.ReadUInt32("QuestGiverPortrait");
            packet.ReadInt32("int21");
            packet.ReadInt32E<QuestInfo>("Type");
            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("int2986+40");
                packet.ReadInt32("int2986+0");
                packet.ReadInt32("int2986+20");
            }

            packet.ReadInt32("int2960");
            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("int3001+16");
                packet.ReadInt32("int3001+0");
            }
            packet.ReadUInt32("Suggested Players");
            packet.ReadInt32("int2972");
            packet.ReadInt32("int2959");
            packet.ReadWoWString("Title", title);
            packet.ReadInt32("int2965");
            packet.ReadInt32("int2978");
            packet.ReadUInt32("RewSkillId");
            packet.ReadInt32("int2982");
            packet.ReadInt32("int2968");
            packet.ReadInt32("int2964");
            packet.ReadInt32("int2957");
            packet.ReadInt32("int2969");
            packet.ReadInt32("int1786");
            packet.ReadUInt32("Sound Accept");
            packet.ReadInt32("int2981");
            packet.ReadInt32("int2961");
            packet.ReadInt32("int15");
            packet.ReadInt32("int2967");
            packet.ReadWoWString("Completed Text", completedText);
            packet.ReadInt32("int25");
            packet.ReadInt32("Quest Id");
            packet.ReadSingle("Point Y");
            packet.ReadInt32("int2974");
            packet.ReadInt32("int2952");
            packet.ReadWoWString("Details", details);
            packet.ReadInt32("Level");
            packet.ReadUInt32("Point Map ID");
            packet.ReadWoWString("string1658", len1658);
            packet.ReadSingle("Point X");
            packet.ReadInt32("int17");
            packet.ReadInt32("int2962");
            packet.ReadWoWString("QuestGiver Text Window", questGiverTextWindow);
            packet.ReadInt32("int2963");
            packet.ReadInt32("int2985");
            packet.ReadInt32E<QuestType>("Method");
            packet.ReadUInt32("RewRepMask");
            packet.ReadInt32("int2953");
            packet.ReadInt32("int2983");
            packet.ReadInt32("int9");
            packet.ReadWoWString("QuestGiver Target Name", questGiverTargetName);
            packet.ReadInt32E<QuestSort>("Sort");
            packet.ReadInt32("int1788");
            packet.ReadUInt32("Sound TurnIn");
            packet.ReadUInt32<ItemId>("Source Item ID");
            packet.ReadWoWString("QuestTurn Target Name", questTurnTargetName);
            packet.ReadUInt32("QuestTurnInPortrait");
            packet.ReadUInt32E<QuestFlags>("Flags");
            packet.ReadInt32("int2954");
            packet.ReadInt32("int2958");
            packet.ReadUInt32("Reward Money Max Level");
            packet.ReadInt32("int1787");
            packet.ReadWoWString("QuestTurn Text Window", questTurnTextWindow);
            packet.ReadInt32("int2977");
            packet.ReadInt32("int2980");
            packet.ReadInt32("int2975");
            packet.ReadInt32<SpellId>("Reward Spell");
            packet.ReadInt32("Reward Money");
            packet.ReadInt32("int2973");
            packet.ReadInt32("int2966");
            packet.ReadInt32("int2976");
            packet.ReadUInt32("Point Opt");
            packet.ReadInt32("int2956");

            var id = packet.ReadEntry("Quest ID");

            packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var npcText = new NpcTextMop
            {
                ID = (uint)entry.Key
            };

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
            var proto = packet.Holder.NpcText = new() { Entry = npcText.ID.Value };
            for (int i = 0; i < 8; ++i)
                proto.Texts.Add(new PacketNpcTextEntry(){Probability = npcText.Probabilities[i], BroadcastTextId = npcText.BroadcastTextId[i]});
        }
    }
}
