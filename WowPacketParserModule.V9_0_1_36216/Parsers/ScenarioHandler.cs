using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.SMSG_SCENARIO_STATE)]
        public static void HandleScenarioState(Packet packet)
        {
            packet.ReadInt32("ScenarioID");
            packet.ReadInt32("CurrentStep");
            packet.ReadInt32<DifficultyId>("DifficultyID");
            packet.ReadInt32("WaveCurrent");
            packet.ReadInt32("WaveMax");
            packet.ReadInt32("TimerDuration");
            var criteriaProgressCount = packet.ReadInt32("CriteriaProgressCount");
            var bonusObjectiveDataCount = packet.ReadInt32("BonusObjectiveDataCount");
            var pickedStepsCount = packet.ReadUInt32("PickedStepsCount");
            var spellsCount = packet.ReadUInt32("SpellsCount");

            for (int i = 0; i < pickedStepsCount; i++)
                packet.ReadUInt32("PickedStep", i);

            packet.ResetBitReader();
            packet.ReadBit("ScenarioComplete");

            for (int i = 0; i < criteriaProgressCount; i++)
                AchievementHandler.ReadCriteriaProgress(packet, "CriteriaProgress", i);

            for (int i = 0; i < bonusObjectiveDataCount; i++)
                V7_0_3_22248.Parsers.ScenarioHandler.ReadBonusObjectiveData(packet, "BonusObjectiveData", i);

            for (int i = 0; i < spellsCount; i++)
                V7_0_3_22248.Parsers.ScenarioHandler.ReadScenarioSpellUpdate(packet, "ScenarioSpellUpdate", i);
        }
    }
}
