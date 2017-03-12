using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliUseEquipmentSet
    {
        public InvUpdate Inv;
        public EquipmentSetItem[/*19*/] Items;
    }
}
