namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct EquipmentSetData
    {
        public ulong Guid;
        public uint SetID;
        public string SetName;
        public string SetIcon;
        public fixed ulong Pieces[19];
    }
}
