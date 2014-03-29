using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_7_18019.Enums;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResp(Packet packet)
        {
            packet.ReadUInt32("QuestID");

            bool QuestIsntAutoComplete = packet.ReadBit("Quest Isn't AutoComplete");

            if (QuestIsntAutoComplete)
            {
                var QuestEndTextLen = packet.ReadBits(9);
                var QuestTitleLen = packet.ReadBits(9);
                var QuestGiverTextWindowLen = packet.ReadBits(10);
                var QuestTurnTargetNameLen = packet.ReadBits(8);
                var QuestObjectivesLen = packet.ReadBits(12);
                var QuestGiverTargetNameLen = packet.ReadBits(8);
                var QuestDetailsLen = packet.ReadBits(12);
                var QuestTurnTextWindowLen = packet.ReadBits(10);
                var QuestCompletedTextLen = packet.ReadBits(11);
                var QuestObjectivesCount = packet.ReadBits("Objectives Count", 19);
                uint[][] ObjectivesCounts = new uint[QuestObjectivesCount][];

                for (var i = 0; i < QuestObjectivesCount; ++i)
                {
                    ObjectivesCounts[i][0] = packet.ReadBits(22);
                    ObjectivesCounts[i][1] = packet.ReadBits(8);
                }

                packet.ResetBitReader();

                for (var i = 0; i < QuestObjectivesCount; ++i)
                {
                    packet.ReadUInt32("Required NPCorGO ID", i);
                    packet.ReadWoWString("Objective Text", ObjectivesCounts[i][0], i);
                    packet.ReadByte("Unk Byte4", i);
                    packet.ReadByte("Unk Byte5", i);
                    packet.ReadUInt32("Required SourceItem ID", i);

                    for (var j = 0; j < ObjectivesCounts[i][1]; j++)
                    {
                        packet.ReadUInt32("Unk Looped DWROD", j);
                    }

                    packet.ReadUInt32("Required SourceItem Count", i);
                    packet.ReadUInt32("Required NPCorGO Count", i);
                }

                packet.ReadUInt32("dword2E3C");
                packet.ReadUInt32("dword2E50");
                packet.ReadUInt32("Rewarded Spell");
                packet.ReadSingle("Point X");
                packet.ReadUInt32("RepObjectiveValue2");
                packet.ReadUInt32("dword2E68");
                packet.ReadSingle("RewardHonorMultiplier");
                packet.ReadUInt32("dword2E2C");
                packet.ReadUInt32("RepObjectiveValue");
                packet.ReadUInt32("dword2E94");
                packet.ReadUInt32("dword2E34");
                packet.ReadWoWString("QuestGiverTargetName", QuestGiverTargetNameLen);
                packet.ReadUInt32("dword2E84");
                packet.ReadUInt32("NextQuestInChain");
                packet.ReadUInt32("dword2E58");
                packet.ReadUInt32("dword2E40");
                packet.ReadUInt32("ZoneOrSort");
                packet.ReadUInt32("BonusTalents");
                packet.ReadWoWString("QuestCompletedText", QuestCompletedTextLen);
                packet.ReadWoWString("QuestEndText", QuestEndTextLen);
                packet.ReadUInt32("dword2E08");
                packet.ReadUInt32("dword2E04");
                packet.ReadUInt32("RewardMoney");
                packet.ReadUInt32("dword2E60");
                packet.ReadUInt32("dword2E0C");
                packet.ReadSingle("Point Y");
                packet.ReadUInt32("RewardMoneyMaxLvl");
                packet.ReadUInt32("PointOpt");
                packet.ReadWoWString("QuestGiverTextWindow", QuestGiverTextWindowLen);

                for (var i = 0; i < 4; ++i)
                {
                    packet.ReadUInt32("1st dword", i);
                    packet.ReadUInt32("2nd dword", i);
                }

                packet.ReadWoWString("QuestObjectives", QuestObjectivesLen);
                packet.ReadUInt32("dword2E4C");
                packet.ReadUInt32("dword2E54");
                packet.ReadUInt32("RewardSpellCasted");

                for (var i = 0; i < 5; ++i)
                {
                    packet.ReadUInt32("2nd dword", i);
                    packet.ReadUInt32("3rd dword", i);
                    packet.ReadUInt32("1st dword", i);  
                }

                packet.ReadWoWString("QuestTurnTargetName", QuestTurnTargetNameLen);
                packet.ReadUInt32("dword2E78");
                packet.ReadUInt32("RewSkillID");
                packet.ReadUInt32("CharTittleID");
                packet.ReadUInt32("dword2E20");
                packet.ReadUInt32("RepObjectiveFaction");
                packet.ReadUInt32("RewardHonorAddition");
                packet.ReadUInt32("dword2E8C");
                packet.ReadUInt32("XPId");
                packet.ReadUInt32("dword2E48");
                packet.ReadWoWString("QuestDetails", QuestDetailsLen);
                packet.ReadUInt32("RewSkillPoints");
                packet.ReadUInt32("QuestLevel");
                packet.ReadUInt32("dword2E7C");
                packet.ReadUInt32("SoundAccept");
                packet.ReadUInt32("dword2E74");
                packet.ReadUInt32("MinimapTargetMask");
                packet.ReadUInt32("SuggestedPlayers");
                packet.ReadUInt32("dword2E44");
                packet.ReadUInt32("PlayersSlain");
                packet.ReadUInt32("Type");
                packet.ReadUInt32("RequiredSpellID");
                packet.ReadUInt32("dword2E90");
                packet.ReadUInt32("SourceItemID");
                packet.ReadUInt32("dword2E80");
                packet.ReadUInt32("dword2E30");
                packet.ReadUInt32("dword2E70");
                packet.ReadUInt32("RepObjectiveFaction2");
                packet.ReadUInt32("PointMapId");
                packet.ReadWoWString("QuestTitle", QuestTitleLen);
                packet.ReadUInt32("dword2E28");
                packet.ReadUInt32("dword2E98");
                packet.ReadUInt32("dword2E5C");
                packet.ReadUInt32("SoundTurnIn");
                packet.ReadWoWString("QuestTurnTextWindow", QuestTurnTextWindowLen);
                packet.ReadUInt32("dword2E9C");
                packet.ReadUInt32("dword2E24");
                packet.ReadUInt32("dword2E38");
                packet.ReadUInt32("dword2E64");
                packet.ReadUInt32("QuestFlags");
                packet.ReadUInt32("RewArenaPoints");
                packet.ReadUInt32("dword2E6C");
                packet.ReadUInt32("MinLevel");
                packet.ReadUInt32("dword2E88");
            }
        }   
    }
}
