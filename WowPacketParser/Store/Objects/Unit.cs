using System.Collections.Generic;

namespace WowPacketParser.Store.Objects
{
    public sealed class Unit : WoWObject
    {
        public ICollection<Aura> Auras;
    }
}
