using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePetError
    {
        public Battlepetresult BattlePetResult;
        public int CreatureID;
    }
}
