﻿using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScript)]
    public class SceneScriptEntry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Script { get; set; }
        public int PreviousScript { get; set; }
        public int NextScript { get; set; }
    }
}
