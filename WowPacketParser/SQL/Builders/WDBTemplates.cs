using System;
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

            foreach (var creatureTemplate in Storage.CreatureTemplates)
            {
                if (creatureTemplate.Item1.FemaleName == null)
                    creatureTemplate.Item1.FemaleName = string.Empty;
            }

            var templatesDb = SQLDatabase.Get(Storage.CreatureTemplates);
            return SQLUtil.Compare(Storage.CreatureTemplates, templatesDb, StoreNameType.Unit);
        }

        [BuilderMethod(true)]
        public static string CreatureTemplateQuestItem()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            if (Settings.TargetedDatabase <= TargetedDatabase.WarlordsOfDraenor)
                return string.Empty;

            if (Storage.CreatureTemplateQuestItems.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CreatureTemplateQuestItems);

            return SQLUtil.Compare(Storage.CreatureTemplateQuestItems, templatesDb, StoreNameType.Unit);
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
                }
            }

            return SQLUtil.Compare(Storage.GameObjectTemplates, templatesDb, StoreNameType.GameObject);
        }

        [BuilderMethod(true)]
        public static string GameObjectTemplateQuestItem()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_template))
                return string.Empty;

            if (Settings.TargetedDatabase <= TargetedDatabase.WarlordsOfDraenor)
                return string.Empty;

            if (Storage.GameObjectTemplateQuestItems.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.GameObjectTemplateQuestItems);

            return SQLUtil.Compare(Storage.GameObjectTemplateQuestItems, templatesDb, StoreNameType.GameObject);
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

            if (!Storage.NpcTextsMop.IsEmpty() && Settings.TargetedDatabase >= TargetedDatabase.WarlordsOfDraenor)
            {
                foreach (var npcText in Storage.NpcTextsMop)
                    npcText.Item1.ConvertToDBStruct();

                var templatesDb = SQLDatabase.Get(Storage.NpcTextsMop);

                return SQLUtil.Compare(Storage.NpcTextsMop, templatesDb, StoreNameType.NpcText);
            }

            return string.Empty;
        }

        [BuilderMethod(true)]
        public static string ScenarioPOI()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.scenario_poi))
                return string.Empty;

            if (Storage.ScenarioPOIs.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ScenarioPOIs);

            return SQLUtil.Compare(Storage.ScenarioPOIs, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string ScenarioPOIPoint()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.scenario_poi))
                return string.Empty;

            if (Storage.ScenarioPOIPoints.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.ScenarioPOIPoints);

            return SQLUtil.Compare(Storage.ScenarioPOIPoints, templatesDb, StoreNameType.None);
        }
    }
}
