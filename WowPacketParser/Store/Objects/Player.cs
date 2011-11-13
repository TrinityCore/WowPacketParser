using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class Player : WoWObject
    {
        public Race Race;

        public Class Class;

        public string Name;

        public bool FirstLogin;

        public int Level;
    }
}
