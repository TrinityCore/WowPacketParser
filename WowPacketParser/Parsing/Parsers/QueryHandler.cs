using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Proto;
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
            PacketQueryPlayerNameResponseWrapper responses = packet.Holder.QueryPlayerNameResponse = new();
            PacketQueryPlayerNameResponse response = new();
            responses.Responses.Add(response);
            WowGuid guid;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                response.PlayerGuid = guid = packet.ReadPackedGuid("GUID");
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
                response.PlayerGuid = guid = packet.ReadGuid("GUID");

            response.HasData = true;
            var name = packet.ReadCString("Name");
            response.PlayerName = name;
            StoreGetters.AddName(guid, name);
            packet.ReadCString("Realm Name");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                response.Race = (uint)packet.ReadByteE<Race>("Race");
                response.Gender = (uint)packet.ReadByteE<Gender>("Gender");
                response.Class = (uint)packet.ReadByteE<Class>("Class");
            }
            else
            {
                response.Race = (uint)packet.ReadInt32E<Race>("Race");
                response.Gender = (uint)packet.ReadInt32E<Gender>("Gender");
                response.Class = (uint)packet.ReadInt32E<Class>("Class");
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
            PacketQueryCreatureResponse response = packet.Holder.QueryCreatureResponse = new PacketQueryCreatureResponse();
            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            CreatureTemplate creature = new CreatureTemplate
            {
                Entry = (uint)entry.Key
            };
            response.Entry = (uint) entry.Key;
            response.HasData = true;

            var name = new string[4];
            for (int i = 0; i < name.Length; i++)
                name[i] = packet.ReadCString("Name", i);
            creature.Name = response.Name = name[0];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914))
            {
                var femaleName = new string[4];
                for (int i = 0; i < femaleName.Length; i++)
                    femaleName[i] = packet.ReadCString("Female Name", i);
                creature.FemaleName = response.NameAlt = femaleName[0];
            }

            creature.SubName = response.Title = packet.ReadCString("Sub Name");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                creature.TitleAlt = response.TitleAlt = packet.ReadCString("TitleAlt");

            creature.IconName = response.IconName = packet.ReadCString("Icon Name");

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            response.TypeFlags = (uint?)creature.TypeFlags ?? 0;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_1_0_13914)) // Might be earlier or later
                creature.TypeFlags2 = response.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            creature.Type = packet.ReadInt32E<CreatureType>("Type");
            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");
            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");

            response.Type = (int?)creature.Type ?? 0;
            response.Family = (int?)creature.Family ?? 0;
            response.Rank = (int?)creature.Rank ?? 0;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                creature.KillCredits = new uint?[2];
                for (int i = 0; i < 2; ++i)
                {
                    creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);
                    response.KillCredits.Add(creature.KillCredits[i] ?? 0);
                }
            }
            else // Did they stop sending pet spell data after 3.1?
            {
                if (ClientVersion.RemovedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadInt32("Unk Int");
                creature.PetSpellDataID = packet.ReadUInt32("Pet Spell Data Id");
            }

            creature.ModelIDs = new uint?[4];
            for (int i = 0; i < 4; i++)
            {
                creature.ModelIDs[i] = packet.ReadUInt32("Model ID", i);
                response.Models.Add(creature.ModelIDs[i] ?? 0);
            }

            creature.HealthModifier = response.HpMod = packet.ReadSingle("Modifier 1");
            creature.ManaModifier = response.ManaMod = packet.ReadSingle("Modifier 2");

            creature.RacialLeader = response.Leader = packet.ReadBool("Racial Leader");

            int qItemCount = ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4;
            //TODO: Move to creature_questitem
            //creature.QuestItems = new uint[qItemCount];

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
            {
                for (int i = 0; i < qItemCount; i++)
                    response.QuestItems.Add((uint)packet.ReadInt32<ItemId>("Quest Item", i));

                creature.MovementID = response.MovementId = packet.ReadUInt32("Movement ID");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                creature.RequiredExpansion = packet.ReadUInt32E<ClientType>("Expansion");
            response.Expansion = (uint?) creature.RequiredExpansion ?? 0;

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

            npcText.Probabilities = new float?[8];
            npcText.Texts0 = new string[8];
            npcText.Texts1 = new string[8];
            npcText.Languages = new Language?[8];
            npcText.EmoteDelays = new uint?[8][];
            npcText.Emotes = new EmoteType?[8][];

            var proto = packet.Holder.NpcTextOld = new() { Entry = npcText.ID.Value };
            for (int i = 0; i < 8; i++)
            {
                var textEntry = new PacketNpcTextOldEntry();
                npcText.Probabilities[i] = textEntry.Probability = packet.ReadSingle("Probability", i);
                npcText.Texts0[i] = textEntry.Text0 = packet.ReadCString("Text0", i);
                npcText.Texts1[i] = textEntry.Text1 = packet.ReadCString("Text1", i);
                npcText.Languages[i] = packet.ReadInt32E<Language>("Language", i);
                textEntry.Language = (int?)npcText.Languages[i] ?? 0;

                npcText.EmoteDelays[i] = new uint?[3];
                npcText.Emotes[i] = new EmoteType?[3];
                for (int j = 0; j < 3; j++)
                {
                    var emote = new BroadcastTextEmote();
                    npcText.EmoteDelays[i][j] = emote.Delay = packet.ReadUInt32("EmoteDelay", i, j);
                    npcText.Emotes[i][j] = packet.ReadUInt32E<EmoteType>("EmoteID", i, j);
                    emote.EmoteId = (uint?)npcText.Emotes[i][j] ?? 0;
                    textEntry.Emotes.Add(emote);
                }
                proto.Texts.Add(textEntry);
            }

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTexts.Add(npcText, packet.TimeSpan);
        }
    }
}
