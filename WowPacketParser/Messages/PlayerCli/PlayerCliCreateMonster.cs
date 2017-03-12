using WowPacketParser.Misc;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliCreateMonster
    {
        public int EntryID;
        public Vector3 Offset;
    }
}
