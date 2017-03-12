using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSpawnedMOCheat
    {
        public float Angle;
        public bool Enabled;
        public string FileName;
        public Vector3 Pos;
        public uint SpawnedMOIndex;
    }
}
