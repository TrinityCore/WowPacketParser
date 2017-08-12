﻿using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    public sealed class CreatureAddon : IDataModel
    {
        [DBFieldName("guid", true, noQuotes: true)]
        public string GUID;

        [DBFieldName("path_id")]
        public uint? PathID;

        [DBFieldName("mount")]
        public uint? Mount;

        [DBFieldName("bytes1")]
        public uint? Bytes1;

        [DBFieldName("bytes2")]
        public uint? Bytes2;

        [DBFieldName("emote")]
        public uint? Emote;

        [DBFieldName("aiAnimKit", TargetedDatabase.Legion)]
        public ushort? AIAnimKit;

        [DBFieldName("movementAnimKit", TargetedDatabase.Legion)]
        public ushort? MovementAnimKit;

        [DBFieldName("meleeAnimKit", TargetedDatabase.Legion)]
        public ushort? MeleeAnimKit;

        [DBFieldName("auras")]
        public string Auras;
    }
}
