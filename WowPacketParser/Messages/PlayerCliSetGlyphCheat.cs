using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetGlyphCheat
    {
        public ushort Glyph;
        public byte Slot;
    }
}
