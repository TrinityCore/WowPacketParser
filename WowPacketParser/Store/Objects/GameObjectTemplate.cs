using WowPacketParser.Enums;
using WowPacketParser.Misc;
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

        [DBFieldName("name", LocaleConstant.enUS)] // ToDo: Added locale support
        public string Name;

        [DBFieldName("IconName")]
        public string IconName;

        [DBFieldName("castBarCaption", LocaleConstant.enUS)] // ToDo: Added locale support
        public string CastCaption;

        [DBFieldName("unk1")]
        public string UnkString;

        [DBFieldName("size")]
        public float Size;

        [DBFieldName("questItem", ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033, 6)]
        public uint[] QuestItems;

        [DBFieldName("Data", ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164, 24, true)]
        [DBFieldName("Data", ClientVersionBuild.V4_0_1_13164, ClientVersionBuild.V6_0_3_19103, 32, true)]
        [DBFieldName("Data", ClientVersionBuild.V6_0_3_19103, 33, true)]
        public int[] Data;

        [DBFieldName("unkInt32", ClientVersionBuild.V4_0_1_13164)]
        public int UnknownInt;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
