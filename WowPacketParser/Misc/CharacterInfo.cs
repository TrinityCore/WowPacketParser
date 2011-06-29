using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public sealed class CharacterInfo
    {
        public Race Race;

        public Class Class;

        public string Name;

        public Guid Guid;

        public bool FirstLogin;

        public int Level;
    }
}
