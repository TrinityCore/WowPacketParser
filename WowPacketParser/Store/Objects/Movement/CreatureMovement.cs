using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.Movement
{
    public class CreatureMovement
    {
        public CreatureMovementFlags Type { get; set; }
        public WowGuid GUID { get; set; }
        public int PathId { get; set; }
        public int Id { get; set; }
        public List<CreatureMovementNode> Waypoints { get; set; } = new();
        public CreatureMovementNode? Destination { get; set; } = null;
        public float? FinalOrientation { get; set; } = null; // signals end of current path and wait time
    }
}
