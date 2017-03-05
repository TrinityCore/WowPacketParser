using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class QueryHandler
    {
        [Parser(Opcode.SMSG_QUERY_TIME_RESPONSE)]
        public static void HandleTimeQueryResponse(Packet packet)
        {
            packet.Translator.ReadTime("Current Time");
            packet.Translator.ReadInt32("Daily Quest Reset");
        }

        [Parser(Opcode.CMSG_NAME_QUERY)]
        public static void HandleNameQuery(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            WowGuid guid;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                guid = packet.Translator.ReadPackedGuid("GUID");
                var end = packet.Translator.ReadByte("Result");
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
                guid = packet.Translator.ReadGuid("GUID");

            var name = packet.Translator.ReadCString("Name");
            StoreGetters.AddName(guid, name);
            packet.Translator.ReadCString("Realm Name");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                packet.Translator.ReadByteE<Race>("Race");
                packet.Translator.ReadByteE<Gender>("Gender");
                packet.Translator.ReadByteE<Class>("Class");
            }
            else
            {
                packet.Translator.ReadInt32E<Race>("Race");
                packet.Translator.ReadInt32E<Gender>("Gender");
                packet.Translator.ReadInt32E<Class>("Class");
            }

            if (!packet.Translator.ReadBool("Name Declined"))
                return;

            for (var i = 0; i < 5; i++)
                packet.Translator.ReadCString("Declined Name", i);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.Player,
                ID = (int)guid.GetLow(),
                Name = name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            var entry = packet.Translator.ReadInt32<UnitId>("Entry");
            var guid = packet.Translator.ReadGuid("GUID");

            if (guid.HasEntry() && (entry != guid.GetEntry()))
                packet.AddValue("Error", "Entry does not match calculated GUID entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value)
                return;

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            var name = new string[4];
            for (int i = 0; i < name.Length; i++)
                name[i] = packet.Translator.ReadCString("Name", i);
            creature.Name = name[0];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914))
            {
                var femaleName = new string[4];
                for (int i = 0; i < femaleName.Length; i++)
                    femaleName[i] = packet.Translator.ReadCString("Female Name", i);
                creature.FemaleName = femaleName[0];
            }

            creature.SubName = packet.Translator.ReadCString("Sub Name");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                packet.Translator.ReadCString("Unk String");

            creature.IconName = packet.Translator.ReadCString("Icon Name");

            creature.TypeFlags = packet.Translator.ReadUInt32E<CreatureTypeFlag>("Type Flags");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914)) // Might be earlier or later
                creature.TypeFlags2 = packet.Translator.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Type = packet.Translator.ReadInt32E<CreatureType>("Type");

            creature.Family = packet.Translator.ReadInt32E<CreatureFamily>("Family");

            creature.Rank = packet.Translator.ReadInt32E<CreatureRank>("Rank");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                creature.KillCredits = new uint?[2];
                for (int i = 0; i < 2; ++i)
                    creature.KillCredits[i] = packet.Translator.ReadUInt32("Kill Credit", i);
            }
            else // Did they stop sending pet spell data after 3.1?
            {
                packet.Translator.ReadInt32("Unk Int");
                creature.PetSpellDataID = packet.Translator.ReadUInt32("Pet Spell Data Id");
            }

            creature.ModelIDs = new uint?[4];
            for (int i = 0; i < 4; i++)
                creature.ModelIDs[i] = packet.Translator.ReadUInt32("Model ID", i);

            creature.HealthModifier = packet.Translator.ReadSingle("Modifier 1");
            creature.ManaModifier = packet.Translator.ReadSingle("Modifier 2");

            creature.RacialLeader = packet.Translator.ReadBool("Racial Leader");

            int qItemCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4;
            //TODO: Move to creature_questitem
            //creature.QuestItems = new uint[qItemCount];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                for (int i = 0; i < qItemCount; i++)
                    /*creature.QuestItems[i] = (uint)*/packet.Translator.ReadInt32<ItemId>("Quest Item", i);

                creature.MovementID = packet.Translator.ReadUInt32("Movement ID");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                creature.RequiredExpansion = packet.Translator.ReadUInt32E<ClientType>("Expansion");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.CreatureTemplates.Add(creature, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = ObjectType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadGuid("GUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            PageText pageText = new PageText();

            uint entry = packet.Translator.ReadUInt32("Entry");

            pageText.ID = entry;
            pageText.Text = packet.Translator.ReadCString("Page Text");
            pageText.NextPageID = packet.Translator.ReadUInt32("Next Page");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");

            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadGuid("GUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            NpcText npcText = new NpcText
            {
                ID = (uint)entry.Key
            };

            npcText.Probabilities = new float[8];
            npcText.Texts1 = new string[8];
            npcText.Texts2 = new string[8];
            npcText.Languages = new Language[8];
            npcText.EmoteDelays = new uint[8][];
            npcText.EmoteIds = new EmoteType[8][];
            for (int i = 0; i < 8; i++)
            {
                npcText.Probabilities[i] = packet.Translator.ReadSingle("Probability", i);

                npcText.Texts1[i] = packet.Translator.ReadCString("Text 1", i);

                npcText.Texts2[i] = packet.Translator.ReadCString("Text 2", i);

                npcText.Languages[i] = packet.Translator.ReadInt32E<Language>("Language", i);

                npcText.EmoteDelays[i] = new uint[3];
                npcText.EmoteIds[i] = new EmoteType[3];
                for (int j = 0; j < 3; j++)
                {
                    npcText.EmoteDelays[i][j] = packet.Translator.ReadUInt32("Emote Delay", i, j);
                    npcText.EmoteIds[i][j] = packet.Translator.ReadUInt32E<EmoteType>("Emote ID", i, j);
                }
            }

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTexts.Add(npcText, packet.TimeSpan);
        }
    }
}
