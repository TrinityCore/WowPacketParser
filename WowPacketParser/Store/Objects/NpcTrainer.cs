using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Misc;

namespace PacketParser.DataStructures
{
    public class NpcTrainer : ITextOutputDisabled
    {
        public TrainerType Type;

        public ICollection<TrainerSpell> TrainerSpells;

        public string Title;
    }
}
