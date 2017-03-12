using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMirrorImageComponentedData
    {
        public byte FaceVariation;
        public List<int> ItemDisplayID;
        public byte SkinColor;
        public byte Gender;
        public byte HairColor;
        public byte ClassID;
        public ulong UnitGUID;
        public byte RaceID;
        public ulong GuildGUID;
        public byte HairVariation;
        public byte BeardVariation;
        public int DisplayID;
    }
}
