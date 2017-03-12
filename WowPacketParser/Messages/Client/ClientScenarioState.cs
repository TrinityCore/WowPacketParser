using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
