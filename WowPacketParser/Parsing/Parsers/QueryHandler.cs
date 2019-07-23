using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Loading;
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
                ObjectType = StoreNameType.Player,
                ID = (int)guid.GetLow(),
                Name = name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_CREATURE)]
        public static void HandleCreatureQuery(Packet packet)
        {
            var entry = packet.ReadInt32<UnitId>("Entry");
            var guid = packet.ReadGuid("GUID");

            if (guid.HasEntry() && (entry != guid.GetEntry()))
                packet.AddValue("Error", "Entry does not match calculated GUID entry");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_CREATURE_RESPONSE)]
        public static void HandleCreatureQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };

            var name = new string[4];
            for (int i = 0; i < name.Length; i++)
                name[i] = packet.ReadCString("Name", i);
            creature.Name = name[0];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914))
            {
                var femaleName = new string[4];
                for (int i = 0; i < femaleName.Length; i++)
                    femaleName[i] = packet.ReadCString("Female Name", i);
                creature.FemaleName = femaleName[0];
            }

            creature.SubName = packet.ReadCString("Sub Name");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                creature.TitleAlt = packet.ReadCString("TitleAlt");

            creature.IconName = packet.ReadCString("Icon Name");

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914)) // Might be earlier or later
                creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Type = packet.ReadInt32E<CreatureType>("Type");

            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");

            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                creature.KillCredits = new uint?[2];
                for (int i = 0; i < 2; ++i)
                    creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);
            }
            else // Did they stop sending pet spell data after 3.1?
            {
                if (ClientVersion.RemovedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Unk Int");
                creature.PetSpellDataID = packet.ReadUInt32("Pet Spell Data Id");
            }

            creature.ModelIDs = new uint?[4];
            for (int i = 0; i < 4; i++)
                creature.ModelIDs[i] = packet.ReadUInt32("Model ID", i);

            creature.HealthModifier = packet.ReadSingle("Modifier 1");
            creature.ManaModifier = packet.ReadSingle("Modifier 2");

            creature.RacialLeader = packet.ReadBool("Racial Leader");

            int qItemCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4;
            //TODO: Move to creature_questitem
            //creature.QuestItems = new uint[qItemCount];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                for (int i = 0; i < qItemCount; i++)
                    /*creature.QuestItems[i] = (uint)*/packet.ReadInt32<ItemId>("Quest Item", i);

                creature.MovementID = packet.ReadUInt32("Movement ID");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("Expansion");

            packet.AddSniffData(StoreNameType.Unit, entry.Key, "QUERY_RESPONSE");

            Storage.CreatureTemplates.Add(creature, packet.TimeSpan);

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

            ObjectName objectName = new ObjectName
            {
                ObjectType = StoreNameType.Unit,
                ID = entry.Key,
                Name = creature.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_PAGE_TEXT)]
        public static void HandlePageTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadGuid("GUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            PageText pageText = new PageText();

            uint entry = packet.ReadUInt32("Entry");

            pageText.ID = entry;
            pageText.Text = packet.ReadCString("Page Text");
            pageText.NextPageID = packet.ReadUInt32("Next Page");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");

            Storage.PageTexts.Add(pageText, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_NPC_TEXT)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadGuid("GUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
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
                npcText.Probabilities[i] = packet.ReadSingle("Probability", i);

                npcText.Texts1[i] = packet.ReadCString("Text 1", i);

                npcText.Texts2[i] = packet.ReadCString("Text 2", i);

                npcText.Languages[i] = packet.ReadInt32E<Language>("Language", i);

                npcText.EmoteDelays[i] = new uint[3];
                npcText.EmoteIds[i] = new EmoteType[3];
                for (int j = 0; j < 3; j++)
                {
                    npcText.EmoteDelays[i][j] = packet.ReadUInt32("Emote Delay", i, j);
                    npcText.EmoteIds[i][j] = packet.ReadUInt32E<EmoteType>("Emote ID", i, j);
                }
            }

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTexts.Add(npcText, packet.TimeSpan);
        }
    }
}
