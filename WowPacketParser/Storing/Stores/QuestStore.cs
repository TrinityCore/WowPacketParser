using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Storing.Stores
{
    public sealed class QuestStore
    {
        public string GetCommand(int entry, QuestMethod method, int level, int minLevel,
            QuestSort sort, QuestType type, int players, int[] factId, int[] factRep,
            int nextQuest, int xpId, int rewReqMoney, int rewMoneyMaxLvl, int rewSpell,
            int rewSpellCast, int rewHonor, float rewHonorBonus, int srcItemId,
            QuestFlag flags, int titleId, int reqPlayerKills, int bonusTalents,
            int bonusArenaPoints, int bonusUnk, int[] rewItemId, int[] rewItemCnt,
            int[] rewChoiceItemId, int[] rewChoiceItemCnt, int[] rewFactionId,
            int[] rewFactionIdx, int[] rewRepOverride, int pointMap, float pointX,
            float pointY, int pointOpt, string title, string objectives, string details,
            string endText, string returnText, KeyValuePair<int, bool>[] reqId, int[] reqCnt,
            int[] srcId, int[] srcCnt, int[] reqItemId, int[] reqItemCnt,
            string[] objectiveText)
        {
            var builder = new CommandBuilder("quest_template");

            builder.AddColumnValue("entry", entry);
            builder.AddColumnValue("Method", (int)method);
            builder.AddColumnValue("ZoneOrSort", (int)sort);
            builder.AddColumnValue("SkillOrClass", 0);
            builder.AddColumnValue("MinLevel", minLevel);
            builder.AddColumnValue("QuestLevel", level);
            builder.AddColumnValue("Type", (int)type);
            builder.AddColumnValue("RequiredRaces", 0);
            builder.AddColumnValue("RequiredSkillValue", 0);
            builder.AddColumnValue("RepObjectiveFaction", factId[0]);
            builder.AddColumnValue("RepObjectiveValue", factRep[0]);

            if (Store.Format == SqlFormat.Trinity)
            {
                builder.AddColumnValue("RepObjectiveFaction2", factId[1]);
                builder.AddColumnValue("RepObjectiveValue2", factRep[1]);
            }

            builder.AddColumnValue("RequiredMinRepFaction", 0);
            builder.AddColumnValue("RequiredMinRepValue", 0);
            builder.AddColumnValue("RequiredMaxRepFaction", 0);
            builder.AddColumnValue("RequiredMaxRepValue", 0);
            builder.AddColumnValue("SuggestedPlayers", players);
            builder.AddColumnValue("LimitTime", 0);
            builder.AddColumnValue("QuestFlags", (int)flags);
            builder.AddColumnValue("SpecialFlags", 0);
            builder.AddColumnValue("CharTitleId", titleId);
            builder.AddColumnValue("PlayersSlain", reqPlayerKills);
            builder.AddColumnValue("BonusTalents", bonusTalents);

            if (Store.Format == SqlFormat.Trinity)
                builder.AddColumnValue("BonusArenaPoints", bonusArenaPoints);

            builder.AddColumnValue("PrevQuestId", 0);
            builder.AddColumnValue("NextQuestId", 0);
            builder.AddColumnValue("ExclusiveGroup", 0);
            builder.AddColumnValue("NextQuestInChain", nextQuest);
            builder.AddColumnValue("RewXPId", xpId);
            builder.AddColumnValue("SrcItemId", srcItemId);
            builder.AddColumnValue("SrcItemCount", 0);
            builder.AddColumnValue("SrcSpell", 0);
            builder.AddColumnValue("Title", title);
            builder.AddColumnValue("Details", details);
            builder.AddColumnValue("Objectives", objectives);
            builder.AddColumnValue("OfferRewardText", string.Empty);
            builder.AddColumnValue("RequestItemsText", string.Empty);
            builder.AddColumnValue("EndText", endText);
            builder.AddColumnValue("CompletedText", returnText);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ObjectiveText" + (i + 1), objectiveText[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ReqItemId" + (i + 1), reqItemId[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ReqItemCount" + (i + 1), reqItemCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ReqSourceId" + (i + 1), srcId[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ReqSourceCount" + (i + 1), srcCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ReqCreatureOrGOId" + (i + 1), reqId[i].Value ?
                    -reqId[i].Key : reqId[i].Key);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ReqCreatureOrGOCount" + (i + 1), reqCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("ReqSpellCast" + (i + 1), 0);

            for (var i = 0; i < 6; i++)
                builder.AddColumnValue("RewChoiceItemId" + (i + 1), rewChoiceItemId[i]);

            for (var i = 0; i < 6; i++)
                builder.AddColumnValue("RewChoiceItemCount" + (i + 1), rewChoiceItemCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("RewItemId" + (i + 1), rewItemId[i]);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("RewItemCount" + (i + 1), rewItemCnt[i]);

            for (var i = 0; i < 5; i++)
                builder.AddColumnValue("RewRepFaction" + (i + 1), rewFactionId[i]);

            for (var i = 0; i < 5; i++)
                builder.AddColumnValue("RewRepValueId" + (i + 1), rewRepOverride[i]);

            for (var i = 0; i < 5; i++)
                builder.AddColumnValue("RewRepValue" + (i + 1), 0);

            builder.AddColumnValue("RewHonorAddition", rewHonor);
            builder.AddColumnValue("RewHonorMultiplier", rewHonorBonus);

            if (Store.Format == SqlFormat.Trinity)
                builder.AddColumnValue("unk0", bonusUnk);

            builder.AddColumnValue("RewOrReqMoney", rewReqMoney);
            builder.AddColumnValue("RewMoneyMaxLevel", rewMoneyMaxLvl);
            builder.AddColumnValue("RewSpell", rewSpell);
            builder.AddColumnValue("RewSpellCast", rewSpellCast);
            builder.AddColumnValue("RewMailTemplateId", 0);
            builder.AddColumnValue("RewMailDelaySecs", 0);
            builder.AddColumnValue("PointMapId", pointMap);
            builder.AddColumnValue("PointX", pointX);
            builder.AddColumnValue("PointY", pointY);
            builder.AddColumnValue("PointOpt", pointOpt);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("DetailsEmote" + (i + 1), 0);

            builder.AddColumnValue("IncompleteEmote", 0);
            builder.AddColumnValue("CompleteEmote", 0);

            for (var i = 0; i < 4; i++)
                builder.AddColumnValue("OfferRewardEmote" + (i + 1), 0);

            builder.AddColumnValue("StartScript", 0);
            builder.AddColumnValue("CompleteScript", 0);

            return builder.BuildInsert(true);
        }
    }
}
