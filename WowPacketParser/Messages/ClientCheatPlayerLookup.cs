using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCheatPlayerLookup
    {
        public byte ExperienceLevel;
        public byte FaceID;
        public uint Flags;
        public uint Flags3;
        public byte RaceID;
        public uint ZoneID;
        public uint PetExperienceLevel;
        public byte FirstLogin;
        public byte SexID;
        public byte HairStyleID;
        public uint MapID;
        public byte ClassID;
        public ulong Guid;
        public Vector3 Position;
        public uint PetCreatureFamilyID;
        public string Name;
        public byte HairColorID;
        public byte SkinID;
        public uint PetDisplayInfoID;
        public ulong GuildGUID;
        public uint Flags2;
        public byte FacialHairStyleID;
    }
}
