using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class WDBTemplates
    {
        [BuilderMethod(true)]
        public static string QuestTemplate()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Storage.QuestTemplates.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestTemplates);

            return SQLUtil.Compare(Storage.QuestTemplates, templatesDb, StoreNameType.Quest);
        }

        [BuilderMethod(true)]
        public static string QuestObjective()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Settings.TargetedDatabase == TargetedDatabase.WrathOfTheLichKing ||
                Settings.TargetedDatabase == TargetedDatabase.Cataclysm)
                return string.Empty;

            if (Storage.QuestObjectives.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestObjectives);

            return SQLUtil.Compare(Storage.QuestObjectives, templatesDb, StoreNameType.QuestObjective);  
        }

        [BuilderMethod(true)]
        public static string QuestVisualEffect()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Settings.TargetedDatabase == TargetedDatabase.WrathOfTheLichKing ||
                Settings.TargetedDatabase == TargetedDatabase.Cataclysm)
                return string.Empty;

            if (Storage.QuestVisualEffects.IsEmpty())
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.QuestVisualEffects);

            return SQLUtil.Compare(Storage.QuestVisualEffects, templateDb, StoreNameType.None);
        }

        [BuilderMethod(true, Units = true)]
        public static string CreatureTemplate(Dictionary<WowGuid, Unit> units)
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            if (Storage.CreatureTemplates.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CreatureTemplates);

            foreach (var cre in Storage.CreatureTemplates) // set some default values
            {
                Unit unit = units.FirstOrDefault(p => p.Key.GetEntry() == cre.Item1.Entry.GetValueOrDefault()).Value;
                var levels = UnitMisc.GetLevels(units);

                if (unit != null)
                {
                    if (unit.GossipId == 0)
                        cre.Item1.GossipMenuID = null;
                    else
                        cre.Item1.GossipMenuID = unit.GossipId;

                    cre.Item1.MinLevel = (int)levels[cre.Item1.Entry.GetValueOrDefault()].Item1;
                    cre.Item1.MaxLevel = (int)levels[cre.Item1.Entry.GetValueOrDefault()].Item2;

                    HashSet<uint> playerFactions = new HashSet<uint> { 1, 2, 3, 4, 5, 6, 115, 116, 1610, 1629, 2203, 2204 };
                    cre.Item1.Faction = unit.Faction.GetValueOrDefault(35);
                    if (playerFactions.Contains(unit.Faction.GetValueOrDefault()))
                        cre.Item1.Faction = 35;
                    
                    cre.Item1.NpcFlag = unit.NpcFlags.GetValueOrDefault(NPCFlags.None);
                    cre.Item1.SpeedWalk = unit.Movement.WalkSpeed;
                    cre.Item1.SpeedRun = unit.Movement.RunSpeed;
                    
                    cre.Item1.BaseAttackTime = unit.MeleeTime.GetValueOrDefault(2000);
                    cre.Item1.RangeAttackTime = unit.RangedTime.GetValueOrDefault(2000);
                    
                    cre.Item1.UnitClass = (uint)unit.Class.GetValueOrDefault(Class.Warrior);

                    cre.Item1.UnitFlags = unit.UnitFlags.GetValueOrDefault(UnitFlags.None);
                    cre.Item1.UnitFlags &= ~UnitFlags.IsInCombat;
                    cre.Item1.UnitFlags &= ~UnitFlags.PetIsAttackingTarget;
                    cre.Item1.UnitFlags &= ~UnitFlags.PlayerControlled;
                    cre.Item1.UnitFlags &= ~UnitFlags.Silenced;
                    cre.Item1.UnitFlags &= ~UnitFlags.PossessedByPlayer;

                    cre.Item1.UnitFlags2 = unit.UnitFlags2.GetValueOrDefault(UnitFlags2.None);

                    if (Settings.TargetedDatabase != TargetedDatabase.WarlordsOfDraenor)
                    {
                        cre.Item1.DynamicFlags = unit.DynamicFlags.GetValueOrDefault(UnitDynamicFlags.None);
                        cre.Item1.DynamicFlags &= ~UnitDynamicFlags.Lootable;
                        cre.Item1.DynamicFlags &= ~UnitDynamicFlags.Tapped;
                        cre.Item1.DynamicFlags &= ~UnitDynamicFlags.TappedByPlayer;
                        cre.Item1.DynamicFlags &= ~UnitDynamicFlags.TappedByAllThreatList;
                    }
                    else
                    {
                        cre.Item1.DynamicFlagsWod = unit.DynamicFlagsWod.GetValueOrDefault(UnitDynamicFlagsWOD.None);
                        cre.Item1.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.Lootable;
                        cre.Item1.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.Tapped;
                        cre.Item1.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.TappedByPlayer;
                        cre.Item1.DynamicFlagsWod &= ~UnitDynamicFlagsWOD.TappedByAllThreatList;
                    }
                    cre.Item1.VehicleID = unit.Movement.VehicleId;
                    cre.Item1.HoverHeight = unit.HoverHeight.GetValueOrDefault(1.0f);

                    //TODO: set TrainerType from SMSG_TRAINER_LIST
                    cre.Item1.TrainerType = 0;

                    cre.Item1.Resistances = new uint?[] {0, 0, 0, 0, 0, 0};
                    for (int i = 0; i < unit.Resistances.Length - 1; i++)
                        cre.Item1.Resistances[i] = unit.Resistances[i + 1];
                }

                // has trainer flag but doesn't have prof nor class trainer flag
                if (cre.Item1.NpcFlag.GetValueOrDefault().HasFlag(NPCFlags.Trainer) &&
                    (!cre.Item1.NpcFlag.GetValueOrDefault().HasFlag(NPCFlags.ProfessionTrainer) ||
                     !cre.Item1.NpcFlag.GetValueOrDefault().HasFlag(NPCFlags.ClassTrainer)))
                {
                    string name = StoreGetters.GetName(StoreNameType.Unit, (int)cre.Item1.Entry.GetValueOrDefault(), false);
                    int firstIndex = name.LastIndexOf('<');
                    int lastIndex = name.LastIndexOf('>');
                    if (firstIndex != -1 && lastIndex != -1)
                    {
                        string subname = name.Substring(firstIndex + 1, lastIndex - firstIndex - 1);

                        if (UnitMisc.ProfessionTrainers.Contains(subname))
                            cre.Item1.NpcFlag |= NPCFlags.ProfessionTrainer;
                        else if (UnitMisc.ClassTrainers.Contains(subname))
                            cre.Item1.NpcFlag |= NPCFlags.ClassTrainer;
                    }
                }

                cre.Item1.DifficultyEntries = new uint?[] {null, null, null};
                cre.Item1.Scale = 1;
                cre.Item1.DmgSchool = 0;
                cre.Item1.BaseVariance = 1;
                cre.Item1.RangeVariance = 1;
                cre.Item1.Resistances = new uint?[] {null, null, null, null, null, null};
                cre.Item1.Spells = new uint?[] {0, 0, 0, 0, 0, 0, 0, 0};
                cre.Item1.HealthModifierExtra = 1;
                cre.Item1.ManaModifierExtra = 1;
                cre.Item1.ArmorModifier = 1;
            }

            foreach (
                var cre in
                    Storage.CreatureTemplates.Where(
                        cre => Storage.SpellsX.ContainsKey(cre.Item1.Entry.GetValueOrDefault())))
            {
                cre.Item1.Spells = Storage.SpellsX[cre.Item1.Entry.GetValueOrDefault()].Item1.ToArray();
            }

            return SQLUtil.Compare(Storage.CreatureTemplates, templatesDb, StoreNameType.Unit);
        }

        [BuilderMethod(true, Gameobjects = true)]
        public static string GameObjectTemplate(Dictionary<WowGuid, GameObject> gameobjects)
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_template))
                return string.Empty;

            if (Storage.GameObjectTemplates.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.GameObjectTemplates);

            foreach (var goT in Storage.GameObjectTemplates)
            {
                GameObject go = gameobjects.FirstOrDefault(p => p.Key.GetEntry() == goT.Item1.Entry.GetValueOrDefault()).Value;

                if (go != null)
                {
                    if (goT.Item1.Size == null) // only true for 3.x and 4.x. WDB field since 5.x
                        goT.Item1.Size = go.Size.GetValueOrDefault(1.0f);

                    HashSet<uint> playerFactions = new HashSet<uint> { 1, 2, 3, 4, 5, 6, 115, 116, 1610, 1629, 2203, 2204 };
                    goT.Item1.Faction = go.Faction.GetValueOrDefault(0);
                    if (playerFactions.Contains(go.Faction.GetValueOrDefault()))
                        goT.Item1.Faction = 0;

                    goT.Item1.Flags = go.Flags.GetValueOrDefault(GameObjectFlag.None);
                    goT.Item1.Flags &= ~GameObjectFlag.Triggered;
                    goT.Item1.Flags &= ~GameObjectFlag.Damaged;
                    goT.Item1.Flags &= ~GameObjectFlag.Destroyed;
                }
            }

            return SQLUtil.Compare(Storage.GameObjectTemplates, templatesDb, StoreNameType.GameObject);
        }

        [BuilderMethod(true)]
        public static string ItemTemplate()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.item_template))
                return string.Empty;

            if (Settings.TargetedDatabase == TargetedDatabase.WarlordsOfDraenor)
                return string.Empty;

            if (Storage.ItemTemplates.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ItemTemplates);

            return SQLUtil.Compare(Storage.ItemTemplates, templatesDb, StoreNameType.Item);
        }

        [BuilderMethod(true)]
        public static string PageText()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.page_text))
                return string.Empty;

            if (Storage.PageTexts.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PageTexts);

            return SQLUtil.Compare(Storage.PageTexts, templatesDb, StoreNameType.PageText);
        }

        [BuilderMethod(true)]
        public static string NpcText()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.npc_text))
                return string.Empty;

            if (!Storage.NpcTexts.IsEmpty() && 
                (Settings.TargetedDatabase == TargetedDatabase.WrathOfTheLichKing || 
                Settings.TargetedDatabase == TargetedDatabase.Cataclysm))
            {
                foreach (var npcText in Storage.NpcTexts)
                    npcText.Item1.ConvertToDBStruct();

                var templatesDb = SQLDatabase.Get(Storage.NpcTexts);

                return SQLUtil.Compare(Storage.NpcTexts, templatesDb, StoreNameType.NpcText);
            }

            if (!Storage.NpcTextsMop.IsEmpty() && Settings.TargetedDatabase == TargetedDatabase.WarlordsOfDraenor)
            {
                foreach (var npcText in Storage.NpcTextsMop)
                    npcText.Item1.ConvertToDBStruct();

                var templatesDb = SQLDatabase.Get(Storage.NpcTextsMop);

                return SQLUtil.Compare(Storage.NpcTextsMop, templatesDb, StoreNameType.NpcText);
            }

            return string.Empty;
        }
    }
}
