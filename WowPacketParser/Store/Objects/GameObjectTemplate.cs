using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class GameObjectTemplate
    {
        public GameObjectType Type;

        public uint DisplayId;

        public string Name;

        public string IconName;

        public string CastCaption;

        public string UnkString;

        public int[] Data;

        public float Size;

        public uint[] QuestItems;

        public uint UnknownUInt;
    }
}
