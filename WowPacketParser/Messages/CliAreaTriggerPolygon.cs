using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliAreaTriggerPolygon
    {
        public List<Vector2> Vertices;
        public List<Vector2> VerticesTarget;
        public float Height;
        public float HeightTarget;
    }
}
