using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    struct Creature
    {
        uint Guid;
        uint Entry;
        uint Map;
        uint phaseMask;
        float positionX;
        float positionY;
        float positionZ;
        float orientation;
        uint spawntimeSecs;

        public Creature(uint Guid, uint Entry, uint Map, uint phaseMask, float positionX, float positionY, float positionZ, float orientation, uint spawntimeSecs)
        {
            this.Guid = Guid;
            this.Entry = Entry;
            this.Map = Map;
            this.phaseMask = phaseMask;
            this.positionX = positionX;
            this.positionY = positionY;
            this.positionZ = positionZ;
            this.orientation = orientation;
            this.spawntimeSecs = spawntimeSecs;
        }
    }

    public sealed class WowObject
    {
        private static List<Creature> creatureList = new List<Creature>();
        public Guid Guid;
        public Vector3 Position;
        public ObjectType Type;
        public MovementInfo Movement;

        public WowObject(Guid guid, ObjectType type, MovementInfo moves)
        {
            Guid = guid;
            Type = type;
            Movement = moves;
        }

        public void AddCreature(uint guid, uint entry, uint map, uint phase, float posX, float posY, float posZ, float orientation, uint spawntimeSecs)
        {
            creatureList.Add(new Creature(guid, entry, map, phase, posX, posY, posZ, orientation, spawntimeSecs));
        }
    }
}
