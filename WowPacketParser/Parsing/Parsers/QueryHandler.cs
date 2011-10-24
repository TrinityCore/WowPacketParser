using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;


namespace WowPacketParser.Parsing.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleTimeQueryResponse(Packet packet)
        {
            var curTime = packet.ReadTime();
            Console.WriteLine("Current Time: " + curTime);

            var dailyReset = packet.ReadInt32();
            Console.WriteLine("Daily Quest Reset: " + dailyReset);
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            if (ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767)
            {
                packet.ReadPackedGuid("GUID");
                var end = packet.ReadBoolean();
                Console.WriteLine("Name Found: " + !end);

                if (end)
                    return;
            }
            else
                packet.ReadGuid("GUID");

            packet.ReadCString("Name");
            packet.ReadCString("Realm Name");

            TypeCode typeCode = ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767 ? TypeCode.Byte : TypeCode.Int32;
            packet.ReadEnum<Race>("Race", typeCode);
            packet.ReadEnum<Gender>("Gender", typeCode);
            packet.ReadEnum<Class>("Class", typeCode);

            if (!packet.ReadBoolean("Name Declined"))
                return;

            for (var i = 0; i < 5; i++)
                packet.ReadCString("Declined Name", i);
        }

        public static void ReadQueryHeader(Packet packet)
        {
            var entry = packet.ReadInt32();
            Console.WriteLine("Entry: " + entry);

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            ReadQueryHeader(packet);
        }

        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry();
            Console.WriteLine("Entry: " + entry.Key);

            if (entry.Value)
                return;

            var name = new string[4];
            for (var i = 0; i < 4; i++)
            {
                name[i] = packet.ReadCString();
                Console.WriteLine("Name " + i + ": " + name[i]);
            }

            var subName = packet.ReadCString();
            Console.WriteLine("Sub Name: " + subName);

            var iconName = packet.ReadCString();
            Console.WriteLine("Icon Name: " + iconName);

            var typeFlags = (CreatureTypeFlag)packet.ReadInt32();
            Console.WriteLine("Type Flags: " + typeFlags);

            var type = (CreatureType)packet.ReadInt32();
            Console.WriteLine("Type: " + type);

            var family = (CreatureFamily)packet.ReadInt32();
            Console.WriteLine("Family: " + family);

            var rank = (CreatureRank)packet.ReadInt32();
            Console.WriteLine("Rank: " + rank);

            var killCredit = new int[2];
            if (ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767)
            {
                for (var i = 0; i < 2; i++)
                    killCredit[i] = packet.ReadInt32("Kill Credit", i);
            }
            else
            {
                packet.ReadInt32("Unk Int");
                packet.ReadInt32("Pet Spell Data Id");
            }

            var dispId = new int[4];
            for (var i = 0; i < 4; i++)
            {
                dispId[i] = packet.ReadInt32();
                Console.WriteLine("Display ID " + i + ": " + dispId[i]);
            }

            var mod1 = packet.ReadSingle();
            Console.WriteLine("Modifier 1: " + mod1);

            var mod2 = packet.ReadSingle();
            Console.WriteLine("Modifier 2: " + mod2);

            var racialLeader = packet.ReadBoolean();
            Console.WriteLine("Racial Leader: " + racialLeader);

            var qItemCount = ClientVersion.Version >= ClientVersionBuild.V3_2_0_10192 ? 6 : 4;
            var qItem = new int[qItemCount];
            var moveId = 0;

            if (ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767)
            {
                for (var i = 0; i < qItemCount; i++)
                    qItem[i] = packet.ReadInt32("Quest Item", i);

                moveId = packet.ReadInt32("Movement ID");
            }

            if (ClientVersion.Version > ClientVersionBuild.V3_3_5a_12340)
                packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            if (ClientVersion.Version >= ClientVersionBuild.V4_1_0_13914)
                packet.ReadInt32("Unknown");

            SQLStore.WriteData(SQLStore.Creatures.GetCommand(entry.Key, name[0], subName, iconName, typeFlags,
                type, family, rank, killCredit, dispId, mod1, mod2, racialLeader, qItem, moveId));
        }

        [Parser(Opcode.CMSG_PAGE_TEXT_QUERY)]
        public static void HandlePageTextQuery(Packet packet)
        {
            ReadQueryHeader(packet);
        }

        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var entry = packet.ReadInt32();
            Console.WriteLine("Entry: " + entry);

            var text = packet.ReadCString();
            Console.WriteLine("Page Text: " + text);

            var pageId = packet.ReadInt32();
            Console.WriteLine("Next Page: " + pageId);

            SQLStore.WriteData(SQLStore.PageTexts.GetCommand(entry, text, pageId));
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            ReadQueryHeader(packet);
        }

        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.ReadInt32();
            Console.WriteLine("Entry: " + entry);

            var prob = new float[8];
            var text1 = new string[8];
            var text2 = new string[8];
            var lang = new Language[8];
            var emDelay = new int[8][];
            var emEmote = new int[8][];
            for (var i = 0; i < 8; i++)
            {
                prob[i] = packet.ReadSingle();
                Console.WriteLine("Probability " + i + ": " + prob[i]);

                text1[i] = packet.ReadCString();
                Console.WriteLine("Text 1 " + i + ": " + text1[i]);

                text2[i] = packet.ReadCString();
                Console.WriteLine("Text 2 " + i + ": " + text2[i]);

                lang[i] = (Language)packet.ReadInt32();
                Console.WriteLine("Language " + i + ": " + lang[i]);

                emDelay[i] = new int[3];
                emEmote[i] = new int[3];
                for (var j = 0; j < 3; j++)
                {
                    emDelay[i][j] = packet.ReadInt32();
                    Console.WriteLine("Emote Delay " + j + ": " + emDelay[i][j]);

                    emEmote[i][j] = packet.ReadInt32();
                    Console.WriteLine("Emote ID " + j + ": " + emEmote[i][j]);
                }
            }

            SQLStore.WriteData(SQLStore.NpcTexts.GetCommand(entry, prob, text1, text2, lang, emDelay, emEmote));
        }
    }
}
