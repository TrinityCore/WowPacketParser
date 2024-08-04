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

        [BuilderMethod(true)]
        public static string QuestRewardDisplaySpell()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                return string.Empty;

            if (Storage.QuestRewardDisplaySpells.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.QuestRewardDisplaySpells);

            return SQLUtil.Compare(Storage.QuestRewardDisplaySpells, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string QuestTreasurePickers()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.quest_template))
                return string.Empty;

            if (Settings.TargetedDatabase < TargetedDatabase.TheWarWithin)
                return string.Empty;

            if (Storage.QuestTreasurePickersStorage.IsEmpty())
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.QuestTreasurePickersStorage);

            return SQLUtil.Compare(Storage.QuestTreasurePickersStorage, templateDb, StoreNameType.Quest);
        }

        [BuilderMethod(true, Units = true)]
        public static string CreatureTemplate(Dictionary<WowGuid, Unit> units)
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            if (!Storage.CreatureTemplates.IsEmpty() && Settings.TargetedDatabase != TargetedDatabase.Classic)
            {
                var templatesDb = SQLDatabase.Get(Storage.CreatureTemplates.Values);
                return SQLUtil.Compare(Storage.CreatureTemplates.Values, templatesDb, StoreNameType.Unit);
            }

            if (!Storage.CreatureTemplatesClassic.IsEmpty() && Settings.TargetedDatabase == TargetedDatabase.Classic)
            {
                var templatesDb = SQLDatabase.Get(Storage.CreatureTemplatesClassic);
                return SQLUtil.Compare(Storage.CreatureTemplatesClassic, templatesDb, StoreNameType.Unit);
            }

            return string.Empty;
        }

        [BuilderMethod(true)]
        public static string CreatureTemplateModel()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            if (Storage.CreatureTemplateModels.IsEmpty())
                return string.Empty;

            if (Settings.TargetedDatabase < TargetedDatabase.BattleForAzeroth)
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CreatureTemplateModels);

            return SQLUtil.Compare(Storage.CreatureTemplateModels, templatesDb, StoreNameType.Unit);
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

        [BuilderMethod(true)]
        public static string CreatureTemplateQuestCurrencies()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return string.Empty;

            if (Storage.CreatureTemplateQuestCurrencies.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.CreatureTemplateQuestCurrencies);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.CreatureTemplateQuestCurrencies.OrderBy(x => x.Item1.CreatureId).ThenBy(y => y.Item1.CurrencyId) : Storage.CreatureTemplateQuestCurrencies, templatesDb, x =>
            {
                string creatureName = StoreGetters.GetName(StoreNameType.Unit, (int)x.CreatureId, false);
                string currencyName = StoreGetters.GetName(StoreNameType.Currency, (int)x.CurrencyId, false);
                return $"{creatureName} - {currencyName}";
            });
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
                        goT.Item1.Size = go.ObjectData.Scale;
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

        [BuilderMethod]
        public static string PlayerChoice()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playerchoice))
                return string.Empty;

            if (Storage.PlayerChoices.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PlayerChoices);

            return SQLUtil.Compare(Storage.PlayerChoices, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string PlayerChoiceResponse()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playerchoice))
                return string.Empty;

            if (Storage.PlayerChoiceResponses.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PlayerChoiceResponses);

            return SQLUtil.Compare(Storage.PlayerChoiceResponses, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string PlayerChoiceResponseReward()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playerchoice))
                return string.Empty;

            if (Storage.PlayerChoiceResponseRewards.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PlayerChoiceResponseRewards);

            return SQLUtil.Compare(Storage.PlayerChoiceResponseRewards, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string PlayerChoiceResponseRewardCurrency()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playerchoice))
                return string.Empty;

            if (Storage.PlayerChoiceResponseRewardCurrencies.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PlayerChoiceResponseRewardCurrencies);

            return SQLUtil.Compare(Storage.PlayerChoiceResponseRewardCurrencies, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string PlayerChoiceResponseRewardFaction()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playerchoice))
                return string.Empty;

            if (Storage.PlayerChoiceResponseRewardFactions.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PlayerChoiceResponseRewardFactions);

            return SQLUtil.Compare(Storage.PlayerChoiceResponseRewardFactions, templatesDb, StoreNameType.None);
        }

        [BuilderMethod]
        public static string PlayerChoiceResponseRewardItem()
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.playerchoice))
                return string.Empty;

            if (Storage.PlayerChoiceResponseRewardItems.IsEmpty())
                return string.Empty;

            var templatesDb = SQLDatabase.Get(Storage.PlayerChoiceResponseRewardItems);

            return SQLUtil.Compare(Storage.PlayerChoiceResponseRewardItems, templatesDb, StoreNameType.None);
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

                // Check for broadcast_texts which fit perfectly (e.g. only 1 entry or only Text0 or only Text1 fitting)
                foreach (var npcText in Storage.NpcTexts)
                {
                    List<int> optionTextList0;
                    List<int> optionTextList1;
                    List<int> optionTextList2;
                    List<int> optionTextList3;
                    List<int> optionTextList4;
                    List<int> optionTextList5;
                    List<int> optionTextList6;
                    List<int> optionTextList7;

                    if (npcText.Item1.Text00 == string.Empty && npcText.Item1.Text01 == string.Empty)
                        npcText.Item1.BroadcastTextID0 = 0;

                    if (npcText.Item1.Text00 != string.Empty && npcText.Item1.Text01 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text00, out optionTextList0))
                        {
                            if (optionTextList0.Count == 1)
                                npcText.Item1.BroadcastTextID0 = (uint)optionTextList0.First();
                        }
                    }
                    else if (npcText.Item1.Text01 != string.Empty && npcText.Item1.Text00 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text01, out optionTextList0))
                        {
                            if (optionTextList0.Count == 1)
                                npcText.Item1.BroadcastTextID0 = (uint)optionTextList0.First();
                        }
                    }

                    if (npcText.Item1.Text10 == string.Empty && npcText.Item1.Text11 == string.Empty)
                        npcText.Item1.BroadcastTextID1 = 0;

                    if (npcText.Item1.Text10 != string.Empty && npcText.Item1.Text11 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text10, out optionTextList1))
                        {
                            if (optionTextList1.Count == 1)
                                npcText.Item1.BroadcastTextID1 = (uint)optionTextList1.First();
                        }
                    }
                    else if (npcText.Item1.Text11 != string.Empty && npcText.Item1.Text10 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text11, out optionTextList1))
                        {
                            if (optionTextList1.Count == 1)
                                npcText.Item1.BroadcastTextID1 = (uint)optionTextList1.First();
                        }
                    }

                    if (npcText.Item1.Text20 == string.Empty && npcText.Item1.Text21 == string.Empty)
                        npcText.Item1.BroadcastTextID2 = 0;

                    if (npcText.Item1.Text20 != string.Empty && npcText.Item1.Text21 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text20, out optionTextList2))
                        {
                            if (optionTextList2.Count == 1)
                                npcText.Item1.BroadcastTextID2 = (uint)optionTextList2.First();
                        }
                    }
                    else if (npcText.Item1.Text21 != string.Empty && npcText.Item1.Text20 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text21, out optionTextList2))
                        {
                            if (optionTextList2.Count == 1)
                                npcText.Item1.BroadcastTextID2 = (uint)optionTextList2.First();
                        }
                    }

                    if (npcText.Item1.Text30 == string.Empty && npcText.Item1.Text31 == string.Empty)
                        npcText.Item1.BroadcastTextID3 = 0;

                    if (npcText.Item1.Text30 != string.Empty && npcText.Item1.Text31 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text30, out optionTextList3))
                        {
                            if (optionTextList3.Count == 1)
                                npcText.Item1.BroadcastTextID3 = (uint)optionTextList3.First();
                        }
                    }
                    else if (npcText.Item1.Text31 != string.Empty && npcText.Item1.Text30 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text31, out optionTextList3))
                        {
                            if (optionTextList3.Count == 1)
                                npcText.Item1.BroadcastTextID3 = (uint)optionTextList3.First();
                        }
                    }

                    if (npcText.Item1.Text40 == string.Empty && npcText.Item1.Text41 == string.Empty)
                        npcText.Item1.BroadcastTextID4 = 0;

                    if (npcText.Item1.Text40 != string.Empty && npcText.Item1.Text41 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text40, out optionTextList4))
                        {
                            if (optionTextList4.Count == 1)
                                npcText.Item1.BroadcastTextID4 = (uint)optionTextList4.First();
                        }
                    }
                    else if (npcText.Item1.Text41 != string.Empty && npcText.Item1.Text40 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text41, out optionTextList4))
                        {
                            if (optionTextList4.Count == 1)
                                npcText.Item1.BroadcastTextID4 = (uint)optionTextList4.First();
                        }
                    }

                    if (npcText.Item1.Text50 == string.Empty && npcText.Item1.Text51 == string.Empty)
                        npcText.Item1.BroadcastTextID5 = 0;

                    if (npcText.Item1.Text50 != string.Empty && npcText.Item1.Text51 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text50, out optionTextList5))
                        {
                            if (optionTextList5.Count == 1)
                                npcText.Item1.BroadcastTextID5 = (uint)optionTextList5.First();
                        }
                    }
                    else if (npcText.Item1.Text51 != string.Empty && npcText.Item1.Text50 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text51, out optionTextList5))
                        {
                            if (optionTextList5.Count == 1)
                                npcText.Item1.BroadcastTextID5 = (uint)optionTextList5.First();
                        }
                    }

                    if (npcText.Item1.Text60 == string.Empty && npcText.Item1.Text61 == string.Empty)
                        npcText.Item1.BroadcastTextID6 = 0;

                    if (npcText.Item1.Text60 != string.Empty && npcText.Item1.Text61 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text60, out optionTextList6))
                        {
                            if (optionTextList6.Count == 1)
                                npcText.Item1.BroadcastTextID6 = (uint)optionTextList6.First();
                        }
                    }
                    else if (npcText.Item1.Text61 != string.Empty && npcText.Item1.Text60 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text61, out optionTextList6))
                        {
                            if (optionTextList6.Count == 1)
                                npcText.Item1.BroadcastTextID6 = (uint)optionTextList6.First();
                        }
                    }

                    if (npcText.Item1.Text70 == string.Empty && npcText.Item1.Text71 == string.Empty)
                        npcText.Item1.BroadcastTextID7 = 0;

                    if (npcText.Item1.Text70 != string.Empty && npcText.Item1.Text71 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastTexts.TryGetValue(npcText.Item1.Text70, out optionTextList7))
                        {
                            if (optionTextList7.Count == 1)
                                npcText.Item1.BroadcastTextID7 = (uint)optionTextList7.First();
                        }
                    }
                    else if (npcText.Item1.Text71 != string.Empty && npcText.Item1.Text70 == string.Empty)
                    {
                        if (SQLDatabase.BroadcastText1s.TryGetValue(npcText.Item1.Text71, out optionTextList7))
                        {
                            if (optionTextList7.Count == 1)
                                npcText.Item1.BroadcastTextID7 = (uint)optionTextList7.First();
                        }
                    }
                }

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
