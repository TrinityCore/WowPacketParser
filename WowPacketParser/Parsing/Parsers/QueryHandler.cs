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

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            WowGuid guid;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                guid = packet.ReadPackedGuid("GUID");
                var end = packet.ReadByte("Result");
                /*
                if (end == 1)
                    DenyItem(&WDB_CACHE_NAME, v11, v12);
                if (end == 2)
                    RetryItem(&WDB_CACHE_NAME, v11, v12);
                if (end == 3)
                {
                    AddItem(&WDB_CACHE_NAME, (int)&v8, v11, v12);
                    SetTemporary(&WDB_CACHE_NAME, v11, v12);
                }
                */
                if (end != 0)
                    return;
            }
            else
                guid = packet.ReadGuid("GUID");

            var name = packet.ReadCString("Name");
            StoreGetters.AddName(guid, name);
            packet.ReadCString("Realm Name");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                packet.ReadByteE<Race>("Race");
                packet.ReadByteE<Gender>("Gender");
                packet.ReadByteE<Class>("Class");
            }
            else
            {
                packet.ReadInt32E<Race>("Race");
                packet.ReadInt32E<Gender>("Gender");
                packet.ReadInt32E<Class>("Class");
            }

            if (!packet.ReadBool("Name Declined"))
                return;

            for (var i = 0; i < 5; i++)
                packet.ReadCString("Declined Name", i);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Player,
                Name = name
            };
            Storage.ObjectNames.Add((uint)guid.GetLow(), objectName, packet.TimeSpan);
        }

        public static void ReadQueryHeader(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");
            var guid = packet.ReadGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_QUERY_CREATURE, Direction.ClientToServer) || packet.Opcode == Opcodes.GetOpcode(Opcode.CMSG_QUERY_GAME_OBJECT, Direction.ClientToServer))
                if (guid.HasEntry() && (entry != guid.GetEntry()))
                    packet.AddValue("Error", "Entry does not match calculated GUID entry");
        }

        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            ReadQueryHeader(packet);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            var creature = new UnitTemplate();

            var name = new string[4];
            for (var i = 0; i < name.Length; i++)
                name[i] = packet.ReadCString("Name", i);
            creature.Name = name[0];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914))
            {
                var femaleName = new string[4];
                for (var i = 0; i < femaleName.Length; i++)
                    femaleName[i] = packet.ReadCString("Female Name", i);
                creature.FemaleName = femaleName[0];
            }

            creature.SubName = packet.ReadCString("Sub Name");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                packet.ReadCString("Unk String");

            creature.IconName = packet.ReadCString("Icon Name");

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914)) // Might be earlier or later
                creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Type = packet.ReadInt32E<CreatureType>("Type");

            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");

            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                creature.KillCredits = new uint[2];
                for (var i = 0; i < 2; ++i)
                    creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);
            }
            else // Did they stop sending pet spell data after 3.1?
            {
                creature.UnkInt = packet.ReadInt32("Unk Int");
                creature.PetSpellData = packet.ReadUInt32("Pet Spell Data Id");
            }

            creature.DisplayIds = new uint[4];
            for (var i = 0; i < 4; i++)
                creature.DisplayIds[i] = packet.ReadUInt32("Display ID", i);

            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            creature.Modifier2 = packet.ReadSingle("Modifier 2");

            creature.RacialLeader = packet.ReadBool("Racial Leader");

            var qItemCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4;
            creature.QuestItems = new uint[qItemCount];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                for (var i = 0; i < qItemCount; i++)
                    creature.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

                creature.MovementId = packet.ReadUInt32("Movement ID");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                creature.Expansion = packet.ReadUInt32E<ClientType>("Expansion");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.UnitTemplates.Add((uint)entry.Key, creature, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                Name = creature.Name
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            ReadQueryHeader(packet);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            var entry = packet.ReadUInt32("Entry");

            pageText.Text = packet.ReadCString("Page Text");

            pageText.NextPageID = packet.ReadUInt32("Next Page");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");

            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            ReadQueryHeader(packet);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcText();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

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

                npcText.Languages[i] = packet.ReadInt32E<Language>("Language", i);

                npcText.EmoteDelays[i] = new uint[3];
                npcText.EmoteIds[i] = new EmoteType[3];
                for (var j = 0; j < 3; j++)
                {
                    npcText.EmoteDelays[i][j] = packet.ReadUInt32("Emote Delay", i, j);
                    npcText.EmoteIds[i][j] = packet.ReadUInt32E<EmoteType>("Emote ID", i, j);
                }
            }

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTexts.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }
    }
}
