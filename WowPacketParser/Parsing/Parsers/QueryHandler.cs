using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;


namespace WowPacketParser.Parsing.Parsers
{
    public static class QueryHandler
    {
        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleTimeQueryResponse(Packet packet)
        {
            packet.ReadTime("Current Time");

            packet.ReadInt32("Daily Quest Reset");
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                packet.ReadPackedGuid("GUID");
                var end = packet.ReadBoolean("Name Not Found");
                if (end)
                    return;
            }
            else
                packet.ReadGuid("GUID");

            packet.ReadCString("Name");
            packet.ReadCString("Realm Name");

            TypeCode typeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) ? TypeCode.Byte : TypeCode.Int32;
            packet.ReadEnum<Race>("Race", typeCode);
            packet.ReadEnum<Gender>("Gender", typeCode);
            packet.ReadEnum<Class>("Class", typeCode);

            if (!packet.ReadBoolean("Name Declined"))
                return;

            for (var i = 0; i < 5; i++)
                packet.ReadCString("Declined Name", i);
        }

        public static void ReadQueryHeader(ref Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
            var guid = packet.ReadGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_CREATURE_QUERY) || packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_GAMEOBJECT_QUERY))
                if (guid.HasEntry() && (entry != guid.GetEntry()))
                    packet.Writer.WriteLine("Entry does not match calculated GUID entry");
        }

        [Parser(Opcode.CMSG_CREATURE_QUERY)]
        public static void HandleCreatureQuery(Packet packet)
        {
            ReadQueryHeader(ref packet);
        }

        [Parser(Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            var creature = new UnitTemplate();

            var nameCount = ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914) ? 8 : 4; // Might be earlier or later
            var name = new string[nameCount];
            for (var i = 0; i < name.Length; i++)
                name[i] = packet.ReadCString("Name", i);
            creature.Name = name[0];

            creature.SubName = packet.ReadCString("Sub Name");

            creature.IconName = packet.ReadCString("Icon Name");

            creature.TypeFlags = packet.ReadEnum<CreatureTypeFlag>("Type Flags", TypeCode.UInt32);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914)) // Might be earlier or later
                creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Type = packet.ReadEnum<CreatureType>("Type", TypeCode.Int32);

            creature.Family = packet.ReadEnum<CreatureFamily>("Family", TypeCode.Int32);

            creature.Rank = packet.ReadEnum<CreatureRank>("Rank", TypeCode.Int32);

            var killCredit = new uint[2];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                for (var i = 0; i < 2; i++)
                    killCredit[i] = packet.ReadUInt32("Kill Credit", i);

                creature.KillCredit1 = killCredit[0];
                creature.KillCredit2 = killCredit[1];
            }
            else
            {
                creature.UnkInt = packet.ReadInt32("Unk Int");
                creature.PetSpellData = packet.ReadUInt32("Pet Spell Data Id");
            }

            creature.DisplayIds = new uint[4];
            for (var i = 0; i < 4; i++)
                creature.DisplayIds[i] = packet.ReadUInt32("Display ID", i);

            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            creature.Modifier2 = packet.ReadSingle("Modifier 2");

            creature.RacialLeader = packet.ReadBoolean("Racial Leader");

            var qItemCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4;
            creature.QuestItems = new uint[qItemCount];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                for (var i = 0; i < qItemCount; i++)
                    creature.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

                creature.MovementId = packet.ReadUInt32("Movement ID");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                creature.Expansion = packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            Stuffing.UnitTemplates.TryAdd((uint)entry.Key, creature);
        }

        [Parser(Opcode.CMSG_PAGE_TEXT_QUERY)]
        public static void HandlePageTextQuery(Packet packet)
        {
            ReadQueryHeader(ref packet);
        }

        [Parser(Opcode.SMSG_PAGE_TEXT_QUERY_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            var entry = packet.ReadUInt32("Entry");

            pageText.Text = packet.ReadCString("Page Tex");

            pageText.NextPageId = packet.ReadUInt32("Next Page");

            Stuffing.PageTexts.TryAdd(entry, pageText);
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            ReadQueryHeader(ref packet);
        }

        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcText();

            var entry = packet.ReadUInt32("Entry");

            npcText.Probabilities = new float[8];
            npcText.Texts1 = new string[8];
            npcText.Texts2 = new string[8];
            npcText.Languages = new Language[8];
            npcText.EmoteDelays = new uint[8][];
            npcText.EmoteIds = new EmoteType[8][];
            for (var i = 0; i < 8; i++)
            {
                npcText.Probabilities[i] = packet.ReadSingle("Probability", i);

                npcText.Texts1[i] = packet.ReadCString("Text 1", i);

                npcText.Texts2[i] = packet.ReadCString("Text 2", i);

                npcText.Languages[i] = packet.ReadEnum<Language>("Language", TypeCode.Int32, i);

                npcText.EmoteDelays[i] = new uint[3];
                npcText.EmoteIds[i] = new EmoteType[3];
                for (var j = 0; j < 3; j++)
                {
                    npcText.EmoteDelays[i][j] = packet.ReadUInt32("Emote Delay", i, j);
                    npcText.EmoteIds[i][j] = packet.ReadEnum<EmoteType>("Emote ID", TypeCode.UInt32, i, j);
                }
            }

            Stuffing.NpcTexts.TryAdd(entry, npcText);
        }
    }
}
