using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class NpcTrainer
    {
        public TrainerType Type;

        public List<TrainerSpell> TrainerSpells;
    }
}
