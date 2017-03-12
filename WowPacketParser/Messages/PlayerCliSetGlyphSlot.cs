using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetGlyphSlot
    {
        public ushort GlyphSlot;
        public byte Slot;
    }
}
