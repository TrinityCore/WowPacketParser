using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject_template")]
    public sealed class GameObjectTemplate
    {
        [DBFieldName("type")]
        public GameObjectType Type;

        [DBFieldName("displayId")]
        public uint DisplayId;

        [DBFieldName("name")]
        public string Name;

        [DBFieldName("IconName")]
        public string IconName;

        [DBFieldName("castBarCaption")]
        public string CastCaption;

        [DBFieldName("unk1")]
        public string UnkString;

        [DBFieldName("data", ClientVersionBuild.V4_0_1_13164, 32, true)]
        [DBFieldName("data", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164, 24, true)]
        public int[] Data;

        [DBFieldName("size")]
        public float Size;

        [DBFieldName("questItem", 6)]
        public uint[] QuestItems;

        [DBFieldName("unkInt32", ClientVersionBuild.V4_0_1_13164)]
        public int UnknownInt;

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
