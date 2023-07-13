using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ScenarioHandler
    {
        public static void ReadBonusObjectiveData(Packet packet, params object[] index)
        {
            packet.ReadInt32("BonusObjectiveId");
            packet.ResetBitReader();
            packet.ReadBit("ObjectiveComplete");
        }

        public static void ReadScenarioSpellUpdate(Packet packet, params object[] index)
        {
            packet.ReadUInt32("SpellID");
            packet.ResetBitReader();
            packet.ReadBit("Usable");
        }

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
                V6_0_2_19033.Parsers.AchievementHandler.ReadCriteriaProgress(packet, "CriteriaProgress", i);

            for (int i = 0; i < bonusObjectiveDataCount; i++)
                ReadBonusObjectiveData(packet, "BonusObjectiveData", i);

            for (int i = 0; i < spellsCount; i++)
                ReadScenarioSpellUpdate(packet, "ScenarioSpellUpdate", i);
        }

        [Parser(Opcode.SMSG_SCENARIO_VACATE)]
        public static void HandleScenarioVacate(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
                packet.ReadPackedGuid128("ScenarioGUID");
            packet.ReadInt32("ScenarioID");
            packet.ReadInt32("Unk1");
            packet.ReadBits("Unk2", 2);
        }
    }
}
