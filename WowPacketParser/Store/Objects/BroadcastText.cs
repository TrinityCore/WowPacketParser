using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("broadcast_text")]
    public class BroadcastText
    {
        public uint[] EmoteID;

        public uint[] EmoteDelay;

        public void ConvertToDBStruct()
        {
            emoteID0 = EmoteID[0];
            emoteID1 = EmoteID[1];
            emoteID2 = EmoteID[2];

            emoteDelay0 = EmoteDelay[0];
            emoteDelay1 = EmoteDelay[1];
            emoteDelay2 = EmoteDelay[2];
        }

        // ReSharper disable InconsistentNaming
        [DBFieldName("Language")]
        public uint language;
        [DBFieldName("MaleText")]
        public string MaleText;
        [DBFieldName("FemaleText")]
        public string FemaleText;
        [DBFieldName("EmoteID0")]
        public uint emoteID0;
        [DBFieldName("EmoteID1")]
        public uint emoteID1;
        [DBFieldName("EmoteID2")]
        public uint emoteID2;
        [DBFieldName("EmoteDelay0")]
        public uint emoteDelay0;
        [DBFieldName("EmoteDelay1")]
        public uint emoteDelay1;
        [DBFieldName("EmoteDelay2")]
        public uint emoteDelay2;
        [DBFieldName("SoundId")]
        public uint soundId;
        [DBFieldName("UnkMoP1")]
        public uint unk1;
        [DBFieldName("UnkMoP2")]
        public uint unk2;
        // ReSharper restore InconsistentNaming

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
