using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAlterAppearance
    {
        public int NewFacialHair;
        public int NewSkinColor;
        public int NewHairColor;
        public int NewHairStyle;
    }
}
