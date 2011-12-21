using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public class NpcText
    {
        public float[] Probabilities;

        public string[] Texts1;

        public string[] Texts2;

        public Language[] Languages;

        public uint[][] EmoteDelays;

        public EmoteType[][] EmoteIds;
    }
}
