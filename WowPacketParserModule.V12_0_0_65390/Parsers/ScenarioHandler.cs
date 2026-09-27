using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class ScenarioHandler
    {
        [Parser(Opcode.SMSG_SCENARIO_STATE, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleScenarioState(Packet packet)
        {
            packet.ReadPackedGuid128("ScenarioGUID");
            packet.ReadInt32("ScenarioID");
            packet.ReadInt32("CurrentStep");
            packet.ReadInt16<DifficultyId>("DifficultyID");
            packet.ReadUInt32("WaveCurrent");
            packet.ReadUInt32("WaveMax");
            packet.ReadUInt32("TimerDuration");
            var criteriaProgressCount = packet.ReadUInt32("CriteriaProgressCount");
            var bonusObjectiveDataCount = packet.ReadUInt32("BonusObjectiveDataCount");
            var pickedStepsCount = packet.ReadUInt32("PickedStepsCount");
            var spellsCount = packet.ReadUInt32("SpellsCount");
            packet.ReadPackedGuid128("PlayerGUID");

            for (var i = 0u; i < criteriaProgressCount; i++)
                V10_0_0_46181.Parsers.AchievementHandler.ReadCriteriaProgress(packet, "CriteriaProgress", i);

            for (var i = 0u; i < bonusObjectiveDataCount; i++)
                V7_0_3_22248.Parsers.ScenarioHandler.ReadBonusObjectiveData(packet, "BonusObjectiveData", i);

            for (var i = 0u; i < pickedStepsCount; i++)
                packet.ReadUInt32("PickedStep", i);

            for (var i = 0u; i < spellsCount; i++)
                V7_0_3_22248.Parsers.ScenarioHandler.ReadScenarioSpellUpdate(packet, "ScenarioSpellUpdate", i);

            packet.ResetBitReader();
            packet.ReadBit("ScenarioComplete");
        }
    }
}
