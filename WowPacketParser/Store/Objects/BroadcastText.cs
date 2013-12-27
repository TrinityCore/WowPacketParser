using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("broadcast_text")]
    public class BroadcastText
    {
        public uint[] EmoteID;

        public uint[] EmoteDelay;

        public string MaleText = "";
        public string FemaleText = "";

        public void ConvertToDBStruct()
        {
            // Seriously...

            emoteID0 = EmoteID[0];
            emoteID1 = EmoteID[1];
            emoteID2 = EmoteID[2];

            emoteDelay0 = EmoteID[0];
            emoteDelay1 = EmoteID[1];
            emoteDelay2 = EmoteID[2];

            maleText = MaleText;
            femaleText = FemaleText;
        }

        // ReSharper disable InconsistentNaming
        [DBFieldName("Language")]
        public uint language;
        [DBFieldName("MaleText")]
        public string maleText;
        [DBFieldName("FemaleText")]
        public string femaleText;
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
        [DBFieldName("Unk1")]
        public uint unk1;
        [DBFieldName("Unk2")]
        public uint unk2;
        // ReSharper restore InconsistentNaming

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
