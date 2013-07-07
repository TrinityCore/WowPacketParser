using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("npc_text")]
    public class NpcText
    {
        public float[] Probabilities;

        public string[] Texts1;

        public string[] Texts2;

        public Language[] Languages;

        public uint[][] EmoteDelays;

        public EmoteType[][] EmoteIds;

        public void ConvertToDBStruct()
        {
            // Seriously...

            prob0 = Probabilities[0];
            prob1 = Probabilities[1];
            prob2 = Probabilities[2];
            prob3 = Probabilities[3];
            prob4 = Probabilities[4];
            prob5 = Probabilities[5];
            prob6 = Probabilities[6];
            prob7 = Probabilities[7];

            lang0 = (uint) Languages[0];
            lang1 = (uint) Languages[1];
            lang2 = (uint) Languages[2];
            lang3 = (uint) Languages[3];
            lang4 = (uint) Languages[4];
            lang5 = (uint) Languages[5];
            lang6 = (uint) Languages[6];
            lang7 = (uint) Languages[7];

            text0_x = new string[2];
            text1_x = new string[2];
            text2_x = new string[2];
            text3_x = new string[2];
            text4_x = new string[2];
            text5_x = new string[2];
            text6_x = new string[2];
            text7_x = new string[2];

            text0_x[0] = Texts1[0];
            text1_x[0] = Texts1[1];
            text2_x[0] = Texts1[2];
            text3_x[0] = Texts1[3];
            text4_x[0] = Texts1[4];
            text5_x[0] = Texts1[5];
            text6_x[0] = Texts1[6];
            text7_x[0] = Texts1[7];

            text0_x[1] = Texts2[0];
            text1_x[1] = Texts2[1];
            text2_x[1] = Texts2[2];
            text3_x[1] = Texts2[3];
            text4_x[1] = Texts2[4];
            text5_x[1] = Texts2[5];
            text6_x[1] = Texts2[6];
            text7_x[1] = Texts2[7];

            em0_x = new uint[6];
            em1_x = new uint[6];
            em2_x = new uint[6];
            em3_x = new uint[6];
            em4_x = new uint[6];
            em5_x = new uint[6];
            em6_x = new uint[6];
            em7_x = new uint[6];

            em0_x[0] = (uint) EmoteIds[0][0];
            em0_x[1] = (uint) EmoteIds[0][1];
            em0_x[2] = (uint) EmoteIds[0][2];
            em0_x[3] =     EmoteDelays[0][0];
            em0_x[4] =     EmoteDelays[0][1];
            em0_x[5] =     EmoteDelays[0][2];
            em1_x[0] = (uint) EmoteIds[1][0];
            em1_x[1] = (uint) EmoteIds[1][1];
            em1_x[2] = (uint) EmoteIds[1][2];
            em1_x[3] =     EmoteDelays[1][0];
            em1_x[4] =     EmoteDelays[1][1];
            em1_x[5] =     EmoteDelays[1][2];
            em2_x[0] = (uint) EmoteIds[2][0];
            em2_x[1] = (uint) EmoteIds[2][1];
            em2_x[2] = (uint) EmoteIds[2][2];
            em2_x[3] =     EmoteDelays[2][0];
            em2_x[4] =     EmoteDelays[2][1];
            em2_x[5] =     EmoteDelays[2][2];
            em3_x[0] = (uint) EmoteIds[3][0];
            em3_x[1] = (uint) EmoteIds[3][1];
            em3_x[2] = (uint) EmoteIds[3][2];
            em3_x[3] =     EmoteDelays[3][0];
            em3_x[4] =     EmoteDelays[3][1];
            em3_x[5] =     EmoteDelays[3][2];
            em4_x[0] = (uint) EmoteIds[4][0];
            em4_x[1] = (uint) EmoteIds[4][1];
            em4_x[2] = (uint) EmoteIds[4][2];
            em4_x[3] =     EmoteDelays[4][0];
            em4_x[4] =     EmoteDelays[4][1];
            em4_x[5] =     EmoteDelays[4][2];
            em5_x[0] = (uint) EmoteIds[5][0];
            em5_x[1] = (uint) EmoteIds[5][1];
            em5_x[2] = (uint) EmoteIds[5][2];
            em5_x[3] =     EmoteDelays[5][0];
            em5_x[4] =     EmoteDelays[5][1];
            em5_x[5] =     EmoteDelays[5][2];
            em6_x[0] = (uint) EmoteIds[6][0];
            em6_x[1] = (uint) EmoteIds[6][1];
            em6_x[2] = (uint) EmoteIds[6][2];
            em6_x[3] =     EmoteDelays[6][0];
            em6_x[4] =     EmoteDelays[6][1];
            em6_x[5] =     EmoteDelays[6][2];
            em7_x[0] = (uint) EmoteIds[7][0];
            em7_x[1] = (uint) EmoteIds[7][1];
            em7_x[2] = (uint) EmoteIds[7][2];
            em7_x[3] =     EmoteDelays[7][0];
            em7_x[4] =     EmoteDelays[7][1];
            em7_x[5] =     EmoteDelays[7][2];
        }

        // ReSharper disable InconsistentNaming
        [DBFieldName("text0_", 2, true)] public string[] text0_x;
        [DBFieldName("lang0")] public uint lang0;
        [DBFieldName("prob0")] public float prob0;
        [DBFieldName("em0_", 6, true)] public uint[] em0_x;
        [DBFieldName("text1_", 2, true)] public string[] text1_x;
        [DBFieldName("lang1")] public uint lang1;
        [DBFieldName("prob1")] public float prob1;
        [DBFieldName("em1_", 6, true)] public uint[] em1_x;
        [DBFieldName("text2_", 2, true)] public string[] text2_x;
        [DBFieldName("lang2")] public uint lang2;
        [DBFieldName("prob2")] public float prob2;
        [DBFieldName("em2_", 6, true)] public uint[] em2_x;
        [DBFieldName("text3_", 2, true)] public string[] text3_x;
        [DBFieldName("lang3")] public uint lang3;
        [DBFieldName("prob3")] public float prob3;
        [DBFieldName("em3_", 6, true)] public uint[] em3_x;
        [DBFieldName("text4_", 2, true)] public string[] text4_x;
        [DBFieldName("lang4")] public uint lang4;
        [DBFieldName("prob4")] public float prob4;
        [DBFieldName("em4_", 6, true)] public uint[] em4_x;
        [DBFieldName("text5_", 2, true)] public string[] text5_x;
        [DBFieldName("lang5")] public uint lang5;
        [DBFieldName("prob5")] public float prob5;
        [DBFieldName("em5_", 6, true)] public uint[] em5_x;
        [DBFieldName("text6_", 2, true)] public string[] text6_x;
        [DBFieldName("lang6")] public uint lang6;
        [DBFieldName("prob6")] public float prob6;
        [DBFieldName("em6_", 6, true)] public uint[] em6_x;
        [DBFieldName("text7_", 2, true)] public string[] text7_x;
        [DBFieldName("lang7")] public uint lang7;
        [DBFieldName("prob7")] public float prob7;
        [DBFieldName("em7_", 6, true)] public uint[] em7_x;
        // ReSharper restore InconsistentNaming

        [DBFieldName("WDBVerified")]
        public int WDBVerified;
    }
}
