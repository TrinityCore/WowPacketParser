using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDebugDrawAura
    {
        public ulong Caster;
        public int SpellID;
        public Vector3 Position;
    }
}
