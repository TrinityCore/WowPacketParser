namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetRenameData
    {
        public int PetNumber;
        public string NewName;
        public bool HasDeclinedNames;
        public string[/*5*/] DeclinedNames;
    }
}
