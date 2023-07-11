using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.SMSG_SCENARIO_STATE, ClientVersionBuild.V10_1_5_50232)]
        public static void HandleScenarioState(Packet packet)
        {
            packet.ReadPackedGuid128("ScenarioGUID");
            packet.ReadInt32("ScenarioID");
            packet.ReadInt32("CurrentStep");
            packet.ReadUInt32<DifficultyId>("DifficultyID");
            packet.ReadUInt32("WaveCurrent");
            packet.ReadUInt32("WaveMax");
            packet.ReadUInt32("TimerDuration");
            var criteriaProgressCount = packet.ReadUInt32("CriteriaProgressCount");
            var bonusObjectiveDataCount = packet.ReadUInt32("BonusObjectiveDataCount");
            var pickedStepsCount = packet.ReadUInt32("PickedStepsCount");
            var spellsCount = packet.ReadUInt32("SpellsCount");
            packet.ReadPackedGuid128("PlayerGUID");

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
