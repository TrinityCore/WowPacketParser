﻿using WowPacketParser.Misc;
﻿using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("defense_message", WPPDatabase.None /* NYI */)]
    public class DefenseMessage
    {
        [DBFieldName("Text")]
        public string Text;

        [DBFieldName("BroadcastText")]
        public string BroadcastText;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
