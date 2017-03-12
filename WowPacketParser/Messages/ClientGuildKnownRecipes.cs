using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildKnownRecipes
    {
        public List<GuildKnownRecipes> Recipes;
    }
}
