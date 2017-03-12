using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetCharacterCustomization
    {
        public int VariationValue;
        public int VariationIndex;
    }
}
