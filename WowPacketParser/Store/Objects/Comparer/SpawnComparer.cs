using System;
using System.Collections.Generic;
using System.Linq;

namespace WowPacketParser.Store.Objects.Comparer
{
    public class SpawnComparer : IEqualityComparer<WoWObject>
    {
        public bool Equals(WoWObject x, WoWObject y)
        {
            if (x == null || y == null)
                return false;

            return x.ObjectData.EntryID == y.ObjectData.EntryID
                && x.Phases.SequenceEqual(y.Phases)
                && x.Movement.Position == y.Movement.Position;
        }

        public int GetHashCode(WoWObject obj)
        {
            int hash = 17;
            hash = hash * 31 + obj.ObjectData.EntryID.GetValueOrDefault(0);
            foreach (var phase in obj.Phases)
            {
                hash = hash * 29 + phase;
            }
            hash = hash * 37 + obj.Movement.Position.GetHashCode();
            return hash;
        }
    }
}
