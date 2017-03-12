using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCharCustomize
    {
        public byte Result;
        public byte HairStyleID;
        public byte SexID;
        public byte FaceID;
        public string CharName;
        public byte HairColorID;
        public byte SkinID;
        public byte FacialHairStyleID;
        public ulong CharGUID;
    }
}
