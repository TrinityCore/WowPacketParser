using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGdfSimCheat
    {
        public float GdfUnitBlend;
        public float GdfToEloMultiplier;
        public float Beta2;
        public int NoShowPenalty0;
        public int NoShowPenalty1;
        public float MinVariance;
        public bool Winner;
        public List<GdfSimPlayer> Players;
        public float Mean;
        public bool BoostType;
    }
}
