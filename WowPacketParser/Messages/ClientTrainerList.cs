using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTrainerList
    {
        public string Greeting;
        public int TrainerType;
        public ulong TrainerGUID;
        public int TrainerID;
        public List<ClientTrainerListSpell> Spells;
    }
}
