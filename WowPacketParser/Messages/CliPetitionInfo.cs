using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPetitionInfo
    {
        public int PetitionID;
        public ulong Petitioner;
        public string Title;
        public string BodyText;
        public int MinSignatures;
        public int MaxSignatures;
        public int DeadLine;
        public int IssueDate;
        public int AllowedGuildID;
        public int AllowedClasses;
        public int AllowedRaces;
        public short AllowedGender;
        public int AllowedMinLevel;
        public int AllowedMaxLevel;
        public int NumChoices;
        public int StaticType;
        public uint Muid;
        public string[/*1*/] Choicetext;
    }
}
