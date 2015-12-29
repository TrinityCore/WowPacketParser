using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class UnitMisc
    {
        [BuilderMethod(Units = true)]
        public static string CreatureTemplateAddon(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template_addon))
                return string.Empty;

            var addons = new DataBag<CreatureTemplateAddon>();
            foreach (var unit in units)
            {
                Unit npc = unit.Value;

                if (Settings.AreaFilters.Length > 0)
                    if (!(npc.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(npc.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                string auras = string.Empty;
                string commentAuras = string.Empty;
                if (npc.Auras != null && npc.Auras.Count != 0)
                {
                    foreach (Aura aura in npc.Auras)
                    {
                        if (aura == null)
                            continue;

                        // usually "template auras" do not have caster
                        if (ClientVersion.AddedInVersion(ClientType.MistsOfPandaria) ? !aura.AuraFlags.HasAnyFlag(AuraFlagMoP.NoCaster) : !aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster))
                            continue;

                        auras += aura.SpellId + " ";
                        commentAuras += StoreGetters.GetName(StoreNameType.Spell, (int) aura.SpellId, false) + ", ";
                    }
                    auras = auras.TrimEnd(' ');
                    commentAuras = commentAuras.TrimEnd(',', ' ');
                }

                CreatureTemplateAddon addon = new CreatureTemplateAddon
                {
                    Entry = unit.Key.GetEntry(),
                    MountID = npc.Mount.GetValueOrDefault(),
                    Bytes1 = npc.Bytes1.GetValueOrDefault(),
                    Bytes2 = npc.Bytes2.GetValueOrDefault(),
                    Auras = auras,
                    CommentAuras = commentAuras
                };

                if (addons.ContainsKey(addon))
                    continue;

                addons.Add(addon);
            }

            var addonsDb = SQLDatabase.Get(addons);
            return SQLUtil.Compare(addons, addonsDb,
                addon =>
                {
                    string comment = StoreGetters.GetName(StoreNameType.Unit, (int)addon.Entry.GetValueOrDefault());
                    if (!string.IsNullOrEmpty(addon.CommentAuras))
                        comment += " - " + addon.CommentAuras;
                    return comment;
                });
        }

        [BuilderMethod(Units = true)]
        public static string ModelData(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_model_info))
                return string.Empty;

            var models = new DataBag<ModelData>();
            foreach (Unit npc in units.Select(unit => unit.Value))
            {
                if (Settings.AreaFilters.Length > 0)
                    if (!(npc.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(npc.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                uint modelId;
                if (npc.Model.HasValue)
                    modelId = npc.Model.Value;
                else
                    continue;

                ModelData model = new ModelData
                {
                    DisplayID = modelId
                };

                if (models.ContainsKey(model))
                    continue;

                float scale = npc.Size.GetValueOrDefault(1.0f);
                model.BoundingRadius = npc.BoundingRadius.GetValueOrDefault(0.306f) / scale;
                model.CombatReach = npc.CombatReach.GetValueOrDefault(1.5f) / scale;
                model.Gender = npc.Gender.GetValueOrDefault(Gender.Male);

                models.Add(model);
            }

            var modelsDb = SQLDatabase.Get(models);
            return SQLUtil.Compare(models, modelsDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string NpcTrainer()
        {
            if (Storage.NpcTrainers.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_trainer))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.NpcTrainers);

            return SQLUtil.Compare(Storage.NpcTrainers, templatesDb, StoreNameType.Unit);
        }

        [BuilderMethod]
        public static string NpcVendor()
        {
            if (Storage.NpcVendors.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_vendor))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.NpcVendors);

            return SQLUtil.Compare(Storage.NpcVendors, templatesDb,
                vendor => StoreGetters.GetName(vendor.Type <= 1 ? StoreNameType.Item : StoreNameType.Currency, vendor.Item.GetValueOrDefault(), false));
        }

        [BuilderMethod(Units = true)]
        public static string CreatureEquip(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_equip_template))
                return string.Empty;

            var equips = new DataBag<CreatureEquipment>();
            foreach (KeyValuePair<WowGuid, Unit> npc in units)
            {
                if (Settings.AreaFilters.Length > 0)
                    if (!(npc.Value.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(npc.Value.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                if (npc.Value.Equipment == null || npc.Value.Equipment.Length != 3)
                    continue;

                if (npc.Value.Equipment[0] == 0 && npc.Value.Equipment[1] == 0 && npc.Value.Equipment[2] == 0)
                    continue;

                CreatureEquipment equip = new CreatureEquipment
                {
                    CreatureID = npc.Key.GetEntry(),
                    ItemID1 = npc.Value.Equipment[0],
                    ItemID2 = npc.Value.Equipment[1],
                    ItemID3 = npc.Value.Equipment[2]
                };

                if (equips.Contains(equip))
                    continue;

                for (uint i = 1;; i++)
                {
                    equip.ID = i;
                    if (!equips.ContainsKey(equip))
                        break;
                }

                equips.Add(equip);
            }

            var equipsDb = SQLDatabase.Get(equips);
            return SQLUtil.Compare(equips, equipsDb, StoreNameType.Unit);
        }

        [BuilderMethod]
        public static string Gossip()
        {
            if (Storage.Gossips.IsEmpty() && Storage.GossipMenuOptions.IsEmpty())
                return string.Empty;

            string result = "";

            // `gossip_menu`
            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gossip_menu))
                result += SQLUtil.Compare(Storage.Gossips, SQLDatabase.Get(Storage.Gossips), StoreNameType.Unit); // BUG: GOs can send gossips too

            // `gossip_menu_option`
            if (Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gossip_menu_option))
                result += SQLUtil.Compare(Storage.GossipMenuOptions, SQLDatabase.Get(Storage.GossipMenuOptions), StoreNameType.None);
            
            return result;
        }

        [BuilderMethod]
        public static string PointsOfInterest()
        {
            if (Storage.GossipPOIs.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.points_of_interest))
                return string.Empty;

            string result = string.Empty;

            if (!Storage.GossipSelects.IsEmpty())
            {
                var gossipPOIsTable = new Dictionary<Tuple<uint, uint>, uint>();

                foreach (var poi in Storage.GossipPOIs)
                {
                    foreach (var gossipSelect in Storage.GossipSelects)
                    {
                        var tuple = Tuple.Create(gossipSelect.Key.Item1, gossipSelect.Key.Item2);

                        if (gossipPOIsTable.ContainsKey(tuple))
                            continue;

                        var timeSpan = poi.Item2 - gossipSelect.Value.Item2;
                        if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                            gossipPOIsTable.Add(tuple, poi.Item1.ID.GetValueOrDefault());
                    }
                }

                var menuOptions = new DataBag<GossipMenuOption>();
                foreach (var u in gossipPOIsTable)
                    menuOptions.Add(new GossipMenuOption {ID = u.Key.Item2, MenuID = u.Key.Item1, ActionPoiID = u.Value});

                result += SQLUtil.Compare(menuOptions, SQLDatabase.Get(menuOptions), StoreNameType.None);
            }

            result += SQLUtil.Compare(Storage.GossipPOIs, SQLDatabase.Get(Storage.GossipPOIs), StoreNameType.None);

            return result;
        }

        //                      entry, <minlevel, maxlevel>
        public static Dictionary<uint, Tuple<uint, uint>> GetLevels(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return null;

            var entries = units.GroupBy(unit => unit.Key.GetEntry());
            var list = new Dictionary<uint, List<uint>>();

            foreach (var pair in entries.SelectMany(entry => entry))
            {
                if (list.ContainsKey(pair.Key.GetEntry()))
                    list[pair.Key.GetEntry()].Add(pair.Value.Level.GetValueOrDefault(1));
                else
                    list.Add(pair.Key.GetEntry(), new List<uint> { pair.Value.Level.GetValueOrDefault(1) });
            }

            var result = list.ToDictionary(pair => pair.Key, pair => Tuple.Create(pair.Value.Min(), pair.Value.Max()));

            return result.Count == 0 ? null : result;
        }

        public static readonly HashSet<string> ProfessionTrainers = new HashSet<string>
        {
            "Alchemy Trainer", "Armorsmith Trainer", "Armorsmithing Trainer", "Blacksmith Trainer",
            "Blacksmithing Trainer", "Blacksmithing Trainer & Supplies", "Cold Weather Flying Trainer",
            "Cooking Trainer", "Cooking Trainer & Supplies", "Dragonscale Leatherworking Trainer",
            "Elemental Leatherworking Trainer", "Enchanting Trainer", "Engineering Trainer",
            "First Aid Trainer", "Fishing Trainer", "Fishing Trainer & Supplies",
            "Gnome Engineering Trainer", "Gnomish Engineering Trainer", "Goblin Engineering Trainer",
            "Grand Master Alchemy Trainer", "Grand Master Blacksmithing Trainer",
            "Grand Master Cooking Trainer", "Grand Master Enchanting Trainer",
            "Grand Master Engineering Trainer", "Grand Master First Aid Trainer",
            "Grand Master Fishing Trainer", "Grand Master Fishing Trainer & Supplies",
            "Grand Master Herbalism Trainer", "Grand Master Inscription Trainer",
            "Grand Master Jewelcrafting Trainer", "Grand Master Leatherworking Trainer",
            "Grand Master Mining Trainer", "Grand Master Skinning Trainer",
            "Grand Master Tailoring Trainer", "Herbalism Trainer",
            "Herbalism Trainer & Supplies", "Inscription Trainer",
            "Jewelcrafting Trainer", "Leatherworking Trainer",
            "Master Alchemy Trainer", "Master Blacksmithing Trainer",
            "Master Enchanting Trainer", "Master Engineering Trainer",
            "Master Fishing Trainer", "Master Herbalism Trainer",
            "Master Inscription Trainer", "Master Jewelcrafting Trainer",
            "Master Leatherworking Trainer", "Master Mining Trainer",
            "Master Skinning Trainer", "Master Tailoring Trainer",
            "Mining Trainer", "Skinning Trainer", "Tailor Trainer", "Tailoring Trainer",
            "Tribal Leatherworking Trainer", "Weaponsmith Trainer", "Weaponsmithing Trainer",
            "Horse Riding Trainer", "Ram Riding Trainer", "Raptor Riding Trainer",
            "Tiger Riding Trainer", "Wolf Riding Trainer", "Mechastrider Riding Trainer",
            "Riding Trainer", "Undead Horse Riding Trainer"
        };

        public static readonly HashSet<string> ClassTrainers = new HashSet<string>
        {
            "Druid Trainer", "Portal Trainer", "Portal: Darnassus Trainer",
            "Portal: Ironforge Trainer", "Portal: Orgrimmar Trainer",
            "Portal: Stormwind Trainer", "Portal: Thunder Bluff Trainer",
            "Portal: Undercity Trainer", "Deathknight Trainer",
            "Hunter Trainer", "Mage Trainer", "Paladin Trainer",
            "Priest Trainer", "Shaman Trainer", "Warlock Trainer",
            "Warrior Trainer"
        };

        /*private static string GetSubName(int entry, bool withEntry)
        {
            string name = StoreGetters.GetName(StoreNameType.Unit, entry, withEntry);
            int firstIndex = name.LastIndexOf('<');
            int lastIndex = name.LastIndexOf('>');
            if (firstIndex != -1 && lastIndex != -1)
                return name.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

            return "";
        }

        private static uint ProcessNpcFlags(string subName)
        {
            if (ProfessionTrainers.Contains(subName))
                return (uint)NPCFlags.ProfessionTrainer;
            if (ClassTrainers.Contains(subName))
                return (uint)NPCFlags.ClassTrainer;

            return 0;
        }*/

        // Non-WDB data but nevertheless data that should be saved to creature_template
        /*[BuilderMethod(Units = true)]
        public static string NpcTemplateNonWDB(Dictionary<WowGuid, Unit> units)
        {
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                return string.Empty;

            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            var levels = GetLevels(units);

            var templates = new StoreDictionary<uint, UnitTemplateNonWDB>();
            foreach (var unit in units)
            {
                if (templates.ContainsKey(unit.Key.GetEntry()))
                    continue;

                var npc = unit.Value;
                var template = new UnitTemplateNonWDB
                {
                    GossipMenuId = npc.GossipId,
                    MinLevel = (int) levels[unit.Key.GetEntry()].Item1,
                    MaxLevel = (int) levels[unit.Key.GetEntry()].Item2,
                    Faction = npc.Faction.GetValueOrDefault(35),
                    NpcFlag = (uint) npc.NpcFlags.GetValueOrDefault(NPCFlags.None),
                    SpeedRun = npc.Movement.RunSpeed,
                    SpeedWalk = npc.Movement.WalkSpeed,
                    BaseAttackTime = npc.MeleeTime.GetValueOrDefault(2000),
                    RangedAttackTime = npc.RangedTime.GetValueOrDefault(2000),
                    UnitClass = (uint) npc.Class.GetValueOrDefault(Class.Warrior),
                    UnitFlag = (uint) npc.UnitFlags.GetValueOrDefault(UnitFlags.None),
                    UnitFlag2 = (uint) npc.UnitFlags2.GetValueOrDefault(UnitFlags2.None),
                    DynamicFlag = (uint) npc.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None),
                    VehicleId = npc.Movement.VehicleId,
                    HoverHeight = npc.HoverHeight.GetValueOrDefault(1.0f)
                };

                if (template.Faction == 1 || template.Faction == 2 || template.Faction == 3 ||
                        template.Faction == 4 || template.Faction == 5 || template.Faction == 6 ||
                        template.Faction == 115 || template.Faction == 116 || template.Faction == 1610 ||
                        template.Faction == 1629 || template.Faction == 2203 || template.Faction == 2204) // player factions
                    template.Faction = 35;

                template.UnitFlag &= ~(uint)UnitFlags.IsInCombat;
                template.UnitFlag &= ~(uint)UnitFlags.PetIsAttackingTarget;
                template.UnitFlag &= ~(uint)UnitFlags.PlayerControlled;
                template.UnitFlag &= ~(uint)UnitFlags.Silenced;
                template.UnitFlag &= ~(uint)UnitFlags.PossessedByPlayer;

                if (!ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                {
                    template.DynamicFlag &= ~(uint)UnitDynamicFlags.Lootable;
                    template.DynamicFlag &= ~(uint)UnitDynamicFlags.Tapped;
                    template.DynamicFlag &= ~(uint)UnitDynamicFlags.TappedByPlayer;
                    template.DynamicFlag &= ~(uint)UnitDynamicFlags.TappedByAllThreatList;
                }
                else
                {
                    template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.Lootable;
                    template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.Tapped;
                    template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.TappedByPlayer;
                    template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.TappedByAllThreatList;
                }

                // has trainer flag but doesn't have prof nor class trainer flag
                if ((template.NpcFlag & (uint) NPCFlags.Trainer) != 0 &&
                    ((template.NpcFlag & (uint) NPCFlags.ProfessionTrainer) == 0 ||
                     (template.NpcFlag & (uint) NPCFlags.ClassTrainer) == 0))
                {
                    CreatureTemplate creatureData;
                    var subname = GetSubName((int)unit.Key.GetEntry(), false); // Fall back
                    if (Storage.CreatureTemplates.TryGetValue(unit.Key.GetEntry(), out creatureData))
                    {
                        if (creatureData.SubName.Length > 0)
                            template.NpcFlag |= ProcessNpcFlags(creatureData.SubName);
                        else // If the SubName doesn't exist or is cached, fall back to DB method
                            template.NpcFlag |= ProcessNpcFlags(subname);
                    }
                    else // In case we have NonWDB data which doesn't have an entry in CreatureTemplates
                        template.NpcFlag |= ProcessNpcFlags(subname);
                }

                templates.Add(unit.Key.GetEntry(), template);
            }

            var templatesDb = SQLDatabase.GetDict<uint, UnitTemplateNonWDB>(templates.Keys());
            return SQLUtil.CompareDicts(templates, templatesDb, StoreNameType.Unit);
        }*/

        // Non-WDB data but nevertheless data that should be saved to creature_template
        /*[BuilderMethod(Units = true)]
        public static string CreatureDifficultyMisc(Dictionary<WowGuid, Unit> units)
        {
            if (Settings.TargetedDatabase != TargetedDatabase.WarlordsOfDraenor)
                return string.Empty;

            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            var templates = new StoreDictionary<uint, CreatureDifficultyMisc>();
            foreach (var unit in units)
            {
                if (SQLDatabase.CreatureDifficultyStores != null)
                {
                    foreach (var creatureDiff in SQLDatabase.CreatureDifficultyStores)
                    {
                        if (!Utilities.EqualValues(unit.Key.GetEntry(), creatureDiff.Value.CreatureID))
                            continue;

                        if (templates.ContainsKey(creatureDiff.Key))
                            continue;

                        var npc = unit.Value;
                        var template = new CreatureDifficultyMisc
                        {
                            CreatureId = unit.Key.GetEntry(),
                            GossipMenuId = npc.GossipId,
                            NpcFlag = (uint)npc.NpcFlags.GetValueOrDefault(NPCFlags.None),
                            SpeedRun = npc.Movement.RunSpeed,
                            SpeedWalk = npc.Movement.WalkSpeed,
                            BaseAttackTime = npc.MeleeTime.GetValueOrDefault(2000),
                            RangedAttackTime = npc.RangedTime.GetValueOrDefault(2000),
                            UnitClass = (uint)npc.Class.GetValueOrDefault(Class.Warrior),
                            UnitFlag = (uint)npc.UnitFlags.GetValueOrDefault(UnitFlags.None),
                            UnitFlag2 = (uint)npc.UnitFlags2.GetValueOrDefault(UnitFlags2.None),
                            DynamicFlag = (uint)npc.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None),
                            VehicleId = npc.Movement.VehicleId,
                            HoverHeight = npc.HoverHeight.GetValueOrDefault(1.0f)
                        };

                        template.UnitFlag &= ~(uint)UnitFlags.IsInCombat;
                        template.UnitFlag &= ~(uint)UnitFlags.PetIsAttackingTarget;
                        template.UnitFlag &= ~(uint)UnitFlags.PlayerControlled;
                        template.UnitFlag &= ~(uint)UnitFlags.Silenced;
                        template.UnitFlag &= ~(uint)UnitFlags.PossessedByPlayer;

                        if (!ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                        {
                            template.DynamicFlag &= ~(uint)UnitDynamicFlags.Lootable;
                            template.DynamicFlag &= ~(uint)UnitDynamicFlags.Tapped;
                            template.DynamicFlag &= ~(uint)UnitDynamicFlags.TappedByPlayer;
                            template.DynamicFlag &= ~(uint)UnitDynamicFlags.TappedByAllThreatList;
                        }
                        else
                        {
                            template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.Lootable;
                            template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.Tapped;
                            template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.TappedByPlayer;
                            template.DynamicFlag &= ~(uint)UnitDynamicFlagsWOD.TappedByAllThreatList;
                        }

                        // has trainer flag but doesn't have prof nor class trainer flag
                        if ((template.NpcFlag & (uint)NPCFlags.Trainer) != 0 &&
                            ((template.NpcFlag & (uint)NPCFlags.ProfessionTrainer) == 0 ||
                             (template.NpcFlag & (uint)NPCFlags.ClassTrainer) == 0))
                        {
                            var name = StoreGetters.GetName(StoreNameType.Unit, (int)unit.Key.GetEntry(), false);
                            var firstIndex = name.LastIndexOf('<');
                            var lastIndex = name.LastIndexOf('>');
                            if (firstIndex != -1 && lastIndex != -1)
                            {
                                var subname = name.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

                                if (ProfessionTrainers.Contains(subname))
                                    template.NpcFlag |= (uint)NPCFlags.ProfessionTrainer;
                                else if (ClassTrainers.Contains(subname))
                                    template.NpcFlag |= (uint)NPCFlags.ClassTrainer;
                            }
                        }

                        templates.Add(creatureDiff.Key, template);
                    }
                }
            }

            var templatesDb = SQLDatabase.GetDict<uint, CreatureDifficultyMisc>(templates.Keys(), "Id");
            return SQLUtil.CompareDicts(templates, templatesDb, StoreNameType.Unit, "Id");
        }*/

        
        [BuilderMethod]
        public static string CreatureText()
        {
            if (Storage.CreatureTexts.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_text))
                return string.Empty;

            // For each sound and emote, if the time they were send is in the +1/-1 seconds range of
            // our texts, add that sound and emote to our Storage.CreatureTexts

            var broadcastTextStoresMale = SQLDatabase.BroadcastTextStores.GroupBy(blub => blub.Item2.MaleText).ToDictionary(group => group.Key, group => group.ToList());
            var broadcastTextStoresFemale = SQLDatabase.BroadcastTextStores.GroupBy(blub => blub.Item2.FemaleText).ToDictionary(group => group.Key, group => group.ToList());

            foreach (var text in Storage.CreatureTexts)
            {
                // For each text
                foreach (var textValue in text.Value)
                {
                    // For each emote
                    var text1 = text;
                    var value1 = textValue;
                    foreach (var emoteValue in from emote in Storage.Emotes where emote.Key.GetEntry() == text1.Key from emoteValue in emote.Value let timeSpan = value1.Item2 - emoteValue.Item2 where timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1) select emoteValue)
                        textValue.Item1.Emote = emoteValue.Item1;

                    // For each sound
                    var value = textValue;
                    foreach (var sound in from sound in Storage.Sounds let timeSpan = value.Item2 - sound.Item2 where timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1) select sound)
                        textValue.Item1.Sound = sound.Item1;

                    if (SQLDatabase.BroadcastTextStores != null)
                    {
                        List<Tuple<uint, BroadcastText>> textList;
                        if (broadcastTextStoresMale.TryGetValue(textValue.Item1.Text, out textList) ||
                            broadcastTextStoresFemale.TryGetValue(textValue.Item1.Text, out textList))
                            foreach (var broadcastTextId in textList)
                            {
                                if (!string.IsNullOrWhiteSpace(textValue.Item1.BroadcastTextID))
                                    textValue.Item1.BroadcastTextID += " - " + broadcastTextId.Item1;
                                else
                                    textValue.Item1.BroadcastTextID = broadcastTextId.Item1.ToString();
                            }
                    }

                    // Set comment
                    string from = null, to = null;
                    if (!textValue.Item1.SenderGUID.IsEmpty())
                    {
                        if (textValue.Item1.SenderGUID.GetObjectType() == ObjectType.Player)
                            from = "Player";
                        else
                            @from = !string.IsNullOrEmpty(textValue.Item1.SenderName) ? textValue.Item1.SenderName : StoreGetters.GetName(StoreNameType.Unit, (int)textValue.Item1.SenderGUID.GetEntry(), false);
                    }

                    if (!textValue.Item1.ReceiverGUID.IsEmpty())
                    {
                        if (textValue.Item1.ReceiverGUID.GetObjectType() == ObjectType.Player)
                            to = "Player";
                        else
                            to = !string.IsNullOrEmpty(textValue.Item1.ReceiverName) ? textValue.Item1.ReceiverName : StoreGetters.GetName(StoreNameType.Unit, (int)textValue.Item1.ReceiverGUID.GetEntry(), false);
                    }

                    Trace.Assert(text.Key == textValue.Item1.SenderGUID.GetEntry() ||
                        text.Key == textValue.Item1.ReceiverGUID.GetEntry());

                    if (from != null && to != null)
                        textValue.Item1.Comment = from + " to " + to;
                    else if (from != null)
                        textValue.Item1.Comment = from;
                    else
                        Trace.Assert(false);
                }
            }

            /* can't use compare DB without knowing values of groupid or id
            var entries = Storage.CreatureTexts.Keys.ToList();
            var creatureTextDb = SQLDatabase.GetDict<uint, CreatureText>(entries);
            */

            var rows = new RowList<CreatureText>();
            foreach (var text in Storage.CreatureTexts)
            {
                foreach (var textValue in text.Value)
                {
                    var row = new Row<CreatureText>
                    {
                        Data = new CreatureText
                        {
                            Entry = text.Key,
                            GroupId = null,
                            ID = null,
                            Text = textValue.Item1.Text,
                            Type = textValue.Item1.Type,
                            Language = textValue.Item1.Language,
                            Probability = 100.0f,
                            Emote = textValue.Item1.Emote,
                            Duration = 0,
                            Sound = textValue.Item1.Sound,
                            BroadcastTextID = textValue.Item1.BroadcastTextID,
                            Comment = textValue.Item1.Comment
                        }
                    };

                    rows.Add(row);
                }
            }

            return new SQLInsert<CreatureText>(rows, false).Build();
        }

        [BuilderMethod]
        public static string VehicleAccessory()
        {
            if (Storage.VehicleTemplateAccessorys.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.vehicle_template_accessory))
                return string.Empty;

            var rows = new RowList<VehicleTemplateAccessory>();
            foreach (var accessory in Storage.VehicleTemplateAccessorys)
            {
                var row = new Row<VehicleTemplateAccessory>();

                if (accessory.Item1.SeatId < 0 || accessory.Item1.SeatId > 7)
                    continue;

                row.Comment = StoreGetters.GetName(StoreNameType.Unit, (int)accessory.Item1.Entry.GetValueOrDefault(), false) + " - ";
                row.Comment += StoreGetters.GetName(StoreNameType.Unit, (int)accessory.Item1.AccessoryEntry.GetValueOrDefault(), false);
                accessory.Item1.Description = row.Comment;
                row.Data = accessory.Item1;

                rows.Add(row);
            }

            return new SQLInsert<VehicleTemplateAccessory>(rows, false).Build();
        }

        [BuilderMethod]
        public static string NpcSpellClick()
        {
            if (Storage.NpcSpellClicks.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_spellclick_spells))
                return string.Empty;

            var rows = new RowList<NpcSpellClick>();

            foreach (var npcSpellClick in Storage.NpcSpellClicks)
            {
                foreach (var spellClick in Storage.SpellClicks)
                {
                    var row = new Row<NpcSpellClick>();

                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Unit && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Unit)
                        spellClick.Item1.CastFlags = 0x0;
                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Player && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Unit)
                        spellClick.Item1.CastFlags = 0x1;
                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Unit && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Player)
                        spellClick.Item1.CastFlags = 0x2;
                    if (spellClick.Item1.CasterGUID.GetObjectType() == ObjectType.Player && spellClick.Item1.TargetGUID.GetObjectType() == ObjectType.Player)
                        spellClick.Item1.CastFlags = 0x3;

                    spellClick.Item1.Entry = npcSpellClick.Item1.GetEntry();
                    row.Data = spellClick.Item1;

                    var timeSpan = spellClick.Item2 - npcSpellClick.Item2;
                    if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                        rows.Add(row);
                }
            }

            return new SQLInsert<NpcSpellClick>(rows, false).Build();
        }

        [BuilderMethod(Units = true)]
        public static string NpcSpellClickMop(Dictionary<WowGuid, Unit> units)
        {
            if (units.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_spellclick_spells))
                return string.Empty;

            var rows = new RowList<NpcSpellClick>();

            foreach (var unit in units)
            {
                var row = new Row<NpcSpellClick>();

                Unit npc = unit.Value;
                if (npc.InteractSpellID == null)
                    continue;

                if (Settings.AreaFilters.Length > 0)
                    if (!(npc.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(npc.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                row.Data.Entry = unit.Key.GetEntry();
                row.Data.SpellID = npc.InteractSpellID.GetValueOrDefault();

                rows.Add(row);
            }

            return new SQLInsert<NpcSpellClick>(rows, false).Build();
        }
    }
}
