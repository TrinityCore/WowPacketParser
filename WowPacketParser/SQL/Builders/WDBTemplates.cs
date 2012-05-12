using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    public static class WDBTemplates
    {
        public static string Quest()
        {
            if (Storage.QuestTemplates.IsEmpty())
                return String.Empty;

            const string tableName = "quest_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (KeyValuePair<uint, Tuple<QuestTemplate, TimeSpan?>> questPair in Storage.QuestTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();
                var quest = questPair.Value.Item1;

                row.AddValue("Id", questPair.Key);
                row.AddValue("Method", quest.Method);
                row.AddValue("Level", quest.Level);
                row.AddValue("MinLevel", quest.MinLevel);
                row.AddValue("ZoneOrSort", quest.ZoneOrSort);
                row.AddValue("Type", quest.Type);
                row.AddValue("SuggestedPlayers", quest.SuggestedPlayers);

                for (var i = 0; i < quest.RequiredFactionId.Length; i++)
                    row.AddValue("RequiredFactionId" + (i + 1), quest.RequiredFactionId[i]);

                for (var i = 0; i < quest.RequiredFactionId.Length; i++)
                    row.AddValue("RequiredFactionValue" + (i + 1), quest.RequiredFactionValue[i]);

                row.AddValue("NextQuestIdChain", quest.NextQuestIdChain);
                row.AddValue("RewardXPId", quest.RewardXPId);
                row.AddValue("RewardOrRequiredMoney", quest.RewardOrRequiredMoney);
                row.AddValue("RewardMoneyMaxLevel", quest.RewardMoneyMaxLevel);
                row.AddValue("RewardSpell", quest.RewardSpell);
                row.AddValue("RewardSpellCast", quest.RewardSpellCast);
                row.AddValue("RewardHonor", quest.RewardHonor);
                row.AddValue("RewardHonorMultiplier", quest.RewardHonorMultiplier);
                row.AddValue("SourceItemId", quest.SourceItemId);
                row.AddValue("Flags", quest.Flags, true);
                row.AddValue("MinimapTargetMark", quest.MinimapTargetMark);
                row.AddValue("RewardTitleId", quest.RewardTitleId);
                row.AddValue("RequiredPlayerKills", quest.RequiredPlayerKills);
                row.AddValue("RewardTalents", quest.RewardTalents);
                row.AddValue("RewardArenaPoints", quest.RewardArenaPoints);
                row.AddValue("RewardSkillId", quest.RewardSkillId);
                row.AddValue("RewardSkillPoints", quest.RewardSkillPoints);
                row.AddValue("RewardReputationMask", quest.RewardReputationMask);
                row.AddValue("QuestGiverPortrait", quest.QuestGiverPortrait);
                row.AddValue("QuestTurnInPortrait", quest.QuestTurnInPortrait);

                for (var i = 0; i < quest.RewardItemId.Length; i++)
                    row.AddValue("RewardItemId" + (i + 1), quest.RewardItemId[i]);

                for (var i = 0; i < quest.RewardItemCount.Length; i++)
                    row.AddValue("RewardItemCount" + (i + 1), quest.RewardItemCount[i]);

                for (var i = 0; i < quest.RewardChoiceItemId.Length; i++)
                    row.AddValue("RewardChoiceItemId" + (i + 1), quest.RewardChoiceItemId[i]);

                for (var i = 0; i < quest.RewardChoiceItemCount.Length; i++)
                    row.AddValue("RewardChoiceItemCount" + (i + 1), quest.RewardChoiceItemCount[i]);

                for (var i = 0; i < quest.RewardFactionId.Length; i++)
                    row.AddValue("RewardFactionId" + (i + 1), quest.RewardFactionId[i]);

                for (var i = 0; i < quest.RewardFactionValueId.Length; i++)
                    row.AddValue("RewardFactionValueId" + (i + 1), quest.RewardFactionValueId[i]);

                for (var i = 0; i < quest.RewardFactionValueIdOverride.Length; i++)
                    row.AddValue("RewardFactionValueIdOverride" + (i + 1), quest.RewardFactionValueIdOverride[i]);

                row.AddValue("PointMapId", quest.PointMapId);
                row.AddValue("PointX", quest.PointX);
                row.AddValue("PointY", quest.PointY);
                row.AddValue("PointOption", quest.PointOption);
                row.AddValue("Title", quest.Title);
                row.AddValue("Objectives", quest.Objectives);
                row.AddValue("Details", quest.Details);
                row.AddValue("EndText", quest.EndText);
                row.AddValue("CompletedText", quest.CompletedText);

                for (var i = 0; i < quest.RequiredNpcOrGo.Length; i++)
                    row.AddValue("RequiredNpcOrGo" + (i + 1), quest.RequiredNpcOrGo[i]);

                for (var i = 0; i < quest.RequiredNpcOrGoCount.Length; i++)
                    row.AddValue("RequiredNpcOrGoCount" + (i + 1), quest.RequiredNpcOrGoCount[i]);

                for (var i = 0; i < quest.RequiredSourceItemId.Length; i++)
                    row.AddValue("RequiredSourceItemId" + (i + 1), quest.RequiredSourceItemId[i]);

                for (var i = 0; i < quest.RequiredSourceItemCount.Length; i++)
                    row.AddValue("RequiredSourceItemCount" + (i + 1), quest.RequiredSourceItemCount[i]);

                for (var i = 0; i < quest.RequiredItemId.Length; i++)
                    row.AddValue("RequiredItemId" + (i + 1), quest.RequiredItemId[i]);

                for (var i = 0; i < quest.RequiredItemCount.Length; i++)
                    row.AddValue("RequiredItemCount" + (i + 1), quest.RequiredItemCount[i]);

                row.AddValue("RequiredSpell", quest.RequiredSpell);

                for (var i = 0; i < quest.ObjectiveText.Length; i++)
                    row.AddValue("ObjectiveText" + (i + 1), quest.ObjectiveText[i]);

                for (var i = 0; i < quest.RewardCurrencyId.Length; i++)
                    row.AddValue("RewardCurrencyId" + (i + 1), quest.RewardCurrencyId[i]);

                for (var i = 0; i < quest.RewardCurrencyCount.Length; i++)
                    row.AddValue("RewardCurrencyCount" + (i + 1), quest.RewardCurrencyCount[i]);

                for (var i = 0; i < quest.RequiredCurrencyId.Length; i++)
                    row.AddValue("RequiredCurrencyId" + (i + 1), quest.RequiredCurrencyId[i]);

                for (var i = 0; i < quest.RequiredCurrencyCount.Length; i++)
                    row.AddValue("RequiredCurrencyCount" + (i + 1), quest.RequiredCurrencyCount[i]);

                row.AddValue("QuestGiverTextWindow", quest.QuestGiverTextWindow);
                row.AddValue("QuestGiverTargetName", quest.QuestGiverTargetName);
                row.AddValue("QuestTurnTextWindow", quest.QuestTurnTextWindow);
                row.AddValue("QuestTurnTargetName", quest.QuestTurnTargetName);
                row.AddValue("SoundAccept", quest.SoundAccept);
                row.AddValue("SoundTurnIn", quest.SoundTurnIn);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }

        public static string Npc()
        {
            if (Storage.UnitTemplates.IsEmpty())
                return String.Empty;

            var entries = Storage.UnitTemplates.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, UnitTemplate>(entries);

            return SQLUtil.CompareDicts(Storage.UnitTemplates, templatesDb, StoreNameType.Unit);
        }

        public static string GameObject()
        {
            if (Storage.GameObjectTemplates.IsEmpty())
                return String.Empty;

            var entries = Storage.GameObjectTemplates.Keys();
            var tempatesDb = SQLDatabase.GetDict<uint, GameObjectTemplate>(entries);

            return SQLUtil.CompareDicts(Storage.GameObjectTemplates, tempatesDb, StoreNameType.GameObject);
        }

        public static string PageText()
        {
            if (Storage.PageTexts.IsEmpty())
                return String.Empty;

            var entries = Storage.PageTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, PageText>(entries);

            return SQLUtil.CompareDicts(Storage.PageTexts, templatesDb, StoreNameType.PageText);
        }

        public static string NpcText()
        {
            if (Storage.NpcTexts.IsEmpty())
                return String.Empty;

            // Not TDB structure
            const string tableName = "npc_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcTextPair in Storage.NpcTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();
                var npcText = npcTextPair.Value.Item1;

                row.AddValue("Id", npcTextPair.Key);

                for (var i = 0; i < npcText.Probabilities.Length; i++)
                    row.AddValue("Probability" + (i + 1), npcText.Probabilities[i]);

                for (var i = 0; i < npcText.Texts1.Length; i++)
                    row.AddValue("Text1_" + (i + 1), npcText.Texts1[i]);

                for (var i = 0; i < npcText.Texts2.Length; i++)
                    row.AddValue("Text2_" + (i + 1), npcText.Texts2[i]);

                for (var i = 0; i < npcText.Languages.Length; i++)
                    row.AddValue("Language" + (i + 1), npcText.Languages[i]);

                for (var i = 0; i < npcText.EmoteDelays[0].Length; i++)
                    for (var j = 0; j < npcText.EmoteDelays[1].Length; j++)
                        row.AddValue("EmoteDelay" + (i + 1) + "_" + (j + 1), npcText.EmoteDelays[i][j]);

                for (var i = 0; i < npcText.EmoteIds[0].Length; i++)
                    for (var j = 0; j < npcText.EmoteIds[1].Length; j++)
                        row.AddValue("EmoteId" + (i + 1) + "_" + (j + 1), npcText.EmoteDelays[i][j]);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }
    }
}
