using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class NpcTrainer
    {
        public TrainerType Type;

        public ICollection<TrainerSpell> TrainerSpells;

        public string Title;
    }
}
