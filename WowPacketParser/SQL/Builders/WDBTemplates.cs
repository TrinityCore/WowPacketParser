using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    public static class WDBTemplates
    {
        public static string Quest()
        {
            if (Storage.QuestTemplates.IsEmpty)
                return String.Empty;

            const string tableName = "quest_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var quest in Storage.QuestTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", quest.Key);
                row.AddValue("Method", quest.Value.Method);
                row.AddValue("Level", quest.Value.Level);
                row.AddValue("MinLevel", quest.Value.MinLevel);
                row.AddValue("ZoneOrSort", quest.Value.ZoneOrSort);
                row.AddValue("Type", quest.Value.Type);
                row.AddValue("SuggestedPlayers", quest.Value.SuggestedPlayers);

                for (var i = 0; i < quest.Value.RequiredFactionId.Length; i++)
                    row.AddValue("RequiredFactionId" + (i + 1), quest.Value.RequiredFactionId[i]);

                for (var i = 0; i < quest.Value.RequiredFactionId.Length; i++)
                    row.AddValue("RequiredFactionValue" + (i + 1), quest.Value.RequiredFactionValue[i]);

                row.AddValue("NextQuestIdChain", quest.Value.NextQuestIdChain);
                row.AddValue("RewardXPId", quest.Value.RewardXPId);
                row.AddValue("RewardOrRequiredMoney", quest.Value.RewardOrRequiredMoney);
                row.AddValue("RewardMoneyMaxLevel", quest.Value.RewardMoneyMaxLevel);
                row.AddValue("RewardSpell", quest.Value.RewardSpell);
                row.AddValue("RewardSpellCast", quest.Value.RewardSpellCast);
                row.AddValue("RewardHonor", quest.Value.RewardHonor);
                row.AddValue("RewardHonorMultiplier", quest.Value.RewardHonorMultiplier);
                row.AddValue("SourceItemId", quest.Value.SourceItemId);
                row.AddValue("Flags", quest.Value.Flags, true);
                row.AddValue("MinimapTargetMark", quest.Value.MinimapTargetMark);
                row.AddValue("RewardTitleId", quest.Value.RewardTitleId);
                row.AddValue("RequiredPlayerKills", quest.Value.RequiredPlayerKills);
                row.AddValue("RewardTalents", quest.Value.RewardTalents);
                row.AddValue("RewardArenaPoints", quest.Value.RewardArenaPoints);
                row.AddValue("RewardSkillId", quest.Value.RewardSkillId);
                row.AddValue("RewardSkillPoints", quest.Value.RewardSkillPoints);
                row.AddValue("RewardReputationMask", quest.Value.RewardReputationMask);
                row.AddValue("QuestGiverPortrait", quest.Value.QuestGiverPortrait);
                row.AddValue("QuestTurnInPortrait", quest.Value.QuestTurnInPortrait);

                for (var i = 0; i < quest.Value.RewardItemId.Length; i++)
                    row.AddValue("RewardItemId" + (i + 1), quest.Value.RewardItemId[i]);

                for (var i = 0; i < quest.Value.RewardItemCount.Length; i++)
                    row.AddValue("RewardItemCount" + (i + 1), quest.Value.RewardItemCount[i]);

                for (var i = 0; i < quest.Value.RewardChoiceItemId.Length; i++)
                    row.AddValue("RewardChoiceItemId" + (i + 1), quest.Value.RewardChoiceItemId[i]);

                for (var i = 0; i < quest.Value.RewardChoiceItemCount.Length; i++)
                    row.AddValue("RewardChoiceItemCount" + (i + 1), quest.Value.RewardChoiceItemCount[i]);

                for (var i = 0; i < quest.Value.RewardFactionId.Length; i++)
                    row.AddValue("RewardFactionId" + (i + 1), quest.Value.RewardFactionId[i]);

                for (var i = 0; i < quest.Value.RewardFactionValueId.Length; i++)
                    row.AddValue("RewardFactionValueId" + (i + 1), quest.Value.RewardFactionValueId[i]);

                for (var i = 0; i < quest.Value.RewardFactionValueIdOverride.Length; i++)
                    row.AddValue("RewardFactionValueIdOverride" + (i + 1), quest.Value.RewardFactionValueIdOverride[i]);

                row.AddValue("PointMapId", quest.Value.PointMapId);
                row.AddValue("PointX", quest.Value.PointX);
                row.AddValue("PointY", quest.Value.PointY);
                row.AddValue("PointOption", quest.Value.PointOption);
                row.AddValue("Title", quest.Value.Title);
                row.AddValue("Objectives", quest.Value.Objectives);
                row.AddValue("Details", quest.Value.Details);
                row.AddValue("EndText", quest.Value.EndText);
                row.AddValue("CompletedText", quest.Value.CompletedText);

                for (var i = 0; i < quest.Value.RequiredNpcOrGo.Length; i++)
                    row.AddValue("RequiredNpcOrGo" + (i + 1), quest.Value.RequiredNpcOrGo[i]);

                for (var i = 0; i < quest.Value.RequiredNpcOrGoCount.Length; i++)
                    row.AddValue("RequiredNpcOrGoCount" + (i + 1), quest.Value.RequiredNpcOrGoCount[i]);

                for (var i = 0; i < quest.Value.RequiredSourceItemId.Length; i++)
                    row.AddValue("RequiredSourceItemId" + (i + 1), quest.Value.RequiredSourceItemId[i]);

                for (var i = 0; i < quest.Value.RequiredSourceItemCount.Length; i++)
                    row.AddValue("RequiredSourceItemCount" + (i + 1), quest.Value.RequiredSourceItemCount[i]);

                for (var i = 0; i < quest.Value.RequiredItemId.Length; i++)
                    row.AddValue("RequiredItemId" + (i + 1), quest.Value.RequiredItemId[i]);

                for (var i = 0; i < quest.Value.RequiredItemCount.Length; i++)
                    row.AddValue("RequiredItemCount" + (i + 1), quest.Value.RequiredItemCount[i]);

                row.AddValue("RequiredSpell", quest.Value.RequiredSpell);

                for (var i = 0; i < quest.Value.ObjectiveText.Length; i++)
                    row.AddValue("ObjectiveText" + (i + 1), quest.Value.ObjectiveText[i]);

                for (var i = 0; i < quest.Value.RewardCurrencyId.Length; i++)
                    row.AddValue("RewardCurrencyId" + (i + 1), quest.Value.RewardCurrencyId[i]);

                for (var i = 0; i < quest.Value.RewardCurrencyCount.Length; i++)
                    row.AddValue("RewardCurrencyCount" + (i + 1), quest.Value.RewardCurrencyCount[i]);

                for (var i = 0; i < quest.Value.RequiredCurrencyId.Length; i++)
                    row.AddValue("RequiredCurrencyId" + (i + 1), quest.Value.RequiredCurrencyId[i]);

                for (var i = 0; i < quest.Value.RequiredCurrencyCount.Length; i++)
                    row.AddValue("RequiredCurrencyCount" + (i + 1), quest.Value.RequiredCurrencyCount[i]);

                row.AddValue("QuestGiverTextWindow", quest.Value.QuestGiverTextWindow);
                row.AddValue("QuestGiverTargetName", quest.Value.QuestGiverTargetName);
                row.AddValue("QuestTurnTextWindow", quest.Value.QuestTurnTextWindow);
                row.AddValue("QuestTurnTargetName", quest.Value.QuestTurnTargetName);
                row.AddValue("SoundAccept", quest.Value.SoundAccept);
                row.AddValue("SoundTurnIn", quest.Value.SoundTurnIn);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }

        public static string Npc()
        {
            if (Storage.UnitTemplates.IsEmpty)
                return String.Empty;

            var entries = Storage.UnitTemplates.Keys.ToList();
            var templatesDb = SQLDatabase.GetDict<uint, UnitTemplate>(entries);
            var templates = Storage.UnitTemplates;

            return SQLUtil.CompareDicts(templates, templatesDb, StoreNameType.Unit);
        }

        public static string GameObject()
        {
            if (Storage.GameObjectTemplates.IsEmpty)
                return String.Empty;

            // Not TDB structure
            const string tableName = "gameobject_template";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var goTemplate in Storage.GameObjectTemplates)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", goTemplate.Key);
                row.AddValue("Type", goTemplate.Value.Type);
                row.AddValue("DisplayId", goTemplate.Value.DisplayId);
                row.AddValue("Name", goTemplate.Value.Name);
                row.AddValue("IconName", goTemplate.Value.IconName);
                row.AddValue("CastCaption", goTemplate.Value.CastCaption);
                row.AddValue("UnkString", goTemplate.Value.UnkString);

                for (var i = 0; i < goTemplate.Value.Data.Length; i++)
                    row.AddValue("Data" + (i + 1), goTemplate.Value.Data[i]);

                row.AddValue("Size", goTemplate.Value.Size);

                for (var i = 0; i < goTemplate.Value.QuestItems.Length; i++)
                    row.AddValue("QuestItem" + (i + 1), goTemplate.Value.QuestItems[i]);

                row.AddValue("UnknownUInt", goTemplate.Value.UnknownUInt);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }

        public static string PageText()
        {
            if (Storage.PageTexts.IsEmpty)
                return String.Empty;

            const string tableName = "page_Text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var pageText in Storage.PageTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("entry", pageText.Key);
                row.AddValue("text", pageText.Value.Text);
                row.AddValue("next_page", pageText.Value.NextPageId);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }

        public static string NpcText()
        {
            if (Storage.NpcTexts.IsEmpty)
                return String.Empty;

            // Not TDB structure
            const string tableName = "npc_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (var npcText in Storage.NpcTexts)
            {
                var row = new QueryBuilder.SQLInsertRow();

                row.AddValue("Id", npcText.Key);

                for (var i = 0; i < npcText.Value.Probabilities.Length; i++)
                    row.AddValue("Probability" + (i + 1), npcText.Value.Probabilities[i]);

                for (var i = 0; i < npcText.Value.Texts1.Length; i++)
                    row.AddValue("Text1_" + (i + 1), npcText.Value.Texts1[i]);

                for (var i = 0; i < npcText.Value.Texts2.Length; i++)
                    row.AddValue("Text2_" + (i + 1), npcText.Value.Texts2[i]);

                for (var i = 0; i < npcText.Value.Languages.Length; i++)
                    row.AddValue("Language" + (i + 1), npcText.Value.Languages[i]);

                for (var i = 0; i < npcText.Value.EmoteDelays[0].Length; i++)
                    for (var j = 0; j < npcText.Value.EmoteDelays[1].Length; j++)
                        row.AddValue("EmoteDelay" + (i + 1) + "_" + (j + 1), npcText.Value.EmoteDelays[i][j]);

                for (var i = 0; i < npcText.Value.EmoteIds[0].Length; i++)
                    for (var j = 0; j < npcText.Value.EmoteIds[1].Length; j++)
                        row.AddValue("EmoteId" + (i + 1) + "_" + (j + 1), npcText.Value.EmoteDelays[i][j]);

                rows.Add(row);
            }

            return new QueryBuilder.SQLInsert(tableName, rows).Build();
        }
    }
}
