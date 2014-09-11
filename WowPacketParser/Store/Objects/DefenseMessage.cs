﻿using WowPacketParser.Misc;
﻿using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("defense_message")]
    public class DefenseMessage
    {
        [DBFieldName("Text")]
        public string text;

        [DBFieldName("BroadcastText")]
        public string broadcastText;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
