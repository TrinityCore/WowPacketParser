using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPlaySceneData
    {
        public ulong TransportGUID;
        public int SceneID;
        public uint PlaybackFlags;
        public uint SceneInstanceID;
        public int SceneScriptPackageID;
        public Vector3 Pos;
        public float Facing;
    }
}
