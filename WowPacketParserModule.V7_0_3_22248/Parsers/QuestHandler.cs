using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class QuestHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            packet.ReadInt32("Entry");

            Bit hasData = packet.ReadBit("Has Data");
            if (!hasData)
                return; // nothing to do

            var id = packet.ReadEntry("Quest ID");

            QuestTemplate quest = new QuestTemplate
            {
                ID = (uint)id.Key
            };

            quest.QuestType = packet.ReadInt32E<QuestType>("QuestType");
            quest.QuestLevel = packet.ReadInt32("QuestLevel");
            quest.QuestPackageID = packet.ReadUInt32("QuestPackageID");
            quest.MinLevel = packet.ReadInt32("QuestMinLevel");
            quest.QuestSortID = (QuestSort)packet.ReadUInt32("QuestSortID");
            quest.QuestInfoID = packet.ReadInt32E<QuestInfo>("QuestInfoID");
            quest.SuggestedGroupNum = packet.ReadUInt32("SuggestedGroupNum");
            quest.RewardNextQuest = packet.ReadUInt32("RewardNextQuest");
            quest.RewardXPDifficulty = packet.ReadUInt32("RewardXPDifficulty");

            quest.RewardXPMultiplier = packet.ReadSingle("RewardXPMultiplier");

            quest.RewardMoney = packet.ReadInt32("RewardMoney");
            quest.RewardMoneyDifficulty = packet.ReadUInt32("RewardMoneyDifficulty");

            quest.RewardMoneyMultiplier = packet.ReadSingle("RewardMoneyMultiplier");

            quest.RewardBonusMoney = packet.ReadUInt32("RewardBonusMoney");

            quest.RewardDisplaySpellLegion = new uint?[3];
            for (int i = 0; i < 3; ++i)
                quest.RewardDisplaySpellLegion[i] = packet.ReadUInt32("RewardDisplaySpell", i);

            quest.RewardSpellWod = packet.ReadUInt32("RewardSpell");
            quest.RewardHonorWod = packet.ReadUInt32("RewardHonor");

            quest.RewardKillHonor = packet.ReadSingle("RewardKillHonor");

            quest.RewardArtifactXPDifficulty = packet.ReadUInt32("RewardArtifactXPDifficulty");
            quest.RewardArtifactXPMultiplier = packet.ReadSingle("RewardArtifactXPMultiplier");
            quest.RewardArtifactCategoryID = packet.ReadUInt32("RewardArtifactCategoryID");

            quest.StartItem = packet.ReadUInt32("StartItem");
            quest.Flags = packet.ReadUInt32E<QuestFlags>("Flags");
            quest.FlagsEx = packet.ReadUInt32E<QuestFlags2>("FlagsEx");

            quest.RewardItem = new uint?[4];
            quest.RewardAmount = new uint?[4];
            quest.ItemDrop = new uint?[4];
            quest.ItemDropQuantity = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardItem[i] = packet.ReadUInt32("RewardItems", i);
                quest.RewardAmount[i] = packet.ReadUInt32("RewardAmount", i);
                quest.ItemDrop[i] = packet.ReadUInt32("ItemDrop", i);
                quest.ItemDropQuantity[i] = packet.ReadUInt32("ItemDropQuantity", i);
            }

            quest.RewardChoiceItemID = new uint?[6];
            quest.RewardChoiceItemQuantity = new uint?[6];
            quest.RewardChoiceItemDisplayID = new uint?[6];
            for (int i = 0; i < 6; ++i) // CliQuestInfoChoiceItem
            {
                quest.RewardChoiceItemID[i] = packet.ReadUInt32("RewardChoiceItemID", i);
                quest.RewardChoiceItemQuantity[i] = packet.ReadUInt32("RewardChoiceItemQuantity", i);
                quest.RewardChoiceItemDisplayID[i] = packet.ReadUInt32("RewardChoiceItemDisplayID", i);
            }

            quest.POIContinent = packet.ReadUInt32("POIContinent");

            quest.POIx = packet.ReadSingle("POIx");
            quest.POIy = packet.ReadSingle("POIy");

            quest.POIPriorityWod = packet.ReadInt32("POIPriority");
            quest.RewardTitle = packet.ReadUInt32("RewardTitle");
            quest.RewardArenaPoints = packet.ReadUInt32("RewardArenaPoints");
            quest.RewardSkillLineID = packet.ReadUInt32("RewardSkillLineID");
            quest.RewardNumSkillUps = packet.ReadUInt32("RewardNumSkillUps");
            quest.QuestGiverPortrait = packet.ReadUInt32("PortraitGiver");
            quest.QuestTurnInPortrait = packet.ReadUInt32("PortraitTurnIn");

            quest.RewardFactionID = new uint?[5];
            quest.RewardFactionOverride = new int?[5];
            quest.RewardFactionValue = new int?[5];
            quest.RewardFactionCapIn = new int?[5];
            for (int i = 0; i < 5; ++i)
            {
                quest.RewardFactionID[i] = packet.ReadUInt32("RewardFactionID", i);
                quest.RewardFactionOverride[i] = packet.ReadInt32("RewardFactionOverride", i);
                quest.RewardFactionValue[i] = packet.ReadInt32("RewardFactionValue", i);
                quest.RewardFactionCapIn[i] = packet.ReadInt32("RewardFactionCapIn", i);
            }

            quest.RewardFactionFlags = packet.ReadUInt32("RewardFactionFlags");

            quest.RewardCurrencyID = new uint?[4];
            quest.RewardCurrencyCount = new uint?[4];
            for (int i = 0; i < 4; ++i)
            {
                quest.RewardCurrencyID[i] = packet.ReadUInt32("RewardCurrencyID");
                quest.RewardCurrencyCount[i] = packet.ReadUInt32("RewardCurrencyQty");
            }

            quest.SoundAccept = packet.ReadUInt32("AcceptedSoundKitID");
            quest.SoundTurnIn = packet.ReadUInt32("CompleteSoundKitID");
            quest.AreaGroupID = packet.ReadUInt32("AreaGroupID");
            quest.TimeAllowed = packet.ReadUInt32("TimeAllowed");
            uint int2946 = packet.ReadUInt32("CliQuestInfoObjective");
            quest.AllowableRacesWod = packet.ReadInt32("AllowableRaces");
            quest.QuestRewardID = packet.ReadInt32("QuestRewardID");

            packet.ResetBitReader();

            uint logTitleLen = packet.ReadBits(9);
            uint logDescriptionLen = packet.ReadBits(12);
            uint questDescriptionLen = packet.ReadBits(12);
            uint areaDescriptionLen = packet.ReadBits(9);
            uint questGiverTextWindowLen = packet.ReadBits(10);
            uint questGiverTargetNameLen = packet.ReadBits(8);
            uint questTurnTextWindowLen = packet.ReadBits(10);
            uint questTurnTargetNameLen = packet.ReadBits(8);
            uint questCompletionLogLen = packet.ReadBits(11);

            for (int i = 0; i < int2946; ++i)
            {
                var objectiveId = packet.ReadEntry("Id", i);

                QuestObjective questInfoObjective = new QuestObjective
                {
                    ID = (uint)objectiveId.Key,
                    QuestID = (uint)id.Key
                };
                questInfoObjective.Type = packet.ReadByteE<QuestRequirementType>("Quest Requirement Type", i);
                questInfoObjective.StorageIndex = packet.ReadSByte("StorageIndex", i);
                questInfoObjective.ObjectID = packet.ReadInt32("ObjectID", i);
                questInfoObjective.Amount = packet.ReadInt32("Amount", i);
                questInfoObjective.Flags = packet.ReadUInt32("Flags", i);
                questInfoObjective.UnkFloat = packet.ReadSingle("Float5", i);

                int visualEffectsCount = packet.ReadInt32("VisualEffects", i);
                for (int j = 0; j < visualEffectsCount; ++j)
                {
                    QuestVisualEffect questVisualEffect = new QuestVisualEffect
                    {
                        ID = questInfoObjective.ID,
                        Index = (uint) j,
                        VisualEffect = packet.ReadInt32("VisualEffectId", i, j)
                    };

                    Storage.QuestVisualEffects.Add(questVisualEffect, packet.TimeSpan);
                }

                packet.ResetBitReader();

                uint bits6 = packet.ReadBits(8);
                questInfoObjective.Description = packet.ReadWoWString("Description", bits6, i);

                if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS && questInfoObjective.Description != string.Empty)
                {
                    QuestObjectivesLocale localesQuestObjectives = new QuestObjectivesLocale
                    {
                        ID = (uint)objectiveId.Key,
                        QuestId = (uint)id.Key,
                        StorageIndex = questInfoObjective.StorageIndex,
                        Description = questInfoObjective.Description
                    };

                    Storage.LocalesQuestObjectives.Add(localesQuestObjectives, packet.TimeSpan);
                }

                Storage.QuestObjectives.Add(questInfoObjective, packet.TimeSpan);
            }

            quest.LogTitle = packet.ReadWoWString("LogTitle", logTitleLen);
            quest.LogDescription = packet.ReadWoWString("LogDescription", logDescriptionLen);
            quest.QuestDescription = packet.ReadWoWString("QuestDescription", questDescriptionLen);
            quest.AreaDescription = packet.ReadWoWString("AreaDescription", areaDescriptionLen);
            quest.QuestGiverTextWindow = packet.ReadWoWString("PortraitGiverText", questGiverTextWindowLen);
            quest.QuestGiverTargetName = packet.ReadWoWString("PortraitGiverName", questGiverTargetNameLen);
            quest.QuestTurnTextWindow = packet.ReadWoWString("PortraitTurnInText", questTurnTextWindowLen);
            quest.QuestTurnTargetName = packet.ReadWoWString("PortraitTurnInName", questTurnTargetNameLen);
            quest.QuestCompletionLog = packet.ReadWoWString("QuestCompletionLog", questCompletionLogLen);

            if (BinaryPacketReader.GetLocale() != LocaleConstant.enUS)
            {
                LocalesQuest localesQuest = new LocalesQuest
                {
                    ID = (uint)id.Key,
                    Locale = BinaryPacketReader.GetClientLocale(),
                    LogTitle            = quest.LogTitle,
                    LogDescription      = quest.LogDescription,
                    QuestDescription    = quest.QuestDescription,
                    AreaDescription     = quest.AreaDescription,
                    PortraitGiverText   = quest.QuestGiverTextWindow,
                    PortraitGiverName   = quest.QuestGiverTargetName,
                    PortraitTurnInText  = quest.QuestTurnTextWindow,
                    PortraitTurnInName  = quest.QuestTurnTargetName,
                    QuestCompletionLog  = quest.QuestCompletionLog
                };

                Storage.LocalesQuests.Add(localesQuest, packet.TimeSpan);
            }

            Storage.QuestTemplates.Add(quest, packet.TimeSpan);
        }
    }
}
