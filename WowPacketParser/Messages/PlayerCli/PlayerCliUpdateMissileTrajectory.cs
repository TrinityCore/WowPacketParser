using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliUpdateMissileTrajectory
    {
        public ulong Guid;
        public ushort MoveMsgID;
        public float Speed;
        public CliMovementStatus? Status; // Optional
        public float Pitch;
        public Vector3 ImpactPos;
        public int SpellID;
        public Vector3 FirePos;
    }
}
