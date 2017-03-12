using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CharFactionChangeDisplayInfo
    {
        public string Name;
        public byte SexID;
        public byte SkinID;
        public byte HairColorID;
        public byte HairStyleID;
        public byte FacialHairStyleID;
        public byte FaceID;
        public byte RaceID;
    }
}
