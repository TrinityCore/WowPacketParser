using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
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
            var entry = packet.ReadEntry("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var creature = new UnitTemplate();

            var lenS3 = (int)packet.ReadBits(11);
            creature.RacialLeader = packet.ReadBit("Racial Leader");

            var stringLens = new int[4][];
            for (var i = 0; i < 4; i++)
            {
                stringLens[i] = new int[2];
                stringLens[i][0] = (int)packet.ReadBits(11);
                stringLens[i][1] = (int)packet.ReadBits(11);
            }

            var lenS4 = (int)packet.ReadBits(6);
            var lenS5 = (int)packet.ReadBits(11);
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

            creature.Modifier1 = packet.ReadSingle("Modifier 1");
            if (lenS3 > 1)
                creature.SubName = packet.ReadCString("Sub Name");

            creature.Rank = packet.ReadInt32E<CreatureRank>("Rank");

            creature.QuestItems = new uint[qItemCount];
            for (var i = 0; i < qItemCount; ++i)
                creature.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            creature.Type = packet.ReadInt32E<CreatureType>("Type");
            creature.KillCredits = new uint[2];
            for (var i = 0; i < 2; ++i)
                creature.KillCredits[i] = packet.ReadUInt32("Kill Credit", i);
            creature.Family = packet.ReadInt32E<CreatureFamily>("Family");
            if (lenS4 > 1)
                creature.IconName = packet.ReadCString("Icon Name");

            creature.DisplayIds = new uint[4];
            creature.DisplayIds[1] = packet.ReadUInt32("Display ID 1");
            creature.DisplayIds[0] = packet.ReadUInt32("Display ID 0");
            creature.MovementId = packet.ReadUInt32("Movement ID");
            creature.DisplayIds[3] = packet.ReadUInt32("Display ID 3");

            creature.TypeFlags = packet.ReadUInt32E<CreatureTypeFlag>("Type Flags");
            creature.TypeFlags2 = packet.ReadUInt32("Creature Type Flags 2"); // Missing enum

            if (lenS5 > 1)
                packet.ReadCString("string5");
            creature.DisplayIds[2] = packet.ReadUInt32("Display ID 2");
            creature.Modifier2 = packet.ReadSingle("Modifier 2");
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
            var byte20 = packet.ReadByte("byte20");

            var bits22 = packet.ReadBits(8);
            var bits278 = packet.ReadBits(8);
            packet.ReadBit();

            packet.ReadWoWString("Realmname (without white char)", bits278);
            packet.ReadWoWString("Realmname", bits22);
        }

        [Parser(Opcode.SMSG_QUERY_PLAYER_NAME_RESPONSE)]
        public static void HandleNameQueryResponse(Packet packet)
        {
            var guid = new byte[8];

            var bit16 = packet.ReadBit();
            packet.StartBitStream(guid, 1, 3, 2);

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.ReadBits("bits", 7);

            var bits32 = packet.ReadBits(6);
            packet.StartBitStream(guid, 6, 4, 0);
            var bit83 = packet.ReadBit();
            packet.StartBitStream(guid, 5, 7);

            packet.ReadXORByte(guid, 1);
            packet.ReadWoWString("Name: ", bits32);
            packet.ReadXORBytes(guid, 0, 7);

            packet.ReadByte("Race");
            packet.ReadByte("unk81");
            packet.ReadByte("Gender");

            if (bit16)
                for (var i = 0; i < 5; ++i)
                    packet.ReadCString("Declined Name");

            packet.ReadByte("Class");
            packet.ReadXORBytes(guid, 4, 6, 5);
            packet.ReadUInt32("Realm Id");
            packet.ReadXORBytes(guid, 3, 2);

            packet.WriteGuid("Guid", guid);
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
            var entry = (uint)packet.ReadInt32("Entry");
            if ((int)entry < 0)
            {
                packet.WriteLine("Row {0} has been removed.", -(int)entry);
                return;
            }

            switch (type)
            {
                case DB2Hash.BroadcastText:
                    {
                        var broadcastText = new BroadcastText();

                        var id = db2File.ReadEntry("Broadcast Text Entry");
                        broadcastText.Language = db2File.ReadInt32("Language");
                        if (db2File.ReadUInt16() > 0)
                            broadcastText.MaleText = db2File.ReadCString("Male Text");
                        if (db2File.ReadUInt16() > 0)
                            broadcastText.FemaleText = db2File.ReadCString("Female Text");

                        broadcastText.EmoteID = new uint[3];
                        broadcastText.EmoteDelay = new uint[3];
                        for (var i = 0; i < 3; ++i)
                            broadcastText.EmoteID[i] = (uint)db2File.ReadInt32("Emote ID", i);
                        for (var i = 0; i < 3; ++i)
                            broadcastText.EmoteDelay[i] = (uint)db2File.ReadInt32("Emote Delay", i);

                        broadcastText.SoundId = db2File.ReadUInt32("Sound Id");
                        broadcastText.UnkEmoteId = db2File.ReadUInt32("Unk 1"); // emote unk
                        broadcastText.Type = db2File.ReadUInt32("Unk 2"); // kind of type?

                        Storage.BroadcastTexts.Add((uint)id.Key, broadcastText, packet.TimeSpan);
                        packet.AddSniffData(StoreNameType.None, id.Key, "BROADCAST_TEXT");
                        break;
                    }
                case DB2Hash.Creature:
                    {
                        db2File.ReadUInt32("Creature Entry");
                        db2File.ReadUInt32("Item Entry 1");
                        db2File.ReadUInt32("Item Entry 2");
                        db2File.ReadUInt32("Item Entry 3");
                        db2File.ReadUInt32("Projectile Entry 1");
                        db2File.ReadUInt32("Projectile Entry 2");
                        db2File.ReadUInt32("Mount");
                        db2File.ReadUInt32("Display Id 1");
                        db2File.ReadUInt32("Display Id 2");
                        db2File.ReadUInt32("Display Id 3");
                        db2File.ReadUInt32("Display Id 4");
                        db2File.ReadSingle("Unk Float 1");
                        db2File.ReadSingle("Unk Float 2");
                        db2File.ReadSingle("Unk Float 3");
                        db2File.ReadSingle("Unk Float 4");
                        if (db2File.ReadUInt16() > 0)
                            db2File.ReadCString("Name");

                        db2File.ReadUInt32("Inhabit Type");
                        break;
                    }
                case DB2Hash.Item:
                    {
                        var item = Storage.ItemTemplates.ContainsKey(entry) ? Storage.ItemTemplates[entry].Item1 : new ItemTemplate();

                        db2File.ReadUInt32<ItemId>("Item Entry");
                        item.Class = db2File.ReadInt32E<ItemClass>("Class");
                        item.SubClass = db2File.ReadUInt32("Sub Class");
                        item.SoundOverrideSubclass = db2File.ReadInt32("Sound Override Subclass");
                        item.Material = db2File.ReadInt32E<Material>("Material");
                        item.DisplayId = db2File.ReadUInt32("Display ID");
                        item.InventoryType = db2File.ReadUInt32E<InventoryType>("Inventory Type");
                        item.SheathType = db2File.ReadInt32E<SheathType>("Sheath Type");

                        Storage.ItemTemplates.Add(entry, item, packet.TimeSpan);
                        packet.AddSniffData(StoreNameType.Item, (int)entry, "DB_REPLY");
                        break;
                    }
                case DB2Hash.Item_sparse:
                    {
                        var item = Storage.ItemTemplates.ContainsKey(entry) ? Storage.ItemTemplates[entry].Item1 : new ItemTemplate();

                        db2File.ReadUInt32<ItemId>("Item Sparse Entry");
                        item.Quality = db2File.ReadInt32E<ItemQuality>("Quality");
                        item.Flags1 = db2File.ReadUInt32E<ItemProtoFlags>("Flags 1");
                        item.Flags2 = db2File.ReadInt32E<ItemFlagExtra>("Flags 2");
                        item.Flags3 = db2File.ReadUInt32("Flags 3");
                        item.Unk430_1 = db2File.ReadSingle("Unk430_1");
                        item.Unk430_2 = db2File.ReadSingle("Unk430_2");
                        item.BuyCount = db2File.ReadUInt32("Buy count");
                        item.BuyPrice = db2File.ReadUInt32("Buy Price");
                        item.SellPrice = db2File.ReadUInt32("Sell Price");
                        item.InventoryType = db2File.ReadInt32E<InventoryType>("Inventory Type");
                        item.AllowedClasses = db2File.ReadInt32E<ClassMask>("Allowed Classes");
                        item.AllowedRaces = db2File.ReadInt32E<RaceMask>("Allowed Races");
                        item.ItemLevel = db2File.ReadUInt32("Item Level");
                        item.RequiredLevel = db2File.ReadUInt32("Required Level");
                        item.RequiredSkillId = db2File.ReadUInt32("Required Skill ID");
                        item.RequiredSkillLevel = db2File.ReadUInt32("Required Skill Level");
                        item.RequiredSpell = (uint)db2File.ReadInt32<SpellId>("Required Spell");
                        item.RequiredHonorRank = db2File.ReadUInt32("Required Honor Rank");
                        item.RequiredCityRank = db2File.ReadUInt32("Required City Rank");
                        item.RequiredRepFaction = db2File.ReadUInt32("Required Rep Faction");
                        item.RequiredRepValue = db2File.ReadUInt32("Required Rep Value");
                        item.MaxCount = db2File.ReadInt32("Max Count");
                        item.MaxStackSize = db2File.ReadInt32("Max Stack Size");
                        item.ContainerSlots = db2File.ReadUInt32("Container Slots");

                        item.StatTypes = new ItemModType[10];
                        for (var i = 0; i < 10; i++)
                        {
                            var statType = db2File.ReadInt32E<ItemModType>("Stat Type", i);
                            item.StatTypes[i] = statType == ItemModType.None ? ItemModType.Mana : statType; // TDB
                        }

                        item.StatValues = new int[10];
                        for (var i = 0; i < 10; i++)
                            item.StatValues[i] = db2File.ReadInt32("Stat Value", i);

                        item.ScalingValue = new int[10];
                        for (var i = 0; i < 10; i++)
                            item.ScalingValue[i] = db2File.ReadInt32("Scaling Value", i);

                        item.SocketCostRate = new int[10];
                        for (var i = 0; i < 10; i++)
                            item.SocketCostRate[i] = db2File.ReadInt32("Socket Cost Rate", i);

                        item.ScalingStatDistribution = db2File.ReadInt32("Scaling Stat Distribution");
                        item.DamageType = db2File.ReadInt32E<DamageType>("Damage Type");
                        item.Delay = db2File.ReadUInt32("Delay");
                        item.RangedMod = db2File.ReadSingle("Ranged Mod");

                        item.TriggeredSpellIds = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellIds[i] = db2File.ReadInt32<SpellId>("Triggered Spell ID", i);

                        item.TriggeredSpellTypes = new ItemSpellTriggerType[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellTypes[i] = db2File.ReadInt32E<ItemSpellTriggerType>("Trigger Spell Type", i);

                        item.TriggeredSpellCharges = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCharges[i] = db2File.ReadInt32("Triggered Spell Charges", i);

                        item.TriggeredSpellCooldowns = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCooldowns[i] = db2File.ReadInt32("Triggered Spell Cooldown", i);

                        item.TriggeredSpellCategories = new uint[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCategories[i] = db2File.ReadUInt32("Triggered Spell Category", i);

                        item.TriggeredSpellCategoryCooldowns = new int[5];
                        for (var i = 0; i < 5; i++)
                            item.TriggeredSpellCategoryCooldowns[i] = db2File.ReadInt32("Triggered Spell Category Cooldown", i);

                        item.Bonding = db2File.ReadInt32E<ItemBonding>("Bonding");

                        if (db2File.ReadUInt16() > 0)
                            item.Name = db2File.ReadCString("Name", 0);

                        for (var i = 1; i < 4; ++i)
                            if (db2File.ReadUInt16() > 0)
                                db2File.ReadCString("Name", i);

                        if (db2File.ReadUInt16() > 0)
                            item.Description = db2File.ReadCString("Description");

                        item.PageText = db2File.ReadUInt32("Page Text");
                        item.Language = db2File.ReadInt32E<Language>("Language");
                        item.PageMaterial = db2File.ReadInt32E<PageMaterial>("Page Material");
                        item.StartQuestId = (uint)db2File.ReadInt32<QuestId>("Start Quest");
                        item.LockId = db2File.ReadUInt32("Lock ID");
                        item.Material = db2File.ReadInt32E<Material>("Material");
                        item.SheathType = db2File.ReadInt32E<SheathType>("Sheath Type");
                        item.RandomPropery = db2File.ReadInt32("Random Property");
                        item.RandomSuffix = db2File.ReadUInt32("Random Suffix");
                        item.ItemSet = db2File.ReadUInt32("Item Set");
                        item.AreaId = db2File.ReadUInt32<AreaId>("Area");
                        var map = db2File.ReadInt32<MapId>("Map ID");
                        item.MapId = map;
                        item.BagFamily = db2File.ReadInt32E<BagFamilyMask>("Bag Family");
                        item.TotemCategory = db2File.ReadInt32E<TotemCategory>("Totem Category");

                        item.ItemSocketColors = new ItemSocketColor[3];
                        for (var i = 0; i < 3; i++)
                            item.ItemSocketColors[i] = db2File.ReadInt32E<ItemSocketColor>("Socket Color", i);

                        item.SocketContent = new uint[3];
                        for (var i = 0; i < 3; i++)
                            item.SocketContent[i] = db2File.ReadUInt32("Socket Item", i);

                        item.SocketBonus = db2File.ReadInt32("Socket Bonus");
                        item.GemProperties = db2File.ReadInt32("Gem Properties");
                        item.ArmorDamageModifier = db2File.ReadSingle("Armor Damage Modifier");
                        item.Duration = db2File.ReadUInt32("Duration");
                        item.ItemLimitCategory = db2File.ReadInt32("Limit Category");
                        item.HolidayId = db2File.ReadInt32E<Holiday>("Holiday");
                        item.StatScalingFactor = db2File.ReadSingle("Stat Scaling Factor");
                        item.CurrencySubstitutionId = db2File.ReadUInt32("Currency Substitution Id");
                        item.CurrencySubstitutionCount = db2File.ReadUInt32("Currency Substitution Count");

                        Storage.ObjectNames.Add(entry, new ObjectName { ObjectType = ObjectType.Item, Name = item.Name }, packet.TimeSpan);
                        packet.AddSniffData(StoreNameType.Item, (int)entry, "DB_REPLY");
                        break;
                    }
                case DB2Hash.KeyChain:
                    {
                        db2File.ReadUInt32("Key Chain Id");
                        db2File.ReadBytes("Key", 32);
                        break;
                    }
                default:
                    {
                        db2File.AddValue("Unknown DB2 file type", string.Format(": {0} (0x{0:x})", type));
                        for (var i = 0; ; ++i)
                        {
                            if (db2File.Length - 4 >= db2File.Position)
                            {
                                var blockVal = db2File.ReadUpdateField();
                                string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                                packet.AddValue("Block Value " + i, value);
                            }
                            else
                            {
                                var left = db2File.Length - db2File.Position;
                                for (var j = 0; j < left; ++j)
                                {
                                    var value = db2File.ReadByte();
                                    packet.AddValue("Byte Value " + i, value);
                                }
                                break;
                            }
                        }
                        break;
                    }
            }

            db2File.ClosePacket(false);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_PAGE_TEXT_RESPONSE)]
        public static void HandlePageTextResponse(Packet packet)
        {
            var pageText = new PageText();

            var entry = packet.ReadUInt32("Entry");
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var textLen = (int)packet.ReadBits(12);
            packet.ResetBitReader();
            pageText.Text = packet.ReadWoWString("Page Text", textLen);
            pageText.NextPageID = packet.ReadUInt32("Next Page");
            packet.ReadUInt32("Entry");

            packet.AddSniffData(StoreNameType.PageText, (int)entry, "QUERY_RESPONSE");
            Storage.PageTexts.Add(entry, pageText, packet.TimeSpan);
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

            var quest = new QuestTemplate();

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
            quest.NextQuestIdChain = (uint)packet.ReadInt32<QuestId>("Next Chain Quest");
            packet.ReadInt32("int2971");
            quest.RewardSpellCast = packet.ReadInt32<SpellId>("Reward Spell Cast");
            packet.ReadInt32("int2955");
            quest.RewardHonorMultiplier = packet.ReadSingle("Reward Honor Multiplier");
            packet.ReadInt32("int2970");
            packet.ReadInt32("int2984");
            packet.ReadInt32("int2979");
            quest.MinLevel = packet.ReadInt32("Min Level");
            quest.RewardSkillPoints = packet.ReadUInt32("RewSkillPoints");
            quest.QuestGiverPortrait = packet.ReadUInt32("QuestGiverPortrait");
            packet.ReadInt32("int21");
            quest.Type = packet.ReadInt32E<QuestType>("Type");
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
            quest.SuggestedPlayers = packet.ReadUInt32("Suggested Players");
            packet.ReadInt32("int2972");
            packet.ReadInt32("int2959");
            quest.Title = packet.ReadWoWString("Title", title);
            packet.ReadInt32("int2965");
            packet.ReadInt32("int2978");
            quest.RewardSkillId = packet.ReadUInt32("RewSkillId");
            packet.ReadInt32("int2982");
            packet.ReadInt32("int2968");
            packet.ReadInt32("int2964");
            packet.ReadInt32("int2957");
            packet.ReadInt32("int2969");
            packet.ReadInt32("int1786");
            quest.SoundAccept = packet.ReadUInt32("Sound Accept");
            packet.ReadInt32("int2981");
            packet.ReadInt32("int2961");
            packet.ReadInt32("int15");
            packet.ReadInt32("int2967");
            quest.CompletedText = packet.ReadWoWString("Completed Text", completedText);
            packet.ReadInt32("int25");
            packet.ReadInt32("Quest Id");
            quest.PointY = packet.ReadSingle("Point Y");
            packet.ReadInt32("int2974");
            packet.ReadInt32("int2952");
            quest.Details = packet.ReadWoWString("Details", details);
            quest.Level = packet.ReadInt32("Level");
            quest.PointMapId = packet.ReadUInt32("Point Map ID");
            packet.ReadWoWString("string1658", len1658);
            quest.PointX = packet.ReadSingle("Point X");
            packet.ReadInt32("int17");
            packet.ReadInt32("int2962");
            quest.QuestGiverTextWindow = packet.ReadWoWString("QuestGiver Text Window", questGiverTextWindow);
            packet.ReadInt32("int2963");
            packet.ReadInt32("int2985");
            quest.Method = packet.ReadInt32E<QuestMethod>("Method");
            quest.RewardReputationMask = packet.ReadUInt32("RewRepMask");
            packet.ReadInt32("int2953");
            packet.ReadInt32("int2983");
            packet.ReadInt32("int9");
            quest.QuestGiverTargetName = packet.ReadWoWString("QuestGiver Target Name", questGiverTargetName);
            quest.ZoneOrSort = packet.ReadInt32E<QuestSort>("Sort");
            packet.ReadInt32("int1788");
            quest.SoundTurnIn = packet.ReadUInt32("Sound TurnIn");
            quest.SourceItemId = packet.ReadUInt32<ItemId>("Source Item ID");
            quest.QuestTurnTargetName = packet.ReadWoWString("QuestTurn Target Name", questTurnTargetName);
            quest.QuestTurnInPortrait = packet.ReadUInt32("QuestTurnInPortrait");
            quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags");
            packet.ReadInt32("int2954");
            packet.ReadInt32("int2958");
            quest.RewardMoneyMaxLevel = packet.ReadUInt32("Reward Money Max Level");
            packet.ReadInt32("int1787");
            quest.QuestTurnTextWindow = packet.ReadWoWString("QuestTurn Text Window", questTurnTextWindow);
            packet.ReadInt32("int2977");
            packet.ReadInt32("int2980");
            packet.ReadInt32("int2975");
            quest.RewardSpell = (uint)packet.ReadInt32<SpellId>("Reward Spell");
            quest.RewardOrRequiredMoney = packet.ReadInt32("Reward Money");
            packet.ReadInt32("int2973");
            packet.ReadInt32("int2966");
            packet.ReadInt32("int2976");
            quest.PointOption = packet.ReadUInt32("Point Opt");
            packet.ReadInt32("int2956");

            var id = packet.ReadEntry("Quest ID");

            packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
            Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_NPC_TEXT_RESPONSE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcTextMop();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            npcText.BroadcastTextId = new uint[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                npcText.BroadcastTextId[i] = pkt.ReadUInt32("Broadcast Text Id", i);

            pkt.ClosePacket(false);

            packet.AddSniffData(StoreNameType.NpcText, entry.Key, "QUERY_RESPONSE");

            Storage.NpcTextsMop.Add((uint)entry.Key, npcText, packet.TimeSpan);
        }
    }
}
