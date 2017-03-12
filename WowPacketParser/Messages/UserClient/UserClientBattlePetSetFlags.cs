using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePetSetFlags
    {
        public ulong BattlePetGUID;
        public uint Flags;
        public clibattlepetsetflagcontroltype ControlType;
    }
}
