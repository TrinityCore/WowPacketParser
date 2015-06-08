using WowPacketParser.Enums;
using WowPacketParser.Misc;
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

            Prob0 = Probabilities[0];
            Prob1 = Probabilities[1];
            Prob2 = Probabilities[2];
            Prob3 = Probabilities[3];
            Prob4 = Probabilities[4];
            Prob5 = Probabilities[5];
            Prob6 = Probabilities[6];
            Prob7 = Probabilities[7];

            Lang0 = (uint) Languages[0];
            Lang1 = (uint) Languages[1];
            Lang2 = (uint) Languages[2];
            Lang3 = (uint) Languages[3];
            Lang4 = (uint) Languages[4];
            Lang5 = (uint) Languages[5];
            Lang6 = (uint) Languages[6];
            Lang7 = (uint) Languages[7];

            Text0X = new string[2];
            Text1X = new string[2];
            Text2X = new string[2];
            Text3X = new string[2];
            Text4X = new string[2];
            Text5X = new string[2];
            Text6X = new string[2];
            Text7X = new string[2];

            Text0X[0] = Texts1[0];
            Text1X[0] = Texts1[1];
            Text2X[0] = Texts1[2];
            Text3X[0] = Texts1[3];
            Text4X[0] = Texts1[4];
            Text5X[0] = Texts1[5];
            Text6X[0] = Texts1[6];
            Text7X[0] = Texts1[7];

            Text0X[1] = Texts2[0];
            Text1X[1] = Texts2[1];
            Text2X[1] = Texts2[2];
            Text3X[1] = Texts2[3];
            Text4X[1] = Texts2[4];
            Text5X[1] = Texts2[5];
            Text6X[1] = Texts2[6];
            Text7X[1] = Texts2[7];

            Em0X = new uint[6];
            Em1X = new uint[6];
            Em2X = new uint[6];
            Em3X = new uint[6];
            Em4X = new uint[6];
            Em5X = new uint[6];
            Em6X = new uint[6];
            Em7X = new uint[6];

            Em0X[0] = (uint) EmoteIds[0][0];
            Em0X[1] = (uint) EmoteIds[0][1];
            Em0X[2] = (uint) EmoteIds[0][2];
            Em0X[3] =     EmoteDelays[0][0];
            Em0X[4] =     EmoteDelays[0][1];
            Em0X[5] =     EmoteDelays[0][2];
            Em1X[0] = (uint) EmoteIds[1][0];
            Em1X[1] = (uint) EmoteIds[1][1];
            Em1X[2] = (uint) EmoteIds[1][2];
            Em1X[3] =     EmoteDelays[1][0];
            Em1X[4] =     EmoteDelays[1][1];
            Em1X[5] =     EmoteDelays[1][2];
            Em2X[0] = (uint) EmoteIds[2][0];
            Em2X[1] = (uint) EmoteIds[2][1];
            Em2X[2] = (uint) EmoteIds[2][2];
            Em2X[3] =     EmoteDelays[2][0];
            Em2X[4] =     EmoteDelays[2][1];
            Em2X[5] =     EmoteDelays[2][2];
            Em3X[0] = (uint) EmoteIds[3][0];
            Em3X[1] = (uint) EmoteIds[3][1];
            Em3X[2] = (uint) EmoteIds[3][2];
            Em3X[3] =     EmoteDelays[3][0];
            Em3X[4] =     EmoteDelays[3][1];
            Em3X[5] =     EmoteDelays[3][2];
            Em4X[0] = (uint) EmoteIds[4][0];
            Em4X[1] = (uint) EmoteIds[4][1];
            Em4X[2] = (uint) EmoteIds[4][2];
            Em4X[3] =     EmoteDelays[4][0];
            Em4X[4] =     EmoteDelays[4][1];
            Em4X[5] =     EmoteDelays[4][2];
            Em5X[0] = (uint) EmoteIds[5][0];
            Em5X[1] = (uint) EmoteIds[5][1];
            Em5X[2] = (uint) EmoteIds[5][2];
            Em5X[3] =     EmoteDelays[5][0];
            Em5X[4] =     EmoteDelays[5][1];
            Em5X[5] =     EmoteDelays[5][2];
            Em6X[0] = (uint) EmoteIds[6][0];
            Em6X[1] = (uint) EmoteIds[6][1];
            Em6X[2] = (uint) EmoteIds[6][2];
            Em6X[3] =     EmoteDelays[6][0];
            Em6X[4] =     EmoteDelays[6][1];
            Em6X[5] =     EmoteDelays[6][2];
            Em7X[0] = (uint) EmoteIds[7][0];
            Em7X[1] = (uint) EmoteIds[7][1];
            Em7X[2] = (uint) EmoteIds[7][2];
            Em7X[3] =     EmoteDelays[7][0];
            Em7X[4] =     EmoteDelays[7][1];
            Em7X[5] =     EmoteDelays[7][2];
        }

        [DBFieldName("text0_", 2, true)] public string[] Text0X;
        [DBFieldName("lang0")] public uint Lang0;
        [DBFieldName("prob0")] public float Prob0;
        [DBFieldName("em0_", 6, true)] public uint[] Em0X;
        [DBFieldName("text1_", 2, true)] public string[] Text1X;
        [DBFieldName("lang1")] public uint Lang1;
        [DBFieldName("prob1")] public float Prob1;
        [DBFieldName("em1_", 6, true)] public uint[] Em1X;
        [DBFieldName("text2_", 2, true)] public string[] Text2X;
        [DBFieldName("lang2")] public uint Lang2;
        [DBFieldName("prob2")] public float Prob2;
        [DBFieldName("em2_", 6, true)] public uint[] Em2X;
        [DBFieldName("text3_", 2, true)] public string[] Text3X;
        [DBFieldName("lang3")] public uint Lang3;
        [DBFieldName("prob3")] public float Prob3;
        [DBFieldName("em3_", 6, true)] public uint[] Em3X;
        [DBFieldName("text4_", 2, true)] public string[] Text4X;
        [DBFieldName("lang4")] public uint Lang4;
        [DBFieldName("prob4")] public float Prob4;
        [DBFieldName("em4_", 6, true)] public uint[] Em4X;
        [DBFieldName("text5_", 2, true)] public string[] Text5X;
        [DBFieldName("lang5")] public uint Lang5;
        [DBFieldName("prob5")] public float Prob5;
        [DBFieldName("em5_", 6, true)] public uint[] Em5X;
        [DBFieldName("text6_", 2, true)] public string[] Text6X;
        [DBFieldName("lang6")] public uint Lang6;
        [DBFieldName("prob6")] public float Prob6;
        [DBFieldName("em6_", 6, true)] public uint[] Em6X;
        [DBFieldName("text7_", 2, true)] public string[] Text7X;
        [DBFieldName("lang7")] public uint Lang7;
        [DBFieldName("prob7")] public float Prob7;
        [DBFieldName("em7_", 6, true)] public uint[] Em7X;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }

    [DBTableName("npc_text")]
    public class NpcTextMop
    {
        public float[] Probabilities;

        public uint[] BroadcastTextId;

        public void ConvertToDBStruct()
        {
            // Seriously...

            Prob0 = Probabilities[0];
            Prob1 = Probabilities[1];
            Prob2 = Probabilities[2];
            Prob3 = Probabilities[3];
            Prob4 = Probabilities[4];
            Prob5 = Probabilities[5];
            Prob6 = Probabilities[6];
            Prob7 = Probabilities[7];

            BroadcastTextId0 = BroadcastTextId[0];
            BroadcastTextId1 = BroadcastTextId[1];
            BroadcastTextId2 = BroadcastTextId[2];
            BroadcastTextId3 = BroadcastTextId[3];
            BroadcastTextId4 = BroadcastTextId[4];
            BroadcastTextId5 = BroadcastTextId[5];
            BroadcastTextId6 = BroadcastTextId[6];
            BroadcastTextId7 = BroadcastTextId[7];
        }

        [DBFieldName("Probability0")]
        public float Prob0;
        [DBFieldName("Probability1")]
        public float Prob1;
        [DBFieldName("Probability2")]
        public float Prob2;
        [DBFieldName("Probability3")]
        public float Prob3;
        [DBFieldName("Probability4")]
        public float Prob4;
        [DBFieldName("Probability5")]
        public float Prob5;
        [DBFieldName("Probability6")]
        public float Prob6;
        [DBFieldName("Probability7")]
        public float Prob7;
        [DBFieldName("BroadcastTextId0")]
        public uint BroadcastTextId0;
        [DBFieldName("BroadcastTextId1")]
        public uint BroadcastTextId1;
        [DBFieldName("BroadcastTextId2")]
        public uint BroadcastTextId2;
        [DBFieldName("BroadcastTextId3")]
        public uint BroadcastTextId3;
        [DBFieldName("BroadcastTextId4")]
        public uint BroadcastTextId4;
        [DBFieldName("BroadcastTextId5")]
        public uint BroadcastTextId5;
        [DBFieldName("BroadcastTextId6")]
        public uint BroadcastTextId6;
        [DBFieldName("BroadcastTextId7")]
        public uint BroadcastTextId7;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
