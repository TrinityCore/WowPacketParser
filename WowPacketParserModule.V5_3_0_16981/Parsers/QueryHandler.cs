using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
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
            packet.Translator.ReadInt32("Entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");
            var hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            var creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            var lenS3 = packet.Translator.ReadBits(11);
            creature.RacialLeader = packet.Translator.ReadBit("Racial Leader");

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.Translator.ReadBits(11);
                stringLens[i][1] = (int)packet.Translator.ReadBits(11);
            }

            var lenS4 = packet.Translator.ReadBits(6);
            var lenS5 = packet.Translator.ReadBits(11);
            var qItemCount = packet.Translator.ReadBits(22);

            packet.Translator.ResetBitReader();

            var name = new string[8];
            for (var i = 0; i < 4; ++i)
            {
                if (stringLens[i][0] > 1)
                    packet.Translator.ReadCString("Female Name", i);
                if (stringLens[i][1] > 1)
                    name[i] = packet.Translator.ReadCString("Name", i);
            }
            creature.Name = name[0];

            creature.HealthModifier = packet.Translator.ReadSingle("HealthModifier");
            if (lenS3 > 1)
                creature.SubName = packet.Translator.ReadCString("Sub Name");

            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Rank");

            //TODO: move to creature_questitems
            //creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                /*creature.QuestItems[i] = (uint)*/packet.Translator.ReadInt32<ItemId>("Quest Item", i);

            creature.Type = packet.Translator.ReadInt32E<CreatureType>("Type");
            creature.KillCredits = new uint?[2];
            for (var i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.Translator.ReadUInt32("Kill Credit", i);
            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("Family");
            if (lenS4 > 1)
                creature.IconName = packet.Translator.ReadCString("Icon Name");

            creature.ModelIDs = new uint?[4];
            creature.ModelIDs[1] = packet.Translator.ReadUInt32("CreatureDisplayID", 1);
            creature.ModelIDs[0] = packet.Translator.ReadUInt32("CreatureDisplayID", 0);
            creature.MovementID = packet.Translator.ReadUInt32("MovementID");
            creature.ModelIDs[3] = packet.Translator.ReadUInt32("CreatureDisplayID", 3);

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2"); // Missing enum

            if (lenS5 > 1)
                packet.Translator.ReadCString("string5");
            creature.ModelIDs[2] = packet.Translator.ReadUInt32("CreatureDisplayID", 2);
            creature.ManaModifier = packet.Translator.ReadSingle("ManaModifier");
            creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("Expansion");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.CreatureTemplates.Add(creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");

            var guid = packet.Translator.StartBitStream(7, 1, 4, 3, 0, 2, 6, 5);
            packet.Translator.ParseBitStream(guid, 4, 5, 6, 7, 1, 0, 2, 3);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_QUERY_PLAYER_NAME)]
        public static void HandlePlayerQueryName(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var bit16 = packet.Translator.ReadBit("bit16");
            guid[6] = packet.Translator.ReadBit();
            var bit24 = packet.Translator.ReadBit("bit24");

            packet.Translator.ParseBitStream(guid, 6, 0, 2, 3, 4, 5, 7, 1);

            if (bit24)
                packet.Translator.ReadUInt32("unk28");

            if (bit16)
                packet.Translator.ReadUInt32("unk20");
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REALM_QUERY)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.Translator.ReadUInt32("Realm Id");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Realm Id");
            packet.Translator.ReadByte("byte20");

            var bits22 = packet.Translator.ReadBits(8);
            var bits278 = packet.Translator.ReadBits(8);
            packet.Translator.ReadBit();

            packet.Translator.ReadWoWString("Realmname (without white char)", bits278);
            packet.Translator.ReadWoWString("Realmname", bits22);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid = new byte[8];

            var bit16 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 1, 3, 2);

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadBits("bits", 7);

            var bits32 = packet.Translator.ReadBits(6);
            packet.Translator.StartBitStream(guid, 6, 4, 0);
            packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid, 5, 7);

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadWoWString("Name: ", bits32);
            packet.Translator.ReadXORBytes(guid, 0, 7);

            packet.Translator.ReadByte("Race");
            packet.Translator.ReadByte("unk81");
            packet.Translator.ReadByte("Gender");

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadCString("Declined Name");

            packet.Translator.ReadByte("Class");
            packet.Translator.ReadXORBytes(guid, 4, 6, 5);
            packet.Translator.ReadUInt32("Realm Id");
            packet.Translator.ReadXORBytes(guid, 3, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            var type = packet.Translator.ReadUInt32E<DB2Hash>("DB2 File");
            packet.Translator.ReadTime("Hotfix date");
            var size = packet.Translator.ReadInt32("Size");

            var data = packet.Translator.ReadBytes(size);
            var db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);

            var entry = packet.Translator.ReadInt32("Entry");
            if (entry < 0)
            {
                packet.Formatter.AppendItem("Row {0} has been removed.", -entry);
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
            var entry = packet.Translator.ReadUInt32("Entry");

            var hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            var pageText = new PageText();
            pageText.ID = entry;

            var textLen = (int)packet.Translator.ReadBits(12);

            packet.Translator.ResetBitReader();

            pageText.Text = packet.Translator.ReadWoWString("Page Text", textLen);
            pageText.NextPageID = packet.Translator.ReadUInt32("Next Page");
            packet.Translator.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse510(Packet packet)
        {
            var hasData = packet.Translator.ReadBit();
            if (!hasData)
            {
                packet.Translator.ReadUInt32("Entry");
                return; // nothing to do
            }

            var questTurnTextWindow = (int)packet.Translator.ReadBits(10);
            var details = (int)packet.Translator.ReadBits(12);
            var questGiverTextWindow = (int)packet.Translator.ReadBits(10);
            var len1658 = (int)packet.Translator.ReadBits(9);
            var completedText = (int)packet.Translator.ReadBits(11);
            var len158 = (int)packet.Translator.ReadBits(12);
            var questGiverTargetName = (int)packet.Translator.ReadBits(8);
            var title = (int)packet.Translator.ReadBits(9);
            var questTurnTargetName = (int)packet.Translator.ReadBits(8);
            var count = (int)packet.Translator.ReadBits("Requirement Count", 19);

            var len294920 = new int[count];
            var counter = new int[count];
            for (var i = 0; i < count; ++i)
            {
                len294920[i] = (int)packet.Translator.ReadBits(8);
                counter[i] = (int)packet.Translator.ReadBits(22);
            }
            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadWoWString("string2949+20", len294920[i], i);
                packet.Translator.ReadInt32("int2949+16", i);
                packet.Translator.ReadByte("byte2949+5", i);
                packet.Translator.ReadByte("byte2949+4", i);
                packet.Translator.ReadInt32("int2949+12", i);

                for (var j = 0; j < counter[i]; ++j)
                    packet.Translator.ReadInt32("Unk UInt32", i, j);

                packet.Translator.ReadInt32("Unk UInt32", i);
                packet.Translator.ReadInt32("int2949+8", i);
            }

            packet.Translator.ReadWoWString("string158", len158);
            packet.Translator.ReadInt32<QuestId>("Next Chain Quest");
            packet.Translator.ReadInt32("int2971");
            packet.Translator.ReadInt32<SpellId>("Reward Spell Cast");
            packet.Translator.ReadInt32("int2955");
            packet.Translator.ReadSingle("Reward Honor Multiplier");
            packet.Translator.ReadInt32("int2970");
            packet.Translator.ReadInt32("int2984");
            packet.Translator.ReadInt32("int2979");
            packet.Translator.ReadInt32("Min Level");
            packet.Translator.ReadUInt32("RewSkillPoints");
            packet.Translator.ReadUInt32("QuestGiverPortrait");
            packet.Translator.ReadInt32("int21");
            packet.Translator.ReadInt32E<QuestInfo>("Type");
            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadInt32("int2986+40");
                packet.Translator.ReadInt32("int2986+0");
                packet.Translator.ReadInt32("int2986+20");
            }

            packet.Translator.ReadInt32("int2960");
            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("int3001+16");
                packet.Translator.ReadInt32("int3001+0");
            }
            packet.Translator.ReadUInt32("Suggested Players");
            packet.Translator.ReadInt32("int2972");
            packet.Translator.ReadInt32("int2959");
            packet.Translator.ReadWoWString("Title", title);
            packet.Translator.ReadInt32("int2965");
            packet.Translator.ReadInt32("int2978");
            packet.Translator.ReadUInt32("RewSkillId");
            packet.Translator.ReadInt32("int2982");
            packet.Translator.ReadInt32("int2968");
            packet.Translator.ReadInt32("int2964");
            packet.Translator.ReadInt32("int2957");
            packet.Translator.ReadInt32("int2969");
            packet.Translator.ReadInt32("int1786");
            packet.Translator.ReadUInt32("Sound Accept");
            packet.Translator.ReadInt32("int2981");
            packet.Translator.ReadInt32("int2961");
            packet.Translator.ReadInt32("int15");
            packet.Translator.ReadInt32("int2967");
            packet.Translator.ReadWoWString("Completed Text", completedText);
            packet.Translator.ReadInt32("int25");
            packet.Translator.ReadInt32("Quest Id");
            packet.Translator.ReadSingle("Point Y");
            packet.Translator.ReadInt32("int2974");
            packet.Translator.ReadInt32("int2952");
            packet.Translator.ReadWoWString("Details", details);
            packet.Translator.ReadInt32("Level");
            packet.Translator.ReadUInt32("Point Map ID");
            packet.Translator.ReadWoWString("string1658", len1658);
            packet.Translator.ReadSingle("Point X");
            packet.Translator.ReadInt32("int17");
            packet.Translator.ReadInt32("int2962");
            packet.Translator.ReadWoWString("QuestGiver Text Window", questGiverTextWindow);
            packet.Translator.ReadInt32("int2963");
            packet.Translator.ReadInt32("int2985");
            packet.Translator.ReadInt32E<QuestType>("Method");
            packet.Translator.ReadUInt32("RewRepMask");
            packet.Translator.ReadInt32("int2953");
            packet.Translator.ReadInt32("int2983");
            packet.Translator.ReadInt32("int9");
            packet.Translator.ReadWoWString("QuestGiver Target Name", questGiverTargetName);
            packet.Translator.ReadInt32E<QuestSort>("Sort");
            packet.Translator.ReadInt32("int1788");
            packet.Translator.ReadUInt32("Sound TurnIn");
            packet.Translator.ReadUInt32<ItemId>("Source Item ID");
            packet.Translator.ReadWoWString("QuestTurn Target Name", questTurnTargetName);
            packet.Translator.ReadUInt32("QuestTurnInPortrait");
            packet.Translator.ReadUInt32E<QuestFlags>("Flags");
            packet.Translator.ReadInt32("int2954");
            packet.Translator.ReadInt32("int2958");
            packet.Translator.ReadUInt32("Reward Money Max Level");
            packet.Translator.ReadInt32("int1787");
            packet.Translator.ReadWoWString("QuestTurn Text Window", questTurnTextWindow);
            packet.Translator.ReadInt32("int2977");
            packet.Translator.ReadInt32("int2980");
            packet.Translator.ReadInt32("int2975");
            packet.Translator.ReadInt32<SpellId>("Reward Spell");
            packet.Translator.ReadInt32("Reward Money");
            packet.Translator.ReadInt32("int2973");
            packet.Translator.ReadInt32("int2966");
            packet.Translator.ReadInt32("int2976");
            packet.Translator.ReadUInt32("Point Opt");
            packet.Translator.ReadInt32("int2956");

            var id = packet.Translator.ReadEntry("Quest ID");

            packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var size = packet.Translator.ReadInt32("Size");
            var data = packet.Translator.ReadBytes(size);
            var hasData = packet.Translator.ReadBit();
            if (!hasData)
                return; // nothing to do

            var npcText = new NpcTextMop
            {
                ID = (uint)entry.Key
            };

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.Translator.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.Translator.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add(npcText, packet.TimeSpan);
        }
    }
}
