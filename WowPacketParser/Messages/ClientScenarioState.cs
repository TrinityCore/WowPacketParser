using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientScenarioState
    {
        public List<BonusObjectiveData> BonusObjectives;
        public bool ScenarioComplete;
        public List<CriteriaProgress> CriteriaProgress;
        public int CurrentStep;
        public uint WaveCurrent;
        public uint DifficultyID;
        public uint TimerDuration;
        public int ScenarioID;
        public uint WaveMax;
    }
}
