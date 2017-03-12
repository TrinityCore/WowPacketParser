using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct WhoRequest
    {
        public int MinLevel;
        public int MaxLevel;
        public string Name;
        public string VirtualRealmName;
        public string Guild;
        public string GuildVirtualRealmName;
        public int RaceFilter;
        public int ClassFilter;
        public List<WhoWord> Words;
        public bool ShowEnemies;
        public bool ShowArenaPlayers;
        public bool ExactName;
        public WhoRequestServerInfo ServerInfo; // Optional
    }
}
