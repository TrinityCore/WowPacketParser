using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.SQL.Builder;

namespace WowPacketParser.SQL.Stores
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
            var builder = new SQLInsert();
            builder.Table = "quest_template";

            builder.AddValue("entry", entry);
            builder.AddValue("Method", (int)method);
            builder.AddValue("ZoneOrSort", (int)sort);
            builder.AddValue("SkillOrClass", 0);
            builder.AddValue("MinLevel", minLevel);
            builder.AddValue("QuestLevel", level);
            builder.AddValue("Type", (int)type);
            builder.AddValue("RequiredRaces", 0);
            builder.AddValue("RequiredSkillValue", 0);
            builder.AddValue("RepObjectiveFaction", factId[0]);
            builder.AddValue("RepObjectiveValue", factRep[0]);
            builder.AddValue("RepObjectiveFaction2", factId[1]);
            builder.AddValue("RepObjectiveValue2", factRep[1]);
            builder.AddValue("RequiredMinRepFaction", 0);
            builder.AddValue("RequiredMinRepValue", 0);
            builder.AddValue("RequiredMaxRepFaction", 0);
            builder.AddValue("RequiredMaxRepValue", 0);
            builder.AddValue("SuggestedPlayers", players);
            builder.AddValue("LimitTime", 0);
            builder.AddValue("QuestFlags", (int)flags);
            builder.AddValue("SpecialFlags", 0);
            builder.AddValue("CharTitleId", titleId);
            builder.AddValue("PlayersSlain", reqPlayerKills);
            builder.AddValue("BonusTalents", bonusTalents);
            builder.AddValue("BonusArenaPoints", bonusArenaPoints);
            builder.AddValue("PrevQuestId", 0);
            builder.AddValue("NextQuestId", 0);
            builder.AddValue("ExclusiveGroup", 0);
            builder.AddValue("NextQuestInChain", nextQuest);
            builder.AddValue("RewXPId", xpId);
            builder.AddValue("SrcItemId", srcItemId);
            builder.AddValue("SrcItemCount", 0);
            builder.AddValue("SrcSpell", 0);
            builder.AddValue("Title", title);
            builder.AddValue("Details", details);
            builder.AddValue("Objectives", objectives);
            builder.AddValue("OfferRewardText", string.Empty);
            builder.AddValue("RequestItemsText", string.Empty);
            builder.AddValue("EndText", endText);
            builder.AddValue("CompletedText", returnText);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ObjectiveText" + (i + 1), objectiveText[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ReqItemId" + (i + 1), reqItemId[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ReqItemCount" + (i + 1), reqItemCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ReqSourceId" + (i + 1), srcId[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ReqSourceCount" + (i + 1), srcCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ReqCreatureOrGOId" + (i + 1), reqId[i].Value ?
                    -reqId[i].Key : reqId[i].Key);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ReqCreatureOrGOCount" + (i + 1), reqCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("ReqSpellCast" + (i + 1), 0);

            for (var i = 0; i < 6; i++)
                builder.AddValue("RewChoiceItemId" + (i + 1), rewChoiceItemId[i]);

            for (var i = 0; i < 6; i++)
                builder.AddValue("RewChoiceItemCount" + (i + 1), rewChoiceItemCnt[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("RewItemId" + (i + 1), rewItemId[i]);

            for (var i = 0; i < 4; i++)
                builder.AddValue("RewItemCount" + (i + 1), rewItemCnt[i]);

            for (var i = 0; i < 5; i++)
                builder.AddValue("RewRepFaction" + (i + 1), rewFactionId[i]);

            for (var i = 0; i < 5; i++)
                builder.AddValue("RewRepValueId" + (i + 1), rewRepOverride[i]);

            for (var i = 0; i < 5; i++)
                builder.AddValue("RewRepValue" + (i + 1), 0);

            builder.AddValue("RewHonorAddition", rewHonor);
            builder.AddValue("RewHonorMultiplier", rewHonorBonus);
            builder.AddValue("unk0", bonusUnk);
            builder.AddValue("RewOrReqMoney", rewReqMoney);
            builder.AddValue("RewMoneyMaxLevel", rewMoneyMaxLvl);
            builder.AddValue("RewSpell", rewSpell);
            builder.AddValue("RewSpellCast", rewSpellCast);
            builder.AddValue("RewMailTemplateId", 0);
            builder.AddValue("RewMailDelaySecs", 0);
            builder.AddValue("PointMapId", pointMap);
            builder.AddValue("PointX", pointX);
            builder.AddValue("PointY", pointY);
            builder.AddValue("PointOpt", pointOpt);

            for (var i = 0; i < 4; i++)
                builder.AddValue("DetailsEmote" + (i + 1), 0);

            builder.AddValue("IncompleteEmote", 0);
            builder.AddValue("CompleteEmote", 0);

            for (var i = 0; i < 4; i++)
                builder.AddValue("OfferRewardEmote" + (i + 1), 0);

            builder.AddValue("StartScript", 0);
            builder.AddValue("CompleteScript", 0);

            builder.AddWhere("entry", entry);

            return builder.Build();
        }
    }
}
