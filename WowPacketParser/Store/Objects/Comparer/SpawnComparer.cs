using System;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.SQL.Builders;

namespace WowPacketParser.Store.Objects.Comparer
{
    public class SpawnComparer : IEqualityComparer<WoWObject>
    {
        public bool Equals(WoWObject x, WoWObject y)
        {
            if (x == null || y == null)
                return false;

            var precision = 0.02f; // warning - some zones shifted by 0.2 in some cases between later expansions
            return x.ObjectData.EntryID == y.ObjectData.EntryID
                && x.Map == y.Map
                && x.CreateType == y.CreateType
                && Spawns.FloatComparison(x.Movement.Position.X, y.Movement.Position.X, precision)
                && Spawns.FloatComparison(x.Movement.Position.Y, y.Movement.Position.Y, precision)
                && Spawns.FloatComparison(x.Movement.Position.Z, y.Movement.Position.Z, precision)
                && x.Phases.SequenceEqual(y.Phases);
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
            hash = hash * 41 + (int)obj.Map;
            hash = hash * 43 + (int)obj.CreateType;
            return hash;
        }
    }
}
