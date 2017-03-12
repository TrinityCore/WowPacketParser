using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
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
